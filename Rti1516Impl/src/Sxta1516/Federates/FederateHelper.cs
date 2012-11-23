using System;
using System.Collections.Generic;
using System.Text;

using Hla.Rti1516;
using Sxta.Rti1516.Serializers.XrtiEncoding;
using Sxta.Rti1516.XrtiUtils;

namespace SxtaFormatter.Federates
{
    public class FederateHelper
    {
        Hla.Rti1516.IRTIambassador rtiAmbassador;

        public FederateHelper(IRTIambassador ambassador)
        {
            rtiAmbassador = ambassador;
        }

        public virtual void SendInteraction(BaseInteractionMessage msg)
        {
            try
            {
                IParameterHandleValueMap phvm = rtiAmbassador.ParameterHandleValueMapFactory.Create(0);

                
                //rtiAmbassador.SendInteraction(decodedValue.InteractionClassHandle, phvm, decodedValue.UserSuppliedTag);
            }
            catch (System.Exception e)
            {
                throw new RTIinternalError(e.ToString());
            }
        }
    }
}
