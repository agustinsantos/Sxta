
namespace Tests.Channels
{
    using System;

    using NUnit.Framework;

    // Import log4net classes.
    using log4net;

    using Sxta.Rti1516.BaseApplication;
    using Sxta.Rti1516.Channels;

    /// <summary>
    /// Some simple channel creation and configuration Tests.
    /// </summary>
    [TestFixture]
    public class ChannelCreationTest
    {
        /// <summary>
        /// Define a static logger variable so that it references the
        ///	Logger instance.
        /// </summary>
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private MainApplication app;
        private ChannelsManager channelMngr;

        /// <summary>
        /// Start up the executive and create a RTI ambassador
        /// </summary>
        [SetUp]
        public void Init()
        {
            app = new MainApplication(new string[2] { @"-config=Resources\TestConfig1.xml",
                                                      @"-dialog=false" });
            // Log Debug level message
            if (log.IsDebugEnabled)
                log.Debug("Test " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType + " Start");

            channelMngr = new ChannelsManager();
        }

        [TearDown]
        public void TearDown()
        {
            if (channelMngr != null)
                channelMngr.Close();

            // Log Debug level message
            if (log.IsDebugEnabled)
                log.Debug("Test " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType + " Ends");
        }

        /// <summary>
        /// Test to create and close a simple TCP acceptor
        /// </summary>
        [Test]
        public void Test_1()
        {
            if (log.IsDebugEnabled)
                log.Debug("In Test: " + System.Reflection.MethodBase.GetCurrentMethod());

            ConnectionInfo connection = new ConnectionInfo();
            connection.Addr = "localhost";
            connection.Port = 7777;

            TCPMessageChannelAcceptor channelAcceptor = channelMngr.StartNewListener(connection);

            Assert.AreEqual(channelAcceptor.IsClosed, false);

            channelAcceptor.Close();

            Assert.AreEqual(channelAcceptor.IsClosed, true);
        }

        /// <summary>
        /// Test to create a simple TCP acceptor
        /// </summary>
        [Test]
        public void Test_2()
        {
            if (log.IsDebugEnabled)
                log.Debug("In Test: " + System.Reflection.MethodBase.GetCurrentMethod());

            ConnectionInfo connection = new ConnectionInfo();
            connection.Addr = "localhost";
            connection.Port = 7777;

            TCPMessageChannelAcceptor channelAcceptor = channelMngr.StartNewListener(connection);

            System.Net.IPHostEntry hostEntry = System.Net.Dns.GetHostEntry(connection.Addr);
            System.Net.IPAddress ipAddress = System.Net.Dns.GetHostEntry(hostEntry.HostName).AddressList[0];

            Assert.AreEqual(channelAcceptor.LocalEndPoint.ToString(), ipAddress + ":7777");
        }

        /// <summary>
        /// Test to re-create an existing TCP acceptor
        /// channelMngr.StartNewListener returns a null channelAcceptor
        /// </summary>
        [Test]
        public void Test_3()
        {
            if (log.IsDebugEnabled)
                log.Debug("In Test: " + System.Reflection.MethodBase.GetCurrentMethod());

            ConnectionInfo connection = new ConnectionInfo();
            connection.Addr = "localhost";
            connection.Port = 7777;

            TCPMessageChannelAcceptor channelAcceptor1 = channelMngr.StartNewListener(connection);

            System.Net.IPHostEntry hostEntry = System.Net.Dns.GetHostEntry(connection.Addr);
            System.Net.IPAddress ipAddress = System.Net.Dns.GetHostEntry(hostEntry.HostName).AddressList[0];

            Assert.AreEqual(channelAcceptor1.LocalEndPoint.ToString(), ipAddress + ":7777");

            TCPMessageChannelAcceptor channelAcceptor2 = channelMngr.StartNewListener(connection);

            Assert.AreEqual(channelAcceptor2, null);
        }

        /// <summary>
        /// Test to re-create an TCP acceptor after created and closed
        /// </summary>
        [Test]
        public void Test_4()
        {
            if (log.IsDebugEnabled)
                log.Debug("In Test: " + System.Reflection.MethodBase.GetCurrentMethod());

            ConnectionInfo connection = new ConnectionInfo();
            connection.Addr = "localhost";
            connection.Port = 7777;

            TCPMessageChannelAcceptor channelAcceptor1 = channelMngr.StartNewListener(connection);
            channelMngr.CloseAcceptor(channelAcceptor1);

            TCPMessageChannelAcceptor channelAcceptor2 = channelMngr.StartNewListener(connection);

            System.Net.IPHostEntry hostEntry = System.Net.Dns.GetHostEntry(connection.Addr);
            System.Net.IPAddress ipAddress = System.Net.Dns.GetHostEntry(hostEntry.HostName).AddressList[0];

            Assert.AreEqual(channelAcceptor1.IsClosed, true);
            Assert.AreEqual(channelAcceptor2.IsClosed, false);
            Assert.AreEqual(channelAcceptor2.LocalEndPoint.ToString(), ipAddress + ":7777");

            channelMngr.DumpChannelsInfo();
            channelMngr.CloseAcceptor(channelAcceptor2);
            Assert.AreEqual(channelAcceptor2.IsClosed, true);
            channelMngr.DumpChannelsInfo();
        }

    }
}
