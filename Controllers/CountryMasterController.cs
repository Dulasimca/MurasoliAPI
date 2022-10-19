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
    public class CountryMasterController : Controller
    {
        [HttpPost(nameof(AddCountrymaster))]

        public string AddCountrymaster(CountryMasterEntity CountryMasterEntity)
        {
            try
            {
                ManageSQLConnection manageSQL = new ManageSQLConnection();
                var result = manageSQL.insertcountrymaster(CountryMasterEntity);
                return JsonConvert.SerializeObject(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return "false";
        }
        [HttpGet(nameof(GetCountryMaster))]
        public string GetCountryMaster()
        {
            {
                ManageSQLConnection manageSQL = new ManageSQLConnection();

                var result = manageSQL.GetCountryMaster();
                return JsonConvert.SerializeObject(result);
            }
        }
    }
    public class CountryMasterEntity
    {
        public int countrycode { get; set; }
        public int countryid { get; set; }
        public string countryname { get; set; }
        public bool flag { get; set; }

    }
}
