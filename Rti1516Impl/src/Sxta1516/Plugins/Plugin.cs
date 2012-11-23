namespace Sxta.Core.Plugins
{
    using System;
    using System.Diagnostics;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.IO;
    using System.Reflection;
    using System.Resources;
    using System.Xml;
    using System.Xml.Schema;
    using System.Text;

    /// <summary>
    /// The <code>Plugin</code> class handles the extensibility of the PluginTree by loading
    /// xml descriptions about nodes to insert.
    /// </summary>
    public class Plugin : IPlugin
    {
        string name = null;
        string author = null;
        string copyright = null;
        string url = null;
        string description = null;
        string version = null;
        string fileName = null;

        IDictionary<string, Assembly> runtimeLibraries = new Dictionary<string, Assembly>();

        IList<Extension> extensions = new List<Extension>();

        /// <summary>
        /// returns the filename of the xml definition in which
        /// this Plugin is defined.
        /// </summary>
        public string FileName
        {
            get { return fileName; }
        }

        /// <summary>
        /// returns the Name of the Plugin
        /// </summary>
        public string Name
        {
            get { return name; }
        }

        /// <summary>
        /// returns the Author of the Plugin
        /// </summary>
        public string Author
        {
            get { return author; }
        }

        /// <summary>
        /// returns a copyright string of the Plugin
        /// </summary>
        public string Copyright
        {
            get { return copyright; }
        }

        /// <summary>
        /// returns a url of the homepage of the plugin
        /// or the author.
        /// </summary>
        public string Url
        {
            get { return url; }
        }

        /// <summary>
        /// returns a brief description of what the plugin
        /// does.
        /// </summary>
        public string Description
        {
            get { return description; }
        }

        /// <summary>
        /// returns the version of the plugin.
        /// </summary>
        public string Version
        {
            get { return version; }
        }

        /// <summary>
        /// returns a hashtable with the runtime libraries
        /// where the key is the assembly name and the value
        /// is the assembly object.
        /// </summary>
        public IDictionary<string, Assembly> RuntimeLibraries
        {
            get { return runtimeLibraries; }
        }

        /// <summary>
        /// returns a arraylist with all extensions defined by
        /// this Plugin.
        /// </summary>
        public IList<Extension> Extensions
        {
            get { return extensions; }
        }

        List<ValidationEventArgs> errors = null;
        void ValidationHandler(object sender, ValidationEventArgs args)
        {
            if (errors == null)
            {
                errors = new List<ValidationEventArgs>();
            }
            errors.Add(args);
        }

        void ReportErrors(string fileName)
        {
            StringBuilder msg = new StringBuilder();
            msg.Append("Could not load Plugin definition file\n " + fileName + "\n. Reason:\n\n");
            foreach (ValidationEventArgs args in errors)
            {
                msg.Append(args.Message);
                msg.Append(Console.Out.NewLine);
            }
            Console.Error.WriteLine(msg.ToString());
        }


        /// <summary>
        /// Initializes this Plugin. It loads the xml definition in file
        /// fileName.
        /// </summary>
        public void Initialize(string fileName)
        {
            this.fileName = fileName;

            XmlDocument doc = new XmlDocument();
            doc.Load(fileName);

            if (errors != null)
            {
                ReportErrors(fileName);
                errors = null;
                return;
            }

            try
            {
                name = doc.DocumentElement.Attributes["name"].InnerText;
                author = doc.DocumentElement.Attributes["author"].InnerText;
                copyright = doc.DocumentElement.Attributes["copyright"].InnerText;
                url = doc.DocumentElement.Attributes["url"].InnerText;
                description = doc.DocumentElement.Attributes["description"].InnerText;
                version = doc.DocumentElement.Attributes["version"].InnerText;
            }
            catch (Exception)
            {
                throw new PluginLoadException("No or malformed 'Plugin' node");
            }

            foreach (object o in doc.DocumentElement.ChildNodes)
            {
                if (o is XmlElement)
                {
                    XmlElement curEl = (XmlElement)o;

                    switch (curEl.Name)
                    {
                        case "Runtime":
                            AddRuntimeLibraries(Path.GetDirectoryName(fileName), curEl);
                            break;
                        case "Extension":
                            AddExtensions(curEl);
                            break;
                    }
                }
            }
        }
        void AddExtensions(XmlElement el)
        {
            if (el.Attributes["path"] == null)
            {
                throw new PluginLoadException("One extension node has no path attribute defined.");
            }
            Extension e = new Extension(el.Attributes["path"].InnerText);
            AddServicesToExtension(e, el);
            extensions.Add(e);
        }

        void AddServicesToExtension(Extension e, XmlElement el)
        {
            foreach (object o in el.ChildNodes)
            {
                if (!(o is XmlElement))
                {
                    continue;
                }

                XmlElement curEl = (XmlElement)o;

                if (curEl.Name.Equals("Module"))
                {
                    IModule module = ModuleManager.Instance.ModuleFactory.CreateModule(this, curEl);


                    AutoInitializeAttributes(module, curEl);

                    e.ModulesCollection.Add(module);
                    if (curEl.ChildNodes.Count > 0)
                    {
                        Extension newExtension = new Extension(e.Path + '/' + module.ID);
                        AddServicesToExtension(newExtension, curEl);
                        extensions.Add(newExtension);
                    }
                }
            }
        }
        void AddRuntimeLibraries(string path, XmlElement el)
        {
            foreach (object o in el.ChildNodes)
            {

                if (!(o is XmlElement))
                {
                    continue;
                }

                XmlElement curEl = (XmlElement)o;

                if (curEl.Attributes["assembly"] == null)
                {
                    throw new PluginLoadException("One import node has no assembly attribute defined.");
                }
                string assemblyName = curEl.Attributes["assembly"].InnerText;
                FileInfo asmFile = new FileInfo(assemblyName);
                Assembly asm = Assembly.LoadFile(asmFile.FullName);
                RuntimeLibraries[assemblyName] = asm;
            }
        }

        /// <summary>
        /// Autoinitialized all fields of the customizer object to the values
        /// in the moduleNode using the XmlMemberAttributeAttribute.
        /// </summary>
        void AutoInitializeAttributes(object customizer, XmlNode moduleNode)
        {
            Type currentType = customizer.GetType();
            while (currentType != typeof(object))
            {
                FieldInfo[] fieldInfoArray = currentType.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);

                foreach (FieldInfo fieldInfo in fieldInfoArray)
                {
                    // process XmlMemberAttributeAttribute attributes
                    XmlMemberAttributeAttribute moduleAttribute = (XmlMemberAttributeAttribute)Attribute.GetCustomAttribute(fieldInfo, typeof(XmlMemberAttributeAttribute));

                    if (moduleAttribute != null)
                    {
                        // get value from xml file
                        XmlNode node = moduleNode.SelectSingleNode("@" + moduleAttribute.Name);

                        if (node != null)
                        {
                            if (fieldInfo.FieldType.IsSubclassOf(typeof(System.Enum)))
                            {
                                fieldInfo.SetValue(customizer, Convert.ChangeType(Enum.Parse(fieldInfo.FieldType, node.Value), fieldInfo.FieldType));
                            }
                            else
                            {
                                PathAttribute pathAttribute = (PathAttribute)Attribute.GetCustomAttribute(fieldInfo, typeof(PathAttribute));
                                if (pathAttribute != null)
                                {
                                    fieldInfo.SetValue(customizer, Path.GetDirectoryName(fileName) + Convert.ChangeType(node.Value, fieldInfo.FieldType).ToString());
                                }
                                else
                                {
                                    fieldInfo.SetValue(customizer, Convert.ChangeType(node.Value, fieldInfo.FieldType));
                                }
                            }
                        }
                        else if (fieldInfo.FieldType.IsClass)
                        {
                            node = moduleNode.SelectSingleNode(moduleAttribute.Name);
                            if (node != null)
                            {
                                object obj = Activator.CreateInstance(fieldInfo.FieldType);
                                AutoInitializeAttributes(obj, node);
                                fieldInfo.SetValue(customizer, obj);
                            }
                        }
                        else
                        {
                            // get value from a xml element
                            XmlNode nodeElem = moduleNode.SelectSingleNode(moduleAttribute.Name);
                            if (nodeElem != null)
                            {
                                if (fieldInfo.FieldType.IsSubclassOf(typeof(System.Enum)))
                                {
                                    fieldInfo.SetValue(customizer, Convert.ChangeType(Enum.Parse(fieldInfo.FieldType, nodeElem.InnerText), fieldInfo.FieldType));
                                }
                                else
                                {
                                    PathAttribute pathAttribute = (PathAttribute)Attribute.GetCustomAttribute(fieldInfo, typeof(PathAttribute));
                                    if (pathAttribute != null)
                                    {
                                        fieldInfo.SetValue(customizer, Path.GetDirectoryName(fileName) + Convert.ChangeType(nodeElem.InnerText, fieldInfo.FieldType).ToString());
                                    }
                                    else
                                    {
                                        fieldInfo.SetValue(customizer, Convert.ChangeType(nodeElem.InnerText, fieldInfo.FieldType));
                                    }
                                }
                            }
                        }

                        // check if its required
                        if (node == null && moduleAttribute.IsRequired)
                        {
                            throw new PluginLoadException(String.Format("{0} is a required attribute for node '{1}' ", moduleAttribute.Name, moduleNode.Name));
                        }

                    }

                    // process XmlMemberAttributeAttribute attributes
                    XmlMemberArrayAttribute moduleArrayAttribute = (XmlMemberArrayAttribute)Attribute.GetCustomAttribute(fieldInfo, typeof(XmlMemberArrayAttribute));
                    if (moduleArrayAttribute != null)
                    {
                        // get value from xml file
                        XmlNode node = moduleNode.SelectSingleNode("@" + moduleArrayAttribute.Name);

                        // check if its required
                        if (node == null && moduleArrayAttribute.IsRequired)
                        {
                            throw new ApplicationException(String.Format("{0} is a required attribute.", moduleArrayAttribute.Name));
                        }

                        if (node != null)
                        {
                            string[] attrArray = node.Value.Split(moduleArrayAttribute.Separator);
                            // TODO : convert array types (currently only string arrays are supported)
                            fieldInfo.SetValue(customizer, attrArray);
                        }
                    }

                    // process XmlMemberAttributeAttribute attributes
                    XmlMemberGenericListAttribute moduleListAttribute = (XmlMemberGenericListAttribute)Attribute.GetCustomAttribute(fieldInfo, typeof(XmlMemberGenericListAttribute));
                    if (moduleListAttribute != null && fieldInfo.FieldType.IsGenericType)
                    {
                        XmlNode parentNode;
                        if (string.IsNullOrEmpty(moduleListAttribute.Name))
                            parentNode = moduleNode;
                        else
                            parentNode = moduleNode.SelectSingleNode(moduleListAttribute.Name);

                        // check if its required
                        if (parentNode == null && moduleListAttribute.IsRequired)
                        {
                            throw new ApplicationException(String.Format("{0} is a required attribute.", moduleListAttribute.Name));
                        }

                        Type[] tt = fieldInfo.FieldType.GetGenericArguments(); ;
                        MethodInfo method = fieldInfo.FieldType.GetMethod("Add");
                        object obj = Activator.CreateInstance(fieldInfo.FieldType);
                        fieldInfo.SetValue(customizer, obj);

                        // get value from xml file
                        XmlNodeList nodeList = parentNode.SelectNodes(moduleListAttribute.EntryName);

                        foreach (XmlNode node in nodeList)
                        {
                            object item = Activator.CreateInstance(tt[0]);
                            AutoInitializeAttributes(item, node);
                            method.Invoke(obj, new object[] { item });
                        }
                    }

                }
                currentType = currentType.BaseType;
            }
        }


        /// <summary>
        /// Creates an object which is related to this plugin.
        /// </summary>
        /// <exception cref="TypeNotFoundException">
        /// If className could not be created.
        /// </exception>
        public object CreateObject(string className)
        {
            object newInstance;
            newInstance = Assembly.GetExecutingAssembly().CreateInstance(className);

            if (newInstance == null)
            {
                foreach (Assembly assembly in runtimeLibraries.Values)
                {
                    newInstance = assembly.CreateInstance(className);
                    if (newInstance != null)
                    {
                        break;
                    }
                }
            }

            if (newInstance == null)
            {
                //MessageBox.Show("Type not found: " + className + ". Please check : " + fileName, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
            }
            return newInstance;
        }
    }
}
