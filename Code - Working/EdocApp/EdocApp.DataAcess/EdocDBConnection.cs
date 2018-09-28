using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;


namespace EdocApp.DataAccess
{
    /// <summary>
    /// Class that performs all four CRUD operations on the stored procedure
    /// with the array of parameters passed into it.
    /// </summary>
    public class EdocDBConnection
    {
        private SqlDataAdapter adapter;
        private SqlConnection conn;

        /// <summary>
        /// Constructor for base DB connection object
        /// Executes all the CRUD operations on the EDOC database
        /// </summary>
        public EdocDBConnection()
        {
            adapter = new SqlDataAdapter();
            
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings
                ["EdocDBConnectionString"].ConnectionString);
            openConnection();
        }

        /// <summary>
        /// Opens the connection if it is closed or broken
        /// </summary>
        /// <returns>A SqlConnection object</returns>
        private SqlConnection openConnection()
        {
            if (conn.State == ConnectionState.Closed || 
                conn.State == ConnectionState.Broken)
            {
                conn.Open();
              
            }
            return conn;
        }

        /// <summary>
        /// Executes a select (READ) query on the database
        /// </summary>
        /// <param name="query">Name of stored procedure to be executed</param>
        /// <param name="sqlParameters">Array of parameters to be passed to the stored proc</param>
        /// <returns>A data table object with query results</returns>
        public DataTable executeSelectQuery(string query, SqlParameter[] sqlParameters)
        {
            SqlCommand command = new SqlCommand();
            DataTable dataTable = new DataTable();
            dataTable = null;
            DataSet set = new DataSet();
            try
            {
             
                command.Connection = openConnection();
                command.CommandText = query;
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddRange(sqlParameters);
             
                command.ExecuteNonQuery();              
                adapter.SelectCommand = command;
                adapter.Fill(set);
                dataTable = set.Tables[0];
                
              
               
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Exception: " + ex.StackTrace.ToString() + "\n");
                return null;
            }
            finally
            {
                command.Connection.Close();
            }
            return dataTable;
        }

        /// <summary>
        /// Executes an insert (CREATE) query on the database
        /// </summary>
        /// <param name="query">Name of stored procedure to be executed</param>
        /// <param name="sqlParameters">Array of parameters to be passed to the stored proc</param>
        /// <returns>A data table object with query results</returns>
        public bool executeInsertQuery(string query, SqlParameter[] sqlParameters)
        {
            SqlCommand command = new SqlCommand();
            try
            {
                command.Connection = openConnection();
                command.CommandText = query;
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddRange(sqlParameters);
                adapter.InsertCommand = command;
                adapter.InsertCommand.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Exception: " + ex.StackTrace.ToString() + "\n");
                return false;
            }
            finally
            {
                command.Connection.Close();
            }
            return true;
        }

        /// <summary>
        /// Executes an update (UPDATE) query on the database
        /// </summary>
        /// <param name="query">Name of stored procedure to be executed</param>
        /// <param name="sqlParameters">Array of parameters to be passed to the stored proc</param>
        /// <returns>A data table object with query results</returns>
        public bool executeUpdateQuery(string query, SqlParameter[] sqlParameters)
        {
            SqlCommand command = new SqlCommand();
            try
            {
                command.Connection = openConnection();
                command.CommandText = query;
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddRange(sqlParameters);
                adapter.UpdateCommand = command;
                adapter.UpdateCommand.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Exception: " + ex.StackTrace.ToString() + "\n");
                return false;
            }
            finally
            {
                command.Connection.Close();
            }
            return true;
        }

        /// <summary>
        /// Executes an delete (DELETE) query on the database
        /// </summary>
        /// <param name="query">Name of stored procedure to be executed</param>
        /// <param name="sqlParameters">Array of parameters to be passed to the stored proc</param>
        /// <returns>A data table object with query results</returns>
        public bool executeDeleteQuery(string query, SqlParameter[] sqlParameters)
        {
            SqlCommand command = new SqlCommand();
            try
            {
                command.Connection = openConnection();
                command.CommandText = query;
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddRange(sqlParameters);
                adapter.DeleteCommand = command;
                command.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Exception: " + ex.StackTrace.ToString() + "\n");
                return false;
            }
            finally
            {
                command.Connection.Close();
            }
            return true;
        }

    }
}
