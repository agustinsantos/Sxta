using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Reflection;

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
        /// Prints a code block with the using clauses 
        /// </summary>
        /// <param name="stream">the print stream to write the code block to
        /// </param>
        private void GeneratePackageDependencies(System.IO.StreamWriter stream, int indentLevel)
        {
            string indentStr = GenerateIndentString(indentLevel);
            foreach (string package in PackageDependencies)
            {
                stream.WriteLine(indentStr + "using " + package + " ;");
            }
        }

        /// <summary> 
        /// Prints a code block that will serialize the specified variable to the
        /// given stream.
        /// </summary>
        /// <param name="ps">the print stream to write the code block to
        /// </param>
        /// <param name="indentLevel">the level of indentation to use
        /// </param>
        /// <param name="iteratorVariable">the name of the iterator variable to use ('i', 'j', 'k'...)
        /// </param>
        /// <param name="dataTypeName">the name of the variable's data type
        /// </param>
        /// <param name="variableName">the name of the variable
        /// </param>
        /// <param name="streamName">the name of the stream
        /// </param>
        private void PrintSerializationBlock(System.IO.StreamWriter ps, int indentLevel, char iteratorVariable, System.String dataTypeName, System.String variableName, System.String streamName)
        {
            if (dataTypeName == null || dataTypeName.Equals("HLAopaqueData") || opaqueTypes.Contains(dataTypeName))
            {
                ps.WriteLine(GenerateIndentString(indentLevel) + streamName + ".WriteHLAopaqueData(" + variableName + ");");
            }
            else if (dataTypeName.Equals("NA"))
            {
                return;
            }
            else if (dataTypeName.Equals("HLAASCIIchar") || dataTypeName.Equals("HLAunicodeChar") ||
                dataTypeName.Equals("HLAboolean") || dataTypeName.Equals("HLAunicodestring") ||
                dataTypeName.Equals("HLAASCIIstring"))
            {
                ps.WriteLine(GenerateIndentString(indentLevel) + streamName + ".Write" + dataTypeName + "(" + variableName + ");");
            }
            else if (descriptorManager.BasicDataTypeMap.ContainsKey(dataTypeName))
            {
                //TODO Check if a Basic Data has a specific serializer
                ps.WriteLine(GenerateIndentString(indentLevel) + streamName + ".Write" + dataTypeName + "(" + variableName + ");");
            }
            else if (descriptorManager.SimpleDataTypeMap.ContainsKey(dataTypeName))
            {
                System.String representation = descriptorManager.SimpleDataTypeMap[dataTypeName].Representation;

                if (descriptorManager.BasicDataTypeMap.ContainsKey(representation))
                {
                    ps.WriteLine(GenerateIndentString(indentLevel) + streamName + ".Write" + representation + "(" + variableName + ");");
                }
                else
                {
                    ps.WriteLine(GenerateIndentString(indentLevel) + streamName + ".WriteHLAopaqueData(" + variableName + ");");
                }
            }
            else if (descriptorManager.ArrayDataTypeMap.ContainsKey(dataTypeName))
            {

                HLAarrayDataType arrData = descriptorManager.ArrayDataTypeMap[dataTypeName];

                if (arrData.HasNativeSerializer)
                {
                    ps.WriteLine(GenerateIndentString(indentLevel) + streamName + ".Write" + dataTypeName + "(" + variableName + ");");
                }
                else
                {
                    if (arrData.Cardinality.ToUpper().Equals("DYNAMIC"))
                    {
                        ps.WriteLine(GenerateIndentString(indentLevel) + streamName + ".WriteHLAinteger32BE((" + variableName + ").Length);");
                        ps.WriteLine();
                        ps.WriteLine(GenerateIndentString(indentLevel) + "for(int " + iteratorVariable + "=0;" + iteratorVariable + "< (" + variableName + ").Length;" + iteratorVariable + "++)");
                    }
                    else
                    {
                        ps.WriteLine(GenerateIndentString(indentLevel) + "for(int " + iteratorVariable + "=0;" + iteratorVariable + "<" + arrData.Cardinality + ";" + iteratorVariable + "++)");
                    }

                    ps.WriteLine(GenerateIndentString(indentLevel) + "{");
                    PrintSerializationBlock(ps, indentLevel + 1, (char)(iteratorVariable + 1), arrData.DataType, "(" + variableName + ")[" + iteratorVariable + "]", streamName);
                    ps.WriteLine(GenerateIndentString(indentLevel) + "}");
                }
            }
            else
            {
                if (variableName.EndsWith("]"))
                {
                    ps.WriteLine(GenerateIndentString(indentLevel) + dataTypeName + "XrtiSerializer.Serialize(" + streamName + ", " + variableName + ");");
                }
                else
                {
                    ps.WriteLine(GenerateIndentString(indentLevel) + dataTypeName + "XrtiSerializer.Serialize(" + streamName + ", " + variableName + ");");
                }
            }
        }


        /// <summary> 
        /// Prints a code block that will deserialize the specified variable from the
        /// given stream.
        /// </summary>
        /// <param name="sw">the print stream to write the code block to
        /// </param>
        /// <param name="indentLevel">the level of indentation to use
        /// </param>
        /// <param name="iteratorVariable">the name of the iterator variable to use ('i', 'j', 'k'...)
        /// </param>
        /// <param name="dataTypeName">the name of the variable's data type
        /// </param>
        /// <param name="variableName">the name of the variable
        /// </param>
        /// <param name="streamName">the name of the stream
        /// </param>
        private void PrintDeserializationBlock(System.IO.StreamWriter ps, int indentLevel, char iteratorVariable, System.String dataTypeName, System.String variableName, System.String streamName)
        {
            if (dataTypeName == null || dataTypeName.Equals("HLAopaqueData") || opaqueTypes.Contains(dataTypeName))
            {
                ps.WriteLine(GenerateIndentString(indentLevel) + variableName + " = " + streamName + ".ReadHLAopaqueData();");
            }
            else if (dataTypeName.Equals("NA"))
            {
                return;
            }
            //TODO all these types are simpleDatatype. Must we consider it as default or try to look for in descriptorManager.SimpleDataTypeMap ???
            else if (dataTypeName.Equals("HLAASCIIchar") || dataTypeName.Equals("HLAunicodeChar") ||
                     dataTypeName.Equals("HLAboolean") || dataTypeName.Equals("HLAunicodestring") ||
                     dataTypeName.Equals("HLAASCIIstring"))
            {
                ps.WriteLine(GenerateIndentString(indentLevel) + variableName + " = " + streamName + ".Read" + dataTypeName + "();");
            }
            else if (descriptorManager.BasicDataTypeMap.ContainsKey(dataTypeName))
            {
                //TODO Check if a Basic Data has a specific deserializer
                ps.WriteLine(GenerateIndentString(indentLevel) + variableName + " = " + streamName + ".Read" + dataTypeName + "();");
            }
            else if (descriptorManager.SimpleDataTypeMap.ContainsKey(dataTypeName))
            {
                System.String representation = descriptorManager.SimpleDataTypeMap[dataTypeName].Representation;

                if (descriptorManager.BasicDataTypeMap.ContainsKey(representation))
                {
                    ps.WriteLine(GenerateIndentString(indentLevel) + variableName + " = " + streamName + ".Read" + representation + "();");
                }
                else
                {
                    ps.WriteLine(GenerateIndentString(indentLevel) + variableName + " = " + streamName + ".ReadHLAopaqueData();");
                }
            }
            else if (descriptorManager.ArrayDataTypeMap.ContainsKey(dataTypeName))
            {
                HLAarrayDataType arrData = descriptorManager.ArrayDataTypeMap[dataTypeName];

                if (arrData.HasNativeSerializer)
                {
                    ps.WriteLine(GenerateIndentString(indentLevel) + variableName + " = " + streamName + ".Read" + dataTypeName + "();");
                }
                else
                {

                    System.String nativeType = NativeTypeForDataType(dataTypeName);
                    System.String preBracket = nativeType.Substring(0, (nativeType.IndexOf((System.Char)']')) - (0));
                    System.String postBracket = nativeType.Substring(nativeType.IndexOf((System.Char)']'));

                    if (arrData.Cardinality.ToUpper().Equals("DYNAMIC"))
                    {
                        ps.WriteLine(GenerateIndentString(indentLevel) + variableName + " = new " + preBracket + " " + streamName + ".ReadHLAinteger32BE() " + postBracket + ";");
                        ps.WriteLine();
                        ps.WriteLine(GenerateIndentString(indentLevel) + "for(int " + iteratorVariable + "=0;" + iteratorVariable + "<" + variableName + ".Length;" + iteratorVariable + "++)");
                    }
                    else
                    {
                        ps.WriteLine(GenerateIndentString(indentLevel) + variableName + " = new " + preBracket + arrData.Cardinality + postBracket + ";");
                        ps.WriteLine();
                        ps.WriteLine(GenerateIndentString(indentLevel) + "for(int " + iteratorVariable + "=0;" + iteratorVariable + "<" + arrData.Cardinality + ";" + iteratorVariable + "++)");
                    }

                    ps.WriteLine(GenerateIndentString(indentLevel) + "{");
                    PrintDeserializationBlock(ps, indentLevel + 1, (char)(iteratorVariable + 1), arrData.DataType, variableName + "[" + iteratorVariable + "]", streamName);
                    ps.WriteLine(GenerateIndentString(indentLevel) + "}");
                }
            }
            else
            {
                if (dataTypeName.StartsWith("HLA") && !dataTypeName.EndsWith("[]") && !dataTypeName.EndsWith("]"))
                {
                    ps.WriteLine(GenerateIndentString(indentLevel) + variableName + " = " + dataTypeName + "XrtiSerializer.Deserialize(" + streamName + ");");
                }
                else
                {
                    if (dataTypeName.StartsWith("HLA") && !dataTypeName.EndsWith("[]") && !variableName.EndsWith("]"))
                        ps.WriteLine(GenerateIndentString(indentLevel) + variableName + " = " + dataTypeName + "XrtiSerializer.Deserialize(" + streamName + ");");
                    else
                        ps.WriteLine(GenerateIndentString(indentLevel) + variableName + " = " + dataTypeName + "XrtiSerializer.Deserialize(" + streamName + ");");
                }

            }
        }

        /// <summary> 
        /// Converts the specified free-form name to a reasonable Native identifier by
        /// removing whitespace and illegal characters and so on.
        /// </summary>
        /// <param name="anyName">the free-form name to convert
        /// </param>
        /// <returns> the converted name
        /// </returns>
        private System.String ConvertToIdentifier(System.String anyName)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            if (anyName.Length == 0 || !(System.Char.IsLetter(anyName[0]) || anyName[0].CompareTo('$') == 0 || anyName[0].CompareTo('_') == 0))
            {
                sb.Append('_');
            }

            for (int i = 0; i < anyName.Length; i++)
            {
                char c = anyName[i];

                if (Char.IsLetterOrDigit(c))
                {
                    sb.Append(c);
                }
                else
                {
                    sb.Append('_');
                }
            }

            return sb.ToString();
        }

        /// <summary> 
        /// Returns the native type name corresponding to the specified data type name.
        /// </summary>
        /// <param name="dataTypeName">
        /// the data type name
        /// </param>
        /// <returns> 
        /// the corresponding native type name
        /// </returns>
        private String NativeTypeForDataType(System.String dataTypeName)
        {
            if (dataTypeName == null || dataTypeName.Equals("HLAopaqueData") || opaqueTypes.Contains(dataTypeName))
            {
                return "byte[]";
            }
            else if (dataTypeName.Equals("NA"))
            {
                return null;
            }
            else if (dataTypeName.Equals("HLAASCIIchar") || dataTypeName.Equals("HLAunicodeChar"))
            {
                return "char";
            }
            else if (dataTypeName.Equals("HLAboolean"))
            {
                return "bool";
            }
            else if (dataTypeName.Equals("HLAASCIIstring") || dataTypeName.Equals("HLAunicodeString"))
            {
                return "String";
            }
            else if (descriptorManager.SimpleDataTypeMap.ContainsKey(dataTypeName))
            {
                return NativeTypeForBasicRepresentation(descriptorManager.SimpleDataTypeMap[dataTypeName].Representation);
            }
            else if (descriptorManager.ArrayDataTypeMap.ContainsKey(dataTypeName))
            {
                HLAarrayDataType arrData = descriptorManager.ArrayDataTypeMap[dataTypeName];

                return NativeTypeForDataType(arrData.DataType) + "[]";
            }
            else if (descriptorManager.BasicDataTypeMap.ContainsKey(dataTypeName))
            {
                return NativeTypeForBasicRepresentation(dataTypeName);
            }

            else
            {
                return dataTypeName;
            }
        }

        /// <summary> 
        /// Returns the Native type name corresponding to the specified basic representation
        /// name.
        /// </summary>
        /// <param name="basicRepresentationName">the basic representation name
        /// </param>
        /// <returns> the corresponding Native type name
        /// </returns>
        private String NativeTypeForBasicRepresentation(String basicRepresentationName)
        {
            if (basicRepresentationName == null)
            {
                return "byte[]";
            }
            else if (basicRepresentationName.Equals("HLAinteger16BE") || basicRepresentationName.Equals("HLAinteger16LE"))
            {
                return "short";
            }
            else if (basicRepresentationName.Equals("HLAinteger32BE") || basicRepresentationName.Equals("HLAinteger32LE"))
            {
                return "int";
            }
            else if (basicRepresentationName.Equals("HLAinteger64BE") || basicRepresentationName.Equals("HLAinteger64LE"))
            {
                return "long";
            }
            else if (basicRepresentationName.Equals("HLAfloat32BE") || basicRepresentationName.Equals("HLAfloat32LE"))
            {
                return "float";
            }
            else if (basicRepresentationName.Equals("HLAfloat64BE") || basicRepresentationName.Equals("HLAfloat64LE"))
            {
                return "double";
            }
            else if (basicRepresentationName.Equals("HLAoctetPairBE") || basicRepresentationName.Equals("HLAoctetPairLE"))
            {
                return "short";
            }
            else if (basicRepresentationName.Equals("HLAoctet"))
            {
                return "byte";
            }
            else
            {
                return "byte[]";
            }
        }


        /// <summary> 
        /// Returns the name of the package specified by the given fully
        /// qualified class name.
        /// </summary>
        /// <param name="className">the fully qualified class name
        /// </param>
        /// <returns> the package name, or <code>null</code> for the base package
        /// </returns>
        private System.String GetPackageName(System.String className)
        {
            int index;

            if ((index = className.LastIndexOf((System.Char)'.')) == -1)
            {
                return null;
            }
            else
            {
                return className.Substring(0, (index) - (0));
            }
        }

        /// <summary> 
        /// Returns the name of the interface specified by the given a name
        /// </summary>
        /// <param name="interfaceName">the fully qualified interface name
        /// </param>
        /// <returns> the interface name
        /// </returns>
        private String GetInterfaceName(System.String interfaceName)
        {
            return "I" + interfaceName;
        }
        /// <summary> 
        /// Prints a class comment.
        /// </summary>
        /// <param name="sw">the print stream to write the comment to
        /// </param>
        /// <param name="body">the body of the comment
        /// </param>
        /// <param name="indentLevel">the indent level of the comments
        /// </param>
        private void PrintClassComment(System.IO.StreamWriter ps, System.String body, int indentLevel)
        {
            string indentStr = GenerateIndentString(indentLevel);
            ps.WriteLine(indentStr + "///<summary>");
            ps.Write(FormatCommentBody(indentLevel, body));
            ps.WriteLine(indentStr + "///</summary>");
            ps.WriteLine(indentStr + "/// <author> " + GetType().FullName + " from " + federationDocumentName + " </author>");
        }

        /// <summary> 
        /// Formats the body of a comment, adding asterisks at the beginning of each line.
        /// </summary>
        /// <param name="indentLevel">
        /// the level of indentation required
        /// </param>
        /// <param name="commentBody">
        /// the comment body to format
        /// </param>
        /// <returns> 
        /// the formatted body
        /// </returns>
        private System.String FormatCommentBody(int indentLevel, System.String commentBody)
        {
            System.String formattedBody = "";
            System.String formattedLinePrefix = GenerateIndentString(indentLevel) + "///";
            System.String formattedLine = formattedLinePrefix;

            Tokenizer st = new Tokenizer(commentBody);

            while (st.HasMoreTokens())
            {
                formattedLine = formattedLine + st.NextToken();

                if (formattedLine.Length > 75)
                {
                    formattedBody = formattedBody + formattedLine + System.Environment.NewLine;

                    formattedLine = formattedLinePrefix;
                }
                else
                {
                    formattedLine = formattedLine + " ";
                }
            }

            if ((System.Object)formattedLine != (System.Object)formattedLinePrefix)
            {
                formattedBody = formattedBody + formattedLine + System.Environment.NewLine;
            }

            return formattedBody;
        }


        /// <summary> 
        /// Prints a variable comment.
        /// </summary>
        /// <param name="sw">
        /// the print stream to write the comment to
        /// </param>
        /// <param name="body">
        /// the body of the comment
        /// </param>
        private void PrintVariableComment(System.IO.StreamWriter ps, System.String body, int indentLevel)
        {
            string indentStr = GenerateIndentString(indentLevel);
            ps.WriteLine(indentStr + "///<summary>");
            ps.Write(FormatCommentBody(indentLevel, body));
            ps.WriteLine(indentStr + "///</summary>");
        }

        /// <summary> 
        /// Generates an indentation string for the specified level of indentation.
        /// </summary>
        /// <param name="indentLevel">the level of indentation desired
        /// </param>
        /// <returns> the indentation string
        /// </returns>
        private System.String GenerateIndentString(int indentLevel)
        {
            System.Text.StringBuilder indent = new System.Text.StringBuilder();

            for (int i = 0; i < indentLevel; i++)
            {
                indent.Append("    ");
            }

            return indent.ToString();
        }


        /// <summary> 
        /// Generates a string of the same length as the parameter, consisting only
        /// of space characters.
        /// </summary>
        /// <param name="referencedString">the string whose length is to be copied
        /// </param>
        /// <returns> the space string
        /// </returns>
        private System.String Spacer(System.String referencedString)
        {
            int len = referencedString.Length;

            System.String spaces = "";

            for (int i = 0; i < len; i++)
            {
                spaces = spaces + " ";
            }

            return spaces;
        }

        /// <summary> 
        /// Converts the specified free-form name to a reasonable Native class name by
        /// removing whitespace and illegal characters, modifying capitalization, and
        /// so on.
        /// </summary>
        /// <param name="anyName">the free-form name to convert
        /// </param>
        /// <returns> 
        /// the converted name
        /// </returns>
        private System.String ConvertToNativeClassName(System.String anyName)
        {
            System.String className = "";
            Tokenizer st = new Tokenizer(anyName, "`~!@#$%^&*()-+=[{]}\\|;:'\",<.>/? \t\n\r\f");

            while (st.HasMoreTokens())
            {
                className = Capitalize(className) + Capitalize(st.NextToken());
            }

            return className;
        }

        /// <summary> 
        /// Capitalizes the first letter of the specified string.
        /// </summary>
        /// <param name="strToCapitalize">the string to Capitalize
        /// </param>
        /// <returns> the capitalized string
        /// </returns>
        private String Capitalize(System.String strToCapitalize)
        {
            if (strToCapitalize.Length <= 1)
            {
                return strToCapitalize.ToUpper();
            }
            else
            {
                return strToCapitalize.Substring(0, (1) - (0)).ToUpper() + strToCapitalize.Substring(1);
            }
        }

        private String DesCapitalize(System.String str)
        {
            if (str.Length <= 1)
            {
                return str.ToLower();
            }
            else
            {
                return str.Substring(0, (1) - (0)).ToLower() + str.Substring(1);
            }
        }

        private String BuildFieldName(System.String str, string className)
        {
            string tmp = Capitalize(str);
            if (str.Equals(className) || tmp.Equals(str))
                return str + "_";
            else
                return str;
        }

        private String BuildPropertyName(System.String str, string className)
        {
            string tmp = Capitalize(str);
            if (tmp.Equals(className))
                tmp = str + "Field";
            return tmp;
        }

        private String BuildRecordFieldName(System.String str, string className, string propertyName)
        {
            string tmp = str;
            if (tmp.Equals(className) || tmp.Equals(propertyName))
                return tmp + "_";
            else
                return tmp;
        }

        private String BuildRecordFieldPropertyName(System.String str, string className)
        {
            string tmp = str;
            if (tmp.Equals(className))
                throw new Exception("Field Record has the same name than the class");
            return tmp;
        }

        private string GetInteractionMessageName(string interactionName)
        {
            return interactionName + "Message";
        }

        /// <summary> The descriptor manager responsible for all descriptors (federation descriptors).</summary>
        private DescriptorManager descriptorManager;

        /// <summary> The target directory.</summary>
        public System.IO.FileInfo targetDirectory;

        /// <summary> The package prefix.</summary>
        public System.String packagePrefix;

        /// <summary> The generated serializers file.</summary>
        public System.String generatedSerializersFile;

        public StringCollection PackageDependencies = new StringCollection();
        public List<Assembly> AssemblyDependencies = new List<Assembly>();
        
        /// <summary> 
        /// The Basic data types assembly declaration file.
        /// </summary>
        public System.String assemblyDeclarationFilename = "AssemblyRTI1516Declarations";

        /// <summary> The proxy ambassador name.</summary>
        public System.String proxyAmbassadorName;

        /// <summary> The class serializer helper name.</summary>
        public System.String classSerializerHelperName;

        /// <summary> The classSerializerHelperName listener name.</summary>
        public System.String interactionListenerName;

        private String federationDocumentName = "DescriptorManager";

        /// <summary> 
        /// The set of opaque types: types that must be stored as byte arrays because
        /// their encodings are non-standard.
        /// </summary>
        private List<string> opaqueTypes = new List<string>();
    }
}
