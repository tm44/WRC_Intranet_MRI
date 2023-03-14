using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MRI_Services;

namespace MRI.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PositivePayController : ControllerBase
    {
        [HttpGet]
        public ActionResult Get(string entity, string start, string end)
        {
            var helper = new PositivePayHelper(entity);
            var bytes = helper.GetPositivePayFile(DateTime.Parse(start), DateTime.Parse(end), false);
            return File(bytes, "text/plain", helper.Filename);
        }
    }
}
