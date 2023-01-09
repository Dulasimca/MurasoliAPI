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
    public class UpdateReporterRegBySlnoController : Controller
    {
        [HttpPost(nameof(UpdateReporterRegBySlno))]
        public string UpdateReporterRegBySlno(UpdateReporterBySlnoEntity UpdateReporterBySlnoEntity)
        {
            try
            {
                ManageSQLConnection manageSQL = new ManageSQLConnection();

                var result = manageSQL.UpdateReporterRegBySlno(UpdateReporterBySlnoEntity);
                return JsonConvert.SerializeObject(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return "false";
        }
    }
    public class UpdateReporterBySlnoEntity
    {
        public int u_slno { get; set; }
        public int u_approvalstatus { get; set; }
    }
}

