using System;
using System.Collections.Generic;
using System.Text;

namespace Sxta.Rti1516.Management
{
    using System;
    using System.IO;
    using System.Collections.Generic;

    using Hla.Rti1516;
    using Sxta.Rti1516.Reflection;
    using Sxta.Rti1516.Interactions;

    using HlaEncodingReader = Sxta.Rti1516.Serializers.XrtiEncoding.HlaEncodingReader;
    using HlaEncodingWriter = Sxta.Rti1516.Serializers.XrtiEncoding.HlaEncodingWriter;
    using Sxta.Rti1516.Serializers.XrtiEncoding;

    public class ILogicalTimeIntervalXrtiSerializer : BaseInteractionMessageXrtiSerializer
    {
        private ILogicalTimeIntervalFactory logicalTimeIntervalFactory;

        public ILogicalTimeIntervalXrtiSerializer(XrtiSerializerManager manager, ILogicalTimeIntervalFactory aLogicalTimeIntervalFactory)
            : base(manager)
        {
            this.logicalTimeIntervalFactory = aLogicalTimeIntervalFactory;
        }

        ///<summary>
        /// Writes this ILogicalTimeInterval to the specified stream.
        ///</summary>
        ///<param name="writer"> the output stream to write to</param>
        ///<param name="HLAfederationName"> the property to serialize</param>
        ///<exception cref="System.IO.IOException"> if an error occurs</exception>
        public override void Serialize(HlaEncodingWriter writer, object HLAlogicalTimeInterval)
        {
            try
            {
                ILogicalTimeInterval logicalTimeInterval = (ILogicalTimeInterval)HLAlogicalTimeInterval;

                byte[] logicalTimeIntervalBytesArray = new byte[logicalTimeInterval.EncodedLength()];
                logicalTimeInterval.Encode(logicalTimeIntervalBytesArray, 0);

                writer.WriteHLAopaqueData(logicalTimeIntervalBytesArray);
            }
            catch (IOException ioe)
            {
                throw new RTIinternalError(ioe.ToString());
            }
        }

        ///<summary>
        /// Reads and returns a ILogicalTimeInterval from the specified stream.
        ///</summary>
        ///<param name="reader"> the input stream to read from</param>
        ///<param name="dummy"> this parameter is not used</param>
        ///<returns> the decoded value</returns>
        ///<exception cref="System.IO.IOException"> if an error occurs</exception>
        public override object Deserialize(HlaEncodingReader reader, ref object dummy)
        {
            ILogicalTimeInterval decodedValue;
            try
            {
                decodedValue = logicalTimeIntervalFactory.Decode(reader.ReadHLAopaqueData(), 0);
                return decodedValue;
            }
            catch (IOException ioe)
            {
                throw new FederateInternalError(ioe.ToString());
            }
        }
    }
}
