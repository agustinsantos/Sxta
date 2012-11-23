using System;
using System.Collections.Generic;
using System.Text;

// Import log4net classes.
using log4net;

using Hla.Rti1516;
using Sxta.Rti1516.Reflection;

namespace Sxta.Rti1516.DynamicCompiler
{

    /// <summary> 
    /// The dynamic proxy compiler.  May be invoked as a command line application or
    /// by using the static <code>CompileProxies</code> method (from within an NAnt
    /// task, for instance).
    /// </summary>
    /// <author> Agustin Santos.
    /// </author>
    public partial class DynamicCompiler
    {
        /// <summary> 
        /// Generates a <code>HLAObjectClassAttribute</code>.
        /// </summary>
        private void GenerateHLAObjectClassAttribute(int localIndentLevel, System.IO.StreamWriter ps, ObjectClassDescriptor objDescriptor)
        {
            string indentStr = GenerateIndentString(localIndentLevel);
            string newLine = "," + Environment.NewLine + indentStr + "                ";
            ps.Write(indentStr + "[HLAObjectClass(Name = \"" + objDescriptor.Name + "\"");
            ps.Write(newLine);
            ps.Write("Sharing = " + objDescriptor.objectDescription.Sharing.GetType() + "." + objDescriptor.objectDescription.Sharing);

            if (!String.IsNullOrEmpty(objDescriptor.objectDescription.SharingNotes))
            {
                ps.Write(newLine);
                ps.Write("SharingNotes = \"" + objDescriptor.objectDescription.SharingNotes + "\"");
            }
            if (!String.IsNullOrEmpty(objDescriptor.objectDescription.Semantics))
            {
                ps.Write(newLine);
                ps.Write("Semantics = \"" + objDescriptor.objectDescription.Semantics + "\"");
            }
            if (!String.IsNullOrEmpty(objDescriptor.objectDescription.SemanticsNotes))
            {
                ps.Write(newLine);
                ps.Write("SemanticsNotes = \"" + objDescriptor.objectDescription.SemanticsNotes + "\"");
            }
            if (!String.IsNullOrEmpty(objDescriptor.objectDescription.NameNotes))
            {
                ps.Write(newLine);
                ps.Write("NameNotes = \"" + objDescriptor.objectDescription.NameNotes + "\"");
            }
            ps.WriteLine(")]");
        }


        /// <summary> 
        /// Generates a <code>HLAAttributeAttribute</code>.
        /// </summary>
        private void GenerateHLAAttributeAttribute(int localIndentLevel, System.IO.StreamWriter ps, AttributeDescriptor attributeDescriptor)
        {
            IHLAattribute attr = attributeDescriptor.attribute;
            string indentStr = GenerateIndentString(localIndentLevel);
            string newLine = "," + Environment.NewLine + indentStr + "              ";

            ps.Write(indentStr + "[HLAAttribute(Name = \"" + attributeDescriptor.Name + "\"");
            ps.Write("," + Environment.NewLine + indentStr + "                ");
            ps.Write("Sharing = " + attr.Sharing.GetType() + "." + attr.Sharing);
            if (!String.IsNullOrEmpty(attr.NameNotes))
            {
                ps.Write(newLine);
                ps.Write("NameNotes = \"" + attr.NameNotes + "\"");
            }

            if (!String.IsNullOrEmpty(attr.SharingNotes))
            {
                ps.Write(newLine);
                ps.Write("SharingNotes = \"" + attr.SharingNotes + "\"");
            }
            if (!String.IsNullOrEmpty(attr.Semantics))
            {
                ps.Write(newLine);
                ps.Write("Semantics = \"" + attr.Semantics + "\"");
            }
            if (!String.IsNullOrEmpty(attr.SemanticsNotes))
            {
                ps.Write(newLine);
                ps.Write("SemanticsNotes = \"" + attr.SemanticsNotes + "\"");
            }
            if (!String.IsNullOrEmpty(attr.DataType))
            {
                ps.Write(newLine);
                ps.Write("DataType = \"" + attr.DataType + "\"");
            }
            ps.Write(newLine);
            ps.Write("UpdateType = " + attr.UpdateType.GetType() + "." + attr.UpdateType);
            if (!String.IsNullOrEmpty(attr.UpdateCondition))
            {
                ps.Write(newLine);
                ps.Write("UpdateCondition = \"" + attr.UpdateCondition + "\"");
            }
            ps.Write(newLine);
            ps.Write("OwnerShip = " + attr.Ownership.GetType() + "." + attr.Ownership);
            if (!String.IsNullOrEmpty(attr.Transportation))
            {
                ps.Write(newLine);
                ps.Write("Transportation = \"" + attr.Transportation + "\"");
            }

            ps.Write(newLine);
            ps.Write("Order = " + attr.Order.GetType() + "." + attr.Order);

            if (!String.IsNullOrEmpty(attr.Dimensions))
            {
                ps.Write(newLine);
                ps.Write("Dimensions = \"" + attr.Dimensions + "\"");
            }
            ps.WriteLine(")]");
        }

