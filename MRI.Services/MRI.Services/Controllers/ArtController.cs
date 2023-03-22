using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MRI.Services.ART;
using MRI_Services;

namespace MRI.Services.Controllers
{
    [Route("api/art")]
    [ApiController]
    public class ArtController : ControllerBase
    {
        // http://mri_services/api/Art/111/202303
        [HttpGet("{entity}/{expensePeriod}")]
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
