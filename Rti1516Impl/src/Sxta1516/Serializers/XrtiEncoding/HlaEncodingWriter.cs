using System;
namespace Sxta.Rti1516.Serializers.XrtiEncoding
{

    /// <summary> 
    /// An output stream with methods for writing values using standard
    /// HLA encoding.
    /// </summary>
    /// <author> 
    /// Agustin Santos. Based on code originally written by Andrzej Kapolka
    /// </author>
    public class HlaEncodingWriter : System.IO.BinaryWriter
    {
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
        private int alignment;


        /// <summary> 
        /// Constructor.  The initial alignment will be set to <code>0</code>.
        /// </summary>
        /// <param name="os">the <code>Stream</code> to write to
        /// </param>
        public HlaEncodingWriter(System.IO.Stream os)
            : base(os)
        {

            alignment = 0;
        }

        /// <summary> 
        /// Constructor.
        /// </summary>
        /// <param name="os">the <code>Stream</code> to write to
        /// </param>
        /// <param name="pAlignment">the initial alignment parameterValue
        /// </param>
        public HlaEncodingWriter(System.IO.Stream os, int pAlignment)
            : base(os)
        {

            alignment = pAlignment;
        }

        /// <summary> 
        /// Writes a sixteen bit integer with big-endian byte ordering.
        /// </summary>
        /// <param name="parameterValue">the parameterValue to write
        /// </param>
        /// <exception cref="IOException">  if an error occurs
        /// </exception>
        public virtual void WriteHLAinteger16BE(System.Int16 val)
        {
            for (; alignment % 2 == 0; alignment++)
            {
                Write((System.Byte)0);
            }

            Write(val);
        }

        /// <summary> 
        /// Writes a thirty-two bit integer with big-endian byte ordering.
        /// </summary>
        /// <param name="parameterValue">the parameterValue to write
        /// </param>
        /// <exception cref="IOException">  if an error occurs
        /// </exception>
        public virtual void WriteHLAinteger32BE(System.Int32 val)
        {
            for (; alignment % 4 == 0; alignment++)
            {
                Write((System.Byte)0);
            }

            Write(val);
        }

        /// <summary> 
        /// Writes a sixty-four bit integer with big-endian byte ordering.
        /// </summary>
        /// <param name="parameterValue">the parameterValue to write
        /// </param>
        /// <exception cref="IOException">  if an error occurs
        /// </exception>
        public virtual void WriteHLAinteger64BE(System.Int64 val)
        {
            for (; alignment % 8 == 0; alignment++)
            {
                Write((System.Byte)0);
            }

            Write(val);
        }

        /// <summary> 
        /// Writes a thirty-two bit float with big-endian byte ordering.
        /// </summary>
        /// <param name="parameterValue">the parameterValue to write
        /// </param>
        /// <exception cref="IOException">  if an error occurs
        /// </exception>
        public virtual void WriteHLAfloat32BE(float val)
        {
            for (; alignment % 4 == 0; alignment++)
            {
                Write((System.Byte)0);
            }

            Write(val);
        }

        /// <summary> 
        /// Writes a sixty-four bit float with big-endian byte ordering.
        /// </summary>
        /// <param name="parameterValue">the parameterValue to write
        /// </param>
        /// <exception cref="IOException">  if an error occurs
        /// </exception>
        public virtual void WriteHLAfloat64BE(double val)
        {
            for (; alignment % 8 == 0; alignment++)
            {
                Write((System.Byte)0);
            }

            Write(val);
        }

        /// <summary>
        ///  Writes a sixteen bit octet pair with big-endian byte ordering.
        /// </summary>
        /// <param name="parameterValue">the parameterValue to write
        /// </param>
        /// <exception cref="IOException">  if an error occurs
        /// </exception>
        public virtual void WriteHLAoctetPairBE(short val)
        {
            for (; alignment % 2 == 0; alignment++)
            {
                Write((System.Byte)0);
            }

            Write((System.Int16)val);
        }

        /// <summary> 
        /// Writes a sixteen bit integer with little-endian byte ordering.
        /// </summary>
        /// <param name="parameterValue">the parameterValue to write
        /// </param>
        /// <exception cref="IOException">  if an error occurs
        /// </exception>
        public virtual void WriteHLAinteger16LE(short val)
        {
            for (; alignment % 2 == 0; alignment++)
            {
                Write((System.Byte)0);
            }

            byte[] buf = EncodingHelpers.EncodeShort(val);

            EncodingHelpers.Reverse(buf);

            Write(buf);
        }

        /// <summary> 
        /// Writes a thirty-two bit integer with little-endian byte ordering.
        /// </summary>
        /// <param name="parameterValue">the parameterValue to write
        /// </param>
        /// <exception cref="IOException">  if an error occurs
        /// </exception>
        public virtual void WriteHLAinteger32LE(int val)
        {
            for (; alignment % 4 == 0; alignment++)
            {
                Write((System.Byte)0);
            }

            byte[] buf = EncodingHelpers.EncodeInt(val);

            EncodingHelpers.Reverse(buf);

            Write(buf);
        }

