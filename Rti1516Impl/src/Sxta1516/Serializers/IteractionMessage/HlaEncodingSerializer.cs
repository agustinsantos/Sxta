namespace Sxta.Rti1516.Serializers.XrtiEncoding
{
    using System;
    using System.IO;
    using System.Collections.Generic;

    //TODO no me gusta depender de un nivel superior. Mirar con detenimiento.
    using Sxta.Rti1516.Interactions;

    public class HlaEncodingSerializer : HlaXrtiBaseSerializer
    {
        /// <summary> 
        /// The "magic number" that appears at the start of every properly formed
        /// packet.
        /// </summary>
        private const int MAGIC_NUMBER = unchecked((int)0xFEEDAFED);

        /// <summary>
        /// The version number of the HLA Encoding Formatter.  The upper 16 bits 
        /// of the integer contain the major version number, and the lower 16 bits contain
        /// the minor version number.
        /// </summary>
        private int hlaEncodingModelVersion;

        private object hlaFormat;

        private IHlaEncodingSerializer baseSerializer;

        /// <summary>
        /// The version number of the HLA Encoding Formatter.  The upper 16 bits 
        /// of the integer contain the major version number, and the lower 16 bits contain
        /// the minor version number.
        /// </summary>
        public HlaEncodingSerializer(int version, object format, XrtiSerializerManager manager)
            : base(manager)
        {
            hlaEncodingModelVersion = version;
            hlaFormat = format;
            //baseSerializer = new BaseIteractionMessageXrtiSerializer(manager);
            baseSerializer = serializerManager.GetSerializer(typeof(BaseInteractionMessage));
        }

        /// <summary>
        /// Reads and returns a HLAparameterHandleValuePair from the specified stream.
        /// </summary>
        /// <param name="reader"> the input stream to read from</param>
        /// <returns> the decoded parameterValue</returns>
        /// <exception cref="IOException"> if an error occurs</exception>
        public override object Deserialize(HlaEncodingReader reader, ref object msg)
        {
            if (reader.ReadHLAinteger32BE() == MAGIC_NUMBER && reader.ReadHLAinteger32BE() == hlaEncodingModelVersion)
            {
                return baseSerializer.Deserialize(reader, ref msg);
            }
            else
                throw new IOException("Wrong message format. Unexpected magic number or version");
        }

        public override void Serialize(HlaEncodingWriter writer, object msg)
        {
            if (writer == null)
            {
                throw new ArgumentNullException("serialization writer");
            }

            writer.WriteHLAinteger32BE(MAGIC_NUMBER);
            writer.WriteHLAinteger32BE(hlaEncodingModelVersion);
            baseSerializer.Serialize(writer, msg);
            writer.Flush();
        }
    }
}
