namespace Sxta.Rti1516.Reflection
{
    using System;

    using Hla.Rti1516;
    
    /// <summary>
    /// A HLA object Model attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly, Inherited = false, AllowMultiple = false)]
    public sealed class HLAObjectModelAttribute : HLAReflectionAttribute
    {

        public HLAObjectModel HLAObjectModelInfo
        {
            get { return ((HLAObjectModel)baseInfo); }
        }

        public string DTDversion
        {
            get { return ((HLAObjectModel)baseInfo).DTDversion; }
            set { ((HLAObjectModel)baseInfo).DTDversion = value; }
        }


        ///               use="required"/>
        public HLAObjectModelType ObjectModelType
        {
            get { return ((HLAObjectModel)baseInfo).ObjectModelType; }
            set { ((HLAObjectModel)baseInfo).ObjectModelType = value; }
        }

        public string TypeNotes
        {
            get { return ((HLAObjectModel)baseInfo).TypeNotes; }
            set { ((HLAObjectModel)baseInfo).TypeNotes = value; }
        }

        public string Version
        {
            get { return ((HLAObjectModel)baseInfo).Version; }
            set { ((HLAObjectModel)baseInfo).Version = value; }
        }

        public string VersionNotes
        {
            get { return ((HLAObjectModel)baseInfo).VersionNotes; }
            set { ((HLAObjectModel)baseInfo).VersionNotes = value; }
        }

        public string Date
        {
            get { return ((HLAObjectModel)baseInfo).Date; }
            set { ((HLAObjectModel)baseInfo).Date = value; }
        }

        public string DateNotes
        {
            get { return ((HLAObjectModel)baseInfo).DateNotes; }
            set { ((HLAObjectModel)baseInfo).DateNotes = value; }
        }

        public string Purpose
        {
            get { return ((HLAObjectModel)baseInfo).Purpose; }
            set { ((HLAObjectModel)baseInfo).Purpose = value; }
        }

        public string PurposeNotes
        {
            get { return ((HLAObjectModel)baseInfo).PurposeNotes; }
            set { ((HLAObjectModel)baseInfo).PurposeNotes = value; }
        }

        public string AppDomain
        {
            get { return ((HLAObjectModel)baseInfo).AppDomain; }
            set { ((HLAObjectModel)baseInfo).AppDomain = value; }
        }

        public string AppDomainNotes
        {
            get { return ((HLAObjectModel)baseInfo).AppDomainNotes; }
            set { ((HLAObjectModel)baseInfo).AppDomainNotes = value; }
        }

        public string Sponsor
        {
            get { return ((HLAObjectModel)baseInfo).Sponsor; }
            set { ((HLAObjectModel)baseInfo).Sponsor = value; }
        }

        public string SponsorNotes
        {
            get { return ((HLAObjectModel)baseInfo).SponsorNotes; }
            set { ((HLAObjectModel)baseInfo).SponsorNotes = value; }
        }

        public string PocName
        {
            get { return ((HLAObjectModel)baseInfo).PocName; }
            set { ((HLAObjectModel)baseInfo).PocName = value; }
        }

        public string PocNameNotes
        {
            get { return ((HLAObjectModel)baseInfo).PocNameNotes; }
            set { ((HLAObjectModel)baseInfo).PocNameNotes = value; }
        }

        public string PocOrg
        {
            get { return ((HLAObjectModel)baseInfo).PocOrg; }
            set { ((HLAObjectModel)baseInfo).PocOrg = value; }
        }

        public string PocOrgNotes
        {
            get { return ((HLAObjectModel)baseInfo).PocOrgNotes; }
            set { ((HLAObjectModel)baseInfo).PocOrgNotes = value; }
        }

        public string PocPhone
        {
            get { return ((HLAObjectModel)baseInfo).PocPhone; }
            set { ((HLAObjectModel)baseInfo).PocPhone = value; }
        }

        public string PocPhoneNotes
        {
            get { return ((HLAObjectModel)baseInfo).PocPhoneNotes; }
            set { ((HLAObjectModel)baseInfo).PocPhoneNotes = value; }
        }

        public string PocEmail
        {
            get { return ((HLAObjectModel)baseInfo).PocEmail; }
            set { ((HLAObjectModel)baseInfo).PocEmail = value; }
        }

        public string PocEmailNotes
        {
            get { return ((HLAObjectModel)baseInfo).PocEmailNotes; }
            set { ((HLAObjectModel)baseInfo).PocEmailNotes = value; }
        }

        public string References
        {
            get { return ((HLAObjectModel)baseInfo).References; }
            set { ((HLAObjectModel)baseInfo).References = value; }
        }

        public string ReferencesNotes
        {
            get { return ((HLAObjectModel)baseInfo).ReferencesNotes; }
            set { ((HLAObjectModel)baseInfo).ReferencesNotes = value; }
        }

        public string Other
        {
            get { return ((HLAObjectModel)baseInfo).Other; }
            set { ((HLAObjectModel)baseInfo).Other = value; }
        }

        public string OtherNotes
        {
            get { return ((HLAObjectModel)baseInfo).OtherNotes; }
            set { ((HLAObjectModel)baseInfo).OtherNotes = value; }
        }

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        public HLAObjectModelAttribute()
        {
            baseInfo = new HLAObjectModel();
        }

    }
}
