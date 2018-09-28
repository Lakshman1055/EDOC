using System;
using System.Runtime.Serialization;

namespace EdocApp.DataModel
{
    /// <summary>
    /// Main data model for a company
    /// Contains getters and setters for all Company object attributes.
    /// *********************************************************************
    /// The Company data model takes on the DataContract attribute and each
    /// of its members take on the DataMember attribute in order to be properly 
    /// serialized across API calls.
    /// </summary>

    [DataContract]
    public class Company
    {
        [DataMember]
        private int companyId;

        [DataMember]
        private string companyName;

        public Company() {}

        public Company(int companyId, string companyName)
        {
            this.companyId = companyId;
            this.companyName = companyName;
        }

        public int getCompanyId()
        {
            return this.companyId;
        }

        public void setCompanyId(int companyId)
        {
            this.companyId = companyId;
        }

        public string getCompanyName()
        {
            return this.companyName;
        }

        public void setCompanyName(string companyName)
        {
            this.companyName = companyName;
        }

        public override string ToString()
        {
            return this.companyName;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj == DBNull.Value || this == null
                || this.companyName == null || ((Company)obj).companyName == null)
            {
                return false;
            }
            else if (((Company)obj).companyName.Equals(this.companyName))
            {
                return true;
            }
            return false;
        }
    }
}
