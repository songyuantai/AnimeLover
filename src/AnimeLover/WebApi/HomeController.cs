using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeLover.WebApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [Route("hello")]
        public string Hello()
        {
            return "hello";
        }

        [Route("video")]
        public IActionResult Video(string src)
        {
            return PhysicalFile(src, "application/octet-stream", true);
        }
    }
}
