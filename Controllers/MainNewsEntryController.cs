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
    public class MainNewsEntryController : Controller
    {
        [HttpPost(nameof(AddMainNewsEntry))]

        public string AddMainNewsEntry(MainNewsEntryEntity MainNewsEntryEntity)
        {
            try
            {
                ManageSQLConnection manageSQL = new ManageSQLConnection();
                var result = manageSQL.InserMainnewsEntry(MainNewsEntryEntity);
                return JsonConvert.SerializeObject(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return "false";
        }
        [HttpGet(nameof(GetMainNewsEntry))]
        public string GetMainNewsEntry()
        {
            {
                ManageSQLConnection manageSQL = new ManageSQLConnection();

                var result = manageSQL.GetMainNewsEntry();
                return JsonConvert.SerializeObject(result);
            }
        }
    }
    public class MainNewsEntryEntity
    {
        public int slno { get; set; }
        public string newstitle { get; set; }
        public string details { get; set; }
        public string image { get; set; }
        public string location { get; set; }
        public int district { get; set; }
        public int state { get; set; }
        public int country { get; set; }
        public int displayside { get; set; }
        public int priority { get; set; }
        public string newstitletamil { get; set; }
        public string newsdetailstamil { get; set; }
        public string incidentdate { get; set; }
        public bool flag { get; set; }
    }
}
