using BurgerPlace.Context;
using BurgerPlace.Models.Database;
using BurgerPlace.Models.Request;
using BurgerPlace.Models.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static BurgerPlace.Models.Response.CommonResponse;

namespace BurgerPlace.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = Roles.UserRoles.User)]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        [HttpPost()]
        [ProducesResponseType(typeof(Duplicated), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(Category), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategory createCategory)
        {
            using (var context = new BurgerPlaceContext())
            {
                if (await context.Categories.Where(x => x.Name == createCategory.Name).FirstOrDefaultAsync() != null)
                {
                    return BadRequest(new Duplicated());
                }
                Category category = new Category();
                category.Name = createCategory.Name;
                await context.Categories.AddAsync(category);
                await context.SaveChangesAsync();
                return Ok(category);
            }
        }

        [HttpPost("update/{id}")]
        [ProducesResponseType(typeof(NotFoundWithThisId), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Category), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateCategory([FromBody] CreateCategory createCategory, int id)
        {
            using (var context = new BurgerPlaceContext())
            {
                var category = await context.Categories.Where(x => x.Id == id).FirstOrDefaultAsync();
                if (category == null)
                {
                    return NotFound(new NotFoundWithThisId());
                }

                category.Name = createCategory.Name;
                await context.SaveChangesAsync();
                return Ok(category);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(NotFoundWithThisId), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Category), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            using (var context = new BurgerPlaceContext())
            {
                var category = await context.Categories.Where(x => x.Id == id).FirstOrDefaultAsync();
                if (category == null)
                {
                    return NotFound(new NotFoundWithThisId());
                }

                context.Remove(category);
                await context.SaveChangesAsync();
                return Ok(category);
            }
        }

    }
}
