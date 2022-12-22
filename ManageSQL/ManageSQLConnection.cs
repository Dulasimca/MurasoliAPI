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
            Security security = new Security();
            var encryptedValue = security.Encryptword(UsersEntity.password);
            try
            {
                if (sqlConnection.State == 0)
                {
                    sqlConnection.Open();
                }
                sqlCommand.Connection = sqlConnection;
                //sqlCommand.CommandText = "insert into users (username,emailid,password,encryptedpassword,roleid,flag) values(@username,@emailid,@password,@encryptedpassword,@roleid,@flag)";
                sqlCommand.CommandText = "call users(@id,@username,@emailid,@password,@encryptedpassword,@roleid,@Flag)";
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Parameters.AddWithValue("@id", UsersEntity.id);
                sqlCommand.Parameters.AddWithValue("@username", UsersEntity.username);
                sqlCommand.Parameters.AddWithValue("@emailid", UsersEntity.emailid);
                sqlCommand.Parameters.AddWithValue("@password", UsersEntity.password);
                sqlCommand.Parameters.AddWithValue("@encryptedpassword", encryptedValue);
                sqlCommand.Parameters.AddWithValue("@roleid", UsersEntity.roleid);
                sqlCommand.Parameters.AddWithValue("@Flag", UsersEntity.Flag);
                sqlCommand.ExecuteNonQuery();
       
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
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

        public bool UpdateUsers(UpdateUsersEntity UpdateUsersEntity)
        {

            sqlConnection = new NpgsqlConnection(GlobalVariable.ConnectionStringForPostgreSQL);
            DataSet ds = new DataSet();
            sqlCommand = new NpgsqlCommand();
            Security security = new Security();
            var encryptedValue = security.Encryptword(UpdateUsersEntity.u_password);
            try
            {
                if (sqlConnection.State == 0)
                {
                    sqlConnection.Open();
                }
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = "call updateusers(@u_id,@u_username,@u_emailid,@u_password,@u_encryptedpassword,@u_roleid,@u_flag)";
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Parameters.AddWithValue("@u_id", UpdateUsersEntity.u_id);
                sqlCommand.Parameters.AddWithValue("@u_username", UpdateUsersEntity.u_username);
                sqlCommand.Parameters.AddWithValue("@u_emailid", UpdateUsersEntity.u_emailid);
                sqlCommand.Parameters.AddWithValue("@u_password", UpdateUsersEntity.u_password);
                sqlCommand.Parameters.AddWithValue("@u_encryptedpassword", encryptedValue);
                sqlCommand.Parameters.AddWithValue("@u_roleid", UpdateUsersEntity.u_roleid);
                sqlCommand.Parameters.AddWithValue("@u_flag", UpdateUsersEntity.flag);
                sqlCommand.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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

        public DataSet GetUsers()
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
                sqlCommand.CommandText = "select * from public.get_users()";
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
                sqlCommand.CommandText = "call insertstatemaster(@stateid,@statename,@statenametamil,@flag)";
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Parameters.AddWithValue("@stateid", StateMasterEntity.statecode);
                sqlCommand.Parameters.AddWithValue("@statename", StateMasterEntity.statename);
                sqlCommand.Parameters.AddWithValue("@statenametamil", StateMasterEntity.statenametamil);
                sqlCommand.Parameters.AddWithValue("@flag", StateMasterEntity.Flag);
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
                sqlCommand.CommandText = "call updatestatemaster(@u_stateid,@u_statename,@u_statenametamil,@u_flag)";
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Parameters.AddWithValue("@u_stateid", UpdateStateMasterEntity.u_statecode);
                sqlCommand.Parameters.AddWithValue("@u_statename", UpdateStateMasterEntity.u_statename);
                sqlCommand.Parameters.AddWithValue("@u_statenametamil", UpdateStateMasterEntity.u_statenametamil);
                sqlCommand.Parameters.AddWithValue("@u_flag", UpdateStateMasterEntity.flag);
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
                sqlCommand.CommandText = "select * from public.get_statemaster()";
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
                sqlCommand.CommandText = "call insertdistrictmaster(@districtid,@districtname,@districtnametamil,@flag)";
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Parameters.AddWithValue("@districtid", DistrictMasterEntity.districtcode);
                sqlCommand.Parameters.AddWithValue("@districtname", DistrictMasterEntity.districtname);
                sqlCommand.Parameters.AddWithValue("@districtnametamil", DistrictMasterEntity.districttamilname);
                sqlCommand.Parameters.AddWithValue("@flag", DistrictMasterEntity.flag);
                sqlCommand.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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
                sqlCommand.CommandText = "call updatedistrictmaster(@u_districtid,@u_districtname,@u_districtnametamil,@u_flag)";
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Parameters.AddWithValue("@u_districtid", UpdateDistrictMasterEntity.u_districtid);
                sqlCommand.Parameters.AddWithValue("@u_districtname", UpdateDistrictMasterEntity.u_districtname);
                sqlCommand.Parameters.AddWithValue("@u_districtnametamil", UpdateDistrictMasterEntity.u_districtnametamil);
                sqlCommand.Parameters.AddWithValue("@u_flag", UpdateDistrictMasterEntity.u_flag);
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
                sqlCommand.CommandText = "select * from public.get_districtmaster()";
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
                sqlCommand.CommandText = "call insertcountrymaster(@countrycode,@countryname,@countrynametamil,@flag)";
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Parameters.AddWithValue("@countrycode", CountryMasterEntity.countrycode);
                sqlCommand.Parameters.AddWithValue("@countryname", CountryMasterEntity.countryname);
                sqlCommand.Parameters.AddWithValue("@countrynametamil", CountryMasterEntity.countrynametamil);
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
                sqlCommand.CommandText = "call updatecountrymaster(@u_countrycode,@u_countryname,@u_countrynametamil,@u_flag)";
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Parameters.AddWithValue("@u_countrycode", UpdateCountryMasterEntity.u_countrycode);
                sqlCommand.Parameters.AddWithValue("@u_countryname", UpdateCountryMasterEntity.u_countryname);
                sqlCommand.Parameters.AddWithValue("@u_countrynametamil", UpdateCountryMasterEntity.u_countrynametamil);
                sqlCommand.Parameters.AddWithValue("@u_flag", UpdateCountryMasterEntity.u_flag);
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
                sqlCommand.CommandText = "select * from public.get_countrymaster()";
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

        //InsertDailynewsEntry
        public bool InsertDailynewsEntry(DailyNewsEntryEntity DailyNewsEntryEntity)
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
                sqlCommand.CommandText = "call insertdailynewsentry(@slno,@newstitle,@details,@image,@location,@district,@state,@country,@displayside,@priority,@newstitletamil,@newsdetailstamil,@flag)";
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Parameters.AddWithValue("@slno", DailyNewsEntryEntity.slno);
                sqlCommand.Parameters.AddWithValue("@newstitle", DailyNewsEntryEntity.newstitle);
                sqlCommand.Parameters.AddWithValue("@details", DailyNewsEntryEntity.details);
                sqlCommand.Parameters.AddWithValue("@image", DailyNewsEntryEntity.image);
                sqlCommand.Parameters.AddWithValue("@location", DailyNewsEntryEntity.location);
                sqlCommand.Parameters.AddWithValue("@district", DailyNewsEntryEntity.district);
                sqlCommand.Parameters.AddWithValue("@state", DailyNewsEntryEntity.state);
                sqlCommand.Parameters.AddWithValue("@country", DailyNewsEntryEntity.country);
                sqlCommand.Parameters.AddWithValue("@displayside", DailyNewsEntryEntity.displayside);
                sqlCommand.Parameters.AddWithValue("@priority", DailyNewsEntryEntity.priority);
                sqlCommand.Parameters.AddWithValue("@newstitletamil", DailyNewsEntryEntity.newstitletamil);
                sqlCommand.Parameters.AddWithValue("@newsdetailstamil", DailyNewsEntryEntity.newsdetailstamil);
                sqlCommand.Parameters.AddWithValue("@flag", DailyNewsEntryEntity.flag);
                sqlCommand.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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


        //UpdateDailyNewsEntry
        public bool UpdateDailyNewsEntry(UpdateDailyNewsEntryEntity UpdateDailyNewsEntryEntity)
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
                sqlCommand.CommandText = "call updatedailynewsentry(@u_slno,@u_newstitle,@u_details,@u_image,@u_location,@u_district,@u_state,@u_country,@u_displayside,@u_priority,@u_newstitletamil,@u_newsdetailstamil,@u_flag)";
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Parameters.AddWithValue("@u_slno", UpdateDailyNewsEntryEntity.u_slno);
                sqlCommand.Parameters.AddWithValue("@u_newstitle", UpdateDailyNewsEntryEntity.u_newstitle);
                sqlCommand.Parameters.AddWithValue("@u_details", UpdateDailyNewsEntryEntity.u_details);
                sqlCommand.Parameters.AddWithValue("@u_image", UpdateDailyNewsEntryEntity.u_image);
                sqlCommand.Parameters.AddWithValue("@u_location", UpdateDailyNewsEntryEntity.u_location);
                sqlCommand.Parameters.AddWithValue("@u_district", UpdateDailyNewsEntryEntity.u_district);
                sqlCommand.Parameters.AddWithValue("@u_state", UpdateDailyNewsEntryEntity.u_state);
                sqlCommand.Parameters.AddWithValue("@u_country", UpdateDailyNewsEntryEntity.u_country);
                sqlCommand.Parameters.AddWithValue("@u_displayside", UpdateDailyNewsEntryEntity.u_displayside);
                sqlCommand.Parameters.AddWithValue("@u_priority", UpdateDailyNewsEntryEntity.u_priority);
                sqlCommand.Parameters.AddWithValue("@u_newstitletamil", UpdateDailyNewsEntryEntity.u_newstitletamil);
                sqlCommand.Parameters.AddWithValue("@u_newsdetailstamil", UpdateDailyNewsEntryEntity.u_newsdetailstamil);
                sqlCommand.Parameters.AddWithValue("@u_flag", UpdateDailyNewsEntryEntity.u_flag);
                sqlCommand.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
                Console.WriteLine(ex.Message);
            }
            finally
            {
                sqlConnection.Close();
                sqlCommand.Dispose();
                ds.Dispose();
                dataAdapter = null;
            }
        }

        //GetDailyNewsEntry
        public DataSet GetDailyNewsEntry()
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
                sqlCommand.CommandText = "select * from public.get_dailynewsentry()";
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

        //InsertMainNewsEntry
        public bool InserMainnewsEntry(MainNewsEntryEntity MainNewsEntryEntity)
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
                sqlCommand.CommandText = "call insertmainnewsentry(@slno,@newstitle,@details,@image,@location,@district,@state,@country,@displayside,@priority,@newstitletamil,@newsdetailstamil,@newsshort,@newsshorttamil,@incidentdate,@flag)";
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Parameters.AddWithValue("@slno", MainNewsEntryEntity.slno);
                sqlCommand.Parameters.AddWithValue("@newstitle", MainNewsEntryEntity.newstitle);
                sqlCommand.Parameters.AddWithValue("@details", MainNewsEntryEntity.details);
                sqlCommand.Parameters.AddWithValue("@image", MainNewsEntryEntity.image);
                sqlCommand.Parameters.AddWithValue("@location", MainNewsEntryEntity.location);
                sqlCommand.Parameters.AddWithValue("@district", MainNewsEntryEntity.district);
                sqlCommand.Parameters.AddWithValue("@state", MainNewsEntryEntity.state);
                sqlCommand.Parameters.AddWithValue("@country", MainNewsEntryEntity.country);
                sqlCommand.Parameters.AddWithValue("@displayside", MainNewsEntryEntity.displayside);
                sqlCommand.Parameters.AddWithValue("@priority", MainNewsEntryEntity.priority);
                sqlCommand.Parameters.AddWithValue("@newstitletamil", MainNewsEntryEntity.newstitletamil);
                sqlCommand.Parameters.AddWithValue("@newsdetailstamil", MainNewsEntryEntity.newsdetailstamil);
                sqlCommand.Parameters.AddWithValue("@newsshort", MainNewsEntryEntity.newsshort);
                sqlCommand.Parameters.AddWithValue("@newsshorttamil", MainNewsEntryEntity.newsshorttamil);
                sqlCommand.Parameters.AddWithValue("@incidentdate", MainNewsEntryEntity.incidentdate);
                sqlCommand.Parameters.AddWithValue("@flag", MainNewsEntryEntity.flag);
                sqlCommand.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
                Console.WriteLine(ex.Message);
            }
            finally
            {
                sqlConnection.Close();
                sqlCommand.Dispose();
                ds.Dispose();
                dataAdapter = null;
            }
        }

        //UpdateMainNewsEntry
        public bool UpdateMainNewsEntry(UpdateMainNewsEntryEntity UpdateMainNewsEntryEntity)
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
                sqlCommand.CommandText = "call updatemainnewsentry(@u_slno,@u_newstitle,@u_details,@u_image,@u_location,@u_district,@u_state,@u_country,@u_displayside,@u_priority,@u_newstitletamil,@u_newsdetailstamil,@u_newsshort,@u_newsshorttamil,@u_incidentdate,@u_flag)";
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Parameters.AddWithValue("@u_slno", UpdateMainNewsEntryEntity.u_slno);
                sqlCommand.Parameters.AddWithValue("@u_newstitle", UpdateMainNewsEntryEntity.u_newstitle);
                sqlCommand.Parameters.AddWithValue("@u_details", UpdateMainNewsEntryEntity.u_details);
                sqlCommand.Parameters.AddWithValue("@u_image", UpdateMainNewsEntryEntity.u_image);
                sqlCommand.Parameters.AddWithValue("@u_location", UpdateMainNewsEntryEntity.u_location);
                sqlCommand.Parameters.AddWithValue("@u_district", UpdateMainNewsEntryEntity.u_district);
                sqlCommand.Parameters.AddWithValue("@u_state", UpdateMainNewsEntryEntity.u_state);
                sqlCommand.Parameters.AddWithValue("@u_country", UpdateMainNewsEntryEntity.u_country);
                sqlCommand.Parameters.AddWithValue("@u_displayside", UpdateMainNewsEntryEntity.u_displayside);
                sqlCommand.Parameters.AddWithValue("@u_priority", UpdateMainNewsEntryEntity.u_priority);
                sqlCommand.Parameters.AddWithValue("@u_newstitletamil", UpdateMainNewsEntryEntity.u_newstitletamil);
                sqlCommand.Parameters.AddWithValue("@u_newsdetailstamil", UpdateMainNewsEntryEntity.u_newsdetailstamil);
                sqlCommand.Parameters.AddWithValue("@u_newsshort", UpdateMainNewsEntryEntity.u_newsshort);
                sqlCommand.Parameters.AddWithValue("@u_newsshorttamil", UpdateMainNewsEntryEntity.u_newstamilshort);
                sqlCommand.Parameters.AddWithValue("@u_incidentdate", UpdateMainNewsEntryEntity.u_incidentdate);
                sqlCommand.Parameters.AddWithValue("@u_flag", UpdateMainNewsEntryEntity.u_flag);
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

        //GetDailyNewsEntry
        public DataSet GetMainNewsEntry()
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
                sqlCommand.CommandText = "select * from public.get_mainnewsentry()";
                sqlCommand.CommandType = CommandType.Text;
                //sqlCommand.Parameters.AddWithValue("@slno", MainNewsEntryEntity.slno);
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

        //getmainnewsentrybyid

        public DataSet GetMainNewsEntrybyId()
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
                sqlCommand.CommandText = "select * from public.get_mainnewsentrybyid(slno)";
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

        //InsertFlashNewsEntry
        public bool InsertFlashNewsEntry(FlashNewsEntryEntity FlashNewsEntryEntity)
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
                sqlCommand.CommandText = "call insertflashnewsentry(@slno,@location,@incidentdate,@newsdetails,@newsdetailstamil,@flag)";
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Parameters.AddWithValue("@slno", FlashNewsEntryEntity.slno);
                sqlCommand.Parameters.AddWithValue("@location", FlashNewsEntryEntity.location);
                sqlCommand.Parameters.AddWithValue("@incidentdate", FlashNewsEntryEntity.incidentdate);
                sqlCommand.Parameters.AddWithValue("@newsdetails", FlashNewsEntryEntity.newsdetails);
                sqlCommand.Parameters.AddWithValue("@newsdetailstamil", FlashNewsEntryEntity.newsdetailstamil);
                sqlCommand.Parameters.AddWithValue("@flag", FlashNewsEntryEntity.flag);
                sqlCommand.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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

        //UpdateFlashNewsEntry
        public bool UpdateFlashNewsEntry(UpdateFlashNewsEntryEntity UpdateFlashNewsEntryEntity)
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
                sqlCommand.CommandText = "call updateflashnewsentry(@u_slno,@u_location,@u_incidentdate,@u_newsdetails,@u_newsdetailstamil,@u_flag)";
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Parameters.AddWithValue("@u_slno", UpdateFlashNewsEntryEntity.u_slno);
                sqlCommand.Parameters.AddWithValue("@u_location", UpdateFlashNewsEntryEntity.u_location);
                sqlCommand.Parameters.AddWithValue("@u_incidentdate", UpdateFlashNewsEntryEntity.u_incidentdate);
                sqlCommand.Parameters.AddWithValue("@u_newsdetails", UpdateFlashNewsEntryEntity.u_newsdetails);
                sqlCommand.Parameters.AddWithValue("@u_newsdetailstamil", UpdateFlashNewsEntryEntity.u_newsdetailstamil);
                sqlCommand.Parameters.AddWithValue("@u_flag", UpdateFlashNewsEntryEntity.u_flag);
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

        //GetFlashNewsEntry
        public DataSet GetFlashNewsEntry()
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
                sqlCommand.CommandText = "select * from public.get_flashnewsentry();";
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

        //insertdailynewspaper
        public bool insertdailynewspaper(DailyNewsPaperEntity DailyNewsPaperEntity)
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
                sqlCommand.CommandText = "call insertdailynewspaper(@id,@districtid,@newspaperdate,@filename,@flag)";
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Parameters.AddWithValue("@id", DailyNewsPaperEntity.id);
                sqlCommand.Parameters.AddWithValue("@districtid", DailyNewsPaperEntity.district);
                sqlCommand.Parameters.AddWithValue("@newspaperdate", DailyNewsPaperEntity.newspaperdate);
                sqlCommand.Parameters.AddWithValue("@filename", DailyNewsPaperEntity.filename);
                sqlCommand.Parameters.AddWithValue("@flag", DailyNewsPaperEntity.flag);
                sqlCommand.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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

        //upatedailynewspaper
        public bool UpdateDailyNewsPaper(UpdateDailyNewsPaperEntity UpdateDailyNewsPaperEntity)
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
                sqlCommand.CommandText = "call updatedailynewspaper(@u_id,@u_newspaperdate,@u_filename,@u_flag)";
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Parameters.AddWithValue("@u_id", UpdateDailyNewsPaperEntity.u_id);
                sqlCommand.Parameters.AddWithValue("@u_districtname", UpdateDailyNewsPaperEntity.u_district);
                sqlCommand.Parameters.AddWithValue("@u_newspaperdate", UpdateDailyNewsPaperEntity.u_newspaperdate);
                sqlCommand.Parameters.AddWithValue("@u_filename", UpdateDailyNewsPaperEntity.u_filename);
                sqlCommand.Parameters.AddWithValue("@u_flag", UpdateDailyNewsPaperEntity.u_flag);
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

        //getdailynewspaper
        public DataSet GetDailyNewsPaper()
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
                sqlCommand.CommandText = "select * from public.get_dailynewspaper()";
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
