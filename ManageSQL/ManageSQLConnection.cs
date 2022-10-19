using System;
using System.Collections.Generic;
using System.Data;
using MurasoliAPI.Controllers;
using Npgsql;

namespace MurasoliAPI.ManageSQL
{
    public class ManageSQLConnection
    {
        NpgsqlConnection sqlConnection = new NpgsqlConnection();
        NpgsqlCommand sqlCommand = new NpgsqlCommand();

        NpgsqlDataAdapter dataAdapter;
        /// <summary>
        /// Gets values from 
        /// </summary>
        /// <param name="procedureName"></param>
        /// <returns></returns>
        public DataSet GetDataSetValues(string procedureName)
        {
            sqlConnection = new NpgsqlConnection(GlobalVariable.ConnectionStringForPostgreSQL);
            DataSet ds = new DataSet();
            sqlCommand = new NpgsqlCommand();
            try
            {
                if (sqlConnection.State == 0)
                {
                    sqlConnection.Open();
                }
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = procedureName;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                dataAdapter = new NpgsqlDataAdapter(sqlCommand);
                dataAdapter.Fill(ds);
                return ds;
            }
            finally
            {
                sqlConnection.Close();
                sqlCommand.Dispose();
                ds.Dispose();
                dataAdapter = null;
            }

        }

        public DataSet GetDataSetValues(string procedureName, List<KeyValuePair<string, string>> parameterList)
        {
            sqlConnection = new NpgsqlConnection(GlobalVariable.ConnectionStringForPostgreSQL);
            DataSet ds = new DataSet();
            sqlCommand = new NpgsqlCommand();
            try
            {
                if (sqlConnection.State == 0)
                {
                    sqlConnection.Open();
                }
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = procedureName;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                foreach (KeyValuePair<string, string> keyValuePair in parameterList)
                {
                    sqlCommand.Parameters.AddWithValue(keyValuePair.Key, keyValuePair.Value);
                }
                sqlCommand.CommandTimeout = 360;
                dataAdapter = new NpgsqlDataAdapter(sqlCommand);
                dataAdapter.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                //AuditLog.WriteError(ex.Message);
                throw ex;
            }
            finally
            {
                sqlConnection.Close();
                sqlCommand.Dispose();
                ds.Dispose();
                dataAdapter = null;
            }
        }

        public bool UpdateValues(string procedureName, List<KeyValuePair<string, string>> parameterList)
        {
            sqlConnection = new NpgsqlConnection(GlobalVariable.ConnectionStringForPostgreSQL);
            DataSet ds = new DataSet();
            sqlCommand = new NpgsqlCommand();
            try
            {
                if (sqlConnection.State == 0)
                {
                    sqlConnection.Open();
                }
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = procedureName;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                foreach (KeyValuePair<string, string> keyValuePair in parameterList)
                {
                    sqlCommand.Parameters.AddWithValue(keyValuePair.Key, keyValuePair.Value);
                }
                sqlCommand.ExecuteNonQuery();
                //  AuditLog.WriteError(affected.ToString());
                return true;
            }
            catch (Exception ex)
            {
                // AuditLog.WriteError(ex.Message + " : " + ex.StackTrace);
                return false;

            }
            finally
            {
                sqlConnection.Close();
                sqlCommand.Dispose();
                ds.Dispose();
                dataAdapter = null;
            }
        }

        public bool InsertData(string procedureName, List<KeyValuePair<string, string>> parameterList)
        {

            sqlConnection = new NpgsqlConnection(GlobalVariable.ConnectionStringForPostgreSQL);
            DataSet ds = new DataSet();
            sqlCommand = new NpgsqlCommand();
            try
            {
                if (sqlConnection.State == 0)
                {
                    sqlConnection.Open();
                }
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = procedureName;
                sqlCommand.CommandType = CommandType.Text;
                foreach (KeyValuePair<string, string> keyValuePair in parameterList)
                {
                    sqlCommand.Parameters.AddWithValue(keyValuePair.Key, keyValuePair.Value);
                }
                sqlCommand.ExecuteNonQuery();
                return true;

            }
            catch (Exception ex)
            {
                //AuditLog.WriteError(ex.Message);
                Console.WriteLine(ex);
                return false;
            }
            finally
            {
                sqlConnection.Close();
                sqlCommand.Dispose();
                ds.Dispose();
                dataAdapter = null;
            }
        }

