using System;
using System.Collections.Generic;
using System.Text;

// Import log4net classes.
using log4net;

using Hla.Rti1516;
using Sxta.Rti1516.Reflection;

namespace Sxta.Rti1516.DynamicCompiler
{

    /// <summary> 
    /// The dynamic proxy compiler.  May be invoked as a command line application or
    /// by using the static <code>CompileProxies</code> method (from within an NAnt
    /// task, for instance).
    /// </summary>
    /// <author> Agustin Santos.
    /// </author>
    public partial class DynamicCompiler
    {
        /// <summary> 
        /// Generates Object Model Info.
        /// </summary>
        private void GenerateObjectModel(System.IO.StreamWriter stream, string fileName)
        {
            if (descriptorManager.ObjectModelInformation == null)
                return;

            int indentLevel = 0;
            string indentStr = GenerateIndentString(indentLevel);
            System.IO.StreamWriter sw;
            if (stream == null)
            {
                String qualifiedTypeName = packagePrefix + fileName;
                String path = qualifiedTypeName.Replace('.', '/') + ".cs";
                System.IO.FileInfo sourceFile = new System.IO.FileInfo(targetDirectory.FullName + "\\" + path);
                System.IO.Directory.CreateDirectory(new System.IO.FileInfo(sourceFile.DirectoryName).FullName);

                System.IO.FileStream fos = new System.IO.FileStream(sourceFile.FullName, System.IO.FileMode.Create);
                sw = new System.IO.StreamWriter(fos);

                sw.WriteLine(indentStr + "using System;");
                sw.WriteLine(indentStr + "using System.Reflection;");
                sw.WriteLine(indentStr + "using Sxta.Rti1516.Reflection;");
            }
            else
            {
                sw = stream;
            }

            GenerateHLAObjectModelAttribute(sw, indentLevel, descriptorManager.ObjectModelInformation);
        }

