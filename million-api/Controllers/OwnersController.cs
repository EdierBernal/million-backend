using Microsoft.AspNetCore.Mvc;

using million_api.Models.Entities;
using million_api.Services;

namespace million_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OwnersController : ControllerBase
    {
        private readonly OwnerService _ownersService;

        public OwnersController(OwnerService ownersService) =>
            _ownersService = ownersService;

        [HttpGet]
        public async Task<List<Owner>> Get() =>
            await _ownersService.GetAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Owner>> Get(string id)
        {
            var Owner = await _ownersService.GetAsync(id);

            if (Owner is null)
            {
                return NotFound();
            }

            return Owner;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Owner newOwner)
        {
            await _ownersService.CreateAsync(newOwner);

            return CreatedAtAction(nameof(Get), new { id = newOwner.Id }, newOwner);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Owner updatedOwner)
        {
            var Owner = await _ownersService.GetAsync(id);

            if (Owner is null)
            {
                return NotFound();
            }

            updatedOwner.Id = Owner.Id;

            await _ownersService.UpdateAsync(id, updatedOwner);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var Owner = await _ownersService.GetAsync(id);

            if (Owner is null)
            {
                return NotFound();
            }

            await _ownersService.RemoveAsync(id);

            return NoContent();
        }
    }
}
