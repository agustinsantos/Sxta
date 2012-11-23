namespace Sxta.Rti1516.BoostrapProtocol
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.IO;

    // Import log4net classes.
    using log4net;

    using Hla.Rti1516;
    using Sxta.Rti1516.Serializers.XrtiEncoding;
    using Sxta.Rti1516.Reflection;
    using Sxta.Rti1516.Interactions;
    using Sxta.Rti1516.Channels;
    using Sxta.Rti1516.XrtiHandles;

    public partial class InteractionManager
    {
        public delegate void ReceiveInteractionDelegate(BaseInteractionMessage msg);

         /// <summary>
        /// The list of interaction listeners.
        /// </summary>
        protected internal List<IInteractionListener> interactionListeners = new List<IInteractionListener>();

        /// <summary>
        /// The list of interaction ambassadors.
        /// </summary>
        private Dictionary<Type, ReceiveInteractionDelegate> interactionDelegates = new Dictionary<Type, ReceiveInteractionDelegate>();
        private IDictionary<Type, TransportationType> interactionChannelMap = new Dictionary<Type, TransportationType>();
        public Dictionary<Type, InteractionClassDescriptor> interactionClassDescriptorMap = new Dictionary<Type, InteractionClassDescriptor>();

        /// <summary> 
        /// The name of the federation execution to which the federate is joined (or
        /// <code>null</code> for none).
        /// </summary>
        protected System.String internalName = null;

        public string InternalName
        {
            get { return internalName; }
            set { internalName = value; }
        }

        public List<IInteractionListener> InteractionListeners
        {
            get { return interactionListeners; }
        }


        /// <summary>Adds a listener for the interactions.</summary>
        /// <param name="il"> the listener object to Add</param>
        public virtual void AddInteractionListener(IInteractionListener il)
        {
            lock (interactionListeners)
            {
                interactionListeners.Add(il);
            }
        }

        /// <summary>Removes a listener for the interactions.</summary>
        /// <param name="il"> the listener object to Remove</param>
        public virtual void RemoveInteractionListener(IInteractionListener il)
        {
            lock (interactionListeners)
            {
                interactionListeners.Remove(il);
            }
        }

        /// <summary>Adds a delegate for the process of interactions.</summary>
        /// <param name="il"> the listener object to Add</param>
        public void AddReceiveInteractionDelegate(Type msgType, string name, ReceiveInteractionDelegate interactionDelegate)
        {
            InteractionClassDescriptor icd = DescriptorManager.GetInteractionClassDescriptor(name);

            interactionClassDescriptorMap[msgType] = icd;
            interactionDelegates[msgType] = interactionDelegate;

            TransportationType transportation = icd.Transportation;
            if (transportation != null)
            {
                interactionChannelMap[msgType] = transportation;
            }
        }

        /// <summary>Remove a delegate for the process of interactions.</summary>
        /// <param name="il"> the listener object to Add</param>
        public void RemoveReceiveInteractionDelegate(Type msgType, ReceiveInteractionDelegate interactionDelegate)
        {
            if (interactionDelegates.ContainsKey(msgType))
                interactionDelegates.Remove(msgType);
        }

        /// <summary>Notifies the federate of a received interaction.</summary>
        /// <param name="decodedValue"> the message of the received interaction</param>
        public void ReceiveInteraction(BaseInteractionMessage msg)
        {
            if (log.IsDebugEnabled)
                log.Debug("Read msg: " + msg);

            try
            {
                if (interactionDelegates.ContainsKey(msg.GetType()))
                {
                    interactionDelegates[msg.GetType()](msg);
                }
                //else
                //{
                    lock (interactionListeners)
                    {
                        foreach (IInteractionListener il in interactionListeners)
                        {
                            (il as IInteractionListener).ReceiveInteraction(msg);
                        }
                    }
                //}
            }
            catch (IOException ioe)
            {
                throw new FederateInternalError(ioe.ToString());
            }
        }

        /// <summary> 
        /// Sends an interaction.
        /// </summary>
        /// <param name="decodedValue">the message or interaction to send
        /// </param>
        public virtual void SendInteraction(BaseInteractionMessage msg)
        {
            TransportationType transport = interactionChannelMap[msg.GetType()];

            // If transport is null, then we send it directly to 
            // our listeners
            if (transport == null)
            {
                ReceiveInteraction(msg);
            }
            else
            {
                //msg.InteractionClassHandle = ((XRTIInteractionClassHandle)interactionClassDescriptorMap[msg.GetType()].Handle).Identifier;
                msg.InteractionClassHandle = SerializerManager.GetHandle(msg.GetType());

                SendInteraction(transport, msg);
            }
        }

    }
}
