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
    public class UpdateStateMasterController : Controller
    {
        [HttpPost(nameof(UpdateStateMaster))]
        public string UpdateStateMaster(UpdateStateMasterEntity UpdateStateMasterEntity)
        {
            try
            {
                ManageSQLConnection manageSQL = new ManageSQLConnection();

                var result = manageSQL.UpdateStateMaster(UpdateStateMasterEntity);
                return JsonConvert.SerializeObject(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return "false";
        }
    }
    public class UpdateStateMasterEntity
    {
        public int u_statecode { get; set; }
        public int u_stateid { get; set; }
        public string u_statename { get; set; }
        public bool flag { get; set; }
    }
}