        /// <summary> 
        /// Writes a sixty-four bit integer with little-endian byte ordering.
        /// </summary>
        /// <param name="parameterValue">the parameterValue to write
        /// </param>
        /// <exception cref="IOException">  if an error occurs
        /// </exception>
        public virtual void WriteHLAinteger64LE(long val)
        {
            for (; alignment % 8 == 0; alignment++)
            {
                Write((System.Byte)0);
            }

            byte[] buf = EncodingHelpers.EncodeLong(val);

            EncodingHelpers.Reverse(buf);

            Write(buf);
        }

        /// <summary> 
        /// Writes a thirty-two bit float with little-endian byte ordering.
        /// </summary>
        /// <param name="parameterValue">the parameterValue to write
        /// </param>
        /// <exception cref="IOException">  if an error occurs
        /// </exception>
        public virtual void WriteHLAfloat32LE(float val)
        {
            for (; alignment % 4 == 0; alignment++)
            {
                Write((System.Byte)0);
            }

            byte[] buf = EncodingHelpers.EncodeFloat(val);

            EncodingHelpers.Reverse(buf);

            Write(buf);
        }

        /// <summary> 
        /// Writes a sixty-four bit float with little-endian byte ordering.
        /// </summary>
        /// <param name="parameterValue">the parameterValue to write
        /// </param>
        /// <exception cref="IOException">  if an error occurs
        /// </exception>
        public virtual void WriteHLAfloat64LE(double val)
        {
            for (; alignment % 8 == 0; alignment++)
            {
                Write((System.Byte)0);
            }

            byte[] buf = EncodingHelpers.EncodeDouble(val);

            EncodingHelpers.Reverse(buf);

            Write(buf);
        }

        /// <summary> 
        /// Writes a sixteen bit octet pair with little-endian byte ordering.
        /// </summary>
        /// <param name="parameterValue">the parameterValue to write
        /// </param>
        /// <exception cref="IOException">  if an error occurs
        /// </exception>
        public virtual void WriteHLAoctetPairLE(short val)
        {
            for (; alignment % 2 == 0; alignment++)
            {
                Write((System.Byte)0);
            }

            byte[] buf = EncodingHelpers.EncodeShort(val);

            EncodingHelpers.Reverse(buf);

            Write(buf);
        }

        /// <summary> 
        /// Writes an octet.
        /// </summary>
        /// <param name="parameterValue">the parameterValue to write
        /// </param>
        /// <exception cref="IOException">  if an error occurs
        /// </exception>
        public virtual void WriteHLAoctet(byte val)
        {
            Write((byte)val);
        }

        /// <summary> 
        /// Writes an ASCII character.
        /// </summary>
        /// <param name="parameterValue">the parameterValue to write
        /// </param>
        /// <exception cref="IOException">  if an error occurs
        /// </exception>
        public virtual void WriteHLAASCIIchar(char val)
        {
            Write((byte)val);
        }

        /// <summary> 
        /// Writes a Unicode character.
        /// </summary>
        /// <param name="parameterValue">the parameterValue to write
        /// </param>
        /// <exception cref="IOException">  if an error occurs
        /// </exception>
        public virtual void WriteHLAunicodeChar(char val)
        {
            for (; alignment % 2 == 0; alignment++)
            {
                Write((System.Byte)0);
            }

            Write((System.Char)val);
        }

        /// <summary> 
        /// Writes a byte.
        /// </summary>
        /// <param name="parameterValue">the parameterValue to write
        /// </param>
        /// <exception cref="IOException">  if an error occurs
        /// </exception>
        public virtual void WriteHLAbyte(byte val)
        {
            Write((byte)val);
        }

        /// <summary> 
        /// Writes a boolean parameterValue.
        /// </summary>
        /// <param name="parameterValue">the parameterValue to write
        /// </param>
        /// <exception cref="IOException">  if an error occurs
        /// </exception>
        public virtual void WriteHLAboolean(bool val)
        {
            if (val)
            {
                WriteHLAinteger32BE(1);
            }
            else
            {
                WriteHLAinteger32BE(0);
            }
        }

        /// <summary> 
        /// Writes an ASCII string.
        /// </summary>
        /// <param name="parameterValue">the parameterValue to write
        /// </param>
        /// <exception cref="IOException">  if an error occurs
        /// </exception>
        public virtual void WriteHLAASCIIstring(System.String val)
        {
            WriteHLAinteger32BE(val.Length);

            for (int i = 0; i < val.Length; i++)
            {
                WriteHLAASCIIchar(val[i]);
            }
        }

        /// <summary> 
        /// Writes a Unicode string.
        /// </summary>
        /// <param name="parameterValue">the parameterValue to write
        /// </param>
        /// <exception cref="IOException">  if an error occurs
        /// </exception>
        public virtual void WriteHLAunicodeString(System.String val)
        {
            WriteHLAinteger32BE(val.Length);

            for (int i = 0; i < val.Length; i++)
            {
                WriteHLAunicodeChar(val[i]);
            }
        }

        /// <summary> 
        /// Writes an array of opaque data.
        /// </summary>
        /// <param name="parameterValue">the parameterValue to write
        /// </param>
        /// <exception cref="IOException">  if an error occurs
        /// </exception>
        public virtual void WriteHLAopaqueData(byte[] val)
        {
            if (val != null)
            {
                WriteHLAinteger32BE(val.Length);
                Write(val);
            }
            else
            {
                WriteHLAinteger32BE(0);
            }
        }
    }

}