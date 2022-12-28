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
    public class ReporterRegistrationController : Controller
    {
        [HttpPost(nameof(AddReporterRegistration))]
        public string AddReporterRegistration(ReporterRegEntity ReporterRegEntity)
        {
            try
            {
                ManageSQLConnection manageSQL = new ManageSQLConnection();
                var result = manageSQL.InsertReporterRegistration(ReporterRegEntity);
                return JsonConvert.SerializeObject(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return "false";
        }
        [HttpGet(nameof(GetReporterRegistration))]
        public string GetReporterRegistration()
        {
            {
                ManageSQLConnection manageSQL = new ManageSQLConnection();

                var result = manageSQL.GetReporterRegistration();
                return JsonConvert.SerializeObject(result);
            }
        }
    }
    public class ReporterRegEntity
    {
        public int slno { get; set; }
        public string name { get; set; }
        public string mailid { get; set; }
        public string dob { get; set; }
        public int gender { get; set; }
        public string phonenumber { get; set; }
        public string address { get; set; }
        public int state { get; set; }
        public int district { get; set; }
        public string city { get; set; }
        public int pincode { get; set; }
        public string landmark { get; set; }
        public int approvalstatus { get; set; }

        public bool flag { get; set; }
    }
}
