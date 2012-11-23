using System;
using System.Collections.Generic;
using System.Text;

using Hla.Rti1516;
using Sxta.Rti1516.BoostrapProtocol;
using Sxta.Rti1516.Management;
using Sxta.Rti1516.XrtiHandles;
using Sxta.Rti1516.Reflection;

namespace Sxta.Rti1516.Ambassadors
{
    public class MetaFederateAmbassador : FederateAmbassador
    {
        public MetaFederateAmbassador(IRTIambassador prtiAmbassador)
            : base(prtiAmbassador)
        {
        }

        /*
        protected ILogicalTime ConvertToFederationLogicalTimeRepresentation(ILogicalTime time)
        {
            ILogicalTimeFactory factory = rti.LogicalTimeFactory;

            byte[] timeByteArray = new byte[time.EncodedLength()];
            time.Encode(timeByteArray, 0);

            return factory.Decode(timeByteArray, 0);
        }

        protected ILogicalTimeInterval ConvertToFederationLogicalTimeIntervalRepresentation(ILogicalTimeInterval timeInterval)
        {
            ILogicalTimeIntervalFactory factory = rti.LogicalTimeIntervalFactory;

            byte[] timeIntervalByteArray = new byte[timeInterval.EncodedLength()];
            timeInterval.Encode(timeIntervalByteArray, 0);

            return factory.Decode(timeIntervalByteArray, 0);
        }

        protected void ConvertFederateTimeProperties(Sxtafederate federate)
        {
            if (federate.HLAlogicalTime != null)
                federate.HLAlogicalTime = ConvertToFederationLogicalTimeRepresentation(federate.HLAlogicalTime);
            if (federate.HLALITS != null)
                federate.HLALITS = ConvertToFederationLogicalTimeRepresentation(federate.HLALITS);
            if (federate.HLApendingTime != null)
                federate.HLApendingTime = ConvertToFederationLogicalTimeRepresentation(federate.HLApendingTime);
            if (federate.HLAlookahead != null)
                federate.HLAlookahead = ConvertToFederationLogicalTimeIntervalRepresentation(federate.HLAlookahead);
        }
        */
         
        public IList<HLAfederation> GetFederations()
        {
            //lock (this) TODO ANGEL: LOCK DUDOSO
            //{
                IList<HLAfederation> listFederations = new List<HLAfederation>();
                foreach (object obj in objectInstanceHandleProxyMap.Values)
                {
                    if (obj is HLAfederation)
                        listFederations.Add((HLAfederation)obj);
                }

                return listFederations;
            //}
        }

        public HLAfederation GetFederation(String federationName)
        {
            //lock (this) TODO ANGEL: LOCK DUDOSO
            //{
                foreach (object obj in objectInstanceHandleProxyMap.Values)
                {
                    if (obj is HLAfederation && ((HLAfederation)obj).HLAfederationName.Equals(federationName))
                        return (HLAfederation)obj;
                }

                return null;
            //}
        }

        public IList<Sxtafederate> GetFederates()
        {
            //lock (this) TODO ANGEL: LOCK DUDOSO
            //{
                IList<Sxtafederate> listFederates = new List<Sxtafederate>();
                foreach (object obj in objectInstanceHandleProxyMap.Values)
                {
                    if (obj is Sxtafederate)
                    {
                        Sxtafederate federate = (Sxtafederate)obj;

                        listFederates.Add(federate);
                    }
                }

                return listFederates;
            //}
        }

        // TODO ANGEL: Posiblemente interese crear una estructura de datos más compleja
        //             para guardar los federados clasificados por la federación a la que pertenecen
        public IList<Sxtafederate> GetFederates(String federationName)
        {
            //lock (this) TODO ANGEL: LOCK DUDOSO
            //{
                IList<Sxtafederate> listFederates = new List<Sxtafederate>();
                foreach (object obj in objectInstanceHandleProxyMap.Values)
                {
                    if (obj is Sxtafederate)
                    {
                        Sxtafederate federate = (Sxtafederate)obj;

                        if (federate.HLAfederationNameJoined.Equals(federationName))
                        {
                            //ConvertFederateTimeProperties(federate);

                            listFederates.Add(federate);
                        }
                    }
                }

                return listFederates;
            //}
        }

