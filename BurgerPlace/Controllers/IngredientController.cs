using BurgerPlace.Models.Request.Ingredients;
using BurgerPlace.Models.Response.Other;
using Microsoft.AspNetCore.Mvc;

namespace BurgerPlace.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = Roles.UserRoles.User)]
    [ApiController]
    public class IngredientController : ControllerBase
    {
        /// <summary>
        /// Creates new <see cref="Ingredient"></see>
        /// </summary>
        /// <param name="ingredient"></param>
        /// <returns></returns>
        [HttpPost()]
        [ProducesResponseType(typeof(Duplicated), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Created), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> CreateIngredient([FromBody] CreateIngredient ingredient)
        {
            using (var context = new BurgerPlaceContext())
            {
                var test = await context.Ingredients.Where(i => i.Name == ingredient.name).FirstOrDefaultAsync();
                if (test != null)
                {
                    return BadRequest(new CommonResponse.Duplicated());
                }
                else
                {
                    Ingredient ingredientToAdd = new();
                    ingredientToAdd.Name = ingredient.name;
                    ingredientToAdd.Price = ingredient.price;
                    await context.AddAsync(ingredientToAdd);
                    await context.SaveChangesAsync();
                    return Ok(new CommonResponse.Created());
                }
            }
        }

        /// <summary>
        /// Removing <see cref="Ingredient">Ingredient</see> by it's Id
        /// </summary>
        /// <param name="id">Id of <see cref="Ingredient">Ingredient</see> we want to remove</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(NotFoundWithThisId), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(RemovedName), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> RemoveIngredient(int id)
        {
            using (var context = new BurgerPlaceContext())
            {
                var test = await context.Ingredients.Where(i => i.Id == id).FirstOrDefaultAsync();
                if (test == null)
                {
                    return BadRequest(new CommonResponse.NotFoundWithThisId());
                }
                else
                {
                    context.Remove(test);
                    await context.SaveChangesAsync();
                    return Ok(new CommonResponse.RemovedName(test.Name));
                }
            }
        }

        /// <summary>
        /// Updates <see cref="Ingredient">Ingredient</see> by it's Id
        /// </summary>
        /// <param name="id">Id of <see cref="Ingredient">Ingredient</see> we want to update</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(NotFoundWithThisId), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Updated), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateIngredient([FromBody] UpdateIngredient updateIngredient, int id)
        {
            using (var context = new BurgerPlaceContext())
            {
                var toUpdate = await context.Ingredients.Where(i => i.Id == id).FirstOrDefaultAsync();
                
                if (toUpdate == null)
                {
                    return BadRequest(new CommonResponse.NotFoundWithThisId());
                }
                else
                {
                    toUpdate.Name = updateIngredient.name;
                    toUpdate.Price = updateIngredient.price;
                    await context.SaveChangesAsync();
                    return Ok(new CommonResponse.Updated());
                }
            }
        }
    }
}
