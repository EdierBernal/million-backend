using Microsoft.AspNetCore.Mvc;

using million_api.Models.Entities;
using million_api.Services;

namespace million_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PropertyTracesController : ControllerBase
    {
        private readonly PropertyTraceService _propertyTracesService;

        public PropertyTracesController(PropertyTraceService PropertyTraceService) =>
            _propertyTracesService = PropertyTraceService;

        [HttpGet]
        public async Task<List<PropertyTrace>> Get() =>
            await _propertyTracesService.GetAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<PropertyTrace>> Get(string id)
        {
            var PropertyTrace = await _propertyTracesService.GetAsync(id);

            if (PropertyTrace is null)
            {
                return NotFound();
            }

            return PropertyTrace;
        }

        [HttpPost]
        public async Task<IActionResult> Post(PropertyTrace newTraceProperty)
        {
            await _propertyTracesService.CreateAsync(newTraceProperty);

            return CreatedAtAction(nameof(Get), new { id = newTraceProperty.Id }, newTraceProperty);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, PropertyTrace updatedTraceProperty)
        {
            var PropertyTrace = await _propertyTracesService.GetAsync(id);

            if (PropertyTrace is null)
            {
                return NotFound();
            }

            updatedTraceProperty.Id = PropertyTrace.Id;

            await _propertyTracesService.UpdateAsync(id, updatedTraceProperty);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var PropertyTrace = await _propertyTracesService.GetAsync(id);

            if (PropertyTrace is null)
            {
                return NotFound();
            }

            await _propertyTracesService.RemoveAsync(id);

            return NoContent();
        }
    }
}
