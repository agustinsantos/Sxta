using System;
using System.Collections.Generic;
using System.Text;

// Import log4net classes.
using log4net;

using Hla.Rti1516;

namespace Sxta.Rti1516.NOSIRVE
{
    #if IS_NOT_USES
    using ProxyAmbassador = Sxta.Rti1516.Impl.XrtiFederateAmbassador; //TODO

    public class MetaFederationExecutiveListener : IMetaFederationObjectModelInteractionListener
    {
        public MetaFederationExecutiveListener(RTIExecutive executive)
        {
            myExecutive = executive;
        }

        #region IMetaFederationObjectModelInteractionListener Members

        /// <summary> 
        /// Creates a new federation execution.
        /// </summary>
        void IMetaFederationObjectModelInteractionListener.OnReceiveHLAcreateFederationExecution(HLAcreateFederationExecutionMessage msg)
        {
            lock (this)
            {
                if (log.IsDebugEnabled)
                    log.Debug("Received create federation =  " + msg.ToString());

                if (!federationsMap.ContainsKey(msg.FederationExecutionName))
                {
                    //HLAfederationExecution federation = new HLAfederationExecution();
                    //federation.FederationName = msg.FederationExecutionName;
                    //federationsMap.Add(msg.FederationExecutionName, federation);

                    //HLAcontinueMessage continueMsg = new HLAcontinueMessage();
                    //myExecutive.SendToClients(continueMsg);
                }
            }
        }

        void IMetaFederationObjectModelInteractionListener.OnReceiveHLAdestroyFederationExecution(HLAdestroyFederationExecutionMessage msg)
        {
            if (log.IsDebugEnabled)
                log.Debug("Received destroy federation =  " + msg.ToString());
            //throw new Exception("The method or operation is not implemented.");

            //HLAcontinueMessage continueMsg = new HLAcontinueMessage();
            //myExecutive.SendToClients(continueMsg);
        }

        void IMetaFederationObjectModelInteractionListener.OnReceiveHLAjoinFederationExecution(HLAjoinFederationExecutionMessage msg)
        {
            lock (this)
            {
                if (log.IsDebugEnabled)
                    log.Debug("Received Join federation =  " + msg.ToString());
                //throw new Exception("The method or operation is not implemented.");

                //HLAcontinueMessage continueMsg = new HLAcontinueMessage();
                //myExecutive.SendToClients(continueMsg);
            }
        }

        #endregion

        #region IInteractionListener Members

        void Sxta.Rti1516.XrtiUtils.IInteractionListener.ReceiveInteraction(Sxta.Rti1516.Serializers.XrtiEncoding.BaseInteractionMessage msg)
        {
            if (log.IsDebugEnabled)
                log.Debug("Received Message =  " + msg.ToString());
        }

        #endregion

        #region protected and private fields

        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected IDictionary<string, HLAfederationExecution> federationsMap = new Dictionary<string, HLAfederationExecution>();

        protected RTIExecutive myExecutive;
        #endregion
    }
    #endif
}
