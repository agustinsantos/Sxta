namespace Sxta.Rti1516.BaseApplication
{

    using System;
    using System.Collections;
    using System.IO;
    using System.Net;
    using System.Xml;

    // Import log4net classes.
    using log4net;

    using Nini.Config;

    /// <summary>
    /// Summary description for Configurator.
    /// </summary>
    public class Configurator
    {
        /// <summary>
        /// Define a static logger variable so that it references the
        ///	Logger instance.
        /// </summary>
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static string configName = "SxtaConfig.xml";
        private const String SXTA_HOME = ".sxta/";

        IConfigSource source;

        public IConfigSource ConfigurationSource
        {
            get { return source; }
        }

        /// <summary>
        /// Constructor for the Configurator object
        /// </summary>
        public Configurator(string configFile, bool showDialog)
        {
            if (!string.IsNullOrEmpty(configFile))
                configName = configFile;

            //make sure the JXTA_HOME directory is present.  Defensive programming
            //ot make sure we dont' go forward while our directories aren't setup.
            Directory.CreateDirectory(SXTA_HOME);

            Load();
            //if (source == null || IsReconf())
            if (showDialog)
            {
                try
                {
                    ConfigDialog dialog = new ConfigDialog(ref source);

                    dialog.UntilDone();
                }
                catch (Exception t)
                {
                    if (log.IsWarnEnabled)
                    {
                        log.Warn("Could not initialize graphical config dialog" + t.Message);

                        log.Warn("The window-based configurator does not seem to be usable.");
                        log.Warn("Try to edit the current configuration by hand");
                    }
                }
            }

            // Save that config.
            Save();
        }



        /// <summary>
        /// Save the configuration in the standard config file.
        /// </summary>
        public void Save()
        {
            SaveTo(configName);
        }

        /// <summary>
        /// Saves the peer configuration.
        /// </summary>
        /// <param name="fileName">The name of the file where to save it.</param>
        private void SaveTo(String fileName)
        {

            // Save the configuration as input for future reconfiguration
            try
            {
                using (FileStream oStream = new FileStream(fileName, FileMode.Create))
                {
                    XmlConfigSource source2 = new XmlConfigSource();
                    source2.Merge(source);
                    source2.Save(oStream);
                }
            }
            catch (Exception e)
            {
                if (log.IsWarnEnabled)
                {
                    log.Warn(e);
                    log.Warn("Could not save to " + fileName);
                }
            }
        }


        /// <summary>
        /// Load the peer information from the standard config file.
        /// </summary>
        public void Load()
        {
            LoadFrom(configName);
        }


        /// <summary>
        /// Loads the Peer configuration from the named file.
        /// </summary>
        /// <param name="fileName"> Name of the file where the configuration is stored</param>
        private void LoadFrom(String fileName)
        {
            if (log.IsDebugEnabled)
                log.Debug("Loading from :" + fileName);
            if (fileName != null)
            {
                if (!File.Exists(fileName))
                {
                    System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();

                    string tmp = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Namespace;
                    Stream configStream = asm.GetManifestResourceStream(tmp + "." + fileName);

                    if (configStream == null)
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
                        throw new Exception("Config file does not exist: " + fileName + "; Process path : " + Directory.GetCurrentDirectory());
                    }

                    System.Xml.XmlReader xmlReader = new System.Xml.XmlTextReader(configStream);
                    source = new XmlConfigSource(xmlReader);
                }
                else
                {

                    FileStream configStream = null;
                    try
                    {
                        configStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);

                        System.Xml.XmlReader xmlReader = new System.Xml.XmlTextReader(configStream);
                        source = new XmlConfigSource(xmlReader);

                        if (log.IsDebugEnabled)
                            log.Debug("Recovered " + fileName);
                    }
                    catch (Exception e)
                    {
                        if (log.IsWarnEnabled)
                        {
                            log.Warn(e);
                            log.Warn("Error Loading  " + fileName);
                        } try
                        {
                            if (configStream != null)
                            {
                                configStream.Close();
                            }

                            // delete that bad file.
                            File.Delete(fileName);
                        }
                        catch (DirectoryNotFoundException notFound)
                        {
                            if (log.IsWarnEnabled)
                                log.Warn("File not Found: " + notFound.Message);
                        }
                        catch (Exception ex1)
                        {
                            if (log.IsWarnEnabled)
                            {
                                log.Warn("Could not remove " + fileName + ". Removed it by hand before retrying. ");
                                log.Warn("Reason: " + ex1.Message);
                            }
                            throw new Exception("Could not remove " + fileName + ". Removed it by hand before retrying");
                        }
                    }
                    try
                    {
                        if (configStream != null)
                        {
                            configStream.Close();
                        }
                    }
                    catch (Exception e)
                    {
                        if (log.IsWarnEnabled)
                            log.Warn(e);
                    }
                }
            }
        }


        /// <summary>
        /// Check if a manual reconf has been forced.
        /// </summary>
        /// <returns>true if a manual reconf is being forced.</returns>
        public bool IsReconf()
        {
            try
            {
                bool forceReconfig = File.Exists(SXTA_HOME + "reconf");

                if (forceReconfig)
                    if (log.IsDebugEnabled)
                        log.Debug("Reconfig requested - 'reconf' file found");

                return forceReconfig;
            }
            catch (Exception ex1)
            {
                if (log.IsWarnEnabled)
                {
                    log.Warn("Could not check reconfig flag. Reason:" + ex1.Message);
                    log.Warn("Assuming it exists.");
                }
                return true;
            }
        }

        /// <summary>
        /// Forces a manual reconf the next time the configurator is invoked.
        /// </summary>
        public void SetReconf()
        {
            try
            {
                File.CreateText(SXTA_HOME + "reconf").Close();
            }
            catch (Exception ex1)
            {
                if (log.IsWarnEnabled)
                {
                    log.Warn("Could not create reconfig flag." + ex1.Message);
                    log.Warn("Create the file reconfig by hand before retrying.");
                }
            }
        }

        /// <summary>
        /// No manual reconf the next time the configurator is invoked.
        /// </summary>
        public void ClearReconf()
        {
            try
            {
                File.Delete(SXTA_HOME + "reconf");
            }
            catch (Exception ex1)
            {
                if (log.IsWarnEnabled)
                {
                    log.Warn("Could not remove reconfig flag. " + ex1.Message);
                    log.Warn("Delete the file reconfig by hand before retrying.");
                }
            }
        }


    }
}
