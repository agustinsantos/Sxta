namespace Hla.Rti1516
{
	using System;

	/// <summary> 
    /// All <code>Set</code> operations are required, none are optional.  
    /// Methods should throw
	/// <code>IllegalArgumentException</code> if the argument is not an
	/// <code>IAttributeHandle</code>.
	/// </summary>
    public interface IAttributeHandleSet : System.Collections.Generic.ICollection<IAttributeHandle>
	{
	}
}