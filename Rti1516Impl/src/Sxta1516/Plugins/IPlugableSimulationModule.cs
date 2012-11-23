namespace Sxta.Core.Plugins
{
    using System;
    using System.Collections.Generic;
    public class ModuleFomEntry
    {
        [XmlMemberAttribute("From", IsRequired = true)]
        string from = null;

        [XmlMemberAttribute("Uri")]
        string uri = null;

        public string From
        {
            get { return from; }
            set { from = value; }
        }

        public string Uri
        {
            get { return uri; }
            set { uri = value; }
        }
    }

    /// <summary>
    /// <code><TcpChannel Name="AChannelReliable" Address="localhost" Port="7777"/></code>
    /// </summary>
    public class TcpChannelEntry
    {
        [XmlMemberAttribute("Name", IsRequired = true)]
        string name = null;

        [XmlMemberAttribute("Address")]
        string address = "localhost";

        [XmlMemberAttribute("Port")]
        int port = 0;


        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        public int Port
        {
            get { return port; }
            set { port = value; }
        }
    }

    /// <summary>
    /// <code><UdpChannel Name="AChannelReliable" Address="localhost" Port="7777"/></code>
    /// </summary>
    public class UdpChannelEntry
    {
        [XmlMemberAttribute("Name", IsRequired = true)]
        string name = null;

        [XmlMemberAttribute("Address")]
        string address = "localhost";

        [XmlMemberAttribute("Port")]
        int port = 0;


        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        public int Port
        {
            get { return port; }
            set { port = value; }
        }
    }

    /// <summary>
    /// <code><MultiChannel Name="AChannelReliable" Address="localhost" Port="7777"/></code>
    /// </summary>
    public class MultiChannelEntry
    {
        [XmlMemberAttribute("Name", IsRequired = true)]
        string name = null;

        [XmlMemberAttribute("Address")]
        string address = "localhost";

        [XmlMemberAttribute("Port")]
        int port = 0;


        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        public int Port
        {
            get { return port; }
            set { port = value; }
        }
    }
    /// <summary>
    /// <code><Transport Name="AChannelReliable" Address="localhost" Port="7777"/></code>
    /// </summary>
    public class TransportEntry
    {
        [XmlMemberAttribute("Name", IsRequired = true)]
        string name = null;

        [XmlMemberAttribute("Use", IsRequired = true)]
        string use = null;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Use
        {
            get { return use; }
            set { use = value; }
        }
    }


    public class ChannelsConfiguration
    {
        [XmlMemberGenericListAttribute("", "TcpChannel")]
        protected List<TcpChannelEntry> tcpChannels;

        [XmlMemberGenericListAttribute("", "UdpChannel")]
        protected List<UdpChannelEntry> udpChannels;
        
        [XmlMemberGenericListAttribute("", "UdpChannel")]
        protected List<MultiChannelEntry> multiChannels;

        [XmlMemberGenericListAttribute("Mapping", "Transport")]
        protected List<TransportEntry> transportsMapping;
    }

    /// <summary>
    /// This interface must be implemented by all modules.
    /// </summary>
    public interface IPluggableSimulationModule : IPluggableModule
    {
        List<ModuleFomEntry> FomList
        {
            get;
        }

        ChannelsConfiguration ChannelsInfo
        {
            get;
        }
    }
}
