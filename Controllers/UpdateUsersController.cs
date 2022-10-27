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
    public class UpdateUsersController : Controller
    {
        [HttpPost(nameof(UpdateUsers))]
        public string UpdateUsers(UpdateUsersEntity UpdateUsersEntity)
        {
            try
            {
                ManageSQLConnection manageSQL = new ManageSQLConnection();

                var result = manageSQL.UpdateUsers(UpdateUsersEntity);
                return JsonConvert.SerializeObject(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return "false";
        }
    }
    public class UpdateUsersEntity
    {
        public int u_id { get; set; }
        public string u_username { get; set; }
        public string u_emailid { get; set; }
        public string u_password { get; set; }
        public int u_roleid { get; set; }
        public bool flag { get; set; }
    }
}
