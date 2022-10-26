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
    public class UpdateCountryMasterController : Controller
    {
        [HttpPost(nameof(UpdateCountryMaster))]
        public string UpdateCountryMaster(UpdateCountryMasterEntity UpdateCountryMasterEntity)
        {
            try
            {
                ManageSQLConnection manageSQL = new ManageSQLConnection();

                var result = manageSQL.UpdateCountryMaster(UpdateCountryMasterEntity);
                return JsonConvert.SerializeObject(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return "false";
        }
    }

    public class UpdateCountryMasterEntity
    {
        public int u_countrycode { get; set; }
        public string u_countryname { get; set; }
        public bool flag { get; set; }
    }
}
