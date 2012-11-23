
namespace Sxta.Rti1516.Interactions
{
    using System;
    
    ///<summary>
    /// Xrti Iteraction listener interface. 
    ///</summary>
    ///<author> Agustín Santos </author>
    public interface IInteractionListener
    {
        void ReceiveInteraction(BaseInteractionMessage msg);
    }
}
