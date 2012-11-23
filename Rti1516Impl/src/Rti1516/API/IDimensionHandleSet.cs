namespace Hla.Rti1516
{
	using System;

	/// <summary>
    /// All <code>Set</code> operations are required, none are optional.
	/// Methods <code>Add</code> and <code>Remove</code> should throw
	/// <code>IllegalArgumentException</code> if the argument is not
	/// a <code>IDimensionHandle</code>.  Methods <code>addAll</code>,
	/// <code>RemoveAll</code> and <code>RetainAll</code> should throw
	/// <code>IllegalArgumentException</code> if the argument is not a
	/// <code>IDimensionHandleSet</code>.
	/// </summary>
    public interface IDimensionHandleSet : System.Collections.Generic.ICollection<IDimensionHandle>
	{
	}
}