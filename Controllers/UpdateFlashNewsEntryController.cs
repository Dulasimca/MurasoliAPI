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
    public class UpdateFlashNewsEntryController : Controller
    {
        [HttpPost(nameof(UpdateFlashNewsEntry))]
        public string UpdateFlashNewsEntry(UpdateFlashNewsEntryEntity UpdateFlashNewsEntryEntity)
        {
            try
            {
                ManageSQLConnection manageSQL = new ManageSQLConnection();

                var result = manageSQL.UpdateFlashNewsEntry(UpdateFlashNewsEntryEntity);
                return JsonConvert.SerializeObject(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return "false";
        }
    }
    public class UpdateFlashNewsEntryEntity
    {
        public int u_slno { get; set; }
        public string u_location { get; set; }
        public string u_incidentdate { get; set; }
        public string u_newsdetails { get; set; }
        public string u_newsdetailstamil { get; set; }
        public bool u_flag { get; set; }
    }
}
