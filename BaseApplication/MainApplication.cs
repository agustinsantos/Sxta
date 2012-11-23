using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Nini.Config;

// Import log4net classes.
using log4net;

using Sxta.Rti1516.Channels;

namespace Sxta.Rti1516.BaseApplication
{
    /// <summary>
    /// This Class is the Core main class, it starts the program.
    /// </summary>
    public class MainApplication
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


        public MainApplication(string[] args)
        {
            commandLineArgs = args;
            argvSource = new ArgvConfigSource(args);
            SetSwitches();

            if (!ProcessArgs(args))
                return;
        }

        /// <summary> The sxta default config file, stored as a XML.</summary>
        private const System.String SXTA_DEFAULT_CONFIG = "./SxtaConfig.xml";

        string[] commandLineArgs = null;
        protected ArgvConfigSource argvSource;
        string configPath = null;
        protected const string configName = "Sxta";
        IConfigSource source;

        public IConfigSource ConfigSource
        {
            get { return source; }
        }

        public string[] CommandLineArgs
        {
            get
            {
                return commandLineArgs;
            }
        }

        public bool IsListener
        {
            get
            {
                return bool.Parse(source.Configs["Channels"].Get("TcpEnable", "false")) ||
                       bool.Parse(source.Configs["Channels"].Get("UdpEnable", "false"));
            }
        }


        public IList<ConnectionInfo> GetTCPListenerInfoList()
        {
            IList<ConnectionInfo> list = new List<ConnectionInfo>();
            ConnectionInfo item = new ConnectionInfo();
            item.Addr = source.Configs["Channels"].Get("DefaultAddr");
            item.Port = source.Configs["Channels"].GetInt("TcpPort");
            list.Add(item);
            return list;
        }

        public IList<ConnectionInfo> UDPChannelsInfoList
        {
            get
            {
                IList<ConnectionInfo> list = new List<ConnectionInfo>();
                ConnectionInfo item = new ConnectionInfo();
                item.Addr = source.Configs["Channels"].Get("DefaultAddr");
                item.Port = source.Configs["Channels"].GetInt("UdpPort");
                list.Add(item);
                return list;
            }
        }

        public string[] BestEffortConnectionList
        {
            get
            {
                string result = source.Configs["RendezVous"].Get("BestEffortAddrs");
                if (string.IsNullOrEmpty(result))
                    return new string[0];
                else
                    return result.Split('|');
            }
        }

        public string[] ReliableConnectionList
        {
            get
            {
                string result = source.Configs["RendezVous"].Get("ReliableAddrs");
                if (string.IsNullOrEmpty(result))
                    return new string[0];
                else
                    return result.Split('|');
            }
        }

        /// <summary>
        /// Processes all arguments.
        /// </summary>
        public bool ProcessArgs(String[] args)
        {
            int length = argvSource.GetArguments().Length;

            if (argvSource.Configs[configName].Get("help") != null)
            {
                PrintUsage();
                return false;
            }

            configPath = ConfigFilePath();

            string configFile = argvSource.Configs[configName].Get("config", "SxtaConfig.xml");
            bool showDialog = bool.Parse(argvSource.Configs[configName].Get("dialog", "true"));

            Configurator config = new Configurator(configFile, showDialog);

            source = config.ConfigurationSource;

            // We set reconf in advance, just in case something screws up, in
            // which case we want the configurator to show-up by default.
            //config.SetReconf();

            // Clear the reconf flag since things seem to be working.
            //config.ClearReconf();

            string logFile = argvSource.Configs[configName].Get("log");
            if (!string.IsNullOrEmpty(logFile) && File.Exists(logFile))
            {
                // Log4Net is configured using a DOMConfigurator.
                log4net.Config.XmlConfigurator.Configure(new System.IO.FileInfo(logFile));
            }
            else
            {
                System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();

                string tmp = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Namespace;
                Stream configStream = asm.GetManifestResourceStream(tmp + "." + "SxtaLog4NetConfig.xml");
                if (configStream == null)
                {
                    // Set up a simple configuration that logs on the console.
                    log4net.Config.BasicConfigurator.Configure();
                }
                else
                {
                    log4net.Config.XmlConfigurator.Configure(configStream);
                }
            }

            return true;
        }

        /// <summary>
        /// Prints out the usage for the program.
        /// </summary>
        protected virtual void PrintUsage()
        {
            Console.WriteLine("Usage: SxtaCore [-options]");
            Console.WriteLine("");
            Console.WriteLine("General Options:");
            Console.WriteLine("  -h,  --help                     Shows this help");
            Console.WriteLine("  -V,  --version                  Displays the application version");
            Console.WriteLine("  -c,  --config=CONFIG_FILE       Selects a Sxta config file");
            Console.WriteLine("  -c,  --config=CONFIG_FILE       Selects a Sxta config file");
            Console.WriteLine("  -l,  --log=LOG4NETCONFIG_FILE   Selects a Log4Net config file");
            Console.WriteLine("  -d,  --dialog                   Show a configuration dialog");
            Console.WriteLine("");
        }

        /// <summary>
        /// Returns the config file (input) path.
        /// </summary>
        protected virtual string ConfigFilePath()
        {
            return argvSource.Configs[configName].Get("config", System.IO.Path.GetFullPath(SXTA_DEFAULT_CONFIG));
        }


        /// <summary>
        /// Sets the switches for the application.
        /// </summary>
        protected virtual void SetSwitches()
        {
            // Application switches
            argvSource.AddSwitch(configName, "help", "h");
            argvSource.AddSwitch(configName, "version", "V");
            argvSource.AddSwitch(configName, "config", "c");
            argvSource.AddSwitch(configName, "log", "l");
            argvSource.AddSwitch(configName, "dialog", "d");
            argvSource.AddSwitch(configName, "IsServer", "s");
        }
    }
}
