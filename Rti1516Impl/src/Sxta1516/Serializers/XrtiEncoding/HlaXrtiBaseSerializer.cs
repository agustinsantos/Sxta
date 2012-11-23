using System;
using System.IO;

namespace Sxta.Rti1516.Serializers.XrtiEncoding
{
    public abstract class HlaXrtiBaseSerializer : IHlaEncodingSerializer
    {
        public XrtiSerializerManager serializerManager;

        public HlaXrtiBaseSerializer(XrtiSerializerManager manager)
        {
            serializerManager = manager;
        }

        public virtual void Serialize(Stream serializationStream, object obj)
        {
            if (serializationStream == null)
            {
                throw new ArgumentNullException("serializationStream");
            }
            Serialize(new HlaEncodingWriter(serializationStream), obj);
        }

        public abstract void Serialize(HlaEncodingWriter writer, object msg);

        public virtual object Deserialize(Stream serializationStream, ref object msg)
        {
            if (serializationStream == null)
            {
                throw new ArgumentNullException("serializationStream");
            }

            if (serializationStream.CanSeek &&
                serializationStream.Length == 0)
            {
                throw new IOException("serializationStream supports seeking, but its length is 0");
            }

            return Deserialize(new HlaEncodingReader(serializationStream), ref msg);
        }

        public abstract object Deserialize(HlaEncodingReader reader, ref object msg);
    }
}
