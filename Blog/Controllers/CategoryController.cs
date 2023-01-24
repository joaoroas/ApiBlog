﻿using Blog.Data;
using Blog.Models;
using Blog.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blog.Controllers
{
    [ApiController]
    public class CategoryController : ControllerBase
    {
        [HttpGet("v1/categories")]
        public async Task<IActionResult> GetAsync(
            [FromServices] BlogDataContext context)
        {
            var categories = await context.Categories.ToListAsync();

            return Ok(categories);
        }

        [HttpGet("v1/categories/{id:int}")]
        public async Task<IActionResult> GetByIdAsync(
           [FromServices] BlogDataContext context, int id)
        {
            var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (category == null)
                return NotFound();

            return Ok(category);
        }

        [HttpPost("v1/categories")]
        public async Task<IActionResult> PostAsync(
            [FromBody] EditorCategoryViewModel model,
            [FromServices] BlogDataContext context)
        {
            if (!ModelState.IsValid) 
                return BadRequest();

            try
            {
                var category = new Category() { Id = 0, Name = model.Name, Slug = model.Slug };
                await context.Categories.AddAsync(category);
                await context.SaveChangesAsync();

                return Created($"v1/categories/{category.Id}", category);

            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = "Não foi possivel criar a categoria", ex.InnerException.Message });
            }
        }

        [HttpPut("v1/categories/{id:int}")]
        public async Task<IActionResult> PutAsync(
            [FromRoute] int id,
            [FromBody] EditorCategoryViewModel model,
            [FromServices] BlogDataContext context)
        {
            var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (category == null)
                return NotFound();
            try
            {
                category.Name = model.Name;
                category.Slug = model.Slug;

                context.Categories.Update(category);
                await context.SaveChangesAsync();

                return Ok(category);

            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = "Não foi possivel atualizar a categoria", ex.InnerException.Message });
            }
        }

        [HttpDelete("v1/categories/{id:int}")]
        public async Task<IActionResult> DeleteAsync(
            [FromRoute] int id,
            [FromServices] BlogDataContext context)
        {
            var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (category == null)
                return NotFound();

            try
            {
                context.Categories.Remove(category);
                await context.SaveChangesAsync();

                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = "Não foi possivel deletar a categoria", ex.InnerException.Message });
            }
        }
    }
}