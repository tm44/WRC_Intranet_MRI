using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MRI.Services.ART;
using MRI.Services.Budget;
using MRI_Services;

namespace MRI.Services.Controllers
{
    [Route("api/budget")]
    [ApiController]
    public class BudgetController : ControllerBase
    {

        [HttpPost]
        public ActionResult Post([FromBody] BudgetRecord data)
        {
            try
            {
                new BudgetHelper().InsertBudgetRow(data);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpDelete("{entity}/{period}")]
        public ActionResult Delete(string entity, string period)
        {
            try
            {
                new BudgetHelper().DeleteBudgetRecord(entity, period);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
