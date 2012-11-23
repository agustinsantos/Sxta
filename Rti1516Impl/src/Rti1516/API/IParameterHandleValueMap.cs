using System;
using System.Collections.Generic;
namespace Hla.Rti1516
{
	
	/// <summary> 
	/// Maps <code>IParameterHandle</code>s to <code>byte[]</code>s.  All operations
	/// are required, none optional.  Null mappings are not allowed.  Methods
	/// should throw <code>IllegalArgumentException</code> to enforce types of keys and mappings.
	/// </summary>
	/// <author>  
    /// DMSO
	/// </author>
    public interface IParameterHandleValueMap : System.Collections.Generic.IDictionary<IParameterHandle, byte[]>
	{
	}
}