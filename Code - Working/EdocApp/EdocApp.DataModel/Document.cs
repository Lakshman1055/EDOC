using System;
using System.Runtime.Serialization;

namespace EdocApp.DataModel
{
    /// <summary>
    /// Main data model for a document
    /// Implements IComparable interface to set rules for sorting in a List.
    /// Contains getters and setters for all Document object attributes.
    /// *********************************************************************
    /// The Document data model takes on the DataContract attribute and each
    /// of its members take on the DataMember attribute in order to be properly 
    /// serialized across API calls.
    /// </summary>

    [DataContract]
    public class Document : IComparable<Document>
    {
        [DataMember]
        private int documentId;

        [DataMember]
        private string documentName;

        [DataMember]
        private Company company;

        [DataMember]
        private Category category;

        [DataMember]
        private Employee employee;

        [DataMember]
        private int employeessn;

        [DataMember]
        private Project project;

        [DataMember]
        private string tags;

        [DataMember]
        private DateTime? documentDate;

        [DataMember]
        private DateTime? uploadedDate;

        [DataMember]
        private DateTime? updatedDate;

        [DataMember]
        private Employee updatedBy;

        public Document()
        {
        }

        public Document(string documentName)
        {
            this.documentName = documentName;
            this.uploadedDate = DateTime.Now;
            this.updatedDate = null;
        }

        public int getDocumentId()
        {
            return this.documentId;
        }

        public void setDocumentId(int documentId)
        {
            this.documentId = documentId;
        }

        public string getDocumentName()
        {
            return this.documentName;
        }

        public void setDocumentName(string documentName)
        {
            this.documentName = documentName;
        }

        public Company getCompany()
        {
            return this.company;
        }

        public void setCompany(Company company)
        {
            this.company = company;
        }

        public Category getCategory()
        {
            return this.category;
        }

        public void setCategory(Category category)
        {
            this.category = category;
        }

        public Employee getEmployee()
        {
            return this.employee;
        }

        public void setEmployee(Employee employee)
        {
            this.employee = employee;
        }

        public int getEmployeeSsn()
        {
            return this.employeessn;
        }

        public void setEmployeeSsn(int empssn)
        {
           this.employeessn = empssn;
        }

        public Project getProject()
        {
            return this.project;
        }

        public void setProject(Project project)
        {
            this.project = project;
        }

        public string getTags()
        {
            return tags;
        }

        public void setTags(string tags)
        {
            this.tags = tags;
        }

        public DateTime? getDocumentDate()
        {
            return this.documentDate;
        }

        public void setDocumentDate(DateTime documentDate)
        {
            this.documentDate = documentDate;
        }

        public DateTime? getUploadedDate()
        {
            return this.uploadedDate;
        }

        public void setUploadedDate(DateTime uploadedDate)
        {
            this.uploadedDate = uploadedDate;
        }

        public DateTime? getUpdatedDate()
        {
            return this.updatedDate;
        }

        public void setUpdatedDate(DateTime updatedDate)
        {
            this.updatedDate = updatedDate;
        }

        public Employee getUpdatedBy()
        {
            return this.updatedBy;
        }

        public void setUpdatedBy(Employee updatedBy)
        {
            this.updatedBy = updatedBy;
        }

        public int CompareTo(Document other)
        {
            if (this.getDocumentDate() > other.getDocumentDate())
            {
                return 1;
            }
            else if (this.getDocumentDate() < other.getDocumentDate())
            {
                return -1;
            }
            return 0;
        }

        public override string ToString()
        {
            string result = "Document: " + documentId + " - " + documentName + "\n"
                            + "Company: " + company.getCompanyId() + " - " + company.getCompanyName() + "\n"
                            + "Category: " + category.getCategoryId() + " - " + category.getCategoryName() +
                            "\n" + "Project: " + project.getProjectId() + "\n"
                            + "Employee: " + employee.getEmployeeId() + " - " +
                            employee.getEmployeeLastName() + ", " + employee.getEmployeeFirstName() + "\n"
                            + "Document Date: " + documentDate + "\n"
                            + "Uploaded Date: " + uploadedDate + "\n"
                            + "Updated Date: " + updatedDate + "\n"
                            + "Updated By: " + updatedBy.getEmployeeId() + " - " +
                            updatedBy.getEmployeeLastName() + ", " + updatedBy.getEmployeeFirstName() + "\n";
            return result;
        }

    }
}