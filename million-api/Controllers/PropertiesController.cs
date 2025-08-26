using Microsoft.AspNetCore.Mvc;

using million_api.Models.Entities;
using million_api.Services;

namespace million_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PropertiesController : ControllerBase
    {
        private readonly PropertyService _propertiesService;

        public PropertiesController(PropertyService propertyService) =>
            _propertiesService = propertyService;

        [HttpGet]
        public async Task<List<Property>> Get() =>
            await _propertiesService.GetAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Property>> Get(string id)
        {
            var Property = await _propertiesService.GetAsync(id);

            if (Property is null)
            {
                return NotFound();
            }

            return Property;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Property newProperty)
        {
            await _propertiesService.CreateAsync(newProperty);

            return CreatedAtAction(nameof(Get), new { id = newProperty.Id }, newProperty);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Property updatedProperty)
        {
            var Property = await _propertiesService.GetAsync(id);

            if (Property is null)
            {
                return NotFound();
            }

            updatedProperty.Id = Property.Id;

            await _propertiesService.UpdateAsync(id, updatedProperty);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var Property = await _propertiesService.GetAsync(id);

            if (Property is null)
            {
                return NotFound();
            }

            await _propertiesService.RemoveAsync(id);

            return NoContent();
        }
    }
}
