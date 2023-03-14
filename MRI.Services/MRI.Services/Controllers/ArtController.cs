using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MRI.Services.ART;
using MRI_Services;

namespace MRI.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtController : ControllerBase
    {
        // http://localhost:5276/api/Art?expensePeriod=202301&entity=001
        [HttpGet]
        public ActionResult Get(string expensePeriod, string entity)
        {
            var helper = new ArtHelper();
            var records = helper.GetUnbilledRecords(expensePeriod, entity);
            return Ok(records);
        }

        [HttpPost]
        public ActionResult Post([FromBody] RunArtRequestData data)
        {
            var helper = new ArtHelper();
            try
            {
                return Ok(helper.RunArt(DateTime.Parse(data.InvoiceDate), data.ExpensePeriod, data.Entity));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            
        }
    }
}
