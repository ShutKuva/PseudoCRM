using BusinessLogicLayer.Abstractions.Database;
using Core;
using Microsoft.AspNetCore.Mvc;

namespace PseudoCRMAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DatabaseController : ControllerBase
    {
        private readonly IDatabaseService _databaseHandler;

        public DatabaseController(IDatabaseService databaseHandler)
        {
            _databaseHandler = databaseHandler;
        }

        [HttpPost]
        public IActionResult Test(string connectionString)
        {
            _databaseHandler.CreateDatabase(connectionString);
            return Ok();
        }
    }
}
