using System;
using System.Text;
using System.IO;

namespace Sxta.Rti1516.Serializers.XrtiText
{
	
	/// <summary> 
	/// An output stream with methods for writing values using a text encoding.
	/// </summary>
	/// <author> 
    /// Agustin Santos.
	/// </author>
	public class HlaTextWriter : StreamWriter
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
		public HlaTextWriter(System.IO.Stream os):base(os)
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
        public HlaTextWriter(System.IO.Stream os, int pAlignment)
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
			Write(val);
		}
		
		/// <summary> 
		/// Writes a thirty-two bit integer with big-endian byte ordering.
		/// </summary>
		/// <param name="parameterValue">the parameterValue to write
		/// </param>
		/// <exception cref="IOException">  if an error occurs
		/// </exception>
		public virtual void  WriteHLAinteger32BE(System.Int32 val)
		{
			Write(val);
		}
		
		/// <summary> 
		/// Writes a sixty-four bit integer with big-endian byte ordering.
		/// </summary>
		/// <param name="parameterValue">the parameterValue to write
		/// </param>
		/// <exception cref="IOException">  if an error occurs
		/// </exception>
		public virtual void  WriteHLAinteger64BE(System.Int64 val)
		{
			Write(val);
		}
		
		/// <summary> 
		/// Writes a thirty-two bit float with big-endian byte ordering.
		/// </summary>
		/// <param name="parameterValue">the parameterValue to write
		/// </param>
		/// <exception cref="IOException">  if an error occurs
		/// </exception>
		public virtual void  WriteHLAfloat32BE(float val)
		{
			Write(val);
		}
		
		/// <summary> 
		/// Writes a sixty-four bit float with big-endian byte ordering.
		/// </summary>
		/// <param name="parameterValue">the parameterValue to write
		/// </param>
		/// <exception cref="IOException">  if an error occurs
		/// </exception>
		public virtual void  WriteHLAfloat64BE(double val)
		{
			Write(val);
		}
		
		/// <summary>
		///  Writes a sixteen bit octet pair with big-endian byte ordering.
		/// </summary>
		/// <param name="parameterValue">the parameterValue to write
		/// </param>
		/// <exception cref=""> IOException if an error occurs
		/// </exception>
		public virtual void  WriteHLAoctetPairBE(short val)
		{
			Write((System.Int16) val);
		}
		
		/// <summary> 
		/// Writes a sixteen bit integer with little-endian byte ordering.
		/// </summary>
		/// <param name="parameterValue">the parameterValue to write
		/// </param>
		/// <exception cref="IOException">  if an error occurs
		/// </exception>
		public virtual void  WriteHLAinteger16LE(short val)
		{
			Write(val);
		}
		
		/// <summary> 
		/// Writes a thirty-two bit integer with little-endian byte ordering.
		/// </summary>
		/// <param name="parameterValue">the parameterValue to write
		/// </param>
		/// <exception cref="IOException">  if an error occurs
		/// </exception>
		public virtual void  WriteHLAinteger32LE(int val)
		{
			Write(val);
		}
		
		/// <summary> 
		/// Writes a sixty-four bit integer with little-endian byte ordering.
		/// </summary>
		/// <param name="parameterValue">the parameterValue to write
		/// </param>
		/// <exception cref="IOException">  if an error occurs
		/// </exception>
		public virtual void  WriteHLAinteger64LE(long val)
		{
			Write(val);
		}
		
		/// <summary> 
		/// Writes a thirty-two bit float with little-endian byte ordering.
		/// </summary>
		/// <param name="parameterValue">the parameterValue to write
		/// </param>
		/// <exception cref="IOException">  if an error occurs
		/// </exception>
		public virtual void  WriteHLAfloat32LE(float val)
		{
			Write(val);
		}
		
		/// <summary> 
		/// Writes a sixty-four bit float with little-endian byte ordering.
		/// </summary>
		/// <param name="parameterValue">the parameterValue to write
		/// </param>
		/// <exception cref="IOException">  if an error occurs
		/// </exception>
		public virtual void  WriteHLAfloat64LE(double val)
		{
			Write(val);
		}
		
		/// <summary> 
		/// Writes a sixteen bit octet pair with little-endian byte ordering.
		/// </summary>
		/// <param name="parameterValue">the parameterValue to write
		/// </param>
		/// <exception cref="IOException">  if an error occurs
		/// </exception>
		public virtual void  WriteHLAoctetPairLE(short val)
		{
			Write(val);
		}
		
		/// <summary> 
		/// Writes an octet.
		/// </summary>
		/// <param name="parameterValue">the parameterValue to write
		/// </param>
		/// <exception cref="IOException">  if an error occurs
		/// </exception>
		public virtual void  WriteHLAoctet(byte val)
		{
			Write((byte) val);
		}
		
		/// <summary> 
		/// Writes an ASCII character.
		/// </summary>
		/// <param name="parameterValue">the parameterValue to write
		/// </param>
		/// <exception cref="IOException">  if an error occurs
		/// </exception>
		public virtual void  WriteHLAASCIIchar(char val)
		{
			Write((byte) val);
		}
		
		/// <summary> 
		/// Writes a Unicode character.
		/// </summary>
		/// <param name="parameterValue">the parameterValue to write
		/// </param>
		/// <exception cref="IOException">  if an error occurs
		/// </exception>
		public virtual void  WriteHLAunicodeChar(char val)
		{
			Write((System.Char) val);
		}
		
		/// <summary> 
		/// Writes a byte.
		/// </summary>
		/// <param name="parameterValue">the parameterValue to write
		/// </param>
		/// <exception cref="IOException">  if an error occurs
		/// </exception>
		public virtual void  WriteHLAbyte(byte val)
		{
			Write((byte) val);
		}
		
		/// <summary> 
		/// Writes a boolean parameterValue.
		/// </summary>
		/// <param name="parameterValue">the parameterValue to write
		/// </param>
		/// <exception cref="IOException">  if an error occurs
		/// </exception>
		public virtual void  WriteHLAboolean(bool val)
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
		public virtual void  WriteHLAASCIIstring(System.String val)
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
		public virtual void  WriteHLAunicodeString(System.String val)
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
		public virtual void  WriteHLAopaqueData(byte[] val)
		{
			WriteHLAinteger32BE(val.Length);
			
			Write(val);
		}
	}

}