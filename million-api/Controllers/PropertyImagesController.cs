using Microsoft.AspNetCore.Mvc;

using million_api.Models.Entities;
using million_api.Services;

namespace million_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PropertyImagesController : ControllerBase
    {
        private readonly PropertyImageService _propertiesImageService;

        public PropertyImagesController(PropertyImageService PropertyImageService) =>
            _propertiesImageService = PropertyImageService;

        [HttpGet]
        public async Task<List<PropertyImage>> Get() =>
            await _propertiesImageService.GetAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<PropertyImage>> Get(string id)
        {
            var PropertyImage = await _propertiesImageService.GetAsync(id);

            if (PropertyImage is null)
            {
                return NotFound();
            }

            return PropertyImage;
        }

        [HttpPost]
        public async Task<IActionResult> Post(PropertyImage newPropertyImage)
        {
            await _propertiesImageService.CreateAsync(newPropertyImage);

            return CreatedAtAction(nameof(Get), new { id = newPropertyImage.Id }, newPropertyImage);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, PropertyImage updatedImageProperty)
        {
            var PropertyImage = await _propertiesImageService.GetAsync(id);

            if (PropertyImage is null)
            {
                return NotFound();
            }

            updatedImageProperty.Id = PropertyImage.Id;

            await _propertiesImageService.UpdateAsync(id, updatedImageProperty);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var PropertyImage = await _propertiesImageService.GetAsync(id);

            if (PropertyImage is null)
            {
                return NotFound();
            }

            await _propertiesImageService.RemoveAsync(id);

            return NoContent();
        }
    }
}
