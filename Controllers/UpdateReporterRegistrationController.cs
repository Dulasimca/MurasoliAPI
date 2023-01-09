using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MurasoliAPI.ManageSQL;
using Newtonsoft.Json;

namespace MurasoliAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UpdateReporterRegistrationController : Controller
    {
        [HttpPost(nameof(UpdateReporterRegistration))]
        public string UpdateReporterRegistration(UpdateReporterRegEntity UpdateReportReg)
        {
            try
            {
                ManageSQLConnection manageSQL = new ManageSQLConnection();

                var result = manageSQL.UpdateReporterRegistration(UpdateReportReg);
                return JsonConvert.SerializeObject(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return "false";
        }
    }
    public class UpdateReporterRegEntity
    {
        public int u_slno { get; set; }
        public string u_name { get; set; }
        public string u_mailid { get; set; }
        public string u_dob { get; set; }
        public int u_gender { get; set; }
        public string u_phonenumber { get; set; }
        public string u_address { get; set; }
        public int u_state { get; set; }
        public int u_district { get; set; }
        public string u_city { get; set; }
        public int u_pincode { get; set; }
        public string u_landmark { get; set; }

        public bool u_flag { get; set; }
    }
}
