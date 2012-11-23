﻿namespace Sxta.Rti1516.BoostrapProtocol
{
    using System;
    using System.IO;
    using System.Collections.Generic;

    using Hla.Rti1516;
    using Sxta.Rti1516.Channels;
    using Sxta.Rti1516.Serializers.XrtiEncoding;
    using Sxta.Rti1516.Interactions;
    using Sxta.Rti1516.Reflection;
    using Sxta.Rti1516.XrtiHandles;

    ///<summary>
    ///Autogenerated interaction and serializer registration Helper. 
    ///</summary>
    /// <author> Sxta.Rti1516.DynamicCompiler.DynamicCompiler from Bootstrap Object Model </author>
    [HLAinteractionHelperAttribute(Name = "BootstrapObjectModelInteractionHelper",
                                   FomName = "Bootstrap Object Model",
                                   Semantics = "A Listener Manager and serializer manager")]
    public class BootstrapObjectModelInteractionHelper
    {
        InteractionManager manager;

        /// <summary>Constructor.</summary>
        /// <param name="interactionManager"> the run-time interaction manager</param>
        public BootstrapObjectModelInteractionHelper(InteractionManager interactionManager)
        {
            Type objType;
            manager = interactionManager;
            XrtiSerializerManager serializerMngr = manager.SerializerManager;
            long handle;

            objType = typeof(HLAattributeHandleValuePair);
            handle = -10;
            serializerMngr.RegisterSerializer(objType, handle, new HLAattributeHandleValuePairXrtiSerializer(serializerMngr));

            objType = typeof(BaseInteractionMessage);
            manager.AddReceiveInteractionDelegate(objType, "BaseInteraction", new InteractionManager.ReceiveInteractionDelegate(this.ReceiveInteraction));
            handle = ((XRTIInteractionClassHandle)manager.DescriptorManager.GetInteractionClassDescriptor("BaseInteraction").Handle).Identifier;
            serializerMngr.RegisterSerializer(objType, handle, new BaseInteractionMessageXrtiSerializer(serializerMngr));

            objType = typeof(HLAGenericInteractionMessage);
            manager.AddReceiveInteractionDelegate(objType, "HLAGenericInteraction", new InteractionManager.ReceiveInteractionDelegate(this.ReceiveInteraction));
            handle = ((XRTIInteractionClassHandle)manager.DescriptorManager.GetInteractionClassDescriptor("HLAGenericInteraction").Handle).Identifier;
            serializerMngr.RegisterSerializer(objType, handle, new HLAGenericInteractionMessageXrtiSerializer(serializerMngr));
#if POSIBLE_MEJORA
            bootstrapInteractionsDelegates[handle].Add(objType, new InteractionDispatcher.ReceiveInteractionDelegate(this.OnReceiveHLAupdateAttributeValuesReliableBase));
#endif
            objType = typeof(HLAinteractionFragmentMessage);
            manager.AddReceiveInteractionDelegate(objType, "HLAinteractionFragment", new InteractionManager.ReceiveInteractionDelegate(this.ReceiveInteraction));
            handle = ((XRTIInteractionClassHandle)manager.DescriptorManager.GetInteractionClassDescriptor("HLAinteractionFragment").Handle).Identifier;
            serializerMngr.RegisterSerializer(objType, handle, new HLAinteractionFragmentMessageXrtiSerializer(serializerMngr));

            objType = typeof(HLAcontinueMessage);
            manager.AddReceiveInteractionDelegate(objType, "HLAcontinue", new InteractionManager.ReceiveInteractionDelegate(this.ReceiveInteraction));
            handle = ((XRTIInteractionClassHandle)manager.DescriptorManager.GetInteractionClassDescriptor("HLAcontinue").Handle).Identifier;
            serializerMngr.RegisterSerializer(objType, handle, new HLAcontinueMessageXrtiSerializer(serializerMngr));

            objType = typeof(PeerAdvertisementInteractionMessage);
            manager.AddReceiveInteractionDelegate(objType, "PeerAdvertisementInteraction", new InteractionManager.ReceiveInteractionDelegate(this.ReceiveInteraction));
            handle = ((XRTIInteractionClassHandle)manager.DescriptorManager.GetInteractionClassDescriptor("PeerAdvertisementInteraction").Handle).Identifier;
            serializerMngr.RegisterSerializer(objType, handle, new PeerAdvertisementInteractionMessageXrtiSerializer(serializerMngr));
        }

#if POSIBLE_MEJORA
        public void OnReceiveHLAGenericInteraction(BaseInteractionMessage msg)
        {
            foreach (IInteractionListener il in manager.InteractionListeners)
            {
                if (il is IBootstrapObjectModelInteractionListener)
                    (il as IBootstrapObjectModelInteractionListener).OnReceiveHLAGenericInteraction(msg as HLAGenericInteractionMessage);
                else
                    il.ReceiveInteraction(msg);
            }
        }
#endif

        /// <summary>Notifies the listener of a received interaction.</summary>
        /// <param name="msg"> the message of the received interaction</param>
        public void ReceiveInteraction(BaseInteractionMessage msg)
        {
 
#if POSIBLE_MEJORA
           if (bootstrapInteractionsDelegates[msg.FederationExecutionHandle].ContainsKey(msg.GetType()))
                bootstrapInteractionsDelegates[msg.FederationExecutionHandle][msg.GetType()](msg);
            else
                bootstrapInteractionsDelegates[msg.FederationExecutionHandle][typeof(BaseInteractionMessage)](msg);
#endif
            try
            {
                if (msg is HLAGenericInteractionMessage)
                {
                    foreach (IInteractionListener il in manager.InteractionListeners)
                    {
                        if (il is IBootstrapObjectModelInteractionListener)
                            (il as IBootstrapObjectModelInteractionListener).OnReceiveHLAGenericInteraction(msg as HLAGenericInteractionMessage);
                        else
                            il.ReceiveInteraction(msg);
                    }
                }
                else if (msg is HLAinteractionFragmentMessage)
                {
                    foreach (IInteractionListener il in manager.InteractionListeners)
                    {
                        if (il is IBootstrapObjectModelInteractionListener)
                            (il as IBootstrapObjectModelInteractionListener).OnReceiveHLAinteractionFragment(msg as HLAinteractionFragmentMessage);
                        else
                            il.ReceiveInteraction(msg);
                    }
                }
                else if (msg is HLAcontinueMessage)
                {
                    foreach (IInteractionListener il in manager.InteractionListeners)
                    {
                        if (il is IBootstrapObjectModelInteractionListener)
                            (il as IBootstrapObjectModelInteractionListener).OnReceiveHLAcontinue(msg as HLAcontinueMessage);
                        else
                            il.ReceiveInteraction(msg);
                    }
                }
                else if (msg is PeerAdvertisementInteractionMessage)
                {
                    foreach (IInteractionListener il in manager.InteractionListeners)
                    {
                        if (il is IBootstrapObjectModelInteractionListener)
                            (il as IBootstrapObjectModelInteractionListener).OnReceivePeerAdvertisementInteraction(msg as PeerAdvertisementInteractionMessage);
                        else
                            il.ReceiveInteraction(msg);
                    }
                }
                else
                    foreach (IInteractionListener il in manager.InteractionListeners)
                    {
                        il.ReceiveInteraction(msg);
                    }
            }
            catch (System.IO.IOException ioe)
            {
                throw new FederateInternalError(ioe.ToString());
            }

        }
    }
}
