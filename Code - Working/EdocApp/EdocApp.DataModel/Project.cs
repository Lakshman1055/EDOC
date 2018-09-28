using System;
using System.Runtime.Serialization;

namespace EdocApp.DataModel
{
    /// <summary>
    /// Main data model for a project
    /// Contains getters and setters for all Project object attributes.
    /// *********************************************************************
    /// The Project data model takes on the DataContract attribute and each
    /// of its members take on the DataMember attribute in order to be properly 
    /// serialized across API calls.
    /// </summary>

    [DataContract]
    public class Project
    {
        [DataMember]
        private int projectId;

        [DataMember]
        private string projectName;

        public Project()
        {
        }

        public Project(int projectId, string projectName)
        {
            this.projectId = projectId;
            this.projectName = projectName;
        }

        public int getProjectId()
        {
            return this.projectId;
        }

        public void setProjectId(int projectId)
        {
            this.projectId = projectId;
        }

        public string getProjectName()
        {
            return this.projectName;
        }

        public void setProjectName(string projectName)
        {
            this.projectName = projectName;
        }

        public override string ToString()
        {
            return this.projectName;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj == DBNull.Value || this == null
                || this.projectName == null || ((Project)obj).projectName == null)
            {
                return false;
            }

            else if (((Project)obj).projectName.Equals(this.projectName))
            {
                return true;
            }
            return false;
        }
    }
}
