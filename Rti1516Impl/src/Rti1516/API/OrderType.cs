namespace Hla.Rti1516
{
	using System;

	/// <summary> 
	/// Represents a type of message ordering.
	/// </summary>
	[Serializable]
	public sealed class OrderType
	{
        public enum  OrderTypeEnum : byte
        {
            Receive = 1,
            TimeStamp = 2
        }

		/// <summary> The receive-order type.</summary>
        public static readonly OrderType RECEIVE = new OrderType(OrderTypeEnum.Receive);
		
		/// <summary> The timestamp-order type.</summary>
        public static readonly OrderType TIMESTAMP = new OrderType(OrderTypeEnum.TimeStamp);
		
		/// <summary> The value of the instance.</summary>
        private OrderTypeEnum val;
		
		
		/// <summary> 
		/// Decodes an order type stored within the specified buffer and
		/// returns a corresponding <code>OrderType</code> instance.
		/// </summary>
		/// <param name="buffer">the buffer in which the encoded value is stored
		/// </param>
		/// <param name="offset">the offset within the buffer at which the encoded
		/// value is located
		/// </param>
		/// <returns> an <code>OrderType</code> corresponding to the decoded
		/// value
		/// </returns>
		/// <exception cref="CouldNotDecode"> if the order type could not be decoded
		/// </exception>
		public static OrderType Decode(byte[] buffer, int offset)
		{
            OrderTypeEnum valueTmp = (OrderTypeEnum)buffer[offset];
			
			if (valueTmp == RECEIVE.val)
			{
				return RECEIVE;
			}
			else if (valueTmp == TIMESTAMP.val)
			{
				return TIMESTAMP;
			}
			else
			{
				throw new CouldNotDecode("invalid order type (neither receive nor timestamp)");
			}
		}
		
		/// <summary> 
		/// Copy constructor.
		/// </summary>
		/// <param name="otherOrderType">the order type to copy
		/// </param>
		public OrderType(OrderType otherOrderType)
		{
			val = otherOrderType.val;
		}
		
		/// <summary> 
		/// Private constructor.
		/// </summary>
		/// <param name="pValue">the enum value corresponding to this order type
		/// </param>
        private OrderType(OrderTypeEnum pValue)
		{
			val = pValue;
		}
		
		/// <summary> 
		/// Compares this order type for equality with another.
		/// </summary>
		/// <param name="otherOrderType">the other order type
		/// </param>
		/// <returns> <code>true</code> if the two order types are equal,
		/// <code>false</code> otherwise
		/// </returns>
		public  override bool Equals(System.Object otherOrderType)
		{
			try
			{
				return (val == ((OrderType) otherOrderType).val);
			}
			catch
			{
				return false;
			}
		}
		
		/// <summary> 
		/// Computes and returns a hash code corresponding to this order type.
		/// </summary>
		/// <returns> a hash code corresponding to this order type
		/// </returns>
		public override int GetHashCode()
		{
			return val.GetHashCode();
		}
		
		/// <summary> 
		/// Returns the encoded length of this order type.
		/// </summary>
		/// <returns> the encoded length of this order type (in bytes)
		/// </returns>
		public int EncodedLength()
		{
			return 1;
		}
		
		/// <summary> 
		/// Encodes this order type, placing the result into the specified buffer.
		/// </summary>
		/// <param name="buffer">the buffer in which to place the encoded value
		/// </param>
		/// <param name="offset">the offset within the buffer at which to store the encoded
		/// value
		/// </param>
		public void  Encode(byte[] buffer, int offset)
		{
			buffer[offset] = (byte) val;
		}
		
		/// <summary> 
		/// Returns a string representation of this order type.
		/// </summary>
		/// <returns> a string representation of this order type
		/// </returns>
		public override System.String ToString()
		{
            return val.ToString();
		}
	}
}