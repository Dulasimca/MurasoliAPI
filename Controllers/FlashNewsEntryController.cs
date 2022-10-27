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
    public class FlashNewsEntryController : Controller
    {
        [HttpPost(nameof(AddFlashNewsEntry))]

        public string AddFlashNewsEntry(FlashNewsEntryEntity FlashNewsEntryEntity)
        {
            try
            {
                ManageSQLConnection manageSQL = new ManageSQLConnection();
                var result = manageSQL.InsertFlashNewsEntry(FlashNewsEntryEntity);
                return JsonConvert.SerializeObject(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return "false";
        }
        [HttpGet(nameof(GetFlashNewsEntry))]
        public string GetFlashNewsEntry()
        {
            {
                ManageSQLConnection manageSQL = new ManageSQLConnection();

                var result = manageSQL.GetFlashNewsEntry();
                return JsonConvert.SerializeObject(result);
            }
        }
    }

    public class FlashNewsEntryEntity
    {
        public int slno { get; set; }
        public string location { get; set; }
        public string incidentdate { get; set; }
        public string newsdetails { get; set; }
        public bool flag { get; set; }
    }
}
