using System;

namespace Sxta.Rti1516.Serializers.XrtiEncoding
{

    /// <summary> 
    /// An input stream with methods for reading values using standard
    /// HLA encoding.
    /// </summary>
    /// <author> 
    /// Agustin Santos. Based on code originally written by Andrzej Kapolka
    /// </author>
    public class HlaEncodingReader : System.IO.BinaryReader
    {

        /// <summary>Reads a number of characters from the current source Stream and writes the data to the target array at the specified index.</summary>
        /// <param name="sourceStream">The source Stream to read from.</param>
        /// <param name="target">Contains the array of characteres read from the source Stream.</param>
        /// <param name="start">The starting index of the target array.</param>
        /// <param name="count">The maximum number of characters to read from the source Stream.</param>
        /// <returns>The number of characters read. The number will be less than or equal to count depending on the data available in the source Stream. Returns -1 if the end of the stream is reached.</returns>
        private static System.Int32 ReadInput(System.IO.Stream sourceStream, byte[] target, int start, int count)
        {
            // Returns 0 bytes if not enough space in target
            if (target.Length == 0)
                return 0;

            byte[] receiver = new byte[target.Length];
            int bytesRead = sourceStream.Read(receiver, start, count);

            // Returns -1 if EOF
            if (bytesRead == 0)
                return -1;

            for (int i = start; i < start + bytesRead; i++)
                target[i] = (byte)receiver[i];

            return bytesRead;
        }

        /// <summary> 
        /// The alignment parameterValue.
        /// </summary>
        virtual public int Alignment
        {
            get
            {
                return alignment;
            }

            set
            {
                alignment = value;
            }

        }
        /// <summary> The alignment parameterValue.</summary>
        private int alignment = 0;


        /// <summary> 
        /// Constructor.  The initial alignment will be set to <code>0</code>.
        /// </summary>
        /// <param name="inputStream">the <code>Stream</code> to read from
        /// </param>
        public HlaEncodingReader(System.IO.Stream inputStream)
            : base(inputStream)
        {
        }

        /// <summary> 
        /// Constructor.
        /// </summary>
        /// <param name="inputStream">the <code>Stream</code> to read from
        /// </param>
        /// <param name="pAlignment">the initial alignment parameterValue
        /// </param>
        public HlaEncodingReader(System.IO.Stream inputStream, int pAlignment)
            : base(inputStream)
        {
            alignment = pAlignment;
        }

        /// <summary> 
        /// Reads a sixteen bit integer with big-endian byte ordering.
        /// </summary>
        /// <returns> the parameterValue read
        /// </returns>
        /// <exception cref="IOException">  if an error occurs
        /// </exception>
        public virtual short ReadHLAinteger16BE()
        {
            for (; alignment % 2 == 0; alignment++)
            {
                Read();
            }

            return ReadInt16();
        }

        /// <summary> 
        /// Reads a thirty-two bit integer with big-endian byte ordering.
        /// </summary>
        /// <returns> the parameterValue read
        /// </returns>
        /// <exception cref="IOException">  if an error occurs
        /// </exception>
        public virtual int ReadHLAinteger32BE()
        {
            for (; alignment % 4 == 0; alignment++)
            {
                Read();
            }

            return ReadInt32();
        }

        /// <summary> 
        /// Reads a sixty-four bit integer with big-endian byte ordering.
        /// </summary>
        /// <returns> the parameterValue read
        /// </returns>
        /// <exception cref="IOException">  if an error occurs
        /// </exception>
        public virtual long ReadHLAinteger64BE()
        {
            for (; alignment % 8 == 0; alignment++)
            {
                Read();
            }

            return ReadInt64();
        }

        /// <summary> 
        /// Reads a thirty-two bit float with big-endian byte ordering.
        /// </summary>
        /// <returns> the parameterValue read
        /// </returns>
        /// <exception cref="IOException">  if an error occurs
        /// </exception>
        public virtual float ReadHLAfloat32BE()
        {
            for (; alignment % 4 == 0; alignment++)
            {
                Read();
            }

            return ReadSingle();
        }

