namespace Hla.Rti1516
{
	using System;

	/// <summary> 
    /// Keys are <code>IAttributeHandle</code>s; values are <code>byte[]</code>.  All 
	/// operations are required, none optional.  Null mappings are not allowed.
	/// Methods <code>put</code>, <code>putAll</code>, and <code>Remove</code> should
	/// throw <code>IllegalArgumentException</code> to enforce types of keys and mappings.
	/// </summary>
    public interface IAttributeHandleValueMap : System.Collections.Generic.IDictionary<IAttributeHandle, byte[]>
	{
	}
}