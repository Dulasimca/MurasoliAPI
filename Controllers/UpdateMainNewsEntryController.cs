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
    public class UpdateMainNewsEntryController : Controller
    {
        [HttpPost(nameof(UpdateMainNewsEntry))]
        public string UpdateMainNewsEntry(UpdateMainNewsEntryEntity UpdateMainNewsEntryEntity)
        {
            try
            {
                ManageSQLConnection manageSQL = new ManageSQLConnection();

                var result = manageSQL.UpdateMainNewsEntry(UpdateMainNewsEntryEntity);
                return JsonConvert.SerializeObject(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return "false";
        }
    }

    public class UpdateMainNewsEntryEntity
    {
        public int u_slno { get; set; }
        public string u_newstitle { get; set; }
        public string u_details { get; set; }
        public string u_image { get; set; }
        public string u_location { get; set; }
        public int u_district { get; set; }
        public int u_state { get; set; }
        public int u_country { get; set; }
        public int u_displayside { get; set; }
        public int u_priority { get; set; }
        public string u_newstitletamil { get; set; }
        public string u_newsdetailstamil { get; set; }
        public string u_incidentdate { get; set; }
        public string u_newsshort { get; set; }
        public string u_newstamilshort { get; set; }
        public bool u_flag { get; set; }
    }
}