        /// <summary> 
        /// Generates all the object class definitions.
        /// </summary>
        private void GenerateObjectClass(System.IO.StreamWriter ps)
        {
            List<string> generatedObjectList = new List<string>();
            foreach (ObjectClassDescriptor objDescriptor in descriptorManager.ObjectClassDescriptors)
            {
                GenerateObjectClassInterface(ps, generatedObjectList, objDescriptor, null);
            }
            generatedObjectList = new List<string>();
            foreach (ObjectClassDescriptor objDescriptor in descriptorManager.ObjectClassDescriptors)
            {
                GenerateObjectInstanceProxy(ps, generatedObjectList, objDescriptor, null);
            }
        }


        /// <summary> 
        /// Generates all the object class definitions.
        /// </summary>
        private void GenerateObjectClassSerializers(System.IO.StreamWriter ps, int indentLevel)
        {
            List<string> generatedObjectList = new List<string>();
            foreach (ObjectClassDescriptor objDescriptor in descriptorManager.ObjectClassDescriptors)
            {
                PrintObjectPropertiesSerializers(ps, objDescriptor, indentLevel);
            }
        }

        /// <summary> 
        /// Generates all the object class interfaces.
        /// </summary>
        private void GenerateObjectClassInterface(System.IO.StreamWriter stream, List<string> generatedObjectClass, ObjectClassDescriptor objDescriptor, String superInterfaceName)
        {
            if (objDescriptor == null)
            {
                return;
            }
            else if (objDescriptor.ParentDescriptors.Count != 0)
            {
                ObjectClassDescriptor parentDescriptor = objDescriptor.ParentDescriptors[0];
                if (!generatedObjectClass.Contains(parentDescriptor.Name))
                    GenerateObjectClassInterface(stream, generatedObjectClass, parentDescriptor, superInterfaceName);
            }

            generatedObjectClass.Add(objDescriptor.Name);
            try
            {
                System.IO.StreamWriter sw;
                int indentLevel = 0;
                string indentStr = GenerateIndentString(indentLevel);
                String interfaceName = GetInterfaceName(objDescriptor.Name);
                String qualifiedInterfaceName = packagePrefix + interfaceName;

                if (stream == null)
                {
                    String path = qualifiedInterfaceName.Replace('.', '/') + ".cs";


                    System.IO.FileInfo sourceFile = new System.IO.FileInfo(targetDirectory.FullName + "\\" + path);
                    System.IO.Directory.CreateDirectory(new System.IO.FileInfo(sourceFile.DirectoryName).FullName);
                    System.IO.FileStream fos = new System.IO.FileStream(sourceFile.FullName, System.IO.FileMode.Create);
                    sw = new System.IO.StreamWriter(fos);

                    System.String packageName = GetPackageName(qualifiedInterfaceName);

                    if (packageName != null)
                    {
                        sw.WriteLine(indentStr + "namespace " + packageName + ";");
                    }
                    else
                    {
                        sw.WriteLine(indentStr + "namespace Sxta.Rti1516.Proxies");
                    }
                    sw.WriteLine(indentStr + "{");
                    indentLevel++;
                    indentStr = GenerateIndentString(indentLevel);

                    sw.WriteLine(indentStr + "using System;");
                    sw.WriteLine();
                    sw.WriteLine(indentStr + "using Hla.Rti1516;");
                    sw.WriteLine(indentStr + "using Sxta.Rti1516.Reflection;");
                }
                else
                {
                    sw = stream;
                }
                sw.WriteLine();

                PrintClassComment(sw, " Autogenerated object instance interface.", indentLevel);
                //GenerateHLAObjectClassAttribute(indentLevel, sw, interactionDescriptor);
                if (superInterfaceName != null)
                {
                    sw.Write(indentStr + "public interface " + interfaceName + " : " + superInterfaceName);

                    if (objDescriptor.ParentDescriptors.Count > 0)
                    {
                        foreach (ObjectClassDescriptor parent in objDescriptor.ParentDescriptors)
                        {
                            if (!parent.Name.Equals(superInterfaceName))
                            {
                                sw.WriteLine(",");
                                sw.Write("                 " + Spacer(interfaceName) + "         " + GetInterfaceName(parent.Name));
                            }
                        }
                    }
                }
                else
                {
                    sw.Write(indentStr + "public interface " + interfaceName);

                    if (objDescriptor.ParentDescriptors.Count > 0)
                    {
                        sw.Write(" : ");

                        ObjectClassDescriptor parent;
                        int length = objDescriptor.ParentDescriptors.Count;
                        for (int i = 0; i < length; i++)
                        {
                            parent = objDescriptor.ParentDescriptors[i];
                            sw.Write(GetInterfaceName(parent.Name));

                            if (i < length - 1)
                            {
                                sw.WriteLine(",");
                                sw.Write("                 " + Spacer(interfaceName) + "         ");
                            }
                        }
                    }
                }
                sw.WriteLine();
                sw.WriteLine(indentStr + "{");
                indentLevel++;
                indentStr = GenerateIndentString(indentLevel);

                foreach (AttributeDescriptor attributeDescriptor in objDescriptor.AttributeDescriptors)
                {
                    String attribute = attributeDescriptor.Name;
                    String capitalizedAttribute = BuildPropertyName(attribute, interfaceName);
                    String attributeType = attributeDescriptor.attribute.DataType;
                    String attributeNativeType = NativeTypeForDataType(attributeType);

                    if (attributeNativeType != null)
                    {
                        sw.WriteLine();
                        sw.WriteLine(indentStr + "///<summary>Gets/Sets the value of the " + attributeDescriptor.Name + " attribute.</summary>");
                        //GenerateHLAAttributeAttribute(indentLevel, sw, parameterDescriptor);
                        if (attributeNativeType.StartsWith("HLA") && !attributeNativeType.EndsWith("[]"))
                            sw.WriteLine(indentStr + attributeNativeType + " " + capitalizedAttribute);
                        else
                            sw.WriteLine(indentStr + attributeNativeType + " " + capitalizedAttribute);
                        sw.WriteLine(indentStr + "{");
                        sw.WriteLine(GenerateIndentString(indentLevel + 1) + "get;");
                        sw.WriteLine(GenerateIndentString(indentLevel + 1) + "set;");
                        sw.WriteLine(indentStr + "}");
                    }
                }

                indentLevel--;
                indentStr = GenerateIndentString(indentLevel);
                sw.WriteLine(indentStr + "}");

                if (stream == null)
                {
                    indentLevel--;
                    indentStr = GenerateIndentString(indentLevel);
                    sw.WriteLine(indentStr + "}");
                    sw.Flush();
                    sw.Close();
                }
            }
            catch (System.IO.IOException ioe)
            {
                if (log.IsErrorEnabled)
                    log.Error("Error generating object instance interface: " + ioe);
            }
        }

