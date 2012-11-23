using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

// Import log4net classes.
using log4net;

using Hla.Rti1516;
using Sxta.Rti1516.Reflection;
using Sxta.Rti1516.Serializers.XrtiEncoding;

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
        /// Generates Interactions Class.
        /// </summary>
        private void GenerateInteractions(System.IO.StreamWriter sw)
        {
            List<string> generatedInteractiontList = new List<string>();
            foreach (InteractionClassDescriptor interaction in DescriptorManager.InteractionClassDescriptors)
            {
                GenerateInteractionsMessages(sw, generatedInteractiontList, interaction, null);
            }
        }

        /// <summary> 
        /// Generates Interactions Class.
        /// </summary>
        private void GenerateInteractionsSerializers(System.IO.StreamWriter sw, int indentLevel)
        {
            List<string> generatedInteractiontList = new List<string>();
            foreach (InteractionClassDescriptor interaction in DescriptorManager.InteractionClassDescriptors)
            {
                GenerateInteractionsSerializers(sw, generatedInteractiontList, interaction, null, indentLevel);
            }
            GenerateInteractionListener(sw, indentLevel);
            generatedInteractiontList = new List<string>();
            GenerateInteractionsAndSerializersHelper(sw, generatedInteractiontList, null, indentLevel);
        }


        /// <summary> 
        /// Generates a <code>HLAInteractionClass</code>.
        /// </summary>
        private void GenerateHLAInteractionClassAttribute(int localIndentLevel, System.IO.StreamWriter ps, InteractionClassDescriptor interactionDescriptor)
        {
            string indentStr = GenerateIndentString(localIndentLevel);
            string newLine = "," + Environment.NewLine + indentStr + "                ";
            ps.Write(indentStr + "[HLAInteractionClass(Name = \"" + interactionDescriptor.Name + "\"");
            ps.Write(newLine);
            ps.Write("Sharing = " + interactionDescriptor.interactionClass.Sharing.GetType() + "." + interactionDescriptor.interactionClass.Sharing);
            ps.Write(newLine);
            ps.Write("Order = " + interactionDescriptor.interactionClass.Order.GetType() + "." + interactionDescriptor.interactionClass.Order);

            if (!String.IsNullOrEmpty(interactionDescriptor.interactionClass.SharingNotes))
            {
                ps.Write(newLine);
                ps.Write("SharingNotes = \"" + interactionDescriptor.interactionClass.SharingNotes + "\"");
            }
            if (!String.IsNullOrEmpty(interactionDescriptor.interactionClass.Semantics))
            {
                ps.Write(newLine);
                ps.Write("Semantics = \"" + interactionDescriptor.interactionClass.Semantics + "\"");
            }
            if (!String.IsNullOrEmpty(interactionDescriptor.interactionClass.SemanticsNotes))
            {
                ps.Write(newLine);
                ps.Write("SemanticsNotes = \"" + interactionDescriptor.interactionClass.SemanticsNotes + "\"");
            }
            if (!String.IsNullOrEmpty(interactionDescriptor.interactionClass.Dimensions))
            {
                ps.Write(newLine);
                ps.Write("Dimensions = \"" + interactionDescriptor.interactionClass.Dimensions + "\"");
            }
            if (!String.IsNullOrEmpty(interactionDescriptor.interactionClass.Transportation))
            {
                ps.Write(newLine);
                ps.Write("Transportation = \"" + interactionDescriptor.interactionClass.Transportation + "\"");
            }
            if (!String.IsNullOrEmpty(interactionDescriptor.interactionClass.NameNotes))
            {
                ps.Write(newLine);
                ps.Write("NameNotes = \"" + interactionDescriptor.interactionClass.NameNotes + "\"");
            }
            ps.WriteLine(")]");
        }

        /// <summary> 
        /// Generates a <code>HLAInteractionParameter</code>.
        /// </summary>
        private void GenerateHLAInteractionParameterAttribute(int localIndentLevel, System.IO.StreamWriter ps, ParameterDescriptor parameterDescriptor)
        {
            string indentStr = GenerateIndentString(localIndentLevel);
            string newLine = "," + Environment.NewLine + indentStr + "              ";

            ps.Write(indentStr + "[HLAInteractionParameter(Name = \"" + parameterDescriptor.Name + "\"");
            if (!String.IsNullOrEmpty(parameterDescriptor.NameNotes))
            {
                ps.Write(newLine);
                ps.Write("NameNotes = \"" + parameterDescriptor.NameNotes + "\"");
            }
            if (!String.IsNullOrEmpty(parameterDescriptor.Semantics))
            {
                ps.Write(newLine);
                ps.Write("Semantics = \"" + parameterDescriptor.Semantics + "\"");
            }
            if (!String.IsNullOrEmpty(parameterDescriptor.SemanticsNotes))
            {
                ps.Write(newLine);
                ps.Write("SemanticsNotes = \"" + parameterDescriptor.SemanticsNotes + "\"");
            }
            if (!String.IsNullOrEmpty(parameterDescriptor.DataType))
            {
                ps.Write(newLine);
                ps.Write("DataType = \"" + parameterDescriptor.DataType + "\"");
            }
            if (!String.IsNullOrEmpty(parameterDescriptor.DataTypeNotes))
            {
                ps.Write(newLine);
                ps.Write("DataTypeNotes = \"" + parameterDescriptor.DataTypeNotes + "\"");
            }
            ps.WriteLine(")]");
        }

        /// <summary> 
        /// Generates Interactions Class.
        /// </summary>
        private void GenerateInteractionsMessages(System.IO.StreamWriter stream, List<string> generatedInteractiontList,
                                                  InteractionClassDescriptor interactionClassDescriptor, string superInteractionName)
        {
            if (interactionClassDescriptor == null)
            {
                return;
            }
            else if (interactionClassDescriptor.ParentDescriptors.Count != 0)
            {
                InteractionClassDescriptor parentDescriptor = interactionClassDescriptor.ParentDescriptors[0];
                if (!generatedInteractiontList.Contains(parentDescriptor.Name))
                    GenerateInteractionsMessages(stream, generatedInteractiontList, parentDescriptor, superInteractionName);
            }
            generatedInteractiontList.Add(interactionClassDescriptor.Name);
            try
            {
                System.IO.StreamWriter sw;
                int indentLevel = 0;
                string indentStr = GenerateIndentString(indentLevel);
                HLAinteractionClass interactionClass = interactionClassDescriptor.interactionClass;
                String interaction = interactionClass.Name;
                List<ParameterDescriptor> parametersDescriptor = interactionClassDescriptor.ParameterDescriptors;
                String interactionMessageName = GetInteractionMessageName(interaction);
                String qualifiedInteractionName = packagePrefix + interactionMessageName;

                if (stream == null)
                {
                    String path = qualifiedInteractionName.Replace('.', Path.DirectorySeparatorChar) + ".cs";


                    System.IO.FileInfo sourceFile = new System.IO.FileInfo(targetDirectory.FullName + Path.DirectorySeparatorChar + path);
                    System.IO.Directory.CreateDirectory(new System.IO.FileInfo(sourceFile.DirectoryName).FullName);
                    System.IO.FileStream fos = new System.IO.FileStream(sourceFile.FullName, System.IO.FileMode.Create);
                    sw = new System.IO.StreamWriter(fos);

                    System.String packageName = GetPackageName(qualifiedInteractionName);

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
                    sw.WriteLine(indentStr + "using Sxta.Rti1516.Serializers.XrtiEncoding;");

                }
                else
                {
                    sw = stream;
                }
                sw.WriteLine();

                //------------------------------------------ Generation of Message -----------------------------------
                sw.WriteLine();
                bool hasParent = true;
                if (!string.IsNullOrEmpty(interactionClass.Semantics))
                {
                    sw.WriteLine(indentStr + "///<summary>");
                    sw.Write(FormatCommentBody(indentLevel, "Message for " + interaction + " iteraction : " + interactionClass.Semantics));
                    sw.WriteLine(indentStr + "///</summary>");

                }
                else
                {
                    sw.WriteLine(indentStr + "///<summary> Message for " + interaction + " iteraction : " + interaction + " classSerializerHelperName.</summary>");
                }
                GenerateHLAInteractionClassAttribute(indentLevel, sw, interactionClassDescriptor);
                if (!String.IsNullOrEmpty(superInteractionName))
                {
                    sw.Write(indentStr + "public class " + interactionMessageName + " : " + GetInteractionMessageName(superInteractionName));

                    if (interactionClassDescriptor.ParentDescriptors.Count > 0)
                    {
                        foreach (InteractionClassDescriptor parent in interactionClassDescriptor.ParentDescriptors)
                        {
                            if (!parent.Name.Equals(superInteractionName))
                            {
                                sw.Write(", " + GetInteractionMessageName(parent.Name));
                            }
                        }
                        sw.WriteLine();
                    }
                    else
                    {
                        hasParent = false;
                    }
                }
                else
                {
                    sw.Write(indentStr + "public class " + interactionMessageName);

                    if (interactionClassDescriptor.ParentDescriptors.Count > 0)
                    {
                        sw.Write(" : ");

                        InteractionClassDescriptor parent;
                        int length = interactionClassDescriptor.ParentDescriptors.Count;
                        for (int i = 0; i < length; i++)
                        {
                            parent = interactionClassDescriptor.ParentDescriptors[i];
                            sw.Write(GetInteractionMessageName(parent.Name));

                            if (i < length - 1)
                            {
                                sw.Write(", ");
                            }
                        }
                        sw.WriteLine();
                    }
                    else
                    {
                        sw.WriteLine(": BaseInteractionMessage");
                    }
                }
                sw.WriteLine();
                sw.WriteLine(indentStr + "{");
                indentLevel += 1;
                indentStr = GenerateIndentString(indentLevel);

                foreach (ParameterDescriptor parameter in parametersDescriptor)
                {
                    System.String parameterName = BuildFieldName(parameter.Name, interaction + "Message");
                    System.String parameterPropertyName = BuildPropertyName(parameter.Name, interaction + "Message");
                    System.String parameterType = parameter.DataType;
                    System.String parameterNativeType = NativeTypeForDataType(parameterType);

                    sw.WriteLine(indentStr + parameterNativeType + " " + parameterName + ";");

                    sw.WriteLine();
                    if (!string.IsNullOrEmpty(parameter.Semantics))
                    {
                        sw.Write(FormatCommentBody(indentLevel, "<summary>" + parameter.Semantics + "</summary>"));
                    }
                    else
                    {
                        sw.WriteLine(indentStr + "///<summary> Gets/Sets the value of the " + parameter.Name + " parameter </summary>");
                    }

                    GenerateHLAInteractionParameterAttribute(indentLevel, sw, parameter);
                    sw.WriteLine(indentStr + "public " + parameterNativeType + " " + parameterPropertyName);
                    sw.WriteLine(indentStr + "{");
                    sw.WriteLine(GenerateIndentString(indentLevel + 1) + "get { return " + parameterName + ";}");
                    sw.WriteLine(GenerateIndentString(indentLevel + 1) + "set { " + parameterName + " = value;}");
                    sw.WriteLine(indentStr + "}");
                    sw.WriteLine();
                }

                sw.WriteLine(indentStr + "///<summary> Returns a string representation of this " + interactionMessageName + ". </summary>");
                sw.WriteLine(indentStr + "///<returns> a string representation of this " + interactionMessageName + "</returns>");
                sw.WriteLine(indentStr + "public override string ToString()");
                sw.WriteLine(indentStr + "{");
                if (!hasParent)
                {
                    sw.WriteLine(indentStr + "    return \"" + interactionMessageName + "();");
                }
                else
                {
                    sw.Write(indentStr + "    return \"" + interactionMessageName + "(\" + base.ToString()");
                }
                foreach (ParameterDescriptor parameter in parametersDescriptor)
                {
                    System.String parameterName = parameter.Name;
                    System.String parameterType = parameter.DataType;
                    sw.WriteLine();
                    sw.Write(indentStr + "           + \", " + Capitalize(parameterName) + ": \" + " + BuildPropertyName(parameter.Name, interactionMessageName));

                }
                if (hasParent)
                    sw.WriteLine(" + \")\";");
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
                if (log.IsErrorEnabled)
                    log.Error("Error generating object instance interface: " + ioe);
            }
        }

        /// <summary> 
        /// Generates Interactions Class serializers.
        /// </summary>
        private void GenerateInteractionsSerializers(System.IO.StreamWriter stream, List<string> generatedInteractiontList,
                                                     InteractionClassDescriptor interactionClassDescriptor, string superInteractionName,
                                                     int originalIndentLevel)
        {
            if (interactionClassDescriptor == null)
            {
                return;
            }
            else if (interactionClassDescriptor.ParentDescriptors.Count != 0)
            {
                InteractionClassDescriptor parentDescriptor = interactionClassDescriptor.ParentDescriptors[0];
                if (!generatedInteractiontList.Contains(parentDescriptor.Name))
                    GenerateInteractionsMessages(stream, generatedInteractiontList, parentDescriptor, superInteractionName);
            }
            generatedInteractiontList.Add(interactionClassDescriptor.Name);
            try
            {
                System.IO.StreamWriter sw;
                int indentLevel = originalIndentLevel;
                string indentStr = GenerateIndentString(indentLevel);
                HLAinteractionClass interactionClass = interactionClassDescriptor.interactionClass;
                String interaction = interactionClass.Name;
                List<ParameterDescriptor> parametersDescriptor = interactionClassDescriptor.ParameterDescriptors;
                String interactionMessageName = GetInteractionMessageName(interaction);
                String qualifiedInteractionName = packagePrefix + interactionMessageName + "XrtiSerializer";
                bool hasParent = true;
                if (stream == null)
                {
                    String path = qualifiedInteractionName.Replace('.', Path.DirectorySeparatorChar) + ".cs";


                    System.IO.FileInfo sourceFile = new System.IO.FileInfo(targetDirectory.FullName + Path.DirectorySeparatorChar + path);
                    System.IO.Directory.CreateDirectory(new System.IO.FileInfo(sourceFile.DirectoryName).FullName);
                    System.IO.FileStream fos = new System.IO.FileStream(sourceFile.FullName, System.IO.FileMode.Create);
                    sw = new System.IO.StreamWriter(fos);

                    System.String packageName = GetPackageName(qualifiedInteractionName);

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
                    sw.WriteLine(indentStr + "using Sxta.Rti1516.Serializers.XrtiEncoding;");
                }
                else
                {
                    sw = stream;
                }
                sw.WriteLine();
                //------------------------------------------ Generation of MessageSerializer -----------------------------------
                sw.WriteLine();
                sw.WriteLine(indentStr + "///<summary>");
                sw.Write(FormatCommentBody(indentLevel, "A HLA serializer for " + interactionMessageName + "."));
                sw.WriteLine(indentStr + "///</summary>");

                if (!String.IsNullOrEmpty(superInteractionName))
                {
                    sw.Write(indentStr + "public class " + interactionMessageName + "XrtiSerializer : " +
                                           GetInteractionMessageName(superInteractionName) + "XrtiSerializer");

                    if (interactionClassDescriptor.ParentDescriptors.Count > 0)
                    {
                        foreach (InteractionClassDescriptor parent in interactionClassDescriptor.ParentDescriptors)
                        {
                            if (!parent.Name.Equals(superInteractionName))
                            {
                                sw.WriteLine(",");
                                sw.Write("                 " + Spacer(interactionMessageName) + "         " +
                                    GetInteractionMessageName(parent.Name) + "XrtiSerializer");
                            }
                        }
                    }
                }
                else
                {
                    sw.Write(indentStr + "public class " + interactionMessageName + "XrtiSerializer");

                    if (interactionClassDescriptor.ParentDescriptors.Count > 0)
                    {
                        sw.Write(" : ");

                        InteractionClassDescriptor parent;
                        int length = interactionClassDescriptor.ParentDescriptors.Count;
                        for (int i = 0; i < length; i++)
                        {
                            parent = interactionClassDescriptor.ParentDescriptors[i];
                            sw.Write(GetInteractionMessageName(parent.Name) + "XrtiSerializer");

                            if (i < length - 1)
                            {
                                sw.WriteLine(",");
                                sw.Write("                 " + Spacer(interactionMessageName + "XrtiSerializer") + "         ");
                            }
                        }
                    }
                    else
                    {
                        sw.WriteLine(": BaseInteractionMessageXrtiSerializer");
                        hasParent = false;
                    }

                }
                sw.WriteLine();
                sw.WriteLine(indentStr + "{");
                indentLevel += 1;
                indentStr = GenerateIndentString(indentLevel);

                sw.WriteLine(indentStr + "///<summary> Constructor </summary>");
                sw.WriteLine(indentStr + "public " + interactionMessageName + "XrtiSerializer(XrtiSerializerManager manager)");
                sw.WriteLine(indentStr + ": base(manager)");
                sw.WriteLine(indentStr + "{");
                sw.WriteLine(indentStr + "}");
                sw.WriteLine();
                sw.WriteLine(indentStr + "///<summary> Writes this " + interactionMessageName + " to the specified stream.</summary>");
                sw.WriteLine(indentStr + "///<param name=\"writer\"> the output stream to write to</param>");
                sw.WriteLine(indentStr + "///<param name=\"obj\"> the object to serialize</param>");
                sw.WriteLine(indentStr + "///<exception cref=\"System.IO.IOException\"> if an error occurs</exception>");
                sw.WriteLine(indentStr + "public override void Serialize(HlaEncodingWriter writer, object obj)");
                sw.WriteLine(indentStr + "{");
                indentStr = GenerateIndentString(indentLevel + 1);
                if (parametersDescriptor.Count > 0)
                {
                    sw.WriteLine(indentStr + "try");
                    sw.WriteLine(indentStr + "{");
                    if (hasParent)
                        sw.WriteLine(GenerateIndentString(indentLevel + 2) + "base.Serialize(writer, obj);");

                    foreach (ParameterDescriptor parameter in interactionClassDescriptor.ParameterDescriptors)
                    {
                        System.String parameterName = parameter.Name;
                        System.String parameterType = parameter.DataType;
                        PrintSerializationBlock(sw, indentLevel + 2, 'i', parameterType, "((" + interactionMessageName + ") obj)." + BuildPropertyName(parameter.Name, interactionMessageName), "writer");
                    }
                    sw.WriteLine(indentStr + "}");
                    sw.WriteLine(indentStr + "catch(System.IO.IOException ioe)");
                    sw.WriteLine(indentStr + "{");
                    sw.WriteLine(GenerateIndentString(indentLevel + 2) + "throw new RTIinternalError(ioe.ToString());");
                    sw.WriteLine(indentStr + "}");
                }
                indentStr = GenerateIndentString(indentLevel);
                sw.WriteLine(indentStr + "}");
                sw.WriteLine();
                sw.WriteLine(indentStr + "///<summary> Reads this " + interactionMessageName + " from the specified stream.</summary>");
                sw.WriteLine(indentStr + "///<param name=\"reader\"> the input stream to read from</param>");
                sw.WriteLine(indentStr + "///<returns> the object</returns>");
                sw.WriteLine(indentStr + "///<exception cref=\"System.IO.IOException\"> if an error occurs</exception>");
                sw.WriteLine(indentStr + "public override object Deserialize(HlaEncodingReader reader, ref object msg)");
                sw.WriteLine(indentStr + "{");
                indentStr = GenerateIndentString(indentLevel + 1);
                sw.WriteLine(indentStr + interactionMessageName + " decodedValue;");
                sw.WriteLine(indentStr + "if (!(msg is " + interactionMessageName + "))");
                sw.WriteLine(indentStr + "{");
                string indent2 = GenerateIndentString(indentLevel + 2);
                sw.WriteLine(indent2 + "decodedValue = new " + interactionMessageName + "();");
                sw.WriteLine(indent2 + "BaseInteractionMessage baseMsg = msg as BaseInteractionMessage;");
                sw.WriteLine(indent2 + "decodedValue.InteractionClassHandle = baseMsg.InteractionClassHandle;");
                sw.WriteLine(indent2 + "decodedValue.FederationExecutionHandle = baseMsg.FederationExecutionHandle;");
                sw.WriteLine(indent2 + "decodedValue.UserSuppliedTag = baseMsg.UserSuppliedTag;");
                sw.WriteLine(indentStr + "}");
                sw.WriteLine(indentStr + "else");
                sw.WriteLine(indentStr + "{");
                sw.WriteLine(indent2 + "decodedValue = msg as " + interactionMessageName + ";");
                sw.WriteLine(indentStr + "}");
                if (hasParent)
                {
                    sw.WriteLine(indentStr + "object tmp = decodedValue;");
                    sw.WriteLine(indentStr + "decodedValue = base.Deserialize(reader, ref tmp) as " + interactionMessageName + ";");
                    //sw.WriteLine(indentStr + interactionMessageName + " msg = (" + interactionMessageName + ") base.Deserialize(reader);");
                }
                else
                {
                    sw.WriteLine();
                    //sw.WriteLine(indentStr + interactionMessageName + " msg = new " + interactionMessageName + "();");
                }
                if (parametersDescriptor.Count > 0)
                {
                    sw.WriteLine(indentStr + "try");
                    sw.WriteLine(indentStr + "{");
                    foreach (ParameterDescriptor parameter in parametersDescriptor)
                    {
                        System.String parameterName = parameter.Name;
                        System.String parameterType = parameter.DataType;
                        PrintDeserializationBlock(sw, indentLevel + 2, 'i', parameterType, "decodedValue." + BuildPropertyName(parameter.Name, interactionMessageName), "reader");
                    }
                    sw.WriteLine(indentStr + "}");
                    sw.WriteLine(indentStr + "catch(System.IO.IOException ioe)");
                    sw.WriteLine(indentStr + "{");
                    sw.WriteLine(GenerateIndentString(indentLevel + 2) + "throw new RTIinternalError(ioe.ToString());");
                    sw.WriteLine(indentStr + "}");
                }
                sw.WriteLine(indentStr + "return decodedValue;");

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
                if (log.IsErrorEnabled)
                    log.Error("Error generating object instance interface: " + ioe);
            }
        }

        /// <summary> 
        /// Generates the interaction listener source file.
        /// </summary>
        /// <param name="fddDocument">the parsed federation description document
        /// </param>
        /// <exception cref="TypeConflictException">  if a type conflict is detected
        /// </exception>
        private void GenerateInteractionListener(System.IO.StreamWriter stream, int originalIndentLevel)
        {
            if (descriptorManager.InteractionClassDescriptors.Count == 0)
                return;
            try
            {
                System.IO.StreamWriter sw;
                int indentLevel = originalIndentLevel;
                string indentStr = GenerateIndentString(indentLevel);
                String classInteractionListenerName = ConvertToNativeClassName(interactionListenerName);
                System.String qualifiedListenerName = packagePrefix + classInteractionListenerName;
                if (stream == null)
                {
                    String path = qualifiedListenerName.Replace('.', Path.DirectorySeparatorChar) + ".cs"; ;

                    System.IO.FileInfo sourceFile = new System.IO.FileInfo(targetDirectory.FullName + Path.DirectorySeparatorChar + path);
                    System.IO.Directory.CreateDirectory(new System.IO.FileInfo(sourceFile.DirectoryName).FullName);
                    System.IO.FileStream fos = new System.IO.FileStream(sourceFile.FullName, System.IO.FileMode.Create);
                    sw = new System.IO.StreamWriter(fos);

                    System.String packageName = GetPackageName(qualifiedListenerName);

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
                    sw.WriteLine(indentStr + "using Sxta.Rti1516.Serializers.XrtiEncoding;");
                    sw.WriteLine(indentStr + "using Sxta.Rti1516.XrtiUtils;");
                }
                else
                {
                    sw = stream;
                }
                sw.WriteLine();

                PrintClassComment(sw, "Autogenerated interaction listener interface.", indentLevel);
                sw.WriteLine(indentStr + "public interface " + interactionListenerName + " : IInteractionListener");
                sw.Write(indentStr + "{");
                indentLevel++;
                indentStr = GenerateIndentString(indentLevel);


                foreach (InteractionClassDescriptor interaction in descriptorManager.InteractionClassDescriptors)
                {
                    if (interaction.interactionClass.Sharing != HLAsharingType.Neither)
                    {
                        sw.WriteLine();
                        System.String interactionName = interaction.interactionClass.Name;

                        if (!string.IsNullOrEmpty(interaction.interactionClass.Semantics))
                        {
                            sw.WriteLine(indentStr + "///<summary>");
                            sw.Write(FormatCommentBody(indentLevel, interaction.interactionClass.Semantics));
                            sw.WriteLine(indentStr + "///</summary>");

                        }
                        else
                        {
                            sw.WriteLine(indentStr + "///<summary> Receives a " + interactionName + " interaction.</summary>");
                        }

                        sw.WriteLine(indentStr + "///<param name=\"msg\"> the message associated with the interaction</param>");
                        sw.WriteLine(indentStr + "///<exception cref=\"InteractionClassNotRecognized\"> if the interaction class was not recognized</exception>");
                        sw.WriteLine(indentStr + "///<exception cref=\"InteractionParameterNotRecognized\"> if a parameter of the interaction was not");
                        sw.WriteLine(indentStr + "/// recognized</exception>");
                        sw.WriteLine(indentStr + "///<exception cref=\"InteractionClassNotSubscribed\"> if the federate had not subscribed to the");
                        sw.WriteLine(indentStr + "/// interaction class</exception>");
                        sw.WriteLine(indentStr + "///<exception cref=\"FederateInternalError\"> if an error occurs in the federate</exception>");
                        sw.WriteLine(indentStr + "void OnReceive" + interactionName + "(" + interactionName + "Message msg);");
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
                if (log.IsErrorEnabled)
                    log.Error("Error generating interaction listener: " + ioe);
            }
        }

        /// <summary> 
        /// Generates Interactions Class Listener.
        /// </summary>
        private void GenerateInteractionsAndSerializersHelper(System.IO.StreamWriter stream, List<string> generatedInteractiontList, string objectModel, int originalIndentLevel)
        {
            try
            {
                System.IO.StreamWriter sw;
                int indentLevel = originalIndentLevel;
                string indentStr = GenerateIndentString(indentLevel);
                String classSerializerHelperName = ConvertToNativeClassName(federationDocumentName) + "InteractionHelper";
                String qualifiedInteractionName = packagePrefix + classSerializerHelperName;
                if (stream == null)
                {
                    String path = qualifiedInteractionName.Replace('.', Path.DirectorySeparatorChar) + ".cs";

                    System.IO.FileInfo sourceFile = new System.IO.FileInfo(targetDirectory.FullName + Path.DirectorySeparatorChar + path);
                    System.IO.Directory.CreateDirectory(new System.IO.FileInfo(sourceFile.DirectoryName).FullName);
                    System.IO.FileStream fos = new System.IO.FileStream(sourceFile.FullName, System.IO.FileMode.Create);
                    sw = new System.IO.StreamWriter(fos);

                    System.String packageName = GetPackageName(qualifiedInteractionName);

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
                    sw.WriteLine(indentStr + "using Sxta.Rti1516.Impl;");
                    sw.WriteLine(indentStr + "using Sxta.Rti1516.Serializers.XrtiEncoding;");
                    sw.WriteLine(indentStr + "using Sxta.Rti1516.XrtiUtils;");
                }
                else
                {
                    sw = stream;
                }
                sw.WriteLine();
                PrintClassComment(sw, "Autogenerated interaction and serializer registration Helper.", indentLevel);
                sw.WriteLine(indentStr + "[HLAinteractionHelperAttribute(Name = \"" + classSerializerHelperName + @""", Semantics = ""A Listener Manager and serializer manager"")]");
                sw.WriteLine(indentStr + "public class " + classSerializerHelperName);
                sw.WriteLine(indentStr + "{");
                indentLevel++;
                indentStr = GenerateIndentString(indentLevel);

                sw.WriteLine(indentStr + "InteractionManager manager;");
                sw.WriteLine();
                sw.WriteLine(indentStr + "/// <summary>Constructor.</summary>");
                sw.WriteLine(indentStr + "/// <param name=\"interactionManager\"> the run-time interaction manager</param>");
                sw.WriteLine(indentStr + "public " + classSerializerHelperName + "(InteractionManager interactionManager)");
                sw.WriteLine(indentStr + "{");
                indentLevel++;
                indentStr = GenerateIndentString(indentLevel);

                sw.WriteLine(indentStr + "Type objType;");
                sw.WriteLine(indentStr + "manager = interactionManager;");
                sw.WriteLine(indentStr + "XrtiSerializerManager serializerMngr = manager.SerializerManager;");
                sw.WriteLine(indentStr + "long handle;");
                sw.WriteLine(indentStr + "ObjectClassDescriptor ocd;");

                /*
                foreach (HLASimpleData simpleData in descriptorManager.SimpleDataTypeList)
                {
                    PrintSerializersRegistration(sw, indentLevel, simpleData.Name);
                }
                foreach (HLAEnumeratedData enumeratedData in descriptorManager.EnumeratedDataTypeList)
                {
                    PrintSerializersRegistration(sw, indentLevel, enumeratedData.Name);
                }
                foreach (HLAFixedRecordData recordData in descriptorManager.FixedRecordDataTypeList)
                {
                    PrintSerializersRegistration(sw, indentLevel, recordData.Name);
                }
                */
                foreach (ObjectClassDescriptor objDescriptor in descriptorManager.ObjectClassDescriptors)
                {
                    PrintObjectClassSerializerRegistration(sw, indentLevel, objDescriptor);
                }

                foreach (InteractionClassDescriptor interactionDescriptor in descriptorManager.InteractionClassDescriptors)
                {
                    PrintInteractionClassSerializerRegistration(sw, indentLevel, interactionDescriptor);
                }

                indentLevel--;
                indentStr = GenerateIndentString(indentLevel);
                sw.WriteLine(indentStr + "}");

                sw.WriteLine();
                sw.WriteLine(indentStr + "/// <summary>Notifies the listener of a received interaction.</summary>");
                sw.WriteLine(indentStr + "/// <param name=\"msg\"> the message of the received interaction</param>");
                sw.WriteLine(indentStr + "public void ReceiveInteraction(BaseInteractionMessage msg)");
                sw.WriteLine(indentStr + "{");
                indentLevel++;
                indentStr = GenerateIndentString(indentLevel);
                sw.WriteLine(indentStr + "try");
                sw.WriteLine(indentStr + "{");
                indentLevel++;
                indentStr = GenerateIndentString(indentLevel);

                bool firstInteraction = true;
                foreach (InteractionClassDescriptor interaction in DescriptorManager.InteractionClassDescriptors)
                {
                    firstInteraction = PrintReceiveInteractionBlock(sw, interaction, interactionListenerName, firstInteraction, indentLevel);
                }
                if (!firstInteraction)
                    sw.WriteLine(indentStr + "else");
                sw.WriteLine(GenerateIndentString(indentLevel + 1) + "foreach (IInteractionListener il in manager.InteractionListeners)");
                sw.WriteLine(GenerateIndentString(indentLevel + 1) + "{");
                sw.WriteLine(GenerateIndentString(indentLevel + 2) + "il.ReceiveInteraction(msg);");
                sw.WriteLine(GenerateIndentString(indentLevel + 1) + "}");

                indentLevel--;
                indentStr = GenerateIndentString(indentLevel);
                sw.WriteLine(indentStr + "}");
                sw.WriteLine(indentStr + "catch(System.IO.IOException ioe)");
                sw.WriteLine(indentStr + "{");
                sw.WriteLine(GenerateIndentString(indentLevel + 1) + "throw new FederateInternalError(ioe.ToString());");
                sw.WriteLine(indentStr + "}");
                sw.WriteLine();

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
                if (log.IsErrorEnabled)
                    log.Error("Error generating interaction and serializers helper: " + ioe);
            }
        }

        /// <summary> 
        /// Prints the receive interaction block for the specified interaction class
        /// element and its sub-elements.
        /// </summary>
        /// <param name="sw">the stream to print to
        /// </param>
        /// <param name="interaction">the interaction class element to process
        /// </param>
        /// <param name="first">whether or not this is the first block being printed
        /// </param>
        /// <returns> the new value for <code>first</code>: <code>true</code> if the first
        /// block has not been printed, <code>false</code> if it has been
        /// </returns>
        /// <exception cref=""> TypeConflictException if a type conflict is detected
        /// </exception>
        private bool PrintReceiveInteractionBlock(System.IO.StreamWriter ps, InteractionClassDescriptor interactionClass, string listener, bool first, int indentLevel)
        {
            string indentStr = GenerateIndentString(indentLevel);

            System.String interaction = interactionClass.Name;

            if (interactionClass.interactionClass.Sharing != HLAsharingType.Neither)
            {
                if (first)
                {
                    ps.WriteLine(indentStr + "if(msg is " + interaction + "Message)");

                    first = false;
                }
                else
                {
                    ps.WriteLine(indentStr + "else if(msg is " + interaction + "Message)");
                }

                ps.WriteLine(indentStr + "{");
                indentStr = GenerateIndentString(indentLevel + 1);
                ps.WriteLine(indentStr + "foreach(IInteractionListener il in manager.InteractionListeners)");
                ps.WriteLine(indentStr + "{");
                ps.WriteLine(GenerateIndentString(indentLevel + 2) + "if (il is " + listener + ")");
                ps.WriteLine(GenerateIndentString(indentLevel + 3) + "(il as " + listener + ").OnReceive" + interaction + "(msg as " + interaction + "Message);");
                ps.WriteLine(GenerateIndentString(indentLevel + 2) + "else");
                ps.WriteLine(GenerateIndentString(indentLevel + 3) + "il.ReceiveInteraction(msg);");
                ps.WriteLine(indentStr + "}"); //Foreach
                indentStr = GenerateIndentString(indentLevel);
                ps.WriteLine(indentStr + "}"); //if
            }
            return first;
        }

        private void PrintSerializersRegistration(System.IO.StreamWriter ps, int indentLevel, string name)
        {
            string indentStr = GenerateIndentString(indentLevel);
            ps.WriteLine();
            ps.WriteLine(indentStr + "objType = typeof(" + name + ");");
            ps.WriteLine(indentStr + "serializerMngr.RegisterSerializer(objType, -10, new " + name + "XrtiSerializer());");
        }

        /// <summary> 
        /// Prints the set of object class handle and Properties serialization helpers for the specified object class
        /// element and its sub-elements.
        /// </summary>
        /// <param name="sw">the stream to print to
        /// </param>
        /// <param name="objectClassElement">the object class element to process
        /// </param>
        /// <exception cref=""> TypeConflictException if a type conflict is detected
        /// </exception>
        private void PrintInteractionClassSerializerRegistration(System.IO.StreamWriter ps, int indentLevel, InteractionClassDescriptor interactionDescriptor)
        {
            string indentStr = GenerateIndentString(indentLevel);

            String interactionMessageName = GetInteractionMessageName(interactionDescriptor.interactionClass.Name);
            String interactionMessageSerializer = interactionMessageName + "XrtiSerializer";

            ps.WriteLine();
            ps.WriteLine(indentStr + "objType = typeof(" + interactionMessageName + ");");
            ps.WriteLine(indentStr + "manager.AddReceiveInteractionDelegate(objType, \"" + interactionDescriptor.interactionClass.Name + "\", new InteractionManager.ReceiveInteractionDelegate(this.ReceiveInteraction));");
            ps.WriteLine(indentStr + "handle = ((XRTIInteractionClassHandle)manager.DescriptorManager.GetInteractionClassDescriptor(\"" + interactionDescriptor.interactionClass.Name + "\").Handle).Identifier;");
            ps.WriteLine(indentStr + "serializerMngr.RegisterSerializer(objType, handle, new " + interactionMessageSerializer + "(serializerMngr));");
        }

        /// <summary> 
        /// Prints the set of object class handle and Properties serialization helpers for the specified object class
        /// element and its sub-elements.
        /// </summary>
        /// <param name="sw">the stream to print to
        /// </param>
        /// <param name="objectClassElement">the object class element to process
        /// </param>
        /// <exception cref=""> TypeConflictException if a type conflict is detected
        /// </exception>
        private void PrintObjectClassSerializerRegistration(System.IO.StreamWriter ps, int indentLevel, ObjectClassDescriptor objDescriptor)
        {
            string indentStr = GenerateIndentString(indentLevel);
            ps.WriteLine();
            bool firstTime = true;
            foreach (AttributeDescriptor attr in objDescriptor.AttributeDescriptors)
            {
                System.String attribute = attr.Name;
                System.String attributeType = attr.attribute.DataType;
                System.String attributeNativeType = NativeTypeForDataType(attributeType);
                string serializerName = objDescriptor.Name + "Property" + attribute;

                if (attributeNativeType != null)
                {
                    if (firstTime)
                    {
                        ps.WriteLine(indentStr + "ocd = manager.DescriptorManager.GetObjectClassDescriptor(\"" + objDescriptor.Name + "\");");
                        firstTime = false;
                    }
                    ps.WriteLine(indentStr + "handle = ((XRTIAttributeHandle)ocd.GetAttributeDescriptor(\"" + attribute + "\").Handle).Identifier;");
                    ps.WriteLine(indentStr + "serializerMngr.RegisterSerializer(null, handle, new " + serializerName + "XrtiSerializer(serializerMngr));");
                    ps.WriteLine();
                }
            }
        }

    }

}
