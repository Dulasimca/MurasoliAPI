using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MurasoliAPI.ManageSQL;

namespace MurasoliAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpPost(nameof(insertUserMaster))]
        public string insertUserMaster(UsersEntity UsersEntity)
        {
            try
            {
                ManageSQLConnection manageSQL = new ManageSQLConnection();
                
                var result = manageSQL.insertUserMaster(UsersEntity);
                return JsonConvert.SerializeObject(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return "false";
        }

        //[HttpPost(nameof(AddStatemaster))]
        //public string AddStatemaster(StateMasterEntity StateMasterEntity)
        //{
        //    try
        //    {
        //        ManageSQLConnection manageSQL = new ManageSQLConnection();

        //        var result = manageSQL.insertstatemaster(StateMasterEntity);
        //        return JsonConvert.SerializeObject(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }
        //    return "false";
        //}

        //[HttpPost(nameof(Insert))]
        //public string Insert(UsersEntity UsersEntity)
        //{
        //    try
        //    {
        //        ManageSQLConnection manageSQL = new ManageSQLConnection();
        //        List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
        //        //sqlParameters.Add(new KeyValuePair<string, string>("@id", Convert.ToString(UsersEntity.id)));
        //        sqlParameters.Add(new KeyValuePair<string, string>("@username", UsersEntity.username));
        //        sqlParameters.Add(new KeyValuePair<string, string>("@emailid", UsersEntity.emailid));
        //        sqlParameters.Add(new KeyValuePair<string, string>("@password", UsersEntity.password));
        //        sqlParameters.Add(new KeyValuePair<string, string>("@encryptedpassword", UsersEntity.encryptedpassword));
        //        sqlParameters.Add(new KeyValuePair<string, string>("@roleid", Convert.ToString(UsersEntity.roleid)));
        //        sqlParameters.Add(new KeyValuePair<string, string>("@Flag", Convert.ToString(UsersEntity.Flag)));
        //        var result = manageSQL.InsertData("call users(@username,@emailid,@password,@encryptedpassword,@roleid,@Flag)", sqlParameters);
        //        return JsonConvert.SerializeObject(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }
        //    return "false";
        //}
        [HttpGet(nameof(GetUsers))]
        public string GetUsers()
        {
            {
                ManageSQLConnection manageSQL = new ManageSQLConnection();

                var result = manageSQL.GetUsers();
                return JsonConvert.SerializeObject(result);
            }
        }
    }
    public class UsersEntity
    {
        public string username { get; set; }
        public string emailid { get; set; }
        public string password { get; set; }
        public int roleid { get; set; }
        public bool Flag { get; set; }
    }


}
