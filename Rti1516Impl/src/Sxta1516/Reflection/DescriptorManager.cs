namespace Sxta.Rti1516.Reflection
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;
    using System.IO;
    using System.Xml;

    // Import log4net classes.
    using log4net;

    using Hla.Rti1516;
    using OrderType = Hla.Rti1516.OrderType;
    using HLAdimension = Sxta.Rti1516.Reflection.HLAdimension;
    using Sxta.Rti1516.XrtiHandles;

    /// <summary> 
    /// Manages a set of descriptors that represent features of the federation
    /// (object classes, interaction classes, and so on).
    /// </summary>
    /// <author> Agustin Santos. Based on code originally written by Andrzej Kapolka
    /// </author>
    public class DescriptorManager
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
        /// Returns the object model information
        /// </summary>
        virtual public HLAObjectModel ObjectModelInformation
        {
            get { return objectModel; }
        }

        /// <summary> 
        /// Returns an immutable collection containing the descriptors of all known object
        /// classes.  Each element of the collection will be an 
        /// <code>ObjectClassDescriptor</code>.
        /// </summary>
        /// <returns> an immutable collection containing the descriptors of all known object
        /// classes
        /// </returns>
        virtual public IList<ObjectClassDescriptor> ObjectClassDescriptors
        {
            get
            {
                return new List<ObjectClassDescriptor>(objectClassHandleDescriptorMap.Values).AsReadOnly();
            }
        }

        /// <summary> 
        /// Returns an immutable collection containing the descriptors of all known attributes.
        /// Each element of the collection will be an <code>AttributeDescriptor</code>.
        /// </summary>
        /// <returns> an immutable collection containing the descriptors of all known 
        /// attributes
        /// </returns>
        virtual public IList<AttributeDescriptor> AttributeDescriptors
        {
            get
            {
                return new List<AttributeDescriptor>(attributeHandleDescriptorMap.Values).AsReadOnly();
            }

        }
        /// <summary>
        ///  Returns an immutable collection containing the descriptors of all known interaction
        /// classes.  Each element of the collection will be an 
        /// <code>InteractionClassDescriptor</code>.
        /// </summary>
        /// <returns> an immutable collection containing the descriptors of all known interaction
        /// classes
        /// </returns>
        virtual public IList<InteractionClassDescriptor> InteractionClassDescriptors
        {
            get
            {
                return new List<InteractionClassDescriptor>(interactionClassHandleDescriptorMap.Values).AsReadOnly();
            }

        }
        /// <summary>
        ///  Returns an immutable collection containing the descriptors of all known parameters.
        /// Each element of the collection will be a <code>ParameterDescriptor</code>.
        /// </summary>
        /// <returns> an immutable collection containing the descriptors of all known 
        /// parameters
        /// </returns>
        virtual public IList<ParameterDescriptor> ParameterDescriptors
        {
            get
            {
                return new List<ParameterDescriptor>(parameterHandleDescriptorMap.Values).AsReadOnly();
            }
        }

        /// <summary> 
        /// Returns an immutable collection containing the descriptors of all known object
        /// instances.  Each element of the collection will be an 
        /// <code>ObjectInstanceDescriptor</code>.
        /// </summary>
        /// <returns> an immutable collection containing the descriptors of all known object
        /// instances
        /// </returns>
        virtual public IList<ObjectInstanceDescriptor> ObjectInstanceDescriptors
        {
            get
            {
                return new List<ObjectInstanceDescriptor>(objectInstanceHandleDescriptorMap.Values).AsReadOnly();
            }
        }

        /// <summary> 
        /// Returns an immutable collection containing the descriptors of all known dimensions.
        /// Each element of the collection will be a <code>DimensionDescriptor</code>.
        /// </summary>
        /// <returns> an immutable collection containing the descriptors of all known dimensions
        /// </returns>
        virtual public IList<DimensionDescriptor> DimensionDescriptors
        {
            get
            {
                return new List<DimensionDescriptor>(dimensionHandleDescriptorMap.Values).AsReadOnly();
            }
        }

        /// <summary> 
        /// Returns an immutable collection containing the descriptors of all known regions.
        /// Each element of the collection will be a <code>RegionDescriptor</code>.
        /// </summary>
        /// <returns> an immutable collection containing the descriptors of all known regions
        /// </returns>
        virtual public IList<RegionDescriptor> RegionDescriptors
        {
            get
            {
                return new List<RegionDescriptor>(regionHandleDescriptorMap.Values).AsReadOnly();
            }

        }

        /// <summary>
        /// Returns a list containing the information of all known basic types.
        /// Each element of the collection will be a <code>HLABasicData</code>.
        /// </summary>
        virtual public IList<HLABasicData> BasicDataTypeList
        {
            get { return new List<HLABasicData>(basicDataMap.Values).AsReadOnly(); }
        }

        /// <summary>
        /// Returns a Map containing the information of all known basic types.
        /// Maps simple type names to their representations.
        /// Each element of the collection will be a pair Key-value <code>string, HLABasicData</code>.
        /// </summary>
        virtual public Dictionary<string, HLABasicData> BasicDataTypeMap
        {
            get { return basicDataMap; }
        }

        /// <summary>
        /// Returns a list containing the information of all known simple types.
        /// Each element of the collection will be a <code>HLASimpleData</code>.
        /// </summary>
        virtual public IList<HLASimpleData> SimpleDataTypeList
        {
            get { return new List<HLASimpleData>(simpleDataMap.Values).AsReadOnly(); }
        }

        /// <summary>
        /// Returns a Map containing the information of all known simple types.
        /// Maps simple type names to their representations.
        /// Each element of the collection will be a pair Key-value <code>string, HLASimpleData</code>.
        /// </summary>
        virtual public Dictionary<string, HLASimpleData> SimpleDataTypeMap
        {
            get { return simpleDataMap; }
        }

        /// <summary>
        /// Returns a list containing the information of all known enumerated types.
        /// Each element of the collection will be a <code>HLAEnumeratedData</code>.
        /// </summary>
        virtual public IList<HLAEnumeratedData> EnumeratedDataTypeList
        {
            get { return new List<HLAEnumeratedData>(enumeratedDataMap.Values).AsReadOnly(); }
        }

        /// <summary>
        /// Returns a Map containing the information of all known enumerated types.
        /// Maps enumerated type names to their representations.
        /// Each element of the collection will be a pair Key-value <code>string, HLAEnumeratedData</code>.
        /// </summary>
        virtual public Dictionary<string, HLAEnumeratedData> EnumeratedDataTypeMap
        {
            get { return enumeratedDataMap; }
        }

        /// <summary>
        /// Returns a list containing the information of all known fixed record data types.
        /// Each element of the collection will be a <code>HLAFixedRecordData</code>.
        /// </summary>
        virtual public IList<HLAFixedRecordData> FixedRecordDataTypeList
        {
            get { return new List<HLAFixedRecordData>(fixedRecordDataMap.Values).AsReadOnly(); }
        }

        /// <summary>
        /// Returns a Map containing the information of all known fixed record data types.
        /// Maps enumerated type names to their representations.
        /// Each element of the collection will be a pair Key-value <code>string, HLAFixedRecordData</code>.
        /// </summary>
        virtual public Dictionary<string, HLAFixedRecordData> FixedRecordDataTypeMap
        {
            get { return fixedRecordDataMap; }
        }

        /// <summary>
        /// Returns a list containing the information of all known array data types.
        /// Each element of the collection will be a <code>HLAarrayDataType</code>.
        /// </summary>
        virtual public IList<HLAarrayDataType> ArrayDataTypeList
        {
            get { return new List<HLAarrayDataType>(arrayDataMap.Values).AsReadOnly(); }
        }

        /// <summary>
        /// Returns a Map containing the information of all known array data types.
        /// Maps array type names to their representations.
        /// Each element of the collection will be a pair Key-value <code>string, HLAarrayDataType</code>.
        /// </summary>
        virtual public Dictionary<string, HLAarrayDataType> ArrayDataTypeMap
        {
            get { return arrayDataMap; }
        }


        private int majorVersion = 0;
        private int minorVersion = 0;
        private List<string> names = new List<string>();

        /// <summary>
        /// The version number of the object model.  The upper 16 bits 
        /// of the integer contain the major version number, and the lower 16 bits contain
        /// the minor version number.
        /// </summary>
        public int Version
        {
            get { return (majorVersion << 16) | minorVersion; }
        }

        /// <summary>
        /// The name of the object model.  
        /// If the information comes from Xml files, this variable store all the DocumentElement.
        /// If the information comes from exploring code, this variable store all the assembly names
        /// The order is important.
        /// </summary>
        public string[] Names
        {
            get
            {
                string[] arrNames = new string[names.Count];
                names.CopyTo(arrNames);
                return arrNames;
            }
        }

        /// <summary> The object class tag.</summary>
        private const System.String OBJECT_CLASS = "objectClass";

        /// <summary> The attribute tag.</summary>
        private const System.String ATTRIBUTE = "attribute";

        /// <summary> The interaction class tag.</summary>
        private const System.String INTERACTION_CLASS = "interactionClass";

        /// <summary> The parameter tag.</summary>
        private const System.String PARAMETER = "parameter";

        /// <summary> The dimension tag.</summary>
        private const System.String DIMENSION = "dimension";

        /// <summary> The name attribute.</summary>
        private const System.String NAME = "name";

        /// <summary> The parents attribute.</summary>
        private const System.String PARENTS = "parents";

        /// <summary> The dimensions attribute.</summary>
        private const System.String DIMENSIONS = "dimensions";

        /// <summary> The transportation attribute.</summary>
        private const System.String TRANSPORTATION = "transportation";

        /// <summary> The order attribute.</summary>
        private const System.String ORDER = "order";

        /// <summary> The upper bound attribute.</summary>
        private const System.String UPPER_BOUND = "upperBound";

        /// <summary> Maps native  class names to object class descriptors.</summary>
        private Dictionary<Type, ObjectClassDescriptor> objectNativeClassNameDescriptorMap
            = new Dictionary<Type, ObjectClassDescriptor>();

        /// <summary> Maps object class names to object class descriptors.</summary>
        private Dictionary<string, ObjectClassDescriptor> objectClassNameDescriptorMap
            = new Dictionary<string, ObjectClassDescriptor>();

        /// <summary> Maps object class handles to object class descriptors.</summary>
        private Dictionary<IObjectClassHandle, ObjectClassDescriptor> objectClassHandleDescriptorMap
            = new Dictionary<IObjectClassHandle, ObjectClassDescriptor>();

        /// <summary> Maps object class names to object class listeners.</summary>
        private Dictionary<string, ArrayList> objectClassNameListenersMap
            = new Dictionary<string, ArrayList>();

        /// <summary> Maps attribute handles to attribute descriptors.</summary>
        private Dictionary<IAttributeHandle, AttributeDescriptor> attributeHandleDescriptorMap
            = new Dictionary<IAttributeHandle, AttributeDescriptor>();

        /// <summary> Maps attribute handles to object class listeners.</summary>
        private Dictionary<IAttributeHandle, ObjectClassDescriptor> attributeHandleListenerMap
            = new Dictionary<IAttributeHandle, ObjectClassDescriptor>();

        /// <summary> Maps interaction class names to interaction class descriptors.</summary>
        private Dictionary<string, InteractionClassDescriptor> interactionClassNameDescriptorMap
            = new Dictionary<string, InteractionClassDescriptor>();

        /// <summary> Maps interaction class handles to interaction class descriptors.</summary>
        private Dictionary<IInteractionClassHandle, InteractionClassDescriptor> interactionClassHandleDescriptorMap
            = new Dictionary<IInteractionClassHandle, InteractionClassDescriptor>();

        /// <summary> Maps interaction class names to interaction class listeners.</summary>
        private Dictionary<string, ArrayList> interactionClassNameListenersMap
            = new Dictionary<string, ArrayList>();

        /// <summary> Maps parameter handles to parameter descriptors.</summary>
        private Dictionary<IParameterHandle, ParameterDescriptor> parameterHandleDescriptorMap
            = new Dictionary<IParameterHandle, ParameterDescriptor>();

        /// <summary> Maps parameter handles to interaction class listeners.</summary>
        private Dictionary<IParameterHandle, InteractionClassDescriptor> parameterHandleListenerMap
            = new Dictionary<IParameterHandle, InteractionClassDescriptor>();

        /// <summary> Maps object instance names to object instance descriptors.</summary>
        private Dictionary<string, ObjectInstanceDescriptor> objectInstanceNameDescriptorMap
            = new Dictionary<string, ObjectInstanceDescriptor>();

        /// <summary> Maps object instance handles to object instance descriptors.</summary>
        private Dictionary<IObjectInstanceHandle, ObjectInstanceDescriptor> objectInstanceHandleDescriptorMap
            = new Dictionary<IObjectInstanceHandle, ObjectInstanceDescriptor>();

        /// <summary> Maps dimension names to dimension descriptors.</summary>
        private Dictionary<string, DimensionDescriptor> dimensionNameDescriptorMap
            = new Dictionary<string, DimensionDescriptor>();

        /// <summary> Maps dimension handles to dimension descriptors.</summary>
        private Dictionary<IDimensionHandle, DimensionDescriptor> dimensionHandleDescriptorMap
            = new Dictionary<IDimensionHandle, DimensionDescriptor>();

        /// <summary> Maps region handles to region descriptors.</summary>
        private Dictionary<IRegionHandle, RegionDescriptor> regionHandleDescriptorMap
            = new Dictionary<IRegionHandle, RegionDescriptor>();

        /// <summary> Maps basic types names to known basic types.</summary>
        private Dictionary<string, HLABasicData> basicDataMap = new Dictionary<string, HLABasicData>();

        /// <summary> Maps simple types names to known simple types.</summary>
        private Dictionary<string, HLASimpleData> simpleDataMap = new Dictionary<string, HLASimpleData>();

        /// <summary> Maps enumerated types names to known enumerated types.</summary>
        private Dictionary<string, HLAEnumeratedData> enumeratedDataMap = new Dictionary<string, HLAEnumeratedData>();

        /// <summary> Maps fixed record data types names to known fixed record data types.</summary>
        private Dictionary<string, HLAFixedRecordData> fixedRecordDataMap = new Dictionary<string, HLAFixedRecordData>();

        /// <summary> Maps array data types names to known array data types.</summary>
        private Dictionary<string, HLAarrayDataType> arrayDataMap = new Dictionary<string, HLAarrayDataType>();

        /// <summary> The handle counter for bootstrap descriptors.</summary>
        private int handleCounter = 1;

        private HLAObjectModel objectModel;

        // PATCH ANGEL

        /// <summary> The mapping tag.</summary>
        private const System.String ROOT_MAPPING_TAG = "mapping";

        /// <summary> The objectClass element.</summary>
        private const System.String OBJECT_CLASS_MAPPING_ELEMENT = "objectClass";

        /// <summary> The name tag.</summary>
        private const System.String NAME_MAPPING_TAG = "name";

        /// <summary> The class tag.</summary>
        private const System.String CLASS_NAME_MAPPING_TAG = "class";

        /// <summary> The assembly tag.</summary>
        private const System.String ASSEMBLY_MAPPING_TAG = "assembly";

        /// <summary> Maps class names to known native types.</summary>
        private Dictionary<string, Type> classNameToNativeTypeMap = new Dictionary<string, Type>();

        // TODO ANGEL:
        // + el nombre de la clase en el OMT tambien puede ser jerarquica
        //   en el descriptor manager no lo tenemos en cuenta, AUN
        //   pero cuando se lea el fichero del OMT eso nos puede generar problemas
        // + ya que estamos podriamos extenderlo para cuando le metamos mano a los serializadores
        //   ahora tenemos dos tipos de serializadores: los manuales y los automaticos.
        //   Quizas deberiamos ir pensando como meterlo.
        //   Ejemplo:
        //      <objectClass name="HLAfederation" class="Sxta.Rti1516.Management.HLAfederation" serializer="automatic"/>
        //      ó
        //      <objectClass name="HLAfederation" class="Sxta.Rti1516.Management.HLAfederation" serializer="Mytools.Otrascosas.MiSerializer"/>
        private void LoadNativeTypeForClassNames(String mappingFile)
        {
            System.Xml.XmlDocument d = new System.Xml.XmlDocument();
            if (File.Exists(mappingFile))
            {
                System.Xml.XmlReader xmlReader = new System.Xml.XmlTextReader(mappingFile);
                d.Load(xmlReader);
                System.Xml.XmlElement documentElement = (System.Xml.XmlElement)d.DocumentElement;

                XmlNodeList childNodes = documentElement.ChildNodes;

                for (int i = 0; i < childNodes.Count; i++)
                {
                    XmlAttributeCollection attributesNode = childNodes[i].Attributes;

                    XmlNode attributeName = attributesNode.GetNamedItem(NAME_MAPPING_TAG);
                    XmlNode attributeClassName = attributesNode.GetNamedItem(CLASS_NAME_MAPPING_TAG);

                    if (attributeName != null && attributeClassName != null)
                    {
                        String name = attributeName.Value;
                        String className = attributeClassName.Value;

                        XmlNode assemblyFile = attributesNode.GetNamedItem(ASSEMBLY_MAPPING_TAG);
                        Type typeClass = null;
                        if (assemblyFile == null)
                            typeClass = Assembly.GetExecutingAssembly().GetType(className);
                        else
                            typeClass = Assembly.LoadFrom(assemblyFile.Value).GetType(className);

                        if (log.IsDebugEnabled)
                            log.Debug("ObjectClass " + name + " = " + className + " [ " + typeClass + " ]");

                        classNameToNativeTypeMap[name] = typeClass;
                    }
                }
            }
        }

        private void SaveNativeTypeForClassNames(Assembly assembly, String mappingFile)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = ("    ");

            XmlWriter writer = XmlWriter.Create(mappingFile, settings);
            writer.WriteStartElement(ROOT_MAPPING_TAG);

            try
            {
                Type[] Types = assembly.GetTypes();

                // Display all the types contained in the specified assembly.
                foreach (Type oType in Types)
                {
                    Sxta.Rti1516.Reflection.HLAObjectClassAttribute objectClass =
                        (Sxta.Rti1516.Reflection.HLAObjectClassAttribute)System.Attribute.GetCustomAttribute(oType, typeof(Sxta.Rti1516.Reflection.HLAObjectClassAttribute));

                    if (objectClass != null)
                    {
                        // Write XML data
                        writer.WriteStartElement(OBJECT_CLASS_MAPPING_ELEMENT);
                        writer.WriteAttributeString(NAME_MAPPING_TAG, objectClass.Name);
                        writer.WriteAttributeString(CLASS_NAME_MAPPING_TAG, oType.FullName);
                        writer.WriteAttributeString(ASSEMBLY_MAPPING_TAG, assembly.GetName().CodeBase);

                        writer.WriteEndElement();
                    }
                }
            }
            catch (Exception e)
            {
                if (log.IsWarnEnabled)
                    log.Warn("Exception in SaveNativeTypeForClassNames: " + e);
            }

            writer.WriteEndElement();
            writer.Close();
        }

        // END PATCH


        /// <summary> Constructor.</summary>
        public DescriptorManager()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            RegisterAssembly(assembly);
        }

        public void RegisterAssembly(Assembly assembly)
        {
            String mappingFile = assembly.ManifestModule.Name.Substring(0, assembly.ManifestModule.Name.Length - 4) + "Mapping.xml";

            if (!File.Exists(mappingFile))
            {
                SaveNativeTypeForClassNames(assembly, mappingFile);
            }

            LoadNativeTypeForClassNames(mappingFile);
        }

        // PATCH ANGEL
        public DescriptorManager(string fddFileName)
        {
            AddDescriptors(fddFileName);
        }
        // END PATCH

        /// <summary>
        /// Load descriptors from an Assembly. It uses reflection and HLA attributes in order to explore the code. 
        /// </summary>
        /// <param name="assembly"> The assembly to explore</param>
        public virtual void AddDescriptorsFromAssembly(Assembly assembly)
        {
            if (assembly != null)
            {
                if (log.IsDebugEnabled)
                    log.Debug("Assembly Name :" + assembly.FullName);
                names.Add(assembly.FullName); //TODO it is not the right name. ??
            }
            else
                return;

            HLABasicDataAttribute[] basicDataArray = (HLABasicDataAttribute[])assembly.GetCustomAttributes(typeof(HLABasicDataAttribute), false);
            foreach (HLABasicDataAttribute basicData in basicDataArray)
            {
                //TODO Check that the corresponding native type is OK (size, etc.)
                basicDataMap.Add(basicData.Name, basicData.BasicDataInfo);
            }

            HLASimpleDataAttribute[] simpleDataArray = (HLASimpleDataAttribute[])assembly.GetCustomAttributes(typeof(HLASimpleDataAttribute), false);
            foreach (HLASimpleDataAttribute simpleData in simpleDataArray)
            {
                //TODO Check that the corresponding native type is OK (size, etc.)
                simpleDataMap.Add(simpleData.Name, simpleData.SimpleDataInfo);
            }

            HLAArrayDataAttribute[] arrayDataArray = (HLAArrayDataAttribute[])assembly.GetCustomAttributes(typeof(HLAArrayDataAttribute), false);
            foreach (HLAArrayDataAttribute arrayData in arrayDataArray)
            {
                arrayDataMap.Add(arrayData.Name, arrayData.ArrayDataInfo);
            }

            // we process ONLY the visible types. 
            Type[] Types = assembly.GetExportedTypes();

            // Display all the types contained in the specified assembly.
            foreach (Type oType in Types)
            {
                HLAObjectClassAttribute objectClass = (HLAObjectClassAttribute)System.Attribute.GetCustomAttribute(oType, typeof(HLAObjectClassAttribute));
                if (objectClass != null)
                {
                    ObjectClassDescriptor ocd = new ObjectClassDescriptor(objectClass.ObjectClassInfo, new XRTIObjectClassHandle(handleCounter++), new List<ObjectClassDescriptor>());

                    foreach (PropertyInfo propInfo in oType.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly))
                    {
                        HLAAttributeAttribute[] arrayOfCustomAttributes = (HLAAttributeAttribute[])propInfo.GetCustomAttributes(typeof(HLAAttributeAttribute), false);
                        foreach (HLAAttributeAttribute custumAttr in arrayOfCustomAttributes)
                        {
                            ocd.AddAttributeDescriptor(new AttributeDescriptor(custumAttr.AttributeInfo, new XRTIAttributeHandle(handleCounter++), new XRTIDimensionHandleSet()));
                        }

                    }
                    AddObjectClassDescriptor(ocd);
                }

                HLASimpleDataAttribute simpleDataAttr = (HLASimpleDataAttribute)System.Attribute.GetCustomAttribute(oType, typeof(HLASimpleDataAttribute));
                if (simpleDataAttr != null)
                {
                    simpleDataAttr.NativeType = oType;
                    simpleDataMap.Add(simpleDataAttr.Name, simpleDataAttr.SimpleDataInfo);
                }

                HLAFixedRecordDataAttribute fixedRecordDataAttr = (HLAFixedRecordDataAttribute)System.Attribute.GetCustomAttribute(oType, typeof(HLAFixedRecordDataAttribute));
                if (fixedRecordDataAttr != null)
                {
                    fixedRecordDataAttr.NativeType = oType;
                    foreach (FieldInfo fieldInfo in oType.GetFields())
                    {
                        HLARecordFieldAttribute[] recordFieldAttrs = (HLARecordFieldAttribute[])fieldInfo.GetCustomAttributes(typeof(HLARecordFieldAttribute), false);
                        foreach (HLARecordFieldAttribute field in recordFieldAttrs)
                        {
                            fixedRecordDataAttr.FixedRecordDataInfo.RecordFields.Add(field.RecordFieldInfo);
                        }
                    }
                    fixedRecordDataMap.Add(fixedRecordDataAttr.Name, fixedRecordDataAttr.FixedRecordDataInfo);
                }


                HLAEnumeratedDataAttribute enumerateDataAttr = (HLAEnumeratedDataAttribute)System.Attribute.GetCustomAttribute(oType, typeof(HLAEnumeratedDataAttribute));
                if (enumerateDataAttr != null)
                {
                    enumerateDataAttr.NativeType = oType;
                    string[] names = Enum.GetNames(oType);
                    Array values = Enum.GetValues(oType);

                    for (int i = 0; i < names.Length; i++)
                    {
                        HLAEnumerator enumerator = new HLAEnumerator();
                        enumerator.Name = names[i];
                        enumerator.Values = ((int)(values.GetValue(i))).ToString();
                        enumerateDataAttr.EnumeratedDataInfo.Enumerators.Add(enumerator);
                    }
                    enumeratedDataMap.Add(enumerateDataAttr.Name, enumerateDataAttr.EnumeratedDataInfo);
                }

                EventInfo[] events = oType.GetEvents(BindingFlags.Public | BindingFlags.Static);
                foreach (EventInfo eventInfo in events)
                {
                    HLAInteractionClassAttribute[] interactionAttrs = (HLAInteractionClassAttribute[])eventInfo.GetCustomAttributes(typeof(HLAInteractionClassAttribute), false);
                    if (interactionAttrs.Length != 0)
                    {
                        foreach (HLAInteractionClassAttribute interacAttr in interactionAttrs)
                        {
                            InteractionClassDescriptor icd = new InteractionClassDescriptor(interacAttr.InteractionClassInfo, new XRTIInteractionClassHandle(handleCounter++));
                            Type delegateType = eventInfo.EventHandlerType;
                            MethodInfo invoke = delegateType.GetMethod("Invoke");
                            ParameterInfo[] pars = invoke.GetParameters();
                            foreach (ParameterInfo p in pars)
                            {
                                HLAInteractionParameterAttribute[] interactionParams = (HLAInteractionParameterAttribute[])p.GetCustomAttributes(typeof(HLAInteractionParameterAttribute), false);
                                if (interactionParams.Length >= 0)
                                    icd.AddParameterDescriptor(new ParameterDescriptor(interactionParams[0].ParameterInfo, new XRTIParameterHandle(handleCounter++)));
                                else
                                    icd.AddParameterDescriptor(new ParameterDescriptor(p.Name, new XRTIParameterHandle(handleCounter++)));
                            }
                            AddInteractionClassDescriptor(icd);
                        }
                    }
                }
            }
        }

        /// <summary> 
        /// Adds a set of descriptors corresponding to the features contained
        /// in the specified federation description document.  Handle
        /// values will be assigned in the order that the described features are
        /// encountered in the document, starting at <code>1</code>.
        /// </summary>
        /// <param name="fddFileName">the filename of the federation description document to interpret
        /// </param>
        public virtual void AddDescriptors(string fddFileName)
        {
            AddDescriptors(Sxta.Rti1516ResourcesNames.GetXmlDocumentResource(fddFileName));
        }

        /// <summary> 
        /// Adds a set of descriptors corresponding to the features contained
        /// in the specified federation description document.  Handle
        /// values will be assigned in the order that the described features are
        /// encountered in the document, starting at <code>1</code>.
        /// </summary>
        /// <param name="fdd">the parsed federation description document to interpret
        /// </param>
        public virtual void AddDescriptors(System.Xml.XmlDocument fdd)
        {
            System.Xml.XmlElement documentElement = (System.Xml.XmlElement)fdd.DocumentElement;

            objectModel = new HLAObjectModel(documentElement);

            names.Add(objectModel.Name);

            System.String version = objectModel.Version;
            if (!string.IsNullOrEmpty(version))
            {
                SupportClass.Tokenizer st = new SupportClass.Tokenizer(version, " .");
                majorVersion = System.Int32.Parse(st.NextToken());
                minorVersion = System.Int32.Parse(st.NextToken());
            }

            // First pass creates descriptors, assigns handles

            System.Xml.XmlNodeList nl = documentElement.GetElementsByTagName("basicData");
            for (int i = 0; i < nl.Count; i++)
            {
                System.Xml.XmlElement basicDataElement = (System.Xml.XmlElement)nl.Item(i);
                HLABasicData basicData = new HLABasicData(basicDataElement);
                basicDataMap[basicData.Name] = basicData;
            }

            nl = documentElement.GetElementsByTagName("simpleData");
            for (int i = 0; i < nl.Count; i++)
            {
                System.Xml.XmlElement simpleDataElement = (System.Xml.XmlElement)nl.Item(i);
                HLASimpleData simpleData = new HLASimpleData(simpleDataElement);
                simpleDataMap[simpleData.Name] = simpleData;
            }
            nl = documentElement.GetElementsByTagName("enumeratedData");
            for (int i = 0; i < nl.Count; i++)
            {
                System.Xml.XmlElement enumeratedDataElement = (System.Xml.XmlElement)nl.Item(i);
                HLAEnumeratedData enumeratedData = new HLAEnumeratedData(enumeratedDataElement);
                enumeratedDataMap[enumeratedData.Name] = enumeratedData;
            }
            nl = documentElement.GetElementsByTagName("fixedRecordData");
            for (int i = 0; i < nl.Count; i++)
            {
                System.Xml.XmlElement fixedRecordDataElement = (System.Xml.XmlElement)nl.Item(i);
                HLAFixedRecordData fixedRecordData = new HLAFixedRecordData(fixedRecordDataElement);
                fixedRecordDataMap[fixedRecordData.Name] = fixedRecordData;
            }
            nl = documentElement.GetElementsByTagName("arrayData");
            for (int i = 0; i < nl.Count; i++)
            {
                System.Xml.XmlElement arrayDataElement = (System.Xml.XmlElement)nl.Item(i);
                HLAarrayDataType arrayData = new HLAarrayDataType(arrayDataElement);
                arrayDataMap[arrayData.Name] = arrayData;
            }

            nl = documentElement.GetElementsByTagName(OBJECT_CLASS);

            for (int i = 0; i < nl.Count; i++)
            {
                System.Xml.XmlElement objectClassElement = (System.Xml.XmlElement)nl.Item(i);

                ObjectClassDescriptor ocd = new ObjectClassDescriptor(objectClassElement, new XRTIObjectClassHandle(handleCounter++), new List<ObjectClassDescriptor>());
                System.Xml.XmlNodeList nl2 = objectClassElement.ChildNodes;
                for (int j = 0; j < nl2.Count; j++)
                {
                    if (nl2.Item(j) is System.Xml.XmlElement && nl2.Item(j).Name.Equals(ATTRIBUTE))
                    {
                        System.Xml.XmlElement attributeElement = (System.Xml.XmlElement)nl2.Item(j);

                        AttributeDescriptor ad = new AttributeDescriptor(attributeElement, new XRTIAttributeHandle(handleCounter++), new XRTIDimensionHandleSet(), "HLAreliable".Equals(attributeElement.GetAttribute(TRANSPORTATION)) ? TransportationType.HLA_RELIABLE : TransportationType.HLA_BEST_EFFORT, "Receive".Equals(attributeElement.GetAttribute(ORDER)) ? Hla.Rti1516.OrderType.RECEIVE : OrderType.TIMESTAMP);
                        ocd.AddAttributeDescriptor(ad);

                        // PATCH ANGEL
                        AddAttributeDescriptor(ad);
                    }
                }

                AddObjectClassDescriptor(ocd);
            }

            nl = documentElement.GetElementsByTagName(INTERACTION_CLASS);

            for (int i = 0; i < nl.Count; i++)
            {
                System.Xml.XmlElement interactionClassElement = (System.Xml.XmlElement)nl.Item(i);

                InteractionClassDescriptor icd = new InteractionClassDescriptor(interactionClassElement, new XRTIInteractionClassHandle(handleCounter++), new List<InteractionClassDescriptor>(), new XRTIDimensionHandleSet(), "HLAreliable".Equals(interactionClassElement.GetAttribute(TRANSPORTATION)) ? TransportationType.HLA_RELIABLE : TransportationType.HLA_BEST_EFFORT, "Receive".Equals(interactionClassElement.GetAttribute(ORDER)) ? Sxta.Rti1516.Reflection.HLAorderType.Receive : Sxta.Rti1516.Reflection.HLAorderType.TimeStamp);

                System.Xml.XmlNodeList nl2 = interactionClassElement.ChildNodes;

                for (int j = 0; j < nl2.Count; j++)
                {
                    if (nl2.Item(j) is System.Xml.XmlElement && nl2.Item(j).Name.Equals(PARAMETER))
                    {
                        System.Xml.XmlElement parameterElement = (System.Xml.XmlElement)nl2.Item(j);

                        icd.AddParameterDescriptor(new ParameterDescriptor(parameterElement, new XRTIParameterHandle(handleCounter++)));
                    }
                }

                AddInteractionClassDescriptor(icd);
            }

            nl = documentElement.GetElementsByTagName(DIMENSION);

            for (int i = 0; i < nl.Count; i++)
            {
                System.Xml.XmlElement dimensionElement = (System.Xml.XmlElement)nl.Item(i);

                long upperBound = System.Int64.MaxValue;

                if (dimensionElement.HasAttribute(UPPER_BOUND))
                {
                    try
                    {
                        upperBound = System.Int64.Parse(dimensionElement.GetAttribute(UPPER_BOUND));
                    }
                    catch (System.FormatException)
                    {
                    }
                }
                AddDimensionDescriptor(new DimensionDescriptor(dimensionElement.GetAttribute(NAME), new XRTIDimensionHandle(handleCounter++), upperBound));
            }

            // Second pass resolves parents, dimensions

            nl = documentElement.GetElementsByTagName(OBJECT_CLASS);

            for (int i = 0; i < nl.Count; i++)
            {
                System.Xml.XmlElement objectClassElement = (System.Xml.XmlElement)nl.Item(i);

                ObjectClassDescriptor ocd = GetObjectClassDescriptor(objectClassElement.GetAttribute(NAME));

                if (objectClassElement.ParentNode.Name.Equals(OBJECT_CLASS))
                {
                    ocd.AddParentDescriptor(GetObjectClassDescriptor(((System.Xml.XmlElement)objectClassElement.ParentNode).GetAttribute(NAME)));
                }

                if (objectClassElement.HasAttribute(PARENTS))
                {
                    SupportClass.Tokenizer st = new SupportClass.Tokenizer(objectClassElement.GetAttribute(PARENTS));

                    // PATCH ANGEL while (st.HasMoreTokens())
                    //{
                    ocd.ParentDescriptors.Add(GetObjectClassDescriptor(st.NextToken()));
                    //}
                }

                System.Xml.XmlNodeList nl2 = objectClassElement.ChildNodes;
                for (int j = 0; j < nl2.Count; j++)
                {
                    if (nl2.Item(j) is System.Xml.XmlElement && nl2.Item(j).Name.Equals(ATTRIBUTE))
                    {
                        System.Xml.XmlElement attributeElement = (System.Xml.XmlElement)nl2.Item(j);

                        if (attributeElement.HasAttribute(DIMENSIONS))
                        {
                            AttributeDescriptor ad = ocd.GetAttributeDescriptor(attributeElement.GetAttribute(NAME));

                            SupportClass.Tokenizer st = new SupportClass.Tokenizer(attributeElement.GetAttribute(DIMENSIONS));

                            // PATCH ANGEL while (st.HasMoreTokens())
                            //{
                            System.String dimension = st.NextToken();

                            if (!dimension.Equals("NA"))
                            {
                                ad.Dimensions.Add(GetDimensionDescriptor(dimension).Handle);
                            }
                            //}
                        }
                    }
                }
            }

            nl = documentElement.GetElementsByTagName(INTERACTION_CLASS);

            for (int i = 0; i < nl.Count; i++)
            {
                System.Xml.XmlElement interactionClassElement = (System.Xml.XmlElement)nl.Item(i);

                InteractionClassDescriptor icd = GetInteractionClassDescriptor(interactionClassElement.GetAttribute(NAME));

                if (interactionClassElement.HasAttribute(DIMENSIONS))
                {
                    SupportClass.Tokenizer st = new SupportClass.Tokenizer(interactionClassElement.GetAttribute(DIMENSIONS));

                    // PATCH ANGEL while (st.HasMoreTokens())
                    //{
                    System.String dimension = st.NextToken();

                    if (!dimension.Equals("NA"))
                    {
                        icd.Dimensions.Add(GetDimensionDescriptor(dimension).Handle);
                    }
                    //}
                }

                if (interactionClassElement.ParentNode.Name.Equals(INTERACTION_CLASS))
                {
                    icd.AddParentDescriptor(GetInteractionClassDescriptor(((System.Xml.XmlElement)interactionClassElement.ParentNode).GetAttribute(NAME)));
                }

                if (interactionClassElement.HasAttribute(PARENTS))
                {
                    SupportClass.Tokenizer st = new SupportClass.Tokenizer(interactionClassElement.GetAttribute(PARENTS));

                    // PATCH ANGEL while (st.HasMoreTokens())
                    //{
                    icd.AddParentDescriptor(GetInteractionClassDescriptor(st.NextToken()));
                    //}
                }
            }

            //PATCH ANGEL CheckAopFdd();
        }

        /// <summary> 
        /// Checks whether the specified object class handle is a bootstrap
        /// handle.
        /// </summary>
        /// <param name="och">the object class handle to check
        /// </param>
        /// <returns> <code>true</code> if the handle is a bootstrap handle,
        /// <code>false</code> otherwise
        /// </returns>
        public virtual bool IsBootstrapHandle(IObjectClassHandle och)
        {
            return ((XRTIObjectClassHandle)och).Identifier < handleCounter;
        }

        /// <summary> 
        /// Checks whether the specified interaction class handle is a bootstrap
        /// handle.
        /// </summary>
        /// <param name="ich">the interaction class handle to check
        /// </param>
        /// <returns> <code>true</code> if the handle is a bootstrap handle,
        /// <code>false</code> otherwise
        /// </returns>
        public virtual bool IsBootstrapHandle(IInteractionClassHandle ich)
        {
            return ((XRTIInteractionClassHandle)ich).Identifier < handleCounter;
        }

        /// <summary> 
        /// Adds a listener for object classes with a particular name.
        /// </summary>
        /// <param name="name">the object class name of interest
        /// </param>
        /// <param name="ocd">the object class descriptor to notify
        /// </param>
        protected internal virtual void AddObjectClassListener(System.String name, ObjectClassDescriptor ocd)
        {
            System.Collections.ArrayList v = objectClassNameListenersMap[name];

            if (v == null)
            {
                v = System.Collections.ArrayList.Synchronized(new System.Collections.ArrayList(10));
                objectClassNameListenersMap[name] = v;
            }

            v.Add(ocd);
        }

        /// <summary>
        ///  Adds a listener for attributes with the specified handle.
        /// </summary>
        /// <param name="handle">the attribute handle of interest
        /// </param>
        /// <param name="ocd">the object class descriptor to notify
        /// </param>
        protected internal virtual void AddAttributeListener(IAttributeHandle handle, ObjectClassDescriptor ocd)
        {
            attributeHandleListenerMap[handle] = ocd;
        }

        /// <summary> 
        /// Adds a listener for interaction classes with a particular name.
        /// </summary>
        /// <param name="name">the interaction class name of interest
        /// </param>
        /// <param name="icd">the interaction class descriptor to notify
        /// </param>
        protected internal virtual void AddInteractionClassListener(System.String name, InteractionClassDescriptor icd)
        {
            System.Collections.ArrayList v = interactionClassNameListenersMap[name];

            if (v == null)
            {
                v = System.Collections.ArrayList.Synchronized(new System.Collections.ArrayList(10));
                interactionClassNameListenersMap[name] = v;
            }

            v.Add(icd);
        }

        /// <summary> 
        /// Adds a listener for parameters with the specified handle.
        /// </summary>
        /// <param name="handle">the parameter handle of interest
        /// </param>
        /// <param name="icd">the interaction class descriptor to notify
        /// </param>
        protected internal virtual void AddParameterListener(IParameterHandle handle, InteractionClassDescriptor icd)
        {
            parameterHandleListenerMap[handle] = icd;
        }

        /// <summary>
        ///  Adds an object class descriptor.
        /// </summary>
        /// <param name="ocd">the object class descriptor to Add
        /// </param>
        public virtual void AddObjectClassDescriptor(ObjectClassDescriptor ocd)
        {
            if (!objectClassNameDescriptorMap.ContainsKey(ocd.Name))
            {
                // PATCH ANGEL
                if (classNameToNativeTypeMap.ContainsKey(ocd.Name))
                    ocd.nativeName = classNameToNativeTypeMap[ocd.Name];
                // END PATCH
                objectClassNameDescriptorMap.Add(ocd.Name, ocd);
                objectClassHandleDescriptorMap.Add(ocd.Handle, ocd);
                if (ocd.NativeName != null)
                    objectNativeClassNameDescriptorMap.Add(ocd.NativeName, ocd);
            }
            else
            {
                if (log.IsWarnEnabled)
                    log.Warn("Object Class Descriptor " + ocd.Name + " already exists. Not replaced.");
            }
        }

        /// <summary> 
        /// Removes an object class descriptor.
        /// </summary>
        /// <param name="ocd">the object class descriptor to Remove
        /// </param>
        public virtual void RemoveObjectClassDescriptor(ObjectClassDescriptor ocd)
        {
            if (objectClassNameDescriptorMap[ocd.Name] == ocd)
            {
                objectClassNameDescriptorMap.Remove(ocd.Name);
            }

            if (objectClassHandleDescriptorMap[ocd.Handle] == ocd)
            {
                objectClassHandleDescriptorMap.Remove(ocd.Handle);
            }
            if (ocd.NativeName != null && objectNativeClassNameDescriptorMap[ocd.NativeName] == ocd)
            {
                objectNativeClassNameDescriptorMap.Remove(ocd.NativeName);
            }
        }

        /// <summary> 
        /// Returns the descriptor for the object class with the given name.
        /// </summary>
        /// <param name="name">the name of the object class
        /// </param>
        /// <returns> the object class descriptor, or <code>null</code> if no such
        /// descriptor exists
        /// </returns>
        public virtual ObjectClassDescriptor GetObjectClassDescriptor(System.String name)
        {
            return objectClassNameDescriptorMap[name];
        }

        /// <summary> 
        /// Returns the descriptor for the object class with the given native class name.
        /// </summary>
        /// <param name="nativeClass">the type of the native class
        /// </param>
        /// <returns> the object class descriptor, or <code>null</code> if no such
        /// descriptor exists
        /// </returns>
        public virtual ObjectClassDescriptor GetObjectClassDescriptorFromNativeName(Type nativeClass)
        {
            return objectNativeClassNameDescriptorMap[nativeClass];
        }

        /// <summary> Returns the descriptor for the object class with the given handle.
        /// 
        /// </summary>
        /// <param name="handle">the handle of the object class
        /// </param>
        /// <returns> the object class descriptor, or <code>null</code> if no such
        /// descriptor exists
        /// </returns>
        public virtual ObjectClassDescriptor GetObjectClassDescriptor(IObjectClassHandle handle)
        {
            return objectClassHandleDescriptorMap[handle];
        }

        /// <summary> 
        /// Adds an attribute descriptor.
        /// </summary>
        /// <param name="ad">the attribute descriptor to Add
        /// </param>
        public virtual void AddAttributeDescriptor(AttributeDescriptor ad)
        {
            attributeHandleDescriptorMap[ad.Handle] = ad;
        }

        /// <summary> 
        /// Removes an attribute descriptor.
        /// </summary>
        /// <param name="ad">the attribute descriptor to Remove
        /// </param>
        public virtual void RemoveAttributeDescriptor(AttributeDescriptor ad)
        {
            if (attributeHandleDescriptorMap[ad.Handle] == ad)
            {
                attributeHandleDescriptorMap.Remove(ad.Handle);
            }
        }

        /// <summary> 
        /// Returns the descriptor for the attribute with the given handle.
        /// </summary>
        /// <param name="handle">the handle of the attribute
        /// </param>
        /// <returns> the attribute descriptor, or <code>null</code> if no such
        /// descriptor exists
        /// </returns>
        public virtual AttributeDescriptor GetAttributeDescriptor(IAttributeHandle handle)
        {
            return attributeHandleDescriptorMap[handle];
        }

        /// <summary> 
        /// Adds an interaction class descriptor.
        /// </summary>
        /// <param name="icd">the interaction class descriptor to Add
        /// </param>
        public virtual void AddInteractionClassDescriptor(InteractionClassDescriptor icd)
        {
            interactionClassNameDescriptorMap[icd.Name] = icd;
            interactionClassHandleDescriptorMap[icd.Handle] = icd;
        }

        /// <summary> 
        /// Removes an interaction class descriptor.
        /// </summary>
        /// <param name="icd">the interaction class descriptor to Remove
        /// </param>
        public virtual void RemoveInteractionClassDescriptor(InteractionClassDescriptor icd)
        {
            if (interactionClassNameDescriptorMap[icd.Name] == icd)
            {
                interactionClassNameDescriptorMap.Remove(icd.Name);
            }

            if (interactionClassHandleDescriptorMap[icd.Handle] == icd)
            {
                interactionClassHandleDescriptorMap.Remove(icd.Handle);
            }
        }

        /// <summary> 
        /// Returns the descriptor for the interaction class with the given name.
        /// </summary>
        /// <param name="name">the name of the interaction class
        /// </param>
        /// <returns> the interaction class descriptor, or <code>null</code> if no such
        /// descriptor exists
        /// </returns>
        public virtual InteractionClassDescriptor GetInteractionClassDescriptor(System.String name)
        {
            return interactionClassNameDescriptorMap[name];
        }

        /// <summary> 
        /// Returns the descriptor for the interaction class with the given handle.
        /// </summary>
        /// <param name="handle">the handle of the interaction class
        /// </param>
        /// <returns> the interaction class descriptor, or <code>null</code> if no such
        /// descriptor exists
        /// </returns>
        public virtual InteractionClassDescriptor GetInteractionClassDescriptor(IInteractionClassHandle handle)
        {
            return interactionClassHandleDescriptorMap[handle];
        }

        /// <summary> 
        /// Adds a parameter descriptor.
        /// </summary>
        /// <param name="pd">the parameter descriptor to Add
        /// </param>
        public virtual void AddParameterDescriptor(ParameterDescriptor pd)
        {
            parameterHandleDescriptorMap[pd.Handle] = pd;
        }

        /// <summary> 
        /// Removes a parameter descriptor.
        /// </summary>
        /// <param name="pd">the parameter descriptor to Remove
        /// </param>
        public virtual void RemoveParameterDescriptor(ParameterDescriptor pd)
        {
            if (parameterHandleDescriptorMap[pd.Handle] == pd)
            {
                parameterHandleDescriptorMap.Remove(pd.Handle);
            }
        }

        /// <summary> 
        /// Returns the descriptor for the parameter with the given handle.
        /// </summary>
        /// <param name="handle">the handle of the parameter
        /// </param>
        /// <returns> the parameter descriptor, or <code>null</code> if no such
        /// descriptor exists
        /// </returns>
        public virtual ParameterDescriptor GetParameterDescriptor(IParameterHandle handle)
        {
            return parameterHandleDescriptorMap[handle];
        }

        /// <summary> 
        /// Adds an object instance descriptor.
        /// </summary>
        /// <param name="oid">the object instance descriptor to Add
        /// </param>
        public virtual void AddObjectInstanceDescriptor(ObjectInstanceDescriptor oid)
        {
            objectInstanceNameDescriptorMap[oid.Name] = oid;
            objectInstanceHandleDescriptorMap[oid.Handle] = oid;
        }

        /// <summary>
        ///  Removes an object instance descriptor.
        /// </summary>
        /// <param name="oid">the object instance descriptor to Remove
        /// </param>
        public virtual void RemoveObjectInstanceDescriptor(ObjectInstanceDescriptor oid)
        {
            if (objectInstanceNameDescriptorMap[oid.Name] == oid)
            {
                objectInstanceNameDescriptorMap.Remove(oid.Name);
            }

            if (objectInstanceHandleDescriptorMap[oid.Handle] == oid)
            {
                objectInstanceHandleDescriptorMap.Remove(oid.Handle);
            }
        }

        /// <summary>
        ///  Returns the descriptor for the object instance with the given name.
        /// </summary>
        /// <param name="name">the name of the object instance
        /// </param>
        /// <returns> the object instance descriptor, or <code>null</code> if no such
        /// descriptor exists
        /// </returns>
        public virtual ObjectInstanceDescriptor GetObjectInstanceDescriptor(System.String name)
        {
            return objectInstanceNameDescriptorMap[name];
        }

        /// <summary> 
        /// Returns the descriptor for the object instance with the given handle.
        /// </summary>
        /// <param name="handle">the handle of the object instance
        /// </param>
        /// <returns> the object instance descriptor, or <code>null</code> if no such
        /// descriptor exists
        /// </returns>
        public virtual ObjectInstanceDescriptor GetObjectInstanceDescriptor(IObjectInstanceHandle handle)
        {
            return objectInstanceHandleDescriptorMap[handle];
        }

        /// <summary>
        ///  Adds a dimension descriptor.
        /// </summary>
        /// <param name="dd">the dimension descriptor to Add
        /// </param>
        public virtual void AddDimensionDescriptor(DimensionDescriptor dd)
        {
            dimensionNameDescriptorMap[dd.Name] = dd;
            dimensionHandleDescriptorMap[dd.Handle] = dd;
        }

        /// <summary> 
        /// Removes a dimension descriptor.
        /// </summary>
        /// <param name="dd">the dimension descriptor to Remove
        /// </param>
        public virtual void RemoveDimensionDescriptor(DimensionDescriptor dd)
        {
            if (dimensionNameDescriptorMap[dd.Name] == dd)
            {
                dimensionNameDescriptorMap.Remove(dd.Name);
            }

            if (dimensionHandleDescriptorMap[dd.Handle] == dd)
            {
                dimensionHandleDescriptorMap.Remove(dd.Handle);
            }
        }

        /// <summary> 
        /// Returns the descriptor for the dimension with the given name.
        /// </summary>
        /// <param name="name">the name of the dimension
        /// </param>
        /// <returns> the dimension descriptor, or <code>null</code> if no such
        /// descriptor exists
        /// </returns>
        public virtual DimensionDescriptor GetDimensionDescriptor(System.String name)
        {
            return (DimensionDescriptor)dimensionNameDescriptorMap[name];
        }

        /// <summary> 
        /// Returns the descriptor for the dimension with the given handle.
        /// </summary>
        /// <param name="handle">the handle of the dimension
        /// </param>
        /// <returns> the dimension descriptor, or <code>null</code> if no such
        /// descriptor exists
        /// </returns>
        public virtual DimensionDescriptor GetDimensionDescriptor(IDimensionHandle handle)
        {
            return dimensionHandleDescriptorMap[handle];
        }

        /// <summary> 
        /// Adds a region descriptor.
        /// </summary>
        /// <param name="rd">the region descriptor to Add
        /// </param>
        public virtual void AddRegionDescriptor(RegionDescriptor rd)
        {
            regionHandleDescriptorMap[rd.Handle] = rd;
        }

        /// <summary>
        ///  Removes a region descriptor.
        /// </summary>
        /// <param name="rd">the region descriptor to Remove
        /// </param>
        public virtual void RemoveRegionDescriptor(RegionDescriptor rd)
        {
            //UPGRADE_TODO: Method 'java.util.HashMap.get' was converted to 'System.Collections.Hashtable.Item' which has a different behavior. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1073_javautilHashMapget_javalangObject_3"'
            if (regionHandleDescriptorMap[rd.Handle] == rd)
            {
                regionHandleDescriptorMap.Remove(rd.Handle);
            }
        }

        /// <summary> 
        /// Returns the descriptor for the region with the given handle.
        /// </summary>
        /// <param name="handle">the handle of the region
        /// </param>
        /// <returns> the region descriptor, or <code>null</code> if no such
        /// descriptor exists
        /// </returns>
        public virtual RegionDescriptor GetRegionDescriptor(IRegionHandle handle)
        {
            //UPGRADE_TODO: Method 'java.util.HashMap.get' was converted to 'System.Collections.Hashtable.Item' which has a different behavior. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1073_javautilHashMapget_javalangObject_3"'
            return (RegionDescriptor)regionHandleDescriptorMap[handle];
        }

        public void CheckAopFdd()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            if (log.IsDebugEnabled) log.Debug("Assembly Name :" + assembly.FullName);
            try
            {
                Type[] Types = assembly.GetTypes();
                // Display all the types contained in the specified assembly.
                foreach (Type oType in Types)
                {
                    Sxta.Rti1516.Reflection.HLAObjectClassAttribute objectClass =
                        (Sxta.Rti1516.Reflection.HLAObjectClassAttribute)System.Attribute.GetCustomAttribute(oType, typeof(Sxta.Rti1516.Reflection.HLAObjectClassAttribute));

                    if (objectClass != null)
                    {
                        //Get the Key value.    
                        if (log.IsDebugEnabled)
                            log.Debug("HLAObjectClassAttribute Found! in " + oType.FullName +
                                      ". Attribute Name: " + objectClass.Name +
                                      ", Semantics :" + objectClass.Semantics);

                        try
                        {
                            ObjectClassDescriptor ocd = this.GetObjectClassDescriptor(objectClass.Name);

                            if (log.IsDebugEnabled)
                                log.Debug("Object Class Data Found: " + ocd);

                            ocd.nativeName = oType;
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
            }
            catch (Exception e)
            {
                if (log.IsWarnEnabled)
                    log.Warn("Exception in CheckAopFdd: " + e);
            }
        }
    }
}