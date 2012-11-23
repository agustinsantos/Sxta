using System;
using System.Reflection;
using System.Collections.Generic;

namespace Sxta.Core.Plugins
{
	/// <summary>
	/// Indicates that field should be treated as a xml attribute for the codon or condition.
	/// The field is treated as a <code>List<sometype></code>
	/// </summary>
	[AttributeUsage(AttributeTargets.Field, Inherited=true)]
	public class XmlMemberGenericListAttribute : Attribute
	{
        string name;
        string entryName;
        bool isRequired;
		
		/// <summary>
		/// Constructs a new instance.
		/// </summary>
        public XmlMemberGenericListAttribute(string name, string entryname)
		{
            this.name = name;
            this.entryName = entryname;
            isRequired = false;
		}
				
		/// <summary>
		/// The name of the attribute.
		/// </summary>
		public string Name {
			get {
				return name;
			}
			set {
				name = value;
			}
		}

        /// <summary>
        /// The name of the attribute of each entry.
        /// </summary>
        public string EntryName
        {
            get
            {
                return entryName;
            }
            set
            {
                entryName = value;
            }
        }

		/// <summary>
		/// returns <code>true</code> if this attribute is required.
		/// </summary>
		public bool IsRequired {
			get {
				return isRequired;
			}
			set {
				isRequired = value;
			}
		}
	}
}
