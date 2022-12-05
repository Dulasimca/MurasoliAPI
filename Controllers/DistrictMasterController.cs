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
    public class DistrictMasterController : Controller
    {
      
        [HttpPost(nameof(AddDistrictmaster))]

        public string AddDistrictmaster(DistrictMasterEntity DistrictMasterEntity)
        {
            try
            {
                ManageSQLConnection manageSQL = new ManageSQLConnection();
                var result = manageSQL.insertdistrictmaster(DistrictMasterEntity);
                return JsonConvert.SerializeObject(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return "false";
        }
        [HttpGet(nameof(GetDistrictMaster))]
        public string GetDistrictMaster()
        {
            {
                ManageSQLConnection manageSQL = new ManageSQLConnection();

                var result = manageSQL.GetDistrictMaster();
                return JsonConvert.SerializeObject(result);
            }
        }
    }
public class DistrictMasterEntity
    {
        public int districtcode { get; set; }
        public string districtname { get; set; }
        public string districttamilname { get; set; }
        public bool  flag { get; set; }

    }
}
