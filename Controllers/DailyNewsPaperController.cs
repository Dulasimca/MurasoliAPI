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
    public class DailyNewsPaperController : Controller
    {
        [HttpPost(nameof(AddDailynewspaper))]

        public string AddDailynewspaper(DailyNewsPaperEntity DailyNewsPaperEntity)
        {
            try
            {
                ManageSQLConnection manageSQL = new ManageSQLConnection();
                var result = manageSQL.insertdailynewspaper(DailyNewsPaperEntity);
                return JsonConvert.SerializeObject(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return "false";
        }
        [HttpGet(nameof(GetDailyNewsPaper))]
        public string GetDailyNewsPaper()
        {
            {
                ManageSQLConnection manageSQL = new ManageSQLConnection();

                var result = manageSQL.GetDailyNewsPaper();
                return JsonConvert.SerializeObject(result);
            }
        }
    }
    public class DailyNewsPaperEntity
    {
        public int id { get; set; }
        public string newspaperdate { get; set; }
        public string filename { get; set; }
        public bool flag { get; set; }

    }
}