        public IList<Sxtafederate> GetConstrainedFederates(String federationName)
        {
            //lock (this) TODO ANGEL: LOCK DUDOSO
            //{
                IList<Sxtafederate> listFederates = new List<Sxtafederate>();
                foreach (object obj in objectInstanceHandleProxyMap.Values)
                {
                    if (obj is Sxtafederate)
                    {
                        Sxtafederate federate = (Sxtafederate)obj;

                        if (federate.HLAtimeConstrained
                            && federate.HLAfederationNameJoined.Equals(federationName))
                        {
                            //ConvertFederateTimeProperties(federate);

                            listFederates.Add(federate);
                        }
                    }
                }

                return listFederates;
            //}
        }

        public override void ReflectAttributeValuesExt(IObjectInstanceHandle theObject, HLAattributeHandleValuePair[] theAttributes, byte[] userSuppliedTag, OrderType sentOrdering, TransportationType theTransport)
        {
            // TODO ANGEL: LOCK DUDOSO
            lock (this)
            {
                base.ReflectAttributeValuesExt(theObject, theAttributes, userSuppliedTag, sentOrdering, theTransport);

                if (objectInstanceHandleProxyMap.ContainsKey(theObject))
                {
                    object instance = objectInstanceHandleProxyMap[theObject];

                    // TODO ANGEL: ¿Esto debería estar aquí o dentro de la funcionalidad del objectManager?
                    //             El problema es que si lo hace objectManager debe poder acceder al federationsMap del rti
                    foreach (HLAattributeHandleValuePair entry in theAttributes)
                    {

                        string attributeName = GetAttributeName(instance, entry.AttributeHandle);

                        // Checks if the object created is an instance of HLAfederation and its properties are modificated
                        if (instance is HLAfederation)
                        {

                            if (attributeName == "HLAfederationName")
                            {
                                rti.federationsMap.Add((string)entry.AttributeValue, instance as HLAfederation);
                            }
                            else if (attributeName == "HLAFDDID")
                            {
                                rti.interactionManager.RegisterHelperClass((string)entry.AttributeValue);
                            }
                        }

                        // Checks if the object created is an instance of HLAfederate and its properties are modificated
                        if (instance is HLAfederate)
                        {
                            if (attributeName == "HLAfederateHandle")
                            {
                                if (instance is Sxtafederate)
                                {
                                    Sxtafederate federate = instance as Sxtafederate;

                                    // TODO ANGEL: OJO! Implica que la propiedad HLAfederationNameJoined se haya recibido antes que ésta
                                    HLAfederateHandle federateHandle = (HLAfederateHandle)entry.AttributeValue;
                                    rti.federationsMap[federate.HLAfederationNameJoined].HLAfederatesinFederation.Add(federateHandle);
                                }
                            }

                            if (attributeName == "HLAfederationNameJoined")
                            {
                                if (instance is Sxtafederate)
                                {
                                    Sxtafederate federate = instance as Sxtafederate;

                                    HLAfederation federation = rti.federationsMap[federate.HLAfederationNameJoined];
                                    federate.Federation = federation;
                                }
                            }

                            /*
                            if (attributeName == "HLAlogicalTime" || attributeName == "HLApendingTime"
                                || attributeName == "HLAlookahead" || attributeName == "HLAGALT" || attributeName == "HLALITS")
                            {
                                //System.Threading.Monitor.Pulse(rti);
                                //rti.PushFederates();
                            }
                            */
                        }
                    }
                }
            }
        }

        private string GetAttributeName(object instance, long attributeHandle)
        {
            IObjectClassHandle whichClass = ((HLAobjectRoot)instance).ClassHandle;
            IAttributeHandle theHandle = new XRTIAttributeHandle(attributeHandle);

            ObjectClassDescriptor ocd = rti.descriptorManager.GetObjectClassDescriptor(whichClass);

            if (ocd == null)
            {
                throw new InvalidObjectClassHandle(whichClass.ToString());
            }
            else
            {
                AttributeDescriptor ad = ocd.GetAttributeDescriptor(theHandle);

                if (ad == null)
                {
                    throw new InvalidAttributeHandle(theHandle.ToString());
                }
                else
                {
                    return ad.Name;
                }
            }
        }
    }

}
