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
    public class UpdateDailyNewsPaperController : Controller
    {
        [HttpPost(nameof(UpdateDailyNewsPaper))]
        public string UpdateDailyNewsPaper(UpdateDailyNewsPaperEntity UpdateDailyNewsPaperEntity)
        {
            try
            {
                ManageSQLConnection manageSQL = new ManageSQLConnection();

                var result = manageSQL.UpdateDailyNewsPaper(UpdateDailyNewsPaperEntity);
                return JsonConvert.SerializeObject(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return "false";
        }
    }
      public class UpdateDailyNewsPaperEntity
    {
        public int u_id { get; set; }
        public int u_district { get; set; }
        public string u_newspaperdate { get; set; }
        public string u_filename { get; set; }
        public bool u_flag { get; set; }
    }
}