        /// <summary> 
        /// Generates a <code>HLAObjectModelAttribute</code>.
        /// </summary>
        private void GenerateHLAObjectModelAttribute(System.IO.StreamWriter ps, int indentLevel,  HLAObjectModel objectModelInfo)
        {
            string indentStr = GenerateIndentString(indentLevel);
            string newLine = "," + Environment.NewLine + indentStr + Spacer("assembly: [HLAObjectModelAttribute(");
            ps.Write(indentStr + "[assembly: HLAObjectModelAttribute(Name = \"" + objectModelInfo.Name + "\"");
            if (!String.IsNullOrEmpty(objectModelInfo.NameNotes))
            {
                ps.Write(newLine);
                ps.Write("NameNotes = \"" + objectModelInfo.NameNotes + "\"");
            }
            if (!String.IsNullOrEmpty(objectModelInfo.DTDversion))
            {
                ps.Write(newLine);
                ps.Write("DTDversion = \"" + objectModelInfo.DTDversion + "\"");
            }
            ps.Write(newLine);
            ps.Write("ObjectModelType = " +objectModelInfo.ObjectModelType.GetType() + "." + objectModelInfo.ObjectModelType);

            if (!String.IsNullOrEmpty(objectModelInfo.TypeNotes))
            {
                ps.Write(newLine);
                ps.Write("TypeNotes = \"" + objectModelInfo.TypeNotes + "\"");
            }
            if (!String.IsNullOrEmpty(objectModelInfo.Version))
            {
                ps.Write(newLine);
                ps.Write("Version = \"" + objectModelInfo.Version + "\"");
            }
            if (!String.IsNullOrEmpty(objectModelInfo.VersionNotes))
            {
                ps.Write(newLine);
                ps.Write("VersionNotes = \"" + objectModelInfo.VersionNotes + "\"");
            }
            if (!String.IsNullOrEmpty(objectModelInfo.Date))
            {
                ps.Write(newLine);
                ps.Write("Date = \"" + objectModelInfo.Date + "\"");
            }
            if (!String.IsNullOrEmpty(objectModelInfo.DateNotes))
            {
                ps.Write(newLine);
                ps.Write("DateNotes = \"" + objectModelInfo.DateNotes + "\"");
            }
            if (!String.IsNullOrEmpty(objectModelInfo.Purpose))
            {
                ps.Write(newLine);
                ps.Write("Purpose = \"" + objectModelInfo.Purpose + "\"");
            }
            if (!String.IsNullOrEmpty(objectModelInfo.PurposeNotes))
            {
                ps.Write(newLine);
                ps.Write("PurposeNotes = \"" + objectModelInfo.PurposeNotes + "\"");
            }
            if (!String.IsNullOrEmpty(objectModelInfo.AppDomain))
            {
                ps.Write(newLine);
                ps.Write("AppDomain = \"" + objectModelInfo.AppDomain + "\"");
            }
            if (!String.IsNullOrEmpty(objectModelInfo.AppDomainNotes))
            {
                ps.Write(newLine);
                ps.Write("AppDomainNotes = \"" + objectModelInfo.AppDomainNotes + "\"");
            }
            if (!String.IsNullOrEmpty(objectModelInfo.Sponsor))
            {
                ps.Write(newLine);
                ps.Write("Sponsor = \"" + objectModelInfo.Sponsor + "\"");
            }
            if (!String.IsNullOrEmpty(objectModelInfo.SponsorNotes))
            {
                ps.Write(newLine);
                ps.Write("SponsorNotes = \"" + objectModelInfo.SponsorNotes + "\"");
            }

            if (!String.IsNullOrEmpty(objectModelInfo.PocName))
            {
                ps.Write(newLine);
                ps.Write("PocName = \"" + objectModelInfo.PocName + "\"");
            }
            if (!String.IsNullOrEmpty(objectModelInfo.PocNameNotes))
            {
                ps.Write(newLine);
                ps.Write("PocNameNotes = \"" + objectModelInfo.PocNameNotes + "\"");
            }
            if (!String.IsNullOrEmpty(objectModelInfo.PocOrg))
            {
                ps.Write(newLine);
                ps.Write("PocOrg = \"" + objectModelInfo.PocOrg + "\"");
            }
            if (!String.IsNullOrEmpty(objectModelInfo.PocOrgNotes))
            {
                ps.Write(newLine);
                ps.Write("PocOrgNotes = \"" + objectModelInfo.PocOrgNotes + "\"");
            }
            if (!String.IsNullOrEmpty(objectModelInfo.PocPhone))
            {
                ps.Write(newLine);
                ps.Write("PocPhone = \"" + objectModelInfo.PocPhone + "\"");
            }
            if (!String.IsNullOrEmpty(objectModelInfo.PocPhoneNotes))
            {
                ps.Write(newLine);
                ps.Write("PocPhoneNotes = \"" + objectModelInfo.PocPhoneNotes + "\"");
            }
            if (!String.IsNullOrEmpty(objectModelInfo.PocEmail))
            {
                ps.Write(newLine);
                ps.Write("PocEmail = \"" + objectModelInfo.PocEmail + "\"");
            }
            if (!String.IsNullOrEmpty(objectModelInfo.PocEmailNotes))
            {
                ps.Write(newLine);
                ps.Write("PocEmailNotes = \"" + objectModelInfo.PocEmailNotes + "\"");
            }
            if (!String.IsNullOrEmpty(objectModelInfo.References))
            {
                ps.Write(newLine);
                ps.Write("References = \"" + objectModelInfo.References + "\"");
            }
            if (!String.IsNullOrEmpty(objectModelInfo.ReferencesNotes))
            {
                ps.Write(newLine);
                ps.Write("ReferencesNotes = \"" + objectModelInfo.ReferencesNotes + "\"");
            }
            if (!String.IsNullOrEmpty(objectModelInfo.Other))
            {
                ps.Write(newLine);
                ps.Write("Other = \"" + objectModelInfo.Other + "\"");
            }
            if (!String.IsNullOrEmpty(objectModelInfo.OtherNotes))
            {
                ps.Write(newLine);
                ps.Write("OtherNotes = \"" + objectModelInfo.OtherNotes + "\"");
            }
            ps.WriteLine(")]");
        }
    }
}