        /// <summary> 
        /// Generates an object instance proxy source file.
        /// </summary>
        /// <param name="classElement">the object instance class element containing the relevant
        /// information
        /// </param>
        /// <param name="superClassName">the name of the proxy superclass
        /// </param>
        /// <exception cref=""> TypeConflictException if a type conflict is detected
        /// </exception>
        private void GenerateObjectInstanceProxy(System.IO.StreamWriter stream, List<string> generatedObjectClass, ObjectClassDescriptor objDescriptor, String superClassName)
        {
            if (objDescriptor == null)
            {
                return;
            }
            else if (objDescriptor.ParentDescriptors.Count != 0)
            {
                ObjectClassDescriptor parentDescriptor = objDescriptor.ParentDescriptors[0];
                if (!generatedObjectClass.Contains(parentDescriptor.Name))
                    GenerateObjectClassInterface(stream, generatedObjectClass, parentDescriptor, superClassName);
            }

            generatedObjectClass.Add(objDescriptor.Name);
            try
            {
                System.IO.StreamWriter sw;
                int indentLevel = 0;
                string indentStr = GenerateIndentString(indentLevel);

                String className = objDescriptor.Name + "";
                String qualifiedClassName = packagePrefix + className;
                if (stream == null)
                {
                    String path = qualifiedClassName.Replace('.', '/') + ".cs";

                    System.IO.FileInfo sourceFile = new System.IO.FileInfo(targetDirectory.FullName + "\\" + path);
                    System.IO.Directory.CreateDirectory(new System.IO.FileInfo(sourceFile.DirectoryName).FullName);


                    System.IO.FileStream fos = new System.IO.FileStream(sourceFile.FullName, System.IO.FileMode.Create);
                    sw = new System.IO.StreamWriter(fos);

                    System.String packageName = GetPackageName(qualifiedClassName);

                    if (packageName != null)
                    {
                        sw.WriteLine(indentStr + "namespace " + packageName + ";");
                    }
                    else
                    {
                        sw.WriteLine(indentStr + "namespace Sxta.Rti1516.Proxies");
                    }
                    sw.WriteLine(indentStr + "{");
                    indentLevel++;
                    indentStr = GenerateIndentString(indentLevel);
                    sw.WriteLine();
                    sw.WriteLine(indentStr + "using System;");
                    sw.WriteLine(indentStr + "using System.IO;");
                    sw.WriteLine(indentStr + "using System.Collections.Generic;");
                    sw.WriteLine();
                    sw.WriteLine(indentStr + "using Hla.Rti1516;");
                    sw.WriteLine();
                    sw.WriteLine(indentStr + "using Hla.Rti1516;");
                    sw.WriteLine(indentStr + "using Sxta.Rti1516.Serializers.XrtiEncoding;");
                    sw.WriteLine(indentStr + "using Sxta.Rti1516.Reflection;");
                    sw.WriteLine(indentStr + "using Sxta.Rti1516.Interactions;");
                    sw.WriteLine(indentStr + "using Sxta.Rti1516.BoostrapProtocol;");


                    if (!string.IsNullOrEmpty(superClassName) && superClassName.Equals("ObjectInstanceProxy"))
                    {
                        sw.WriteLine(indentStr + "using ObjectInstanceProxy = Sxta.Rti1516.XrtiUtils.ObjectInstanceProxy;");
                    }
                    else
                    {
                        String qualifiedSuperClassName = packagePrefix + superClassName;
                        String superClassPackage = GetPackageName(qualifiedSuperClassName);

                        if ((packageName == null && superClassPackage != null) ||
                            (packageName != null && superClassPackage == null) ||
                            (packageName != null && superClassPackage != null &&
                            !packageName.Equals(superClassPackage)))
                        {
                            sw.WriteLine(indentStr + "using " + qualifiedSuperClassName + ";");
                        }
                    }

                }
                else
                {
                    sw = stream;
                }
                sw.WriteLine();

                if (!string.IsNullOrEmpty(objDescriptor.objectDescription.Semantics))
                {
                    PrintClassComment(sw, objDescriptor.objectDescription.Semantics, indentLevel);
                }
                else
                {
                    PrintClassComment(sw, "Autogenerated object instance proxy.", indentLevel);
                }
                GenerateHLAObjectClassAttribute(indentLevel, sw, objDescriptor);
                if (string.IsNullOrEmpty(superClassName))
                {
                    sw.WriteLine(indentStr + "public class " + className + " : " + " I" + objDescriptor.Name);
                    if (objDescriptor.ParentDescriptors.Count > 0)
                    {
                        foreach (ObjectClassDescriptor parent in objDescriptor.ParentDescriptors)
                        {
                            if (!parent.Name.Equals(superClassName))
                            {
                                sw.WriteLine(",");
                                sw.Write("                 " + Spacer(objDescriptor.Name) + "         " + GetInterfaceName(parent.Name));
                            }
                        }
                    }

                }
                else
                {
                    sw.WriteLine(indentStr + "public class " + className + " : " + superClassName + " , I" + objDescriptor.Name);
                    foreach (ObjectClassDescriptor parent in objDescriptor.ParentDescriptors)
                    {
                        if (!parent.Name.Equals(superClassName))
                        {
                            sw.WriteLine(",");
                            sw.Write("                 " + Spacer(objDescriptor.Name) + "         " + GetInterfaceName(parent.Name));
                        }
                    }
                    sw.WriteLine("                 " + Spacer(objDescriptor.Name) + "         " + " , I" + objDescriptor.Name);
                }
                sw.WriteLine(indentStr + "{");
                sw.WriteLine();
                indentLevel++;
                indentStr = GenerateIndentString(indentLevel);

                foreach (AttributeDescriptor attrDescriptor in objDescriptor.AttributeDescriptors)
                {
                    System.String attribute = attrDescriptor.Name;
                    if (!string.IsNullOrEmpty(NativeTypeForDataType(attrDescriptor.attribute.DataType)))
                    {
                        if (!string.IsNullOrEmpty(attrDescriptor.attribute.Semantics))
                        {
                            PrintVariableComment(sw, attrDescriptor.attribute.Semantics, indentLevel);
                        }
                        else
                        {
                            PrintVariableComment(sw, "Attribute #" + attrDescriptor.Name + ".", indentLevel);
                        }

                        sw.WriteLine(indentStr + "private " + NativeTypeForDataType(attrDescriptor.attribute.DataType) + " " + BuildFieldName(attrDescriptor.Name, className) + ";");
                        sw.WriteLine();
                    }
                }

                //----------------------------------------
                // FixedRecordDataType.ToString
                //----------------------------------------

                sw.WriteLine(indentStr + "///<summary> Returns a string representation of this " + className + ". </summary>");
                sw.WriteLine(indentStr + "///<returns> a string representation of this " + className + "</returns>");
                sw.WriteLine(indentStr + "public override String ToString()");
                sw.WriteLine(indentStr + "{");
                indentLevel++;
                indentStr = GenerateIndentString(indentLevel);

                sw.WriteLine(indentStr + "return \"" + className + "(\" +");

                bool first = true;

                for (int i = 0; i < objDescriptor.AttributeDescriptors.Count; i++)
                {
                    IHLAattribute field = objDescriptor.AttributeDescriptors[i].attribute;

                    System.String nativeType = NativeTypeForDataType(field.DataType);
                    string nativefieldName = BuildFieldName(field.Name, className);

                    if (nativeType != null)
                    {
                        if (!first)
                        {
                            sw.WriteLine(" + \", \" +");
                        }
                        else
                        {
                            first = false;
                        }

                        sw.Write(indentStr + "         \"" + field.Name + ": \" + " + nativefieldName);
                    }
                }

                if (!first)
                {
                    sw.WriteLine(" + ");
                }
                sw.WriteLine(indentStr + "       \")\";");
                indentLevel--;
                indentStr = GenerateIndentString(indentLevel);
                sw.WriteLine(indentStr + "}");

                //----------------------------------------
                // FixedRecordDataType.Attributes (Gets/Sets)
                //----------------------------------------

                for (int i = 0; i < objDescriptor.AttributeDescriptors.Count; i++)
                {
                    AttributeDescriptor attrDescrip = objDescriptor.AttributeDescriptors[i];
                    IHLAattribute field = attrDescrip.attribute;

                    System.String fieldName = BuildFieldName(field.Name, className);
                    System.String capitalizedFieldName = BuildPropertyName(field.Name, className);
                    System.String fieldType = field.DataType;
                    System.String fieldNativeType = NativeTypeForDataType(fieldType);


                    if (fieldNativeType != null)
                    {
                        sw.WriteLine();
                        sw.WriteLine(indentStr + "///<summary>");
                        sw.WriteLine(indentStr + "/// Gets/Sets the value of the " + field.Name + " field.");
                        sw.WriteLine(indentStr + "///</summary>");
                        GenerateHLAAttributeAttribute(indentLevel, sw, attrDescrip);
                        sw.WriteLine(indentStr + "public " + fieldNativeType + " " + capitalizedFieldName);
                        sw.WriteLine(indentStr + "{");
                        sw.WriteLine(indentStr + "    set {" + fieldName + " = value; }");
                        sw.WriteLine(indentStr + "    get { return " + fieldName + "; }");
                        sw.WriteLine(indentStr + "}");
                        sw.WriteLine();

                    }
                }

                indentLevel--;
                indentStr = GenerateIndentString(indentLevel);
                sw.WriteLine(indentStr + "}");

                if (stream == null)
                {
                    indentLevel--;
                    indentStr = GenerateIndentString(indentLevel);
                    sw.WriteLine(indentStr + "}");
                    sw.Flush();
                    sw.Close();
                }
            }
            catch (System.IO.IOException ioe)
            {
                System.Console.Error.WriteLine("Error generating object instance proxy: " + ioe);
            }
        }

