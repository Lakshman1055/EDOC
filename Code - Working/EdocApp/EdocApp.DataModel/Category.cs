using System;
using System.Runtime.Serialization;

namespace EdocApp.DataModel
{
    /// <summary>
    /// Main data model for a category
    /// Contains getters and setters for all Category object attributes.
    /// *********************************************************************
    /// The Category data model takes on the DataContract attribute and each
    /// of its members take on the DataMember attribute in order to be properly 
    /// serialized across API calls.
    /// </summary>

    [DataContract]
    public class Category
    {
        [DataMember]
        private int categoryId;

        [DataMember]
        private string categoryName;

        [DataMember] 
        private int parentId;

        [DataMember]
        private bool hasChildren;

        public Category() {}

        public Category(int categoryId, string categoryName)
        {
            this.categoryId = categoryId;
            this.categoryName = categoryName;
        }

        public Category(int categoryId, string categoryName, int parentId, bool hasChildren)
        {
            this.categoryId = categoryId;
            this.categoryName = categoryName;
            this.parentId = parentId;
            this.hasChildren = hasChildren;
        }

        public int getCategoryId()
        {
            return this.categoryId;
        }

        public void setCategoryId(int categoryId)
        {
            this.categoryId = categoryId;
        }

        public string getCategoryName()
        {
            return this.categoryName;
        }

        public void setCategoryName(string categoryName)
        {
            this.categoryName = this.categoryName;
        }

        public int getParentId()
        {
            return this.parentId;
        }

        public void setParentId(int parentId)
        {
            this.parentId = parentId;
        }

        public bool getHasChildren()
        {
            return this.hasChildren;
        }

        public void setHasChildren(bool hasChildren)
        {
            this.hasChildren = hasChildren;
        }

        public override string ToString()
        {
            return this.categoryName;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj == DBNull.Value || this == null
                || this.categoryName == null || ((Category) obj).categoryName == null)
            {
                return false;
            }
            else if (((Category) obj).categoryName.Equals(this.categoryName) && ((Category) obj).categoryId == this.categoryId)
            {
                return true;
            }
            return false;
        }
    }
}
