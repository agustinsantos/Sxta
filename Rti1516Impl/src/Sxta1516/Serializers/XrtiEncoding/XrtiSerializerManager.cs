using System;
using System.Collections.Generic;
using System.Text;

namespace Sxta.Rti1516.Serializers.XrtiEncoding
{
    public class XrtiSerializerManager
    {
        protected Dictionary<Type, IHlaEncodingSerializer> serializersTypeMap = new Dictionary<Type, IHlaEncodingSerializer>();
        protected Dictionary<Type, long> serializersTypeHandleMap = new Dictionary<Type, long>();
        protected Dictionary<long, IHlaEncodingSerializer> serializersHandleMap = new Dictionary<long, IHlaEncodingSerializer>();

        public IHlaEncodingSerializer GetSerializer(Type objectType)
        {
            return serializersTypeMap[objectType];
        }

        public IHlaEncodingSerializer GetSerializer(long handle)
        {
            return serializersHandleMap[handle];
        }

        public long GetHandle(Type objectType)
        {
            return serializersTypeHandleMap[objectType];
        }

        public void RegisterSerializer(Type objectType, long handle, IHlaEncodingSerializer aSerializer)
        {
            if (objectType != null)
            {
                serializersTypeMap[objectType] = aSerializer;
                serializersTypeHandleMap[objectType] = handle;
            }
            serializersHandleMap[handle] = aSerializer;
        }
    }
}
