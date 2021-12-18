using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using My_Books.Data.Services;
using My_Books.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace My_Books.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishersController : ControllerBase
    {
        public PublishersService _publishersService;
        public PublishersController(PublishersService publishersService)
        {
            _publishersService = publishersService;
        }

        [HttpPost("add")]
        public IActionResult AddPublisher([FromBody] PublisherVM publisher)
        {
            var response = _publishersService.AddPublisher(publisher);
            return Ok(response);
        }

    }
}
