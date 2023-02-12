using Microsoft.AspNetCore.Mvc;
using PMA_SagaService.Models;
using System.Collections;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PMA_SagaService.Controllers
{
    [Route("api/v1/userTask")]
    [ApiController]
    public class UserTaskController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public UserTaskController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("tasksService");
        }

        //// GET: api/v1/userTask/all?limit={limit}
        //[HttpGet("all")]
        //public async Task<ActionResult<IEnumerable<UserTaskViewModel>>>GetAll(int limit = 100)
        //{
        //    var request = new HttpRequestMessage(
        //            HttpMethod.Post,
        //            _httpClient.BaseAddress + $"api/v1/userTask/all?limit={limit}");
        //
        //    var response = await _httpClient.SendAsync(request);
        //
        //}

        // GET api/<UserTaskController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UserTaskController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<UserTaskController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UserTaskController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
