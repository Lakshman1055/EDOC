using System.Runtime.Serialization;

namespace EdocApp.DataModel
{
    /// <summary>
    /// Main data model for an employee
    /// Contains getters and setters for all Employee object attributes.
    /// *********************************************************************
    /// The Employee data model takes on the DataContract attribute and each
    /// of its members take on the DataMember attribute in order to be properly 
    /// serialized across API calls.
    /// </summary>

    [DataContract]
    public class Employee
    {
        [DataMember]
        private string employeeId;

        [DataMember]
        private string employeeFirstName;

        [DataMember]
        private string employeeLastName;



        public Employee() {}

        public Employee(string employeeFirstName, string employeeLastName)
        {
            this.employeeFirstName = employeeFirstName;
            this.employeeLastName = employeeLastName;
        }

        public Employee(string employeeId, string employeeFirstName, string employeeLastName)
        {
            this.employeeId = employeeId;
            this.employeeFirstName = employeeFirstName;
            this.employeeLastName = employeeLastName;
        }

        public string getEmployeeId()
        {
            return this.employeeId;
        }

        public void setEmployeeId(string employeeId)
        {
            this.employeeId = employeeId;
        }

        public string getEmployeeFirstName()
        {
            return this.employeeFirstName;
        }

        public void setEmployeeFirstName(string employeeFirstName)
        {
            this.employeeFirstName = employeeFirstName;
        }

        public string getEmployeeLastName()
        {
            return this.employeeLastName;
        }

        public void setEmployeeLastName(string employeeLastName)
        {
            this.employeeLastName = employeeLastName;
        }

        public override string ToString()
        {
            return employeeLastName + ", " + employeeFirstName;
        }

        public override bool Equals(object obj)
        {
            if ((Employee) obj == null || this == null)
            {
                return false;
            }
            else if (((Employee) obj).employeeFirstName.ToLower().Equals(this.employeeFirstName.ToLower())
                     && ((Employee) obj).employeeLastName.ToLower().Equals(this.employeeLastName.ToLower()))
            {
                return true;
            }
            return false;
        }
    }
}