        public bool insertUserMaster(UsersEntity UsersEntity)
        {

            sqlConnection = new NpgsqlConnection(GlobalVariable.ConnectionStringForPostgreSQL);
            DataSet ds = new DataSet();
            sqlCommand = new NpgsqlCommand();
            try
            {
                if (sqlConnection.State == 0)
                {
                    sqlConnection.Open();
                }
                sqlCommand.Connection = sqlConnection;
                //sqlCommand.CommandText = "insert into users (username,emailid,password,encryptedpassword,roleid,flag) values(@username,@emailid,@password,@encryptedpassword,@roleid,@flag)";
                sqlCommand.CommandText = "call users(@username,@emailid,@password,@encryptedpassword,@roleid,@Flag)";
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Parameters.AddWithValue("@username", UsersEntity.username);
                sqlCommand.Parameters.AddWithValue("@emailid", UsersEntity.emailid);
                sqlCommand.Parameters.AddWithValue("@password", UsersEntity.password);
                sqlCommand.Parameters.AddWithValue("@encryptedpassword", UsersEntity.encryptedpassword);
                sqlCommand.Parameters.AddWithValue("@roleid", UsersEntity.roleid);
                sqlCommand.Parameters.AddWithValue("@Flag", UsersEntity.Flag);
                sqlCommand.ExecuteNonQuery();
       
                return true;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlConnection.Close();
                sqlCommand.Dispose();
                ds.Dispose();
                dataAdapter = null;
            }
        }

        //statemaster
        public bool insertstatemaster(StateMasterEntity StateMasterEntity)
        {

            sqlConnection = new NpgsqlConnection(GlobalVariable.ConnectionStringForPostgreSQL);
            DataSet ds = new DataSet();
            sqlCommand = new NpgsqlCommand();
            try
            {
                if (sqlConnection.State == 0)
                {
                    sqlConnection.Open();
                }
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = "call insertstatemaster(@stateid,@statename,@Flag)";
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Parameters.AddWithValue("@stateid", StateMasterEntity.stateid);
                sqlCommand.Parameters.AddWithValue("@statename", StateMasterEntity.statename);
                sqlCommand.Parameters.AddWithValue("@Flag", StateMasterEntity.Flag);
                sqlCommand.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlConnection.Close();
                sqlCommand.Dispose();
                ds.Dispose();
                dataAdapter = null;
            }
        }

        public bool UpdateStateMaster(UpdateStateMasterEntity UpdateStateMasterEntity)
        {

            sqlConnection = new NpgsqlConnection(GlobalVariable.ConnectionStringForPostgreSQL);
            DataSet ds = new DataSet();
            sqlCommand = new NpgsqlCommand();
            try
            {
                if (sqlConnection.State == 0)
                {
                    sqlConnection.Open();
                }
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = "call updatestatemaster(@u_statecode,@u_stateid,@u_statename,@flag)";
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Parameters.AddWithValue("@u_statecode", UpdateStateMasterEntity.u_statecode);
                sqlCommand.Parameters.AddWithValue("@u_stateid", UpdateStateMasterEntity.u_stateid);
                sqlCommand.Parameters.AddWithValue("@u_statename", UpdateStateMasterEntity.u_statename);
                sqlCommand.Parameters.AddWithValue("@flag", UpdateStateMasterEntity.flag);
                sqlCommand.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlConnection.Close();
                sqlCommand.Dispose();
                ds.Dispose();
                dataAdapter = null;
            }
        }

        public DataSet GetStateMaster()
        {
            sqlConnection = new NpgsqlConnection(GlobalVariable.ConnectionStringForPostgreSQL);
            DataSet ds = new DataSet();
            sqlCommand = new NpgsqlCommand();
            try
            {
                if (sqlConnection.State == 0)
                {
                    sqlConnection.Open();
                }
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = "select * from statemaster";
                sqlCommand.CommandType = CommandType.Text;
                dataAdapter = new NpgsqlDataAdapter(sqlCommand);
                dataAdapter.Fill(ds);
                return ds;
            }
            finally
            {
                sqlConnection.Close();
                sqlCommand.Dispose();
                ds.Dispose();
                dataAdapter = null;
            }
        }

        //districtmaster
        public bool insertdistrictmaster(DistrictMasterEntity DistrictMasterEntity)
        {

            sqlConnection = new NpgsqlConnection(GlobalVariable.ConnectionStringForPostgreSQL);
            DataSet ds = new DataSet();
            sqlCommand = new NpgsqlCommand();
            try
            {
                if (sqlConnection.State == 0)
                {
                    sqlConnection.Open();
                }
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = "call insertdistrictmaster(@districtid,@districtname,@flag)";
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Parameters.AddWithValue("@districtid", DistrictMasterEntity.districtid);
                sqlCommand.Parameters.AddWithValue("@districtname", DistrictMasterEntity.districtname);
                sqlCommand.Parameters.AddWithValue("@flag", DistrictMasterEntity.flag);
                sqlCommand.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlConnection.Close();
                sqlCommand.Dispose();
                ds.Dispose();
                dataAdapter = null;
            }
        }

