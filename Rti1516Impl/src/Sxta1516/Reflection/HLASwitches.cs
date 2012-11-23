using System;
using System.Collections.Generic;
using System.Text;

namespace Sxta.Rti1516.Reflection
{

    /// <summary>
    /// Represents Switch values.
    /// </summary>
    [HLAObjectClassAttribute(Name = "HLASwitches",
                             Sharing = HLAsharingType.PublishSubscribe,
                             Semantics = "Represents Switch values.")]
    public class HLASwitches : HLAreflection
    {
        /// <summary>
        /// 
        /// </summary>
        [HLAAttribute(Name = "autoProvide",
                      Semantics = "TODO.")]
        public HLASwitchType AutoProvide
        {
            get { return autoProvide; }
            set { autoProvide = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        [HLAAttribute(Name = "AutoProvideNotes",
                      Semantics = "TODO.")]
        public string AutoProvideNotes
        {
            get { return autoProvideNotes; }
            set { autoProvideNotes = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        [HLAAttribute(Name = "conveyRegionDesignatorSets",
                      Semantics = "TODO.")]
        public HLASwitchType ConveyRegionDesignatorSets
        {
            get { return conveyRegionDesignatorSets; }
            set { conveyRegionDesignatorSets = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        [HLAAttribute(Name = "conveyRegionDesignatorSetsNotes",
                      Semantics = "TODO.")]
        public string ConveyRegionDesignatorSetsNotes
        {
            get { return conveyRegionDesignatorSetsNotes; }
            set { conveyRegionDesignatorSetsNotes = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        [HLAAttribute(Name = "attributeScopeAdvisory",
                      Semantics = "TODO.")]
        public HLASwitchType AttributeScopeAdvisory
        {
            get { return attributeScopeAdvisory; }
            set { attributeScopeAdvisory = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        [HLAAttribute(Name = "attributeScopeAdvisoryNotes",
                      Semantics = "TODO.")]
        public string AttributeScopeAdvisoryNotes
        {
            get { return attributeScopeAdvisoryNotes; }
            set { attributeScopeAdvisoryNotes = value; }
        }


        /// <summary>
        /// 
        /// </summary>
        [HLAAttribute(Name = "attributeRelevanceAdvisory",
                      Semantics = "TODO.")]
        public HLASwitchType AttributeRelevanceAdvisory
        {
            get { return attributeRelevanceAdvisory; }
            set { attributeRelevanceAdvisory = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        [HLAAttribute(Name = "attributeRelevanceAdvisoryNotes",
                      Semantics = "TODO.")]
        public string AttributeRelevanceAdvisoryNotes
        {
            get { return attributeRelevanceAdvisoryNotes; }
            set { attributeRelevanceAdvisoryNotes = value; }
        }


        /// <summary>
        /// 
        /// </summary>
        [HLAAttribute(Name = "objectClassRelevanceAdvisory",
                      Semantics = "TODO.")]
        public HLASwitchType ObjectClassRelevanceAdvisory
        {
            get { return objectClassRelevanceAdvisory; }
            set { objectClassRelevanceAdvisory = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        [HLAAttribute(Name = "objectClassRelevanceAdvisoryNotes",
                      Semantics = "TODO.")]
        public string ObjectClassRelevanceAdvisoryNotes
        {
            get { return objectClassRelevanceAdvisoryNotes; }
            set { objectClassRelevanceAdvisoryNotes = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        [HLAAttribute(Name = "interactionRelevanceAdvisory",
                      Semantics = "TODO.")]
        public HLASwitchType InteractionRelevanceAdvisory
        {
            get { return interactionRelevanceAdvisory; }
            set { interactionRelevanceAdvisory = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        [HLAAttribute(Name = "interactionRelevanceAdvisoryNotes",
                      Semantics = "TODO.")]
        public string InteractionRelevanceAdvisoryNotes
        {
            get { return interactionRelevanceAdvisoryNotes; }
            set { interactionRelevanceAdvisoryNotes = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        [HLAAttribute(Name = "serviceReporting",
                      Semantics = "TODO.")]
        public HLASwitchType ServiceReporting
        {
            get { return serviceReporting; }
            set { serviceReporting = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        [HLAAttribute(Name = "serviceReportingNotes",
                      Semantics = "TODO.")]
        public string ServiceReportingNotes
        {
            get { return serviceReportingNotes; }
            set { serviceReportingNotes = value; }
        }



        public HLASwitches()
            : base()
        { }

        public HLASwitches(System.Xml.XmlElement switchesElement)
            : base(switchesElement)
        {
            autoProvide = (HLASwitchType)Enum.Parse(typeof(HLASwitchType), switchesElement.GetAttribute("autoProvide"));
            autoProvideNotes = switchesElement.GetAttribute("autoProvideNotes");
            conveyRegionDesignatorSets = (HLASwitchType)Enum.Parse(typeof(HLASwitchType), switchesElement.GetAttribute("conveyRegionDesignatorSets"));
            conveyRegionDesignatorSetsNotes = switchesElement.GetAttribute("conveyRegionDesignatorSetsNotes");
            attributeScopeAdvisory = (HLASwitchType)Enum.Parse(typeof(HLASwitchType), switchesElement.GetAttribute("attributeScopeAdvisory"));
            attributeScopeAdvisoryNotes = switchesElement.GetAttribute("attributeScopeAdvisoryNotes");
            attributeRelevanceAdvisory = (HLASwitchType)Enum.Parse(typeof(HLASwitchType), switchesElement.GetAttribute("attributeRelevanceAdvisory"));
            attributeRelevanceAdvisoryNotes = switchesElement.GetAttribute("attributeRelevanceAdvisoryNotes");
            objectClassRelevanceAdvisory = (HLASwitchType)Enum.Parse(typeof(HLASwitchType), switchesElement.GetAttribute("objectClassRelevanceAdvisory"));
            objectClassRelevanceAdvisoryNotes = switchesElement.GetAttribute("objectClassRelevanceAdvisoryNotes");
            interactionRelevanceAdvisory = (HLASwitchType)Enum.Parse(typeof(HLASwitchType), switchesElement.GetAttribute("interactionRelevanceAdvisory"));
            interactionRelevanceAdvisoryNotes = switchesElement.GetAttribute("interactionRelevanceAdvisoryNotes");
            serviceReporting = (HLASwitchType)Enum.Parse(typeof(HLASwitchType), switchesElement.GetAttribute("serviceReporting"));
            serviceReportingNotes = switchesElement.GetAttribute("serviceReportingNotes");
        }

        protected HLASwitchType autoProvide = HLASwitchType.NA;
        protected string autoProvideNotes;
        protected HLASwitchType conveyRegionDesignatorSets = HLASwitchType.NA;
        protected string conveyRegionDesignatorSetsNotes;
        protected HLASwitchType attributeScopeAdvisory = HLASwitchType.NA;
        protected string attributeScopeAdvisoryNotes;
        protected HLASwitchType attributeRelevanceAdvisory = HLASwitchType.NA;
        protected string attributeRelevanceAdvisoryNotes;
        protected HLASwitchType objectClassRelevanceAdvisory = HLASwitchType.NA;
        protected string objectClassRelevanceAdvisoryNotes;
        protected HLASwitchType interactionRelevanceAdvisory = HLASwitchType.NA;
        protected string interactionRelevanceAdvisoryNotes;
        protected HLASwitchType serviceReporting = HLASwitchType.NA;
        protected string serviceReportingNotes;


    }
}
