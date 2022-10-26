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
    public class UpdateDistrictMasterController : Controller
    {
        [HttpPost(nameof(UpdateDistrictMaster))]
        public string UpdateDistrictMaster(UpdateDistrictMasterEntity UpdateDistrictMasterEntity)
        {
            try
            {
                ManageSQLConnection manageSQL = new ManageSQLConnection();

                var result = manageSQL.UpdateDistrictMaster(UpdateDistrictMasterEntity);
                return JsonConvert.SerializeObject(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return "false";
        }
    }
    public class UpdateDistrictMasterEntity
    {
        public int u_districtcode { get; set; }
        public string u_districtname { get; set; }
        public bool flag { get; set; }
    }
}