        public bool UpdateDistrictMaster(UpdateDistrictMasterEntity UpdateDistrictMasterEntity)
        {

            sqlConnection = new NpgsqlConnection(GlobalVariable.ConnectionStringForPostgreSQL);
            DataSet ds = new DataSet();
            sqlCommand = new NpgsqlCommand();
            try
            {
                if (sqlConnection.State == 0)
                {
                    sqlConnection.Open();
                }
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = "call updatedistrictmaster(@u_districtcode,@u_districtid,@u_districtname,@flag)";
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Parameters.AddWithValue("@u_districtcode", UpdateDistrictMasterEntity.u_districtcode);
                sqlCommand.Parameters.AddWithValue("@u_districtid", UpdateDistrictMasterEntity.u_districtid);
                sqlCommand.Parameters.AddWithValue("@u_districtname", UpdateDistrictMasterEntity.u_districtname);
                sqlCommand.Parameters.AddWithValue("@flag", UpdateDistrictMasterEntity.flag);
                sqlCommand.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlConnection.Close();
                sqlCommand.Dispose();
                ds.Dispose();
                dataAdapter = null;
            }
        }

        public DataSet GetDistrictMaster()
        {
            sqlConnection = new NpgsqlConnection(GlobalVariable.ConnectionStringForPostgreSQL);
            DataSet ds = new DataSet();
            sqlCommand = new NpgsqlCommand();
            try
            {
                if (sqlConnection.State == 0)
                {
                    sqlConnection.Open();
                }
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = "select * from districtmaster";
                sqlCommand.CommandType = CommandType.Text;
                dataAdapter = new NpgsqlDataAdapter(sqlCommand);
                dataAdapter.Fill(ds);
                return ds;
            }
            finally
            {
                sqlConnection.Close();
                sqlCommand.Dispose();
                ds.Dispose();
                dataAdapter = null;
            }
        }

        //countrymaster
        public bool insertcountrymaster(CountryMasterEntity CountryMasterEntity)
        {

            sqlConnection = new NpgsqlConnection(GlobalVariable.ConnectionStringForPostgreSQL);
            DataSet ds = new DataSet();
            sqlCommand = new NpgsqlCommand();
            try
            {
                if (sqlConnection.State == 0)
                {
                    sqlConnection.Open();
                }
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = "call insertcountrymaster(@countrycode,@countryid,@countryname,@flag)";
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Parameters.AddWithValue("@countrycode", CountryMasterEntity.countrycode);
                sqlCommand.Parameters.AddWithValue("@countryid", CountryMasterEntity.countryid);
                sqlCommand.Parameters.AddWithValue("@countryname", CountryMasterEntity.countryname);
                sqlCommand.Parameters.AddWithValue("@flag", CountryMasterEntity.flag);
                sqlCommand.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlConnection.Close();
                sqlCommand.Dispose();
                ds.Dispose();
                dataAdapter = null;
            }
        }

        public bool UpdateCountryMaster(UpdateCountryMasterEntity UpdateCountryMasterEntity)
        {

            sqlConnection = new NpgsqlConnection(GlobalVariable.ConnectionStringForPostgreSQL);
            DataSet ds = new DataSet();
            sqlCommand = new NpgsqlCommand();
            try
            {
                if (sqlConnection.State == 0)
                {
                    sqlConnection.Open();
                }
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = "call updatecountrymaster(@u_countrycode,@u_countryid,@u_countryname,@flag)";
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Parameters.AddWithValue("@u_countrycode", UpdateCountryMasterEntity.u_countrycode);
                sqlCommand.Parameters.AddWithValue("@u_countryid", UpdateCountryMasterEntity.u_countryid);
                sqlCommand.Parameters.AddWithValue("@u_countryname", UpdateCountryMasterEntity.u_countryname);
                sqlCommand.Parameters.AddWithValue("@flag", UpdateCountryMasterEntity.flag);
                sqlCommand.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlConnection.Close();
                sqlCommand.Dispose();
                ds.Dispose();
                dataAdapter = null;
            }
        }

        public DataSet GetCountryMaster()
        {
            sqlConnection = new NpgsqlConnection(GlobalVariable.ConnectionStringForPostgreSQL);
            DataSet ds = new DataSet();
            sqlCommand = new NpgsqlCommand();
            try
            {
                if (sqlConnection.State == 0)
                {
                    sqlConnection.Open();
                }
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = "select * from countrymaster";
                sqlCommand.CommandType = CommandType.Text;
                dataAdapter = new NpgsqlDataAdapter(sqlCommand);
                dataAdapter.Fill(ds);
                return ds;
            }
            finally
            {
                sqlConnection.Close();
                sqlCommand.Dispose();
                ds.Dispose();
                dataAdapter = null;
            }
        }      

    }
}
