namespace Sxta.Rti1516.TimeManagementSample
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using Hla.Rti1516;
    using Sxta.Rti1516.Ambassadors;
    using Sxta.Rti1516.Reflection;

    public class TimeManagementFederateAmbassador : XrtiFederateAmbassador
    {
        //private TimeManagementForm form;
        public Boolean canAdvanceTime;

        public TimeManagementFederateAmbassador(IRTIambassador prtiAmbassador)//, TimeManagementForm aForm)
            : base(prtiAmbassador)
        {
            //this.form = aForm;
        }

        public override void TimeAdvanceGrant(ILogicalTime theTime)
        {
            base.TimeAdvanceGrant(theTime);

            canAdvanceTime = true;

 
            //form.UpdateTimeManagementValueLabels();
        }

        public IList<Home> GetRemoteHomes()
        {
            lock (this)
            {
                IList<Home> remoteHomes = new List<Home>();

                Home h;
                ICollection<HLAobjectRoot> objs = objectInstanceHandleProxyMap.Values;
                foreach (HLAobjectRoot obj in objs)
                {
                    // Se haya creado el destino y tenga unas coordenadas válidas
                    if (obj is Home && !obj.HLAprivilegeToDeleteObject &&
                        ((Home)obj).PosX != -1 && ((Home)obj).PosY != -1)
                    {
                        h = (Home)obj;
                        remoteHomes.Add(h);
                    }
                }

                return remoteHomes;
            }
        }
    }
}
