using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.CodeDom.Compiler;

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
        /// Define a static logger variable so that it references the
        ///	Logger instance.
        /// 
        /// NOTE that using System.Reflection.MethodBase.GetCurrentMethod().DeclaringType
        /// is equivalent to typeof(LoggingExample) but is more portable
        /// i.e. you can copy the code directly into another class without
        /// needing to edit the code.
        /// </summary>
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        /// <summary> 
        /// Manages a set of descriptors that represent features of the federation
        /// (object classes, classSerializerHelperName classes, and so on).       
        /// </summary>
        virtual public DescriptorManager DescriptorManager
        {
            get { return descriptorManager; }
            set
            {
                descriptorManager = value;
                string[] documentNames = descriptorManager.Names;
                federationDocumentName = documentNames[documentNames.Length - 1];
            }
        }

        /// <summary> 
        /// The target directory in which to place generated source files.
        /// </summary>
        virtual public System.IO.FileInfo TargetDirectory
        {
            get { return targetDirectory; }
            set { targetDirectory = value; }
        }

        /// <summary> 
        /// The package prefix.
        /// </summary>
        virtual public System.String PackagePrefix
        {
            get { return packagePrefix; }
            set { packagePrefix = value; }
        }

        /// <summary> 
        /// The Basic data types assembly declaration file.
        /// </summary>
        virtual public System.String AssemblyDeclarationFilename
        {
            get { return assemblyDeclarationFilename; }
            set { assemblyDeclarationFilename = value; }
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public DynamicCompiler(DescriptorManager descriptorMnger)
        {
            DescriptorManager = descriptorMnger;
        }

        /// <summary>
        /// Compile native source code for a set of proxy classes corresponding to
        /// the object and classSerializerHelperName classes described in the federation
        /// description document.  Any existing files will be overwritten.
        /// </summary>
        public virtual void CompileProxies()
        {
            //make sure the Target directory is present.  Defensive programming
            //ot make sure we dont' go forward while our directories aren't setup.
            Directory.CreateDirectory(this.TargetDirectory.FullName);

            string result = GenerateProxiesCodeToMemoryStream();

            //code generator and code provider
            Microsoft.CSharp.CSharpCodeProvider codeProvider = new Microsoft.CSharp.CSharpCodeProvider();
            string[] assemblyNames = new string[] { "Sxta1516.dll", "Rti1516.dll" };
            System.CodeDom.Compiler.CompilerParameters parameters = new CompilerParameters(assemblyNames);
            parameters.GenerateInMemory = true;
            parameters.GenerateExecutable = false;
            parameters.OutputAssembly = "Proxys.dll";
            CompilerResults results = codeProvider.CompileAssemblyFromSource(parameters, result);
            if (results.Errors.Count > 0)
            {
                foreach (CompilerError CompErr in results.Errors)
                {
                    if (log.IsErrorEnabled)
                        log.Error("Line Number " + CompErr.Line + ", Error Code: " + CompErr.ErrorNumber + ", '" + CompErr.ErrorText + ";");
                }
            }
        }

        /// <summary>
        /// Generates native source code for a set of proxy classes corresponding to
        /// the object and classSerializerHelperName classes described in the federation
        /// description document.  Any existing files will be overwritten.
        /// </summary>
        public virtual void GenerateCodeForProxies(System.IO.StreamWriter sw)
        {
            if (targetDirectory == null)
            {
                targetDirectory = new System.IO.FileInfo(".");
            }

            if (packagePrefix == null)
            {
                packagePrefix = "";
            }
            else if (!packagePrefix.EndsWith("."))
            {
                packagePrefix = packagePrefix + ".";
            }

            string[] documentNames = DescriptorManager.Names;
            if (classSerializerHelperName == null)
            {
                classSerializerHelperName = ConvertToNativeClassName(federationDocumentName) + "SerializerHelperAutogenerated";
            }

            if (interactionListenerName == null)
            {
                interactionListenerName = "I" + ConvertToNativeClassName(federationDocumentName) + "InteractionListener";
            }

            GenerateObjectModel(sw, AssemblyDeclarationFilename);
            GenerateAssemblyDataTypes(sw, AssemblyDeclarationFilename);
            GenerateNamespaceDataTypes(sw);
            GenerateObjectClass(sw);
            GenerateInteractions(sw);

            GenerateSerializer(null); //TODO ANGEL
        }

        public void GenerateSerializer(StreamWriter stream)
        {
            try
            {
                int indentLevel = 0;
                string indentStr = GenerateIndentString(indentLevel);

                System.IO.StreamWriter streamWriter;
                if (stream == null)
                {
                    String qualifiedTypeName = packagePrefix + "Serializers";
                    String path = qualifiedTypeName.Replace('.', Path.DirectorySeparatorChar) + ".cs";
                    System.IO.FileInfo sourceFile = new System.IO.FileInfo(targetDirectory.FullName + Path.DirectorySeparatorChar + path);
                    generatedSerializersFile = sourceFile.FullName;
                    System.IO.Directory.CreateDirectory(new System.IO.FileInfo(sourceFile.DirectoryName).FullName);
                    System.IO.FileStream fos = new System.IO.FileStream(sourceFile.FullName, System.IO.FileMode.Create);

                    streamWriter = new System.IO.StreamWriter(fos);

                    System.String packageName = qualifiedTypeName;

                    if (packageName != null)
                    {
                        streamWriter.WriteLine(indentStr + "namespace " + packageName);
                    }
                    else
                    {
                        streamWriter.WriteLine(indentStr + "namespace Sxta.Rti1516.Proxies");
                    }

                    streamWriter.WriteLine(indentStr + "{");
                    indentLevel++;
                    indentStr = GenerateIndentString(indentLevel);

                    streamWriter.WriteLine(indentStr + "using System;");
                    streamWriter.WriteLine(indentStr + "using System.IO;");
                    streamWriter.WriteLine();
                    streamWriter.WriteLine(indentStr + "using Hla.Rti1516;");
                    streamWriter.WriteLine();
                    streamWriter.WriteLine(indentStr + "using Sxta.Rti1516.Serializers.XrtiEncoding;");
                    streamWriter.WriteLine(indentStr + "using Sxta.Rti1516.Reflection;");
                    streamWriter.WriteLine(indentStr + "using Sxta.Rti1516.Interactions;");
                    streamWriter.WriteLine(indentStr + "using Sxta.Rti1516.BoostrapProtocol;");
                    streamWriter.WriteLine(indentStr + "using Sxta.Rti1516.XrtiHandles;");

                    GeneratePackageDependencies(streamWriter, indentLevel);
                }
                else
                {
                    streamWriter = stream;
                }

                streamWriter.WriteLine();
                GenerateNamespaceDataTypesSerializers(streamWriter, indentLevel);
                GenerateObjectClassSerializers(streamWriter, indentLevel);
                GenerateInteractionsSerializers(streamWriter, indentLevel);
                if (stream == null)
                {
                    indentLevel--;
                    indentStr = GenerateIndentString(indentLevel);
                    streamWriter.WriteLine(indentStr + "}");

                    streamWriter.Flush();
                    streamWriter.Close();
                }
            }
            catch (System.IO.IOException ioe)
            {
                System.Console.Error.WriteLine("Error generating fixed record data type: " + ioe);
            }
        }

        public virtual string GenerateProxiesCodeToMemoryStream()
        {
            //TODO ANGEL
            System.IO.FileStream stream = new System.IO.FileStream(TargetDirectory.FullName + Path.DirectorySeparatorChar + "generated.cs",
                                                                   System.IO.FileMode.Create);
            //System.IO.Stream stream = new MemoryStream();


            using (System.IO.StreamWriter streamWriter = PrepareMemoryStream(stream))
            {
                GenerateCodeForProxies(streamWriter);
                streamWriter.Flush();

                if (stream is MemoryStream)
                {
                    stream.Position = 0;
                    TextReader reader = new StreamReader(stream);
                    string result = reader.ReadToEnd();
                    return result;
                }
                else
                {
                    return "";
                }
            }
        }

        private System.IO.StreamWriter PrepareMemoryStream(Stream stream)
        {
            System.IO.StreamWriter sw = new System.IO.StreamWriter(stream);

            string indentStr = GenerateIndentString(0);
            sw.WriteLine(indentStr + "using System;");
            sw.WriteLine(indentStr + "using System.Reflection;");
            sw.WriteLine();
            sw.WriteLine(indentStr + "using Hla.Rti1516;");
            sw.WriteLine(indentStr + "using Sxta.Rti1516.Reflection;");
            sw.WriteLine(indentStr + "using Sxta.Rti1516.Impl;");
            sw.WriteLine(indentStr + "using Sxta.Rti1516.Serializers.XrtiEncoding;");
            sw.WriteLine(indentStr + "using Sxta.Rti1516.XrtiUtils;");

            return sw;
        }

        public void CloseMemoryStream(System.IO.StreamWriter sw)
        {
            if (sw != null)
            {
                sw.Flush();
                sw.Close();
            }
        }

        /// <summary> 
        /// Generates data type defined as Assembly Attributes.
        /// </summary>
        private void GenerateAssemblyDataTypes(System.IO.StreamWriter sw, string assemblyFileName)
        {
            GenerateBasicDataTypes(sw, assemblyFileName);
            GenerateArrayDataTypes(sw, assemblyFileName);
        }


        /// <summary> 
        /// Generates data type classes.
        /// </summary>
        private void GenerateNamespaceDataTypes(System.IO.StreamWriter sw)
        {
            GenerateSimpleDataTypes(sw);
            GenerateEnumeratedDataTypes(sw);
            GenerateFixedRecordDataTypes(sw);
            //GenerateVariantRecordDataTypes(sw);
        }


        /// <summary> 
        /// Generates data type classes.
        /// </summary>
        private void GenerateNamespaceDataTypesSerializers(System.IO.StreamWriter sw, int indentLevel)
        {
            GenerateEnumeratedDataTypesSerializers(sw, indentLevel);
            GenerateFixedRecordDataTypesSerializers(sw, indentLevel);
            //GenerateVariantRecordDataTypes(sw);
        }

        /// <summary> 
        /// Generates all basic data types.
        /// </summary>
        private void GenerateBasicDataTypes(System.IO.StreamWriter stream, string fileName)
        {
            int indentLevel = 0;
            string indentStr = GenerateIndentString(indentLevel);

            try
            {
                System.IO.StreamWriter sw;
                if (stream == null)
                {
                    String qualifiedTypeName = packagePrefix + fileName;
                    String path = qualifiedTypeName.Replace('.', Path.DirectorySeparatorChar) + ".cs";
                    System.IO.FileInfo sourceFile = new System.IO.FileInfo(targetDirectory.FullName + Path.DirectorySeparatorChar + path);
                    System.IO.Directory.CreateDirectory(new System.IO.FileInfo(sourceFile.DirectoryName).FullName);

                    System.IO.FileStream fos = new System.IO.FileStream(sourceFile.FullName, System.IO.FileMode.Create);
                    sw = new System.IO.StreamWriter(fos);

                    sw.WriteLine(indentStr + "using System;");
                    sw.WriteLine(indentStr + "using System.Reflection;");
                    sw.WriteLine(indentStr + "using Sxta.Rti1516.Reflection;");
                }
                else
                {
                    sw = stream;
                }

                string indent = "," + Environment.NewLine + indentStr + "                                 ";
                foreach (HLABasicData basicData in descriptorManager.BasicDataTypeList)
                {
                    sw.WriteLine();
                    sw.Write(indentStr + "[assembly: HLABasicDataAttribute(Name = \"" + basicData.Name + "\"");
                    sw.Write(indent + "NativeType = typeof(" + basicData.NativeType.Name + ")");
                    sw.Write(indent + "Endian = " + basicData.Endian.GetType() + "." + basicData.Endian);
                    sw.Write(indent + "Size = " + basicData.Size);
                    if (!string.IsNullOrEmpty(basicData.Interpretation))
                        sw.Write(indent + "Interpretation = \"" + basicData.Interpretation + "\"");
                    if (!string.IsNullOrEmpty(basicData.Encoding))
                        sw.Write(indent + "Encoding = \"" + basicData.Encoding + "\"");
                    sw.WriteLine(")]");
                }

                if (stream == null)
                {
                    sw.Flush();
                    sw.Close();
                }
            }
            catch (System.IO.IOException ioe)
            {
                System.Console.Error.WriteLine("Error generating basic data type: " + ioe);
            }
        }


        /// <summary> 
        /// Generates all array data types.
        /// </summary>
        private void GenerateArrayDataTypes(System.IO.StreamWriter stream, string fileName)
        {
            int indentLevel = 0;
            string indentStr = GenerateIndentString(indentLevel);

            try
            {
                System.IO.StreamWriter sw;
                if (stream == null)
                {
                    String qualifiedTypeName = packagePrefix + fileName;
                    String path = qualifiedTypeName.Replace('.', Path.DirectorySeparatorChar) + ".cs";
                    System.IO.FileInfo sourceFile = new System.IO.FileInfo(targetDirectory.FullName + Path.DirectorySeparatorChar + path);
                    System.IO.Directory.CreateDirectory(new System.IO.FileInfo(sourceFile.DirectoryName).FullName);

                    System.IO.FileStream fos = new System.IO.FileStream(sourceFile.FullName, System.IO.FileMode.Create);
                    sw = new System.IO.StreamWriter(fos);

                    sw.WriteLine(indentStr + "using System;");
                    sw.WriteLine(indentStr + "using System.Reflection;");
                    sw.WriteLine(indentStr + "using Sxta.Rti1516.Reflection;");
                }
                else
                {
                    sw = stream;
                }

                string indent = "," + Environment.NewLine + indentStr + "                                 ";
                // Generates something similar to:
                //[assembly: HLAArrayDataAttribute(Name = "HLAunicodeString",
                //                                 DataType = "HLAunicodeChar",
                //                                 Cardinality = "Dynamic",
                //                                 Encoding = "HLAvariableArray",
                //                                 HasNativeSerializer = true,
                //                                 Semantics = "Unicode string representation.")]
                foreach (HLAarrayDataType arrayData in descriptorManager.ArrayDataTypeList)
                {
                    sw.WriteLine();
                    sw.Write(indentStr + "[assembly: HLAArrayDataAttribute(Name = \"" + arrayData.Name + "\"");
                    sw.Write(indent + "DataType = \"" + arrayData.DataType + "\"");
                    sw.Write(indent + "HasNativeSerializer = " + arrayData.HasNativeSerializer.ToString().ToLower());
                    if (!string.IsNullOrEmpty(arrayData.DataTypeNotes))
                        sw.Write(indent + "DataTypeNotes = \"" + arrayData.DataTypeNotes + "\"");
                    sw.Write(indent + "Cardinality = \"" + arrayData.Cardinality + "\"");
                    if (!string.IsNullOrEmpty(arrayData.CardinalityNotes))
                        sw.Write(indent + "CardinalityNotes = \"" + arrayData.CardinalityNotes + "\"");
                    if (!string.IsNullOrEmpty(arrayData.Encoding))
                        sw.Write(indent + "Encoding = \"" + arrayData.Encoding + "\"");
                    if (!string.IsNullOrEmpty(arrayData.EncodingNotes))
                        sw.Write(indent + "EncodingNotes = \"" + arrayData.EncodingNotes + "\"");
                    if (!string.IsNullOrEmpty(arrayData.Semantics))
                        sw.Write(indent + "Semantics = \"" + arrayData.Semantics + "\"");
                    if (!string.IsNullOrEmpty(arrayData.SemanticsNotes))
                        sw.Write(indent + "SemanticsNotes = \"" + arrayData.SemanticsNotes + "\"");
                    sw.WriteLine(")]");
                }

                if (stream == null)
                {
                    sw.Flush();
                    sw.Close();
                }
            }
            catch (System.IO.IOException ioe)
            {
                System.Console.Error.WriteLine("Error generating array data type: " + ioe);
            }
        }


        /// <summary> 
        /// Generates all Simple data types.
        /// </summary>
        private void GenerateSimpleDataTypes(System.IO.StreamWriter stream)
        {
            foreach (HLASimpleData simpleData in descriptorManager.SimpleDataTypeList)
            {
                GenerateSimpleDataType(stream, simpleData);
            }
        }

        /// <summary> 
        /// Generates all Simple data types.
        /// </summary>
        private void GenerateSimpleDataType(System.IO.StreamWriter stream, HLASimpleData simpleData)
        {
            int indentLevel = 0;
            string indentStr = GenerateIndentString(indentLevel);


            try
            {
                System.IO.StreamWriter sw;
                String typeName = simpleData.Name;
                if (stream == null)
                {
                    String qualifiedTypeName = packagePrefix + typeName;
                    String path = qualifiedTypeName.Replace('.', Path.DirectorySeparatorChar) + ".cs";
                    System.IO.FileInfo sourceFile = new System.IO.FileInfo(targetDirectory.FullName + Path.DirectorySeparatorChar + path);
                    System.IO.Directory.CreateDirectory(new System.IO.FileInfo(sourceFile.DirectoryName).FullName);

                    System.IO.FileStream fos = new System.IO.FileStream(sourceFile.FullName, System.IO.FileMode.Create);
                    sw = new System.IO.StreamWriter(fos);
                    System.String packageName = GetPackageName(qualifiedTypeName);

                    if (packageName != null)
                    {
                        sw.WriteLine(indentStr + "namespace " + packageName);
                    }
                    else
                    {
                        sw.WriteLine(indentStr + "namespace Sxta.Rti1516.Proxies");
                    }

                    sw.WriteLine(indentStr + "{");
                    indentLevel++;
                    indentStr = GenerateIndentString(indentLevel);

                    sw.WriteLine(indentStr + "using System;");
                    sw.WriteLine(indentStr + "using System.IO;");
                    sw.WriteLine();
                    sw.WriteLine(indentStr + "using HlaEncodingReader = Sxta.Rti1516.Serializers.XrtiEncoding.HlaEncodingReader;");
                    sw.WriteLine(indentStr + "using HlaEncodingWriter = Sxta.Rti1516.Serializers.XrtiEncoding.HlaEncodingWriter;");
                    sw.WriteLine(indentStr + "using Sxta.Rti1516.Reflection;");
                }
                else
                {
                    sw = stream;
                }
                sw.WriteLine();
                if (!string.IsNullOrEmpty(simpleData.Semantics))
                {
                    PrintClassComment(sw, simpleData.Semantics, indentLevel);
                }
                else
                {
                    PrintClassComment(sw, "Autogenerated simple data type.", indentLevel);
                }
                GenerateHLASimpleDataAttribute(indentLevel, sw, simpleData);
                sw.WriteLine(indentStr + "public struct " + typeName);

                sw.WriteLine(indentStr + "{");
                indentLevel++;
                indentStr = GenerateIndentString(indentLevel);

                String representation = simpleData.Representation;
                if (string.IsNullOrEmpty(representation))
                {
                    throw new RTIexception("SimpleData :" + simpleData.Name + "doesn't have a representation");
                }
                String nativeType = NativeTypeForBasicRepresentation(representation);
                string indentStr1 = GenerateIndentString(indentLevel + 1);
                sw.WriteLine(indentStr + "public " + nativeType + " data;");
                sw.WriteLine();
                sw.WriteLine(indentStr + "public static implicit operator " + nativeType + "(" + simpleData.Name + " val)");
                sw.WriteLine(indentStr + "{");
                sw.WriteLine(indentStr1 + "return val.data;");
                sw.WriteLine(indentStr + "}");
                sw.WriteLine();
                sw.WriteLine(indentStr + "public static implicit operator " + simpleData.Name + "(" + nativeType + " val)");
                sw.WriteLine(indentStr + "{");
                sw.WriteLine(indentStr1 + simpleData.Name + " tmp = new " + simpleData.Name + "();");
                sw.WriteLine(indentStr1 + "tmp.data = val;");
                sw.WriteLine(indentStr1 + "return tmp;");
                sw.WriteLine(indentStr + "}");

                sw.WriteLine();
                indentLevel--;
                indentStr = GenerateIndentString(indentLevel);
                sw.WriteLine(indentStr + "}");
                sw.WriteLine();

                sw.WriteLine(indentStr + "[Serializable]");
                sw.WriteLine(indentStr + "public sealed class " + typeName + "XrtiSerializer");
                sw.WriteLine(indentStr + "{");
                indentLevel++;
                indentStr = GenerateIndentString(indentLevel);

                sw.WriteLine(indentStr + "///<summary> Reads and returns a " + typeName + " from the specified stream.</summary>");
                sw.WriteLine(indentStr + "///<param name=\"reader\"> the input stream to read from</param>");
                sw.WriteLine(indentStr + "///<returns>return the decoded value</returns>");
                sw.WriteLine(indentStr + "///<exception cref=\"IOException\"> if an error occurs</exception>");
                sw.WriteLine(indentStr + "public static " + typeName + " Deserialize(HlaEncodingReader reader)");
                sw.WriteLine(indentStr + "{");
                sw.WriteLine(GenerateIndentString(indentLevel + 1) + "return (" + typeName + ")reader.Read" + representation + "();");
                sw.WriteLine(indentStr + "}");
                sw.WriteLine();

                sw.WriteLine(indentStr + "///<summary>Writes this " + typeName + " to the specified stream.</summary>");
                sw.WriteLine(indentStr + "///<param name=\"writer\"> the stream to write to</param>");
                sw.WriteLine(indentStr + "///<exception cref=\"IOException\"> if an error occurs</exception>");
                sw.WriteLine(indentStr + "public static void Serialize(HlaEncodingWriter writer, " + typeName + " val)");
                sw.WriteLine(indentStr + "{");
                sw.WriteLine(GenerateIndentString(indentLevel + 1) + "writer.Write" + representation + "((" + nativeType + ")val);");
                sw.WriteLine(indentStr + "}");
                sw.WriteLine();

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
                System.Console.Error.WriteLine("Error generating simple data type: " + ioe);
            }
        }


        /// <summary> 
        /// Generates all enumerated data types.
        /// </summary>
        private void GenerateEnumeratedDataTypes(System.IO.StreamWriter sw)
        {
            foreach (HLAEnumeratedData enumeratedData in descriptorManager.EnumeratedDataTypeList)
            {
                GenerateEnumeratedDataType(sw, enumeratedData);
            }
        }


        /// <summary> 
        /// Generates all enumerated data types.
        /// </summary>
        private void GenerateEnumeratedDataTypesSerializers(System.IO.StreamWriter sw, int indentLevel)
        {
            foreach (HLAEnumeratedData enumeratedData in descriptorManager.EnumeratedDataTypeList)
            {
                GenerateEnumeratedDataTypeSerializer(sw, enumeratedData, indentLevel);
            }
        }

        /// <summary> 
        /// Generates an enumerated data type class.
        /// </summary>
        private void GenerateEnumeratedDataType(System.IO.StreamWriter stream, HLAEnumeratedData enumeratedData)
        {
            int indentLevel = 0;
            string indentStr = GenerateIndentString(indentLevel);

            String typeName = enumeratedData.Name;
            try
            {
                System.IO.StreamWriter sw;
                if (stream == null)
                {

                    String qualifiedTypeName = packagePrefix + typeName;
                    String path = qualifiedTypeName.Replace('.', Path.DirectorySeparatorChar) + ".cs";
                    System.IO.FileInfo sourceFile = new System.IO.FileInfo(targetDirectory.FullName + Path.DirectorySeparatorChar + path);
                    System.IO.Directory.CreateDirectory(new System.IO.FileInfo(sourceFile.DirectoryName).FullName);

                    System.IO.FileStream fos = new System.IO.FileStream(sourceFile.FullName, System.IO.FileMode.Create);
                    sw = new System.IO.StreamWriter(fos);

                    System.String packageName = GetPackageName(qualifiedTypeName);

                    if (packageName != null)
                    {
                        sw.WriteLine(indentStr + "namespace " + packageName);
                    }
                    else
                    {
                        sw.WriteLine(indentStr + "namespace Sxta.Rti1516.Proxies");
                    }

                    sw.WriteLine(indentStr + "{");
                    indentLevel++;
                    indentStr = GenerateIndentString(indentLevel);

                    sw.WriteLine(indentStr + "using System;");
                    sw.WriteLine(indentStr + "using System.IO;");
                    sw.WriteLine();
                    sw.WriteLine(indentStr + "using HlaEncodingReader = Sxta.Rti1516.Serializers.XrtiEncoding.HlaEncodingReader;");
                    sw.WriteLine(indentStr + "using HlaEncodingWriter = Sxta.Rti1516.Serializers.XrtiEncoding.HlaEncodingWriter;");
                    sw.WriteLine(indentStr + "using Sxta.Rti1516.Reflection;");
                }
                else
                {
                    sw = stream;
                }

                sw.WriteLine();
                if (!string.IsNullOrEmpty(enumeratedData.Semantics))
                {
                    PrintClassComment(sw, enumeratedData.Semantics, indentLevel);
                }
                else
                {
                    PrintClassComment(sw, "Autogenerated enumerated data type.", indentLevel);
                }
                GenerateHLAEnumeratedDataAttribute(indentLevel, sw, enumeratedData);
                sw.WriteLine(indentStr + "public enum  " + typeName);

                sw.WriteLine(indentStr + "{");
                indentLevel++;
                indentStr = GenerateIndentString(indentLevel);

                int len = enumeratedData.Enumerators.Count;
                int i = 0;
                foreach (HLAEnumerator enumerator in enumeratedData.Enumerators)
                {
                    sw.Write(indentStr + ConvertToIdentifier(enumerator.Name));
                    if (!string.IsNullOrEmpty(enumerator.Values))
                        sw.Write(" = " + enumerator.Values);
                    if (i < len - 1)
                    {
                        //Last case without ","
                        sw.WriteLine(",");
                    }
                    i++;
                }
                sw.WriteLine();
                indentLevel--;
                indentStr = GenerateIndentString(indentLevel);
                sw.WriteLine(indentStr + "}");
                sw.WriteLine();
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
                System.Console.Error.WriteLine("Error generating enumerated data type: " + ioe);
            }
        }

        /// <summary> 
        /// Generates an enumerated data type class.
        /// </summary>
        private void GenerateEnumeratedDataTypeSerializer(System.IO.StreamWriter stream, HLAEnumeratedData enumeratedData, int indentLevel)
        {
            string indentStr = GenerateIndentString(indentLevel);

            String typeName = enumeratedData.Name;
            try
            {
                System.IO.StreamWriter sw;
                if (stream == null)
                {

                    String qualifiedTypeName = packagePrefix + typeName + "XrtiSerializer";
                    String path = qualifiedTypeName.Replace('.', Path.DirectorySeparatorChar) + ".cs";
                    System.IO.FileInfo sourceFile = new System.IO.FileInfo(targetDirectory.FullName + Path.DirectorySeparatorChar + path);
                    System.IO.Directory.CreateDirectory(new System.IO.FileInfo(sourceFile.DirectoryName).FullName);

                    System.IO.FileStream fos = new System.IO.FileStream(sourceFile.FullName, System.IO.FileMode.Create);
                    sw = new System.IO.StreamWriter(fos);

                    System.String packageName = GetPackageName(qualifiedTypeName);

                    if (packageName != null)
                    {
                        sw.WriteLine(indentStr + "namespace " + packageName);
                    }
                    else
                    {
                        sw.WriteLine(indentStr + "namespace Sxta.Rti1516.Proxies");
                    }

                    sw.WriteLine(indentStr + "{");
                    indentLevel++;
                    indentStr = GenerateIndentString(indentLevel);

                    sw.WriteLine(indentStr + "using System;");
                    sw.WriteLine(indentStr + "using System.IO;");
                    sw.WriteLine();
                    sw.WriteLine(indentStr + "using HlaEncodingReader = Sxta.Rti1516.Serializers.XrtiEncoding.HlaEncodingReader;");
                    sw.WriteLine(indentStr + "using HlaEncodingWriter = Sxta.Rti1516.Serializers.XrtiEncoding.HlaEncodingWriter;");
                    sw.WriteLine(indentStr + "using Sxta.Rti1516.Reflection;");
                }
                else
                {
                    sw = stream;
                }

                sw.WriteLine();
                PrintClassComment(sw, "Autogenerated Serializer for enumerated data type.", indentLevel);
                sw.WriteLine(indentStr + "public sealed class " + typeName + "XrtiSerializer");
                sw.WriteLine(indentStr + "{");
                indentLevel++;
                indentStr = GenerateIndentString(indentLevel);

                String representation = enumeratedData.Representation;
                if (string.IsNullOrEmpty(representation))
                {
                    //TODO
                    representation = "HLAInteger32BE";
                }
                String nativeType = NativeTypeForBasicRepresentation(representation);
                sw.WriteLine(indentStr + "///<summary> Reads and returns a " + typeName + " from the specified stream.</summary>");
                sw.WriteLine(indentStr + "///<param name=\"reader\"> the input stream to read from</param>");
                sw.WriteLine(indentStr + "///<returns>return the decoded value</returns>");
                sw.WriteLine(indentStr + "///<exception cref=\"IOException\"> if an error occurs</exception>");
                sw.WriteLine(indentStr + "public static " + typeName + " Deserialize(HlaEncodingReader reader)");
                sw.WriteLine(indentStr + "{");
                sw.WriteLine(GenerateIndentString(indentLevel + 1) + "return (" + typeName + ")reader.Read" + representation + "();");
                sw.WriteLine(indentStr + "}");
                sw.WriteLine();

                sw.WriteLine(indentStr + "///<summary>Writes this " + typeName + " to the specified stream.</summary>");
                sw.WriteLine(indentStr + "///<param name=\"writer\"> the stream to write to</param>");
                sw.WriteLine(indentStr + "///<exception cref=\"IOException\"> if an error occurs</exception>");
                sw.WriteLine(indentStr + "public static void Serialize(HlaEncodingWriter writer, " + typeName + " val)");
                sw.WriteLine(indentStr + "{");
                sw.WriteLine(GenerateIndentString(indentLevel + 1) + "writer.Write" + representation + "((" + nativeType + ")val);");
                sw.WriteLine(indentStr + "}");
                sw.WriteLine();

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
                System.Console.Error.WriteLine("Error generating enumerated data type: " + ioe);
            }
        }

        /// <summary> 
        /// Generates a <code>GenerateHLAEnumeratedDataAttribute</code>.
        /// </summary>
        private void GenerateHLAEnumeratedDataAttribute(int localIndentLevel, System.IO.StreamWriter ps, HLAEnumeratedData enumData)
        {
            string indentStr = GenerateIndentString(localIndentLevel);
            string newLine = "," + Environment.NewLine + indentStr + "                ";
            ps.Write(indentStr + "[HLAEnumeratedData(Name = \"" + enumData.Name + "\"");
            if (!String.IsNullOrEmpty(enumData.Representation))
            {
                ps.Write(newLine);
                ps.Write("Representation = \"" + enumData.Representation + "\"");
            }
            if (!String.IsNullOrEmpty(enumData.RepresentationNotes))
            {
                ps.Write(newLine);
                ps.Write("RepresentationNotes = \"" + enumData.RepresentationNotes + "\"");
            }
            if (!String.IsNullOrEmpty(enumData.Semantics))
            {
                ps.Write(newLine);
                ps.Write("Semantics = \"" + enumData.Semantics + "\"");
            }
            if (!String.IsNullOrEmpty(enumData.SemanticsNotes))
            {
                ps.Write(newLine);
                ps.Write("SemanticsNotes = \"" + enumData.SemanticsNotes + "\"");
            }
            if (!String.IsNullOrEmpty(enumData.NameNotes))
            {
                ps.Write(newLine);
                ps.Write("NameNotes = \"" + enumData.NameNotes + "\"");
            }
            ps.WriteLine(")]");
        }

        /// <summary> 
        /// Generates a <code>GenerateHLASimpleDataAttribute</code>.
        /// </summary>
        private void GenerateHLASimpleDataAttribute(int localIndentLevel, System.IO.StreamWriter ps, HLASimpleData simpleData)
        {
            string indentStr = GenerateIndentString(localIndentLevel);
            string newLine = "," + Environment.NewLine + indentStr + "                ";
            ps.Write(indentStr + "[HLASimpleData(Name = \"" + simpleData.Name + "\"");
            if (!String.IsNullOrEmpty(simpleData.NameNotes))
            {
                ps.Write(newLine);
                ps.Write("NameNotes = \"" + simpleData.NameNotes + "\"");
            }
            if (!String.IsNullOrEmpty(simpleData.Representation))
            {
                ps.Write(newLine);
                ps.Write("Representation = \"" + simpleData.Representation + "\"");
            }
            if (!String.IsNullOrEmpty(simpleData.RepresentationNotes))
            {
                ps.Write(newLine);
                ps.Write("RepresentationNotes = \"" + simpleData.RepresentationNotes + "\"");
            }
            if (!String.IsNullOrEmpty(simpleData.Semantics))
            {
                ps.Write(newLine);
                ps.Write("Semantics = \"" + simpleData.Semantics + "\"");
            }
            if (!String.IsNullOrEmpty(simpleData.SemanticsNotes))
            {
                ps.Write(newLine);
                ps.Write("SemanticsNotes = \"" + simpleData.SemanticsNotes + "\"");
            }
            if (!String.IsNullOrEmpty(simpleData.Accuracy))
            {
                ps.Write(newLine);
                ps.Write("Accuracy = \"" + simpleData.Accuracy + "\"");
            }
            if (!String.IsNullOrEmpty(simpleData.AccuracyNotes))
            {
                ps.Write(newLine);
                ps.Write("AccuracyNotes = \"" + simpleData.AccuracyNotes + "\"");
            }
            if (!String.IsNullOrEmpty(simpleData.Resolution))
            {
                ps.Write(newLine);
                ps.Write("Resolution = \"" + simpleData.Resolution + "\"");
            }
            if (!String.IsNullOrEmpty(simpleData.ResolutionNotes))
            {
                ps.Write(newLine);
                ps.Write("ResolutionNotes = \"" + simpleData.ResolutionNotes + "\"");
            }
            if (!String.IsNullOrEmpty(simpleData.Units))
            {
                ps.Write(newLine);
                ps.Write("Units = \"" + simpleData.Units + "\"");
            }
            if (!String.IsNullOrEmpty(simpleData.UnitsNotes))
            {
                ps.Write(newLine);
                ps.Write("UnitsNotes = \"" + simpleData.UnitsNotes + "\"");
            }
            ps.WriteLine(")]");
        }

    }
}
