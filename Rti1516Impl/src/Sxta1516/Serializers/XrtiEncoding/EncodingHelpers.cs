namespace Sxta.Rti1516.Serializers.XrtiEncoding
{
    using System;
    using System.Text;
    using System.Net;


    /// <summary> 
    /// Utility methods for encoding and decoding basic types, modeled after those
    /// supplied with the DMSO RTI.
    /// 
    /// TODO. I have to test if the byte order ("endianess") in which data is stored in this computer architecture.
    /// is OK. I'm using network byte order.
    /// </summary>
    /// <author> 
    /// Agustin Santos. Based on code originally written by Andrzej Kapolka
    /// </author>
    public class EncodingHelpers
    {

        /// <summary> 
        /// Encodes the specified parameterValue, returning the result as a byte array.
        /// </summary>
        /// <param name="parameterValue">the parameterValue to Encode
        /// </param>
        /// <returns> a byte array containing the encoded parameterValue
        /// </returns>
        public static byte[] EncodeBoolean(bool val)
        {
            return BitConverter.GetBytes(val);
        }

        /// <summary> 
        /// Encodes the specified parameterValue, returning the result as a byte array.
        /// </summary>
        /// <param name="parameterValue">the parameterValue to Encode
        /// </param>
        /// <returns> a byte array containing the encoded parameterValue
        /// </returns>
        public static byte[] EncodeByte(byte val)
        {
            return BitConverter.GetBytes(val);
        }

        /// <summary> 
        /// Encodes the specified parameterValue, returning the result as a byte array.
        /// </summary>
        /// <param name="parameterValue">the parameterValue to Encode
        /// </param>
        /// <returns> a byte array containing the encoded parameterValue
        /// </returns>
        public static byte[] EncodeChar(char val)
        {
            return BitConverter.GetBytes(val);
        }

        /// <summary> 
        /// Encodes the specified parameterValue, returning the result as a byte array.
        /// </summary>
        /// <param name="parameterValue">the parameterValue to Encode
        /// </param>
        /// <returns> a byte array containing the encoded parameterValue
        /// </returns>
        public static byte[] EncodeDouble(double val)
        {
            return BitConverter.GetBytes(val);
        }

        /// <summary> 
        /// Encodes the specified parameterValue, returning the result as a byte array.
        /// </summary>
        /// <param name="parameterValue">the parameterValue to Encode
        /// </param>
        /// <returns> a byte array containing the encoded parameterValue
        /// </returns>
        public static byte[] EncodeFloat(float val)
        {
            return BitConverter.GetBytes(val);
        }

        /// <summary> 
        /// Encodes the specified parameterValue, returning the result as a byte array.
        /// </summary>
        /// <param name="parameterValue">the parameterValue to Encode
        /// </param>
        /// <returns> a byte array containing the encoded parameterValue
        /// </returns>
        public static byte[] EncodeInt(int val)
        {
            return BitConverter.GetBytes(IPAddress.HostToNetworkOrder(val));
        }

        /// <summary> 
        /// Encodes the specified parameterValue, returning the result as a byte array.
        /// </summary>
        /// <param name="parameterValue">the parameterValue to Encode
        /// </param>
        /// <returns> a byte array containing the encoded parameterValue
        /// </returns>
        public static byte[] EncodeLong(long val)
        {
            return BitConverter.GetBytes(IPAddress.HostToNetworkOrder(val));
        }

        /// <summary> 
        /// Encodes the specified parameterValue, returning the result as a byte array.
        /// </summary>
        /// <param name="parameterValue">the parameterValue to Encode
        /// </param>
        /// <returns> a byte array containing the encoded parameterValue
        /// </returns>
        public static byte[] EncodeShort(short val)
        {
            return BitConverter.GetBytes(IPAddress.HostToNetworkOrder(val));
        }


        /// <summary> 
        /// Encodes the specified parameterValue, returning the result as a byte array.
        /// As character encoding use Unicode UTF-16 format with the little-endian byte order 
        /// </summary>
        /// <param name="parameterValue">the parameterValue to Encode
        /// </param>
        /// <returns> a byte array containing the encoded parameterValue
        /// </returns>
        public static byte[] EncodeString(System.String val)
        {
            return Encoding.Unicode.GetBytes(val);
        }

        /// <summary> 
        /// Decodes and returns the parameterValue stored in the specified bufferStream.
        /// </summary>
        /// <param name="bufferStream">the bufferStream containing the encoded parameterValue
        /// </param>
        /// <returns> the decoded parameterValue
        /// </returns>
        /// <exception cref="System.IO.System.IO.IOException"> if the parameterValue could not be decoded
        /// </exception>
        public static bool DecodeBoolean(byte[] buffer)
        {
            try
            {
                return BitConverter.ToBoolean(buffer, 0);
            }
            catch
            {
                throw new System.IO.IOException("bad boolean");
            }
        }

        public static bool DecodeBoolean(byte[] buffer, int startIndex)
        {
            try
            {
                return BitConverter.ToBoolean(buffer, startIndex);
            }
            catch
            {
                throw new System.IO.IOException("bad boolean");
            }
        }

        /// <summary> 
        /// Decodes and returns the parameterValue stored in the specified bufferStream.
        /// </summary>
        /// <param name="bufferStream">the bufferStream containing the encoded parameterValue
        /// </param>
        /// <returns> the decoded parameterValue
        /// </returns>
        /// <exception cref="System.IO.IOException"> if the parameterValue could not be decoded
        /// </exception>
        public static byte DecodeByte(byte[] buffer)
        {
            return buffer[0];
        }


        /// <summary> 
        /// Decodes and returns the parameterValue stored in the specified bufferStream.
        /// </summary>
        /// <param name="bufferStream">the bufferStream containing the encoded parameterValue
        /// </param>
        /// <returns> the decoded parameterValue
        /// </returns>
        /// <exception cref="System.IO.IOException"> if the parameterValue could not be decoded
        /// </exception>
        public static char DecodeChar(byte[] buffer)
        {
            try
            {
                return BitConverter.ToChar(buffer, 0);
            }
            catch
            {
                throw new System.IO.IOException("bad char");
            }
        }
        public static char DecodeChar(byte[] buffer, int startIndex)
        {
            try
            {
                return BitConverter.ToChar(buffer, startIndex);
            }
            catch
            {
                throw new System.IO.IOException("bad char");
            }
        }

        /// <summary> 
        /// Decodes and returns the parameterValue stored in the specified bufferStream.
        /// </summary>
        /// <param name="bufferStream">the bufferStream containing the encoded parameterValue
        /// </param>
        /// <returns> the decoded parameterValue
        /// </returns>
        /// <exception cref="System.IO.IOException"> if the parameterValue could not be decoded
        /// </exception>
        public static double DecodeDouble(byte[] buffer)
        {
            try
            {
                return BitConverter.ToDouble(buffer, 0);
            }
            catch
            {
                throw new System.IO.IOException("bad double");
            }
        }
        public static double DecodeDouble(byte[] buffer, int startIndex)
        {
            try
            {
                return BitConverter.ToDouble(buffer, startIndex);
            }
            catch
            {
                throw new System.IO.IOException("bad double");
            }
        }


        /// <summary> 
        /// Decodes and returns the parameterValue stored in the specified bufferStream.
        /// </summary>
        /// <param name="bufferStream">the bufferStream containing the encoded parameterValue
        /// </param>
        /// <returns> the decoded parameterValue
        /// </returns>
        /// <exception cref="System.IO.IOException"> if the parameterValue could not be decoded
        /// </exception>
        public static float DecodeFloat(byte[] buffer)
        {
            try
            {
                return BitConverter.ToSingle(buffer, 0);
            }
            catch
            {
                throw new System.IO.IOException("bad float");
            }
        }
        public static float DecodeFloat(byte[] buffer, int startIndex)
        {
            try
            {
                return BitConverter.ToSingle(buffer, startIndex);
            }
            catch
            {
                throw new System.IO.IOException("bad float");
            }
        }


        /// <summary> 
        /// Decodes and returns the parameterValue stored in the specified bufferStream.
        /// </summary>
        /// <param name="bufferStream">the bufferStream containing the encoded parameterValue
        /// </param>
        /// <returns> the decoded parameterValue
        /// </returns>
        /// <exception cref="System.IO.IOException"> if the parameterValue could not be decoded
        /// </exception>
        public static int DecodeInt(byte[] buffer)
        {
            try
            {
                return IPAddress.NetworkToHostOrder(BitConverter.ToInt32(buffer, 0));
            }
            catch
            {
                throw new System.IO.IOException("bad int");
            }
        }
        public static int DecodeInt(byte[] buffer, int startIndex)
        {
            try
            {
                return IPAddress.NetworkToHostOrder(BitConverter.ToInt32(buffer, startIndex));
            }
            catch
            {
                throw new System.IO.IOException("bad int");
            }
        }


        /// <summary> 
        /// Decodes and returns the parameterValue stored in the specified bufferStream.
        /// </summary>
        /// <param name="bufferStream">the bufferStream containing the encoded parameterValue
        /// </param>
        /// <returns> the decoded parameterValue
        /// </returns>
        /// <exception cref="System.IO.IOException"> if the parameterValue could not be decoded
        /// </exception>
        public static long DecodeLong(byte[] buffer)
        {
            try
            {
                return IPAddress.NetworkToHostOrder(BitConverter.ToInt64(buffer, 0));
            }
            catch
            {
                throw new System.IO.IOException("bad long");
            }
        }
        public static long DecodeLong(byte[] buffer, int startIndex)
        {
            try
            {
                return IPAddress.NetworkToHostOrder(BitConverter.ToInt64(buffer, startIndex));
            }
            catch
            {
                throw new System.IO.IOException("bad long");
            }
        }

        /// <summary> 
        /// Decodes and returns the parameterValue stored in the specified bufferStream.
        /// </summary>
        /// <param name="bufferStream">the bufferStream containing the encoded parameterValue
        /// </param>
        /// <returns> the decoded parameterValue
        /// </returns>
        /// <exception cref="System.IO.IOException"> if the parameterValue could not be decoded
        /// </exception>
        public static short DecodeShort(byte[] buffer)
        {
            try
            {
                return IPAddress.NetworkToHostOrder(BitConverter.ToInt16(buffer, 0));
            }
            catch
            {
                throw new System.IO.IOException("bad short");
            }
        }

        public static short DecodeShort(byte[] buffer, int startIndex)
        {
            try
            {
                return IPAddress.NetworkToHostOrder(BitConverter.ToInt16(buffer, startIndex));
            }
            catch
            {
                throw new System.IO.IOException("bad short");
            }
        }


        /// <summary> 
        /// Decodes and returns the parameterValue stored in the specified bufferStream.
        /// As character encoding use Unicode UTF-16 format with the little-endian byte order 
        /// </summary>
        /// <param name="bufferStream">the bufferStream containing the encoded parameterValue
        /// </param>
        /// <returns> the decoded parameterValue
        /// </returns>
        /// <exception cref="System.IO.System.IO.IOException"> if the parameterValue could not be decoded
        /// </exception>
        public static System.String DecodeString(byte[] buffer)
        {
            try
            {
                return Encoding.Unicode.GetString(buffer);
            }
            catch
            {
                throw new System.IO.IOException("bad string");

            }
        }

        /// <summary> 
        /// Reverses the specified byte array in-place.
        /// </summary>
        /// <param name="bufferStream">the byte array to Reverse
        /// </param>
        public static void Reverse(byte[] buffer)
        {
            byte tmp;

            for (int i = 0, j = buffer.Length - 1; i < j; i++, j--)
            {
                tmp = buffer[i];

                buffer[i] = buffer[j];

                buffer[j] = tmp;
            }
        }
    }

    
    internal sealed class BitConverterLE
    {
        private BitConverterLE()
        {
        }

        unsafe private static byte[] GetUShortBytes(byte* bytes)
        {
            if (BitConverter.IsLittleEndian)
                return new byte[] { bytes[0], bytes[1] };
            else
                return new byte[] { bytes[1], bytes[0] };
        }

        unsafe private static byte[] GetUIntBytes(byte* bytes)
        {
            if (BitConverter.IsLittleEndian)
                return new byte[] { bytes[0], bytes[1], bytes[2], bytes[3] };
            else
                return new byte[] { bytes[3], bytes[2], bytes[1], bytes[0] };
        }

        unsafe private static byte[] GetULongBytes(byte* bytes)
        {
            if (BitConverter.IsLittleEndian)
                return new byte[] { bytes [0], bytes [1], bytes [2], bytes [3],
						     bytes [4], bytes [5], bytes [6], bytes [7] };
            else
                return new byte[] { bytes [7], bytes [6], bytes [5], bytes [4],
						     bytes [3], bytes [2], bytes [1], bytes [0] };
        }

        unsafe internal static byte[] GetBytes(bool value)
        {
            return new byte[] { value ? (byte)1 : (byte)0 };
        }

        unsafe internal static byte[] GetBytes(char value)
        {
            return GetUShortBytes((byte*)&value);
        }

        unsafe internal static byte[] GetBytes(short value)
        {
            return GetUShortBytes((byte*)&value);
        }

        unsafe internal static byte[] GetBytes(int value)
        {
            return GetUIntBytes((byte*)&value);
        }

        unsafe internal static byte[] GetBytes(long value)
        {
            return GetULongBytes((byte*)&value);
        }

        unsafe internal static byte[] GetBytes(ushort value)
        {
            return GetUShortBytes((byte*)&value);
        }

        unsafe internal static byte[] GetBytes(uint value)
        {
            return GetUIntBytes((byte*)&value);
        }

        unsafe internal static byte[] GetBytes(ulong value)
        {
            return GetULongBytes((byte*)&value);
        }

        unsafe internal static byte[] GetBytes(float value)
        {
            return GetUIntBytes((byte*)&value);
        }

        unsafe internal static byte[] GetBytes(double value)
        {
            return GetULongBytes((byte*)&value);
        }

        unsafe private static void UShortFromBytes(byte* dst, byte[] src, int startIndex)
        {
            if (BitConverter.IsLittleEndian)
            {
                dst[0] = src[startIndex];
                dst[1] = src[startIndex + 1];
            }
            else
            {
                dst[0] = src[startIndex + 1];
                dst[1] = src[startIndex];
            }
        }

        unsafe private static void UIntFromBytes(byte* dst, byte[] src, int startIndex)
        {
            if (BitConverter.IsLittleEndian)
            {
                dst[0] = src[startIndex];
                dst[1] = src[startIndex + 1];
                dst[2] = src[startIndex + 2];
                dst[3] = src[startIndex + 3];
            }
            else
            {
                dst[0] = src[startIndex + 3];
                dst[1] = src[startIndex + 2];
                dst[2] = src[startIndex + 1];
                dst[3] = src[startIndex];
            }
        }

        unsafe private static void ULongFromBytes(byte* dst, byte[] src, int startIndex)
        {
            if (BitConverter.IsLittleEndian)
            {
                for (int i = 0; i < 8; ++i)
                    dst[i] = src[startIndex + i];
            }
            else
            {
                for (int i = 0; i < 8; ++i)
                    dst[i] = src[startIndex + (7 - i)];
            }
        }

        unsafe internal static bool ToBoolean(byte[] value, int startIndex)
        {
            return value[startIndex] != 0;
        }

        unsafe internal static char ToChar(byte[] value, int startIndex)
        {
            char ret;

            UShortFromBytes((byte*)&ret, value, startIndex);

            return ret;
        }

        unsafe internal static short ToInt16(byte[] value, int startIndex)
        {
            short ret;

            UShortFromBytes((byte*)&ret, value, startIndex);

            return ret;
        }

        unsafe internal static int ToInt32(byte[] value, int startIndex)
        {
            int ret;

            UIntFromBytes((byte*)&ret, value, startIndex);

            return ret;
        }

        unsafe internal static long ToInt64(byte[] value, int startIndex)
        {
            long ret;

            ULongFromBytes((byte*)&ret, value, startIndex);

            return ret;
        }

        unsafe internal static ushort ToUInt16(byte[] value, int startIndex)
        {
            ushort ret;

            UShortFromBytes((byte*)&ret, value, startIndex);

            return ret;
        }

        unsafe internal static uint ToUInt32(byte[] value, int startIndex)
        {
            uint ret;

            UIntFromBytes((byte*)&ret, value, startIndex);

            return ret;
        }

        unsafe internal static ulong ToUInt64(byte[] value, int startIndex)
        {
            ulong ret;

            ULongFromBytes((byte*)&ret, value, startIndex);

            return ret;
        }

        unsafe internal static float ToSingle(byte[] value, int startIndex)
        {
            float ret;

            UIntFromBytes((byte*)&ret, value, startIndex);

            return ret;
        }

        unsafe internal static double ToDouble(byte[] value, int startIndex)
        {
            double ret;

            ULongFromBytes((byte*)&ret, value, startIndex);

            return ret;
        }
    }
}