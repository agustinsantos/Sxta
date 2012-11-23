namespace Sxta.Rti1516.Reflection
{
    using System;

    using Hla.Rti1516;
    
    /// <summary>
    /// A simple data type.
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly, Inherited = false, AllowMultiple = false)]
    public sealed class HLASwitchesAttribute : HLAReflectionAttribute
    {

        public HLASwitches HLASwitchesInfo
        {
            get { return ((HLASwitches)baseInfo); }
        }

                /// <summary>
        /// 
        /// </summary>
        public HLASwitchType AutoProvide
        {
            get { return ((HLASwitches)baseInfo).AutoProvide; }
            set { ((HLASwitches)baseInfo).AutoProvide = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string AutoProvideNotes
        {
            get { return ((HLASwitches)baseInfo).AutoProvideNotes; }
            set { ((HLASwitches)baseInfo).AutoProvideNotes = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public HLASwitchType ConveyRegionDesignatorSets
        {
            get { return ((HLASwitches)baseInfo).ConveyRegionDesignatorSets; }
            set { ((HLASwitches)baseInfo).ConveyRegionDesignatorSets = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string ConveyRegionDesignatorSetsNotes
        {
            get { return ((HLASwitches)baseInfo).ConveyRegionDesignatorSetsNotes; }
            set { ((HLASwitches)baseInfo).ConveyRegionDesignatorSetsNotes = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public HLASwitchType AttributeScopeAdvisory
        {
            get { return ((HLASwitches)baseInfo).AttributeScopeAdvisory; }
            set { ((HLASwitches)baseInfo).AttributeScopeAdvisory = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string AttributeScopeAdvisoryNotes
        {
            get { return ((HLASwitches)baseInfo).AttributeScopeAdvisoryNotes; }
            set { ((HLASwitches)baseInfo).AttributeScopeAdvisoryNotes = value; }
        }


        /// <summary>
        /// 
        /// </summary>
        public HLASwitchType AttributeRelevanceAdvisory
        {
            get { return ((HLASwitches)baseInfo).AttributeRelevanceAdvisory; }
            set { ((HLASwitches)baseInfo).AttributeRelevanceAdvisory = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string AttributeRelevanceAdvisoryNotes
        {
            get { return ((HLASwitches)baseInfo).AttributeRelevanceAdvisoryNotes; }
            set { ((HLASwitches)baseInfo).AttributeRelevanceAdvisoryNotes = value; }
        }


        /// <summary>
        /// 
        /// </summary>
        public HLASwitchType ObjectClassRelevanceAdvisory
        {
            get { return ((HLASwitches)baseInfo).ObjectClassRelevanceAdvisory; }
            set { ((HLASwitches)baseInfo).ObjectClassRelevanceAdvisory = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string ObjectClassRelevanceAdvisoryNotes
        {
            get { return ((HLASwitches)baseInfo).ObjectClassRelevanceAdvisoryNotes; }
            set { ((HLASwitches)baseInfo).ObjectClassRelevanceAdvisoryNotes = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public HLASwitchType InteractionRelevanceAdvisory
        {
            get { return ((HLASwitches)baseInfo).InteractionRelevanceAdvisory; }
            set { ((HLASwitches)baseInfo).InteractionRelevanceAdvisory = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string InteractionRelevanceAdvisoryNotes
        {
            get { return ((HLASwitches)baseInfo).InteractionRelevanceAdvisoryNotes; }
            set { ((HLASwitches)baseInfo).InteractionRelevanceAdvisoryNotes = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public HLASwitchType ServiceReporting
        {
            get { return ((HLASwitches)baseInfo).ServiceReporting; }
            set { ((HLASwitches)baseInfo).ServiceReporting = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string ServiceReportingNotes
        {
            get { return ((HLASwitches)baseInfo).ServiceReportingNotes; }
            set { ((HLASwitches)baseInfo).ServiceReportingNotes = value; }
        }

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        public HLASwitchesAttribute()
        {
            baseInfo = new HLASwitches();
        }

    }
}