        /// <summary> 
        /// Prints the set of serializers class for the Properties of the specified object class
        /// element.
        /// </summary>
        /// <param name="sw">the stream to print to
        /// </param>
        /// <param name="objectClassElement">the object class element to process
        /// </param>
        private void PrintObjectPropertiesSerializers(System.IO.StreamWriter stream, ObjectClassDescriptor objDescriptor, int originalIndentLevel)
        {
            foreach (AttributeDescriptor attr in objDescriptor.AttributeDescriptors)
            {
                System.IO.StreamWriter sw;
                int indentLevel = originalIndentLevel;
                string indentStr = GenerateIndentString(indentLevel);

                System.String attribute = attr.Name;
                System.String attributeType = attr.attribute.DataType;
                System.String attributeNativeType = NativeTypeForDataType(attributeType);
                string serializerName = objDescriptor.Name + "Property" + attribute;

                if (attributeNativeType != null)
                {
                    try
                    {
                        String qualifiedClassName = packagePrefix + serializerName;
                        if (stream == null)
                        {
                            String path = qualifiedClassName.Replace('.', '/') + ".cs";

                            System.IO.FileInfo sourceFile = new System.IO.FileInfo(targetDirectory.FullName + "\\" + path);
                            System.IO.Directory.CreateDirectory(new System.IO.FileInfo(sourceFile.DirectoryName).FullName);


                            System.IO.FileStream fos = new System.IO.FileStream(sourceFile.FullName, System.IO.FileMode.Create);
                            sw = new System.IO.StreamWriter(fos);

                            System.String packageName = GetPackageName(qualifiedClassName);

                            if (packageName != null)
                            {
                                sw.WriteLine(indentStr + "namespace " + packageName + ";");
                            }
                            else
                            {
                                sw.WriteLine(indentStr + "namespace Sxta.Rti1516.Proxies");
                            }
                            sw.WriteLine(indentStr + "{");
                            indentLevel++;
                            indentStr = GenerateIndentString(indentLevel);
                            sw.WriteLine();
                            sw.WriteLine(indentStr + "using System;");
                            sw.WriteLine(indentStr + "using System.IO;");
                            sw.WriteLine(indentStr + "using System.Collections.Generic;");
                            sw.WriteLine();
                            sw.WriteLine(indentStr + "using Hla.Rti1516;");
                            sw.WriteLine();
                            sw.WriteLine(indentStr + "using HlaEncodingReader = Sxta.Rti1516.Serializers.XrtiEncoding.HlaEncodingReader;");
                            sw.WriteLine(indentStr + "using HlaEncodingWriter = Sxta.Rti1516.Serializers.XrtiEncoding.HlaEncodingWriter;");
                            sw.WriteLine(indentStr + "using Sxta.Rti1516.Reflection;");
                            sw.WriteLine(indentStr + "using Sxta.Rti1516.Serializers.XrtiEncoding;");
                        }
                        else
                        {
                            sw = stream;
                        }
                        sw.WriteLine();
                        PrintClassComment(sw, "Autogenerated Serializer Helper. Serializes and deserializes " + objDescriptor.Name + "." + attribute + " parameters into and from HLA formats.", indentLevel);
                        sw.WriteLine(indentStr + "public class " + serializerName + "XrtiSerializer : BaseInteractionMessageXrtiSerializer");
                        sw.WriteLine(indentStr + "{");
                        indentLevel++;
                        indentStr = GenerateIndentString(indentLevel);
                        sw.WriteLine(indentStr + "///<summary>Constructor for the serializer of " + objDescriptor.Name + "." + attribute + " property.");
                        sw.WriteLine(indentStr + "/// </summary>");
                        sw.WriteLine(indentStr + "public " + serializerName + "XrtiSerializer(XrtiSerializerManager manager)");
                        sw.WriteLine(indentStr + ": base(manager)");
                        sw.WriteLine(indentStr + "{");
                        sw.WriteLine(indentStr + "}");
                        sw.WriteLine();

                        sw.WriteLine(indentStr + "///<summary>");
                        sw.WriteLine(indentStr + "/// Writes this " + objDescriptor.Name + "." + attribute + " to the specified stream.");
                        sw.WriteLine(indentStr + "///</summary>");
                        sw.WriteLine(indentStr + "///<param name=\"writer\"> the output stream to write to</param>");
                        sw.WriteLine(indentStr + "///<param name=\"" + attribute + "\"> the property to serialize</param>");
                        sw.WriteLine(indentStr + "///<exception cref=\"System.IO.IOException\"> if an error occurs</exception>");
                        sw.WriteLine(indentStr + "public override void Serialize(HlaEncodingWriter writer, object " + attribute + ")");
                        sw.WriteLine(indentStr + "{");
                        indentLevel++;
                        indentStr = GenerateIndentString(indentLevel);

                        sw.WriteLine(indentStr + "try");
                        sw.WriteLine(indentStr + "{");

                        PrintSerializationBlock(sw, indentLevel + 1, 'i', attributeType, "(" + attributeNativeType + ")" + attribute, "writer");

                        sw.WriteLine(indentStr + "}");
                        sw.WriteLine(indentStr + "catch(IOException ioe)");
                        sw.WriteLine(indentStr + "{");
                        sw.WriteLine(GenerateIndentString(indentLevel + 1) + "throw new RTIinternalError(ioe.ToString());");
                        sw.WriteLine(indentStr + "}");
                        sw.WriteLine();

                        indentLevel--;
                        indentStr = GenerateIndentString(indentLevel);
                        sw.WriteLine(indentStr + "}");
                        sw.WriteLine();

                        sw.WriteLine(indentStr + "///<summary>");
                        sw.WriteLine(indentStr + "/// Reads and returns a " + objDescriptor.Name + "." + attribute + " from the specified stream.");
                        sw.WriteLine(indentStr + "///</summary>");
                        sw.WriteLine(indentStr + "///<param name=\"reader\"> the input stream to read from</param>");
                        sw.WriteLine(indentStr + "///<param name=\"dummy\"> this parameter is not used</param>");
                        sw.WriteLine(indentStr + "///<returns> the decoded value</returns>");
                        sw.WriteLine(indentStr + "///<exception cref=\"System.IO.IOException\"> if an error occurs</exception>");
                        sw.WriteLine(indentStr + "public override object Deserialize(HlaEncodingReader reader, ref object dummy)");
                        sw.WriteLine(indentStr + "{");
                        indentLevel++;
                        indentStr = GenerateIndentString(indentLevel);

                        sw.WriteLine(indentStr + attributeNativeType + " decodedValue;");
                        sw.WriteLine(indentStr + "try");
                        sw.WriteLine(indentStr + "{");
                        PrintDeserializationBlock(sw, indentLevel + 1, 'i', attributeType, "decodedValue", "reader");
                        sw.WriteLine(GenerateIndentString(indentLevel + 1) + "return decodedValue;");
                        sw.WriteLine(indentStr + "}");
                        sw.WriteLine(indentStr + "catch(IOException ioe)");
                        sw.WriteLine(indentStr + "{");
                        sw.WriteLine(GenerateIndentString(indentLevel + 1) + "throw new FederateInternalError(ioe.ToString());");
                        sw.WriteLine(indentStr + "}");

                        indentLevel--;
                        indentStr = GenerateIndentString(indentLevel);
                        sw.WriteLine(indentStr + "}");


                        indentLevel--;
                        indentStr = GenerateIndentString(indentLevel);
                        sw.WriteLine(indentStr + "}");

                        if (stream == null)
                        {
                            indentLevel--;
                            indentStr = GenerateIndentString(indentLevel);
                            sw.WriteLine(indentStr + "}");
                            sw.Flush();
                            sw.Close();
                        }
                    }
                    catch (System.IO.IOException ioe)
                    {
                        System.Console.Error.WriteLine("Error generating object properties serializers: " + ioe);
                    }
                }
            }
        }
    }
}
