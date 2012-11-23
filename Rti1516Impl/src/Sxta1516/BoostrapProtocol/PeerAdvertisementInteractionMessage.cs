
namespace Sxta.Rti1516.BoostrapProtocol
{
    using System;
    using System.IO;
    using System.Collections.Generic;

    using Hla.Rti1516;
    using Sxta.Rti1516.Serializers.XrtiEncoding;
    using Sxta.Rti1516.Interactions;
    using Sxta.Rti1516.Reflection;

    ///<summary>
    ///Message for PeerAdvertisementInteraction iteraction : A Peer Advertisement
    ///describes a peer and the resources it provides to the system. The Peer Advertisement
    ///holds specific information about the peer such as its unique id, and optionally
    ///its name and descriptive information. It may also contain endpoint addresses
    ///and any run-time attributes that individual peer services want to publish
    ///(such as being a rendezvous peer for a group). 
    ///</summary>
    [HLAInteractionClass(Name = "PeerAdvertisementInteraction",
                    Sharing = Sxta.Rti1516.Reflection.HLAsharingType.PublishSubscribe,
                    Order = Sxta.Rti1516.Reflection.HLAorderType.Receive,
                    Semantics = "A Peer Advertisement describes a peer and the resources it provides to the system. The Peer Advertisement holds specific information about the peer such as its unique id, and optionally its name and descriptive information. It may also contain endpoint addresses and any run-time attributes that individual peer services want to publish (such as being a rendezvous peer for a group).",
                    Transportation = "Broadcast")]
    public class PeerAdvertisementInteractionMessage : BaseInteractionMessage
    {
        String PeerName_;

        ///<summary>The name of the peer.</summary> 
        [HLAInteractionParameter(Name = "PeerName",
                      Semantics = "The name of the peer.",
                      DataType = "HLAunicodeString")]
        public String PeerName
        {
            get { return PeerName_; }
            set { PeerName_ = value; }
        }

        String PeerDescription_;

        ///<summary>A description of the peer.</summary> 
        [HLAInteractionParameter(Name = "PeerDescription",
                      Semantics = "A description of the peer.",
                      DataType = "HLAunicodeString")]
        public String PeerDescription
        {
            get { return PeerDescription_; }
            set { PeerDescription_ = value; }
        }

        ConnectionList PeerChannels_;

        ///<summary>A list of URI describing the open channels (TCP/UDP).</summary>
        [HLAInteractionParameter(Name = "PeerChannels",
                      Semantics = "A list of URI describing the open channels (TCP/UDP).",
                      DataType = "ConnectionList")]
        public ConnectionList PeerChannels
        {
            get { return PeerChannels_; }
            set { PeerChannels_ = value; }
        }

        ///<summary> Returns a string representation of this PeerAdvertisementInteractionMessage. </summary>
        ///<returns> a string representation of this PeerAdvertisementInteractionMessage</returns>
        public override string ToString()
        {
            return "PeerAdvertisementInteractionMessage(" + base.ToString()
                   + ", PeerName: " + PeerName
                   + ", PeerDescription: " + PeerDescription
                   + ", PeerChannels: " + PeerChannels + ")";
        }
    }

    public class ConnectionList : List<string>
    {
        ///<summary> Returns a string representation of this PeerAdvertisementInteractionMessage. </summary>
        ///<returns> a string representation of this PeerAdvertisementInteractionMessage</returns>
        public override string ToString()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            Action<string> action = delegate(string s)
            {
                sb.Append(s);
                sb.Append("|");
            };

            sb.Append("ConnectionList(");
            this.ForEach(action);
            sb.Append(")");
            return sb.ToString();
        }
    }

    ///<summary>
    ///A HLA serializer for PeerAdvertisementInteractionMessage. 
    ///</summary>
    public class PeerAdvertisementInteractionMessageXrtiSerializer : BaseInteractionMessageXrtiSerializer
    {
        ///<summary> Constructor </summary>
        public PeerAdvertisementInteractionMessageXrtiSerializer(XrtiSerializerManager manager)
            : base(manager)
        {
        }

        ///<summary> Writes this PeerAdvertisementInteractionMessage to the specified stream.</summary>
        ///<param name="writer"> the output stream to write to</param>
        ///<param name="obj"> the object to serialize</param>
        ///<exception cref="System.IO.IOException"> if an error occurs</exception>
        public override void Serialize(HlaEncodingWriter writer, object obj)
        {
            try
            {
                writer.WriteHLAunicodeString(((PeerAdvertisementInteractionMessage)obj).PeerName);
                writer.WriteHLAunicodeString(((PeerAdvertisementInteractionMessage)obj).PeerDescription);
                writer.WriteHLAinteger32BE((((PeerAdvertisementInteractionMessage)obj).PeerChannels).Count);

                for (int i = 0; i < (((PeerAdvertisementInteractionMessage)obj).PeerChannels).Count; i++)
                {
                    writer.WriteHLAunicodeString((((PeerAdvertisementInteractionMessage)obj).PeerChannels)[i]);
                }
            }
            catch (System.IO.IOException ioe)
            {
                throw new RTIinternalError(ioe.ToString());
            }
        }

        ///<summary> Reads this PeerAdvertisementInteractionMessage from the specified stream.</summary>
        ///<param name="reader"> the input stream to read from</param>
        ///<returns> the object</returns>
        ///<exception cref="System.IO.IOException"> if an error occurs</exception>
        public override object Deserialize(HlaEncodingReader reader, ref object msg)
        {
            PeerAdvertisementInteractionMessage decodedValue;
            if (!(msg is PeerAdvertisementInteractionMessage))
            {
                decodedValue = new PeerAdvertisementInteractionMessage();
                BaseInteractionMessage baseMsg = msg as BaseInteractionMessage;
                decodedValue.InteractionClassHandle = baseMsg.InteractionClassHandle;
                decodedValue.FederationExecutionHandle = baseMsg.FederationExecutionHandle;
                decodedValue.UserSuppliedTag = baseMsg.UserSuppliedTag;
            }
            else
            {
                decodedValue = msg as PeerAdvertisementInteractionMessage;
            }
            //object tmp = decodedValue;
            //decodedValue = base.Deserialize(reader, ref tmp) as PeerAdvertisementInteractionMessage;
            try
            {
                decodedValue.PeerName = reader.ReadHLAunicodeString();
                decodedValue.PeerDescription = reader.ReadHLAunicodeString();
                decodedValue.PeerChannels = new ConnectionList();
                int PeerChannelsLength = reader.ReadHLAinteger32BE();

                for (int i = 0; i < PeerChannelsLength; i++)
                {
                    decodedValue.PeerChannels.Add(reader.ReadHLAunicodeString());
                }
            }
            catch (System.IO.IOException ioe)
            {
                throw new RTIinternalError(ioe.ToString());
            }
            return decodedValue;
        }
    }
}
