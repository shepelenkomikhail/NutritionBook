using Microsoft.AspNetCore.Mvc;
using NutritionalRecipeBook.Application.Contracts;

namespace NutritionalRecipeBook.Api.Controllers
{
    public class DummyController : Controller
    {
        private readonly IDummyService _dummyService;
        private readonly ILogger<DummyController> _logger; 

        public DummyController(IDummyService dummyService,
            ILogger<DummyController> logger) 
        {
            _dummyService = dummyService;
            _logger = logger;
        }

        // GET: DummyController/Details/5
        /// <summary>
        /// Endpoint for retreiving single dummy by it's unique identifier
        /// </summary>
        /// <param name="id">Unique dummy identifier</param>
        /// <returns></returns>
        [HttpGet]
        [Route("[controller]/Details/{id}")]
        public async Task<ActionResult> Details(Guid id)
        {
            _logger.LogInformation($"Request processing started for action GET Dummy/Details/{id}");
            var dummyEntity = await _dummyService.GetDummy(id);

            return Ok(dummyEntity);
        }
    }
}