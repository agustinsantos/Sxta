using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

// Import log4net classes.
using log4net;

using Hla.Rti1516;

using Sxta.Rti1516.Interactions;
using Sxta.Rti1516.Ambassadors;
using Sxta.Rti1516.Reflection;
using Sxta.Rti1516.BoostrapProtocol;
using Sxta.Rti1516.XrtiHandles;

namespace Sxta.Rti1516.Management
{
    public class ManagementObjectModelInteractionListener : AbstractManagementObjectModelInteractionListener
    {
        protected XrtiExecutiveAmbassador parent;

        protected static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public ManagementObjectModelInteractionListener(XrtiExecutiveAmbassador p, String aName)
            : base(aName)
        {
            parent = p;
        }

        protected void SendInfoAboutObjects(IList<HLAobjectRoot> objects, HLAsubscribeObjectClassAttributesMessage msg)
        {
            foreach (HLAobjectRoot obj in objects)
            {
                // if it is a local object and its class handle is equal to the object class handle sent
                if (obj.HLAprivilegeToDeleteObject && obj.ClassHandle.Equals(msg.HLAobjectClass))
                {
                    parent.RegisterObjectInstance(obj);

                    IList<HLAattributeHandleValuePair> propertyValuePair = new List<HLAattributeHandleValuePair>();

                    // TODO ANGEL: Esto es incorrecto y esta mal. Puede haber propiedades que no sean remotas
                    foreach (IAttributeHandle attribute in msg.HLAattributeList)
                    {
                        AttributeDescriptor ad = parent.descriptorManager.GetAttributeDescriptor(attribute);
                        if (ad == null)
                        {
                            throw new InvalidAttributeHandle(attribute.ToString());
                        }
                        else
                        {
                            PropertyInfo pi = obj.GetType().BaseType.GetProperty(ad.Name);

                            if (!ad.Name.Equals("HLAprivilegeToDeleteObject") && pi != null)
                            {
                                object value = pi.GetValue(obj, null);

                                if (value != null)
                                {
                                    propertyValuePair.Add(new HLAattributeHandleValuePair(((XRTIAttributeHandle)ad.Handle).Identifier, value));
                                }
                            }
                        }
                    }

                    HLAattributeHandleValuePair[] propertyValuePairArray = new HLAattributeHandleValuePair[propertyValuePair.Count];
                    propertyValuePair.CopyTo(propertyValuePairArray, 0);

                    // TODO ANGEL: Que pasa con el userTag
                    parent.UpdateAttributeValues(obj.InstanceHandle, propertyValuePairArray, null);
                }
            }
        }

        # region IManagementObjectModelInteractionListener Members

        public override void OnReceiveHLApublishInteractionClass(HLApublishInteractionClassMessage msg)
        {
            if (log.IsDebugEnabled)
                log.Debug("[" + federationName + "] Received HLApublishInteractionClass =  " + msg.ToString());
        }

        public override void OnReceiveHLApublishObjectClassAttributes(HLApublishObjectClassAttributesMessage msg)
        {
            if (log.IsDebugEnabled)
                log.Debug("[" + federationName + "] Received HLApublishObjectClassAttributes =  " + msg.ToString());
        }

        public override void OnReceiveHLAsubscribeInteractionClass(HLAsubscribeInteractionClassMessage msg)
        {
            if (log.IsDebugEnabled)
                log.Debug("[" + federationName + "] Received HLAsubscribeInteractionClass =  " + msg.ToString());
        }

        public override void OnReceiveHLAsubscribeObjectClassAttributes(HLAsubscribeObjectClassAttributesMessage msg)
        {
            if (log.IsDebugEnabled)
                log.Debug("[" + federationName + "] Received HLAsubscribeObjectClassAttributes =  " + msg.ToString());
        }

        # endregion

        #region IInteractionListener Members

        public override void ReceiveInteraction(BaseInteractionMessage msg)
        {
            if (log.IsDebugEnabled)
                log.Debug("Received BaseInteractionMessage =  " + msg.ToString());
        }

        #endregion
    }

    public class FederationManagementObjectModelInteractionListener : ManagementObjectModelInteractionListener
    {

        public FederationManagementObjectModelInteractionListener(XrtiExecutiveAmbassador p, String aName)
            : base(p, aName)
        {
        }

        # region IManagementObjectModelInteractionListener Members

        public override void OnReceiveHLAsubscribeObjectClassAttributes(HLAsubscribeObjectClassAttributesMessage msg)
        {
            base.OnReceiveHLAsubscribeObjectClassAttributes(msg);

            // TODO ANGEL: Habría que registrar al federado con la info en la que está interesado

            IList<HLAobjectRoot> objects = ((FederateAmbassador)parent.FederateAmbassador).GetObjects();

            this.SendInfoAboutObjects(objects, msg);
        }

        # endregion
    }

    public class MetaFederationManagementObjectModelInteractionListener : ManagementObjectModelInteractionListener
    {

        public MetaFederationManagementObjectModelInteractionListener(XrtiExecutiveAmbassador p, String aName)
            : base(p, aName)
        {
        }

        # region IManagementObjectModelInteractionListener Members

        public override void OnReceiveHLAsubscribeObjectClassAttributes(HLAsubscribeObjectClassAttributesMessage msg)
        {
            base.OnReceiveHLAsubscribeObjectClassAttributes(msg);

            // TODO ANGEL: Habría que registrar al federado con la info en la que está interesado

            IList<HLAobjectRoot> objects = ((FederateAmbassador)parent.MetaFederateAmbassador).GetObjects();

            this.SendInfoAboutObjects(objects, msg);
        }

        # endregion
    }
}