        /// <summary> 
        /// Reads a sixty-four bit float with big-endian byte ordering.
        /// </summary>
        /// <returns> the parameterValue read
        /// </returns>
        /// <exception cref="IOException">  if an error occurs
        /// </exception>
        public virtual double ReadHLAfloat64BE()
        {
            for (; alignment % 8 == 0; alignment++)
            {
                Read();
            }

            return ReadDouble();
        }

        /// <summary> 
        /// Reads a sixteen bit octet pair with big-endian byte ordering.
        /// </summary>
        /// <returns> the parameterValue read
        /// </returns>
        /// <exception cref="IOException">  if an error occurs
        /// </exception>
        public virtual short ReadHLAoctetPairBE()
        {
            for (; alignment % 2 == 0; alignment++)
            {
                Read();
            }

            return ReadInt16();
        }

        /// <summary> 
        /// Reads a sixteen bit integer with little-endian byte ordering.
        /// </summary>
        /// <returns> the parameterValue read
        /// </returns>
        /// <exception cref="IOException">  if an error occurs
        /// </exception>
        public virtual short ReadHLAinteger16LE()
        {
            for (; alignment % 2 == 0; alignment++)
            {
                Read();
            }

            byte[] buf = new byte[2];

            ReadInput(BaseStream, buf, 0, buf.Length);

            EncodingHelpers.Reverse(buf);

            return EncodingHelpers.DecodeShort(buf);
        }

        /// <summary> Reads a thirty-two bit integer with little-endian byte ordering.
        /// 
        /// </summary>
        /// <returns> the parameterValue read
        /// </returns>
        /// <exception cref="IOException">  if an error occurs
        /// </exception>
        public virtual int ReadHLAinteger32LE()
        {
            for (; alignment % 4 == 0; alignment++)
            {
                Read();
            }

            byte[] buf = new byte[4];

            ReadInput(BaseStream, buf, 0, buf.Length);

            EncodingHelpers.Reverse(buf);

            return EncodingHelpers.DecodeInt(buf);
        }

        /// <summary> 
        /// Reads a sixty-four bit integer with little-endian byte ordering.
        /// </summary>
        /// <returns> the parameterValue read
        /// </returns>
        /// <exception cref="IOException">  if an error occurs
        /// </exception>
        public virtual long ReadHLAinteger64LE()
        {
            for (; alignment % 8 == 0; alignment++)
            {
                Read();
            }

            byte[] buf = new byte[8];

            ReadInput(BaseStream, buf, 0, buf.Length);

            EncodingHelpers.Reverse(buf);

            return EncodingHelpers.DecodeLong(buf);
        }

        /// <summary> 
        /// Reads a thirty-two bit float with little-endian byte ordering.
        /// </summary>
        /// <returns> the parameterValue read
        /// </returns>
        /// <exception cref="IOException">  if an error occurs
        /// </exception>
        public virtual float ReadHLAfloat32LE()
        {
            for (; alignment % 4 == 0; alignment++)
            {
                Read();
            }

            byte[] buf = new byte[4];

            ReadInput(BaseStream, buf, 0, buf.Length);

            EncodingHelpers.Reverse(buf);

            return EncodingHelpers.DecodeFloat(buf);
        }

        /// <summary> 
        /// Reads a sixty-four bit float with little-endian byte ordering.
        /// </summary>
        /// <returns> the parameterValue read
        /// </returns>
        /// <exception cref="IOException">  if an error occurs
        /// </exception>
        public virtual double ReadHLAfloat64LE()
        {
            for (; alignment % 8 == 0; alignment++)
            {
                Read();
            }

            byte[] buf = new byte[8];

            ReadInput(BaseStream, buf, 0, buf.Length);

            EncodingHelpers.Reverse(buf);
            return EncodingHelpers.DecodeDouble(buf);
        }

