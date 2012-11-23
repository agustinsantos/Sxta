namespace Sxta
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.IO;
    using System.Xml;

    // Import log4net classes.
    using log4net;

    public class Rti1516ResourcesNames
    {
        /// <summary> The Hello World Object Model, stored as a resource.</summary>
        public const System.String HelloWorldObjectModel = "Resources/HelloWorldObjectModel.xml";

        /// <summary> The bootstrap object model (included as a resource).</summary>
        public const System.String BootstrapObjectModel = "Resources/BootstrapObjectModel.xml";

        /// <summary> The Low Level Management object model (included as a resource).</summary>
        public const System.String LowLevelManagementObjectModel = "Resources/LowLevelManagementObjectModel.xml";

        /// <summary> The meta-federation object model (included as a resource).</summary>
        public const System.String MetaFederationObjectModel = "Resources/MetaFederationObjectModel.xml";

        /// <summary> The reflection object model (included as a resource).</summary>
        public const System.String ReflectionObjectModel = "Resources/ReflectionObjectModel.xml";

        /// <summary> The management object model (included as a resource).</summary>
        public const System.String ManagementObjectModel = "Resources/ManagementObjectModel.xml";

        public const System.String SxtaObjectModel = "Resources/SxtaObjectModel.xml";    // PATCH ANGEL


        public static System.Xml.XmlDocument GetXmlDocumentResource(string resourceFileName)
        {
            System.Xml.XmlDocument d = new System.Xml.XmlDocument();
            if (!File.Exists(resourceFileName))
            {
                System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();

                // Embedded resources are located using the namespace plus the directory/file name.
                // In our case, the default namespace is Sxta.Rti1516.
                string resourceName = "Sxta.Rti1516." + resourceFileName.Replace('/', '.');
                Stream resourceStream = asm.GetManifestResourceStream(resourceName);

                if (resourceStream == null)
                {
                    string[] resNames = asm.GetManifestResourceNames();
                    if (log.IsDebugEnabled)
                    {
                        log.Debug("Config file nor resource SxtaConfig.xml do not exist.");
                        log.Debug("This application has the following Manifest Resource Names:");
                        foreach (string resname in resNames)
                        {
                            log.Debug("Resources :" + resname);
                        }
                    }
                    throw new Exception(resourceFileName + "file nor " + resourceName + "resource does not exist");
                }

                System.Xml.XmlReader xmlReader = new System.Xml.XmlTextReader(resourceStream);
                d.Load(xmlReader);
            }
            else
            {
                d.Load(resourceFileName);
            }
            return d;
        }

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
    }
}
