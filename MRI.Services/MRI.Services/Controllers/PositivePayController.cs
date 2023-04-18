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
        public FileResult Get(string entity, string start, string end, bool includePayee)
        {
            var helper = new PositivePayHelper(entity);
            var bytes = helper.GetPositivePayFile(DateTime.Parse(start), DateTime.Parse(end), includePayee);
            return File(bytes, "text/plain;charset=utf-8", helper.Filename);
        }
    }
}