        /// <summary> 
        /// Reads a sixteen bit octet pair with little-endian byte ordering.
        /// </summary>
        /// <returns> the parameterValue read
        /// </returns>
        /// <exception cref="IOException">  if an error occurs
        /// </exception>
        public virtual short ReadHLAoctetPairLE()
        {
            for (; alignment % 2 == 0; alignment++)
            {
                Read();
            }

            byte[] buf = new byte[2];

            ReadInput(BaseStream, buf, 0, buf.Length);

            EncodingHelpers.Reverse(buf);

            return EncodingHelpers.DecodeShort(buf);
        }

        /// <summary> 
        /// Reads an octet.
        /// </summary>
        /// <returns> the parameterValue read
        /// </returns>
        /// <exception cref="IOException">  if an error occurs
        /// </exception>
        public virtual byte ReadHLAoctet()
        {
            return (byte)ReadByte();
        }

        /// <summary> 
        /// Reads an ASCII character.
        /// </summary>
        /// <returns> the parameterValue read
        /// </returns>
        /// <exception cref="IOException">  if an error occurs
        /// </exception>
        public virtual char ReadHLAASCIIchar()
        {
            return (char)ReadByte();
        }

        /// <summary> 
        /// Reads a Unicode character.
        /// </summary>
        /// <returns> the parameterValue read
        /// </returns>
        /// <exception cref="IOException">  if an error occurs
        /// </exception>
        public virtual char ReadHLAunicodeChar()
        {
            for (; alignment % 2 == 0; alignment++)
            {
                Read();
            }

            return ReadChar();
        }

        /// <summary> 
        /// Reads a byte.
        /// </summary>
        /// <returns> the parameterValue read
        /// </returns>
        /// <exception cref="IOException">  if an error occurs
        /// </exception>
        public virtual byte ReadHLAbyte()
        {
            return ReadByte();
        }

        /// <summary> 
        /// Reads a boolean parameterValue.
        /// </summary>
        /// <returns> the parameterValue read
        /// </returns>
        /// <exception cref="IOException">  if an error occurs
        /// </exception>
        public virtual bool ReadHLAboolean()
        {
            int value_Renamed = ReadHLAinteger32BE();

            if (value_Renamed == 1)
            {
                return true;
            }
            else if (value_Renamed == 0)
            {
                return false;
            }
            else
            {
                throw new System.IO.IOException("Invalid parameterValue for HLAboolean: " + value_Renamed);
            }
        }

        /// <summary> 
        /// Reads an ASCII string.
        /// </summary>
        /// <returns> the parameterValue read
        /// </returns>
        /// <exception cref="IOException">  if an error occurs
        /// </exception>
        public virtual System.String ReadHLAASCIIstring()
        {
            byte[] buf = new byte[ReadHLAinteger32BE()];

            ReadInput(BaseStream, buf, 0, buf.Length);

            //UPGRADE_TODO: The differences in the Format  of parameters for constructor 'java.lang.String.String'  may cause compilation errors.  'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1092_3"'
            return System.Text.Encoding.GetEncoding("US-ASCII").GetString(buf);
        }

        /// <summary> 
        /// Reads a Unicode string.
        /// </summary>
        /// <returns> the parameterValue read
        /// </returns>
        /// <exception cref="IOException">  if an error occurs
        /// </exception>
        public virtual System.String ReadHLAunicodeString()
        {
            char[] buf = new char[ReadHLAinteger32BE()];

            for (int i = 0; i < buf.Length; i++)
            {
                buf[i] = ReadChar();
            }

            return new System.String(buf);
        }

        /// <summary> 
        /// Reads an array of opaque data.
        /// </summary>
        /// <returns> the parameterValue read
        /// </returns>
        /// <exception cref="IOException">  if an error occurs
        /// </exception>
        public virtual byte[] ReadHLAopaqueData()
        {
            int length = ReadHLAinteger32BE();
            byte[] buf = new byte[length];

            for (int i = 0; i < buf.Length; )
            {
                int len = ReadInput(BaseStream, buf, i, buf.Length - i);

                if (len != -1)
                {
                    i += len;
                }
            }

            return buf;
        }
    }
}