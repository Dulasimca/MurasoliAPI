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
    public class DailyNewsEntryController : Controller
    {
        [HttpPost(nameof(AddDailyNewsEntry))]

        public string AddDailyNewsEntry(DailyNewsEntryEntity DailyNewsEntryEntity)
        {
            try
            {
                ManageSQLConnection manageSQL = new ManageSQLConnection();
                var result = manageSQL.InsertDailynewsEntry(DailyNewsEntryEntity);
                return JsonConvert.SerializeObject(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return "false";
        }
        [HttpGet(nameof(GetDailyNewsEntry))]
        public string GetDailyNewsEntry()
        {
            {
                ManageSQLConnection manageSQL = new ManageSQLConnection();

                var result = manageSQL.GetDailyNewsEntry();
                return JsonConvert.SerializeObject(result);
            }
        }
    }
    public class DailyNewsEntryEntity
    {
        public int slno { get; set; }
        public string newstitle { get; set; }
        public string details { get; set; }
        public string image { get; set; }
        public int location { get; set; }
        public int district { get; set; }
        public int state { get; set; }
        public int country { get; set; }
        public int displayside { get; set; }
        public int priority { get; set; }
    }
}
