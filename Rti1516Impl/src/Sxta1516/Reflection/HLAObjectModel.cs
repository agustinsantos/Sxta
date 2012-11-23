using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Sxta.Rti1516.Reflection
{

    /// <summary>
    /// Represents an HLA object Model .
    /// </summary>
    /// <code>
    /// <xsd:attribute name="DTDversion"
    ///               type="xsd:string"
    ///               fixed="1516.2"/>
    ///<xsd:attribute name="name"
    ///               type="xsd:string"
    ///               use="required"/>
    ///<xsd:attribute name="nameNotes"
    ///               type="xsd:IDREFS"/>
    ///<xsd:attribute name="type"
    ///               type="objectModelType"
    ///               use="required"/>
    ///<xsd:attribute name="typeNotes"
    ///               type="xsd:IDREFS"/>
    ///<xsd:attribute name="version"
    ///               type="xsd:string"/>
    ///<xsd:attribute name="versionNotes"
    ///               type="xsd:IDREFS"/>
    ///<xsd:attribute name="date"
    ///               type="xsd:string"/>
    ///<xsd:attribute name="dateNotes"
    ///               type="xsd:IDREFS"/>
    ///<xsd:attribute name="purpose"
    ///               type="xsd:string"/>
    ///<xsd:attribute name="purposeNotes"
    ///               type="xsd:IDREFS"/>
    ///<xsd:attribute name="appDomain"
    ///               type="xsd:string"/>
    ///<xsd:attribute name="appDomainNotes"
    ///               type="xsd:IDREFS"/>
    ///<xsd:attribute name="sponsor"
    ///               type="xsd:string"/>
    ///<xsd:attribute name="sponsorNotes"
    ///               type="xsd:IDREFS"/>
    ///<xsd:attribute name="pocName"
    ///               type="xsd:string"/>
    ///<xsd:attribute name="pocNameNotes"
    ///               type="xsd:IDREFS"/>
    ///<xsd:attribute name="pocOrg"
    ///               type="xsd:string"/>
    ///<xsd:attribute name="pocOrgNotes"
    ///               type="xsd:IDREFS"/>
    ///<xsd:attribute name="pocPhone"
    ///               type="xsd:string"/>
    ///<xsd:attribute name="pocPhoneNotes"
    ///               type="xsd:IDREFS"/>
    ///<xsd:attribute name="pocEmail"
    ///               type="xsd:string"/>
    ///<xsd:attribute name="pocEmailNotes"
    ///               type="xsd:IDREFS"/>
    ///<xsd:attribute name="references"
    ///               type="xsd:string"/>
    ///<xsd:attribute name="referencesNotes"
    ///               type="xsd:IDREFS"/>
    ///<xsd:attribute name="other"
    ///               type="xsd:string"/>
    ///<xsd:attribute name="otherNotes"
    ///               type="xsd:IDREFS"/>
    /// </code>
    [HLAObjectClassAttribute(Name = "HLAObjectModel",
                             Sharing = HLAsharingType.PublishSubscribe,
                            Semantics = "Represents an HLA object Model .")]
    public class HLAObjectModel : HLAreflection
    {
        public string DTDversion
        {
            get { return DTDversion_; }
            set { DTDversion_ = value; }
        }


        ///               use="required"/>
        public HLAObjectModelType ObjectModelType
        {
            get { return type; }
            set { type = value; }
        }

        public string TypeNotes
        {
            get { return typeNotes; }
            set { typeNotes = value; }
        }

        public string Version
        {
            get { return version; }
            set { version = value; }
        }

        public string VersionNotes
        {
            get { return versionNotes; }
            set { versionNotes = value; }
        }

        public string Date
        {
            get { return date; }
            set { date = value; }
        }

        public string DateNotes
        {
            get { return dateNotes; }
            set { dateNotes = value; }
        }

        public string Purpose
        {
            get { return purpose; }
            set { purpose = value; }
        }

        public string PurposeNotes
        {
            get { return purposeNotes; }
            set { purposeNotes = value; }
        }

        public string AppDomain
        {
            get { return appDomain; }
            set { appDomain = value; }
        }

        public string AppDomainNotes
        {
            get { return appDomainNotes; }
            set { appDomainNotes = value; }
        }

        public string Sponsor
        {
            get { return sponsor; }
            set { sponsor = value; }
        }

        public string SponsorNotes
        {
            get { return sponsorNotes; }
            set { sponsorNotes = value; }
        }

        public string PocName
        {
            get { return pocName; }
            set { pocName = value; }
        }

        public string PocNameNotes
        {
            get { return pocNameNotes; }
            set { pocNameNotes = value; }
        }

        public string PocOrg
        {
            get { return pocOrg; }
            set { pocOrg = value; }
        }

        public string PocOrgNotes
        {
            get { return pocOrgNotes; }
            set { pocOrgNotes = value; }
        }

        public string PocPhone
        {
            get { return pocPhone; }
            set { pocPhone = value; }
        }

        public string PocPhoneNotes
        {
            get { return pocPhoneNotes; }
            set { pocPhoneNotes = value; }
        }

        public string PocEmail
        {
            get { return pocEmail; }
            set { pocEmail = value; }
        }

        public string PocEmailNotes
        {
            get { return pocEmailNotes; }
            set { pocEmailNotes = value; }
        }

        public string References
        {
            get { return references; }
            set { references = value; }
        }

        public string ReferencesNotes
        {
            get { return referencesNotes; }
            set { referencesNotes = value; }
        }

        public string Other
        {
            get { return other; }
            set { other = value; }
        }

        public string OtherNotes
        {
            get { return otherNotes; }
            set { otherNotes = value; }
        }

        public HLAObjectModel()
            : base()
        { }
        public HLAObjectModel(System.Xml.XmlElement documentElement)
            : base(documentElement)
        {
            DTDversion = documentElement.GetAttribute("DTDversion");

            //               use="required"/>
            ObjectModelType = (HLAObjectModelType)Enum.Parse(typeof(HLAObjectModelType), documentElement.GetAttribute("type"));
            TypeNotes = documentElement.GetAttribute("typeNotes");
            Version = documentElement.GetAttribute("version");
            VersionNotes = documentElement.GetAttribute("versionNotes");
            Date = documentElement.GetAttribute("date");
            DateNotes = documentElement.GetAttribute("dateNotes");
            Purpose = documentElement.GetAttribute("purpose");
            PurposeNotes = documentElement.GetAttribute("purposeNotes");
            AppDomain = documentElement.GetAttribute("appDomain");
            AppDomainNotes = documentElement.GetAttribute("appDomainNotes");
            Sponsor = documentElement.GetAttribute("sponsor");
            SponsorNotes = documentElement.GetAttribute("sponsorNotes");
            PocName = documentElement.GetAttribute("pocName");
            PocNameNotes = documentElement.GetAttribute("pocNameNotes");
            PocOrg = documentElement.GetAttribute("pocOrg");
            PocOrgNotes = documentElement.GetAttribute("pocOrgNotes");
            PocPhone = documentElement.GetAttribute("pocPhone");
            PocPhoneNotes = documentElement.GetAttribute("pocPhoneNotes");
            PocEmail = documentElement.GetAttribute("pocEmail");
            PocEmailNotes = documentElement.GetAttribute("pocEmailNotes");
            References = documentElement.GetAttribute("references");
            ReferencesNotes = documentElement.GetAttribute("referencesNotes");
            Other = documentElement.GetAttribute("other");
            OtherNotes = documentElement.GetAttribute("otherNotes");
        }

        protected string DTDversion_;

        ///               use="required"/>
        protected HLAObjectModelType type;
        protected string typeNotes;
        protected string version;
        protected string versionNotes;
        protected string date;
        protected string dateNotes;
        protected string purpose;
        protected string purposeNotes;
        protected string appDomain;
        protected string appDomainNotes;
        protected string sponsor;
        protected string sponsorNotes;
        protected string pocName;
        protected string pocNameNotes;
        protected string pocOrg;
        protected string pocOrgNotes;
        protected string pocPhone;
        protected string pocPhoneNotes;
        protected string pocEmail;
        protected string pocEmailNotes;
        protected string references;
        protected string referencesNotes;
        protected string other;
        protected string otherNotes;
    }
}
