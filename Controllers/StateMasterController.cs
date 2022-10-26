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
    public class StateMasterController : ControllerBase
    {
        [HttpPost(nameof(AddStatemaster))]
        public string AddStatemaster(StateMasterEntity StateMasterEntity)
        {
            try
            {
                ManageSQLConnection manageSQL = new ManageSQLConnection();

                var result = manageSQL.insertstatemaster(StateMasterEntity);
                return JsonConvert.SerializeObject(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return "false";
        }
        [HttpGet(nameof(GetStateMaster))]
        public string GetStateMaster()
        {
            {
                ManageSQLConnection manageSQL = new ManageSQLConnection();

                var result = manageSQL.GetStateMaster();
                return JsonConvert.SerializeObject(result);
            }
        }
    }
    public class StateMasterEntity
    {
        public int statecode { get; set; }
        public string statename { get; set; }
        public bool Flag { get; set; }
    }
}
