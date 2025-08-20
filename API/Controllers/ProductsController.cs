using API.RequestHelpers;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;


public class ProductsController(IUnitOfWork unit) : BaseAPIController
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts(
        [FromQuery] ProductSpecParames specParames)
    {
        var spec = new ProductSpecification(specParames);

        // Simulate fetching products from a database or service
        return await CreatePageResult(unit.Repository<Product>(), spec, specParames.PageIndex, specParames.PageSize);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Product>> GetProducts(int id)
    {
        // Simulate fetching products from a database or service
        var product = await unit.Repository<Product>().GetByIdAsync(id);

        if (product == null)
        {
            return NotFound();
        }

        return product;
    }

    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct(Product product)
    {

        unit.Repository<Product>().Add(product);
        if (await unit.Complete())
        {
            return CreatedAtAction("GetProducts", new { id = product.Id }, product);
        }
        return BadRequest("Failed to create product");
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateProduct(int id, Product product)
    {
        if (product.Id != id || !ProductExists(id))
        {
            return BadRequest();
        }

        unit.Repository<Product>().Update(product);
        if (await unit.Complete())
        {
            return NoContent();

        }
        return BadRequest("Failed to update product");
  }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var product = await unit.Repository<Product>().GetByIdAsync(id);
        if (product == null)
        {
            return NotFound();
        }

        unit.Repository<Product>().Remove(product);
        if (await unit.Complete())
        {
            return NoContent();

        }
        return BadRequest("Failed to delete product");
    }

    [HttpGet("brands")]
    public async Task<ActionResult<IReadOnlyList<string>>> GetBrands()
    {
        var spec = new BrandListSpecification();
        var brands = await unit.Repository<Product>().ListAsync(spec);
        return Ok(brands);
    }

    [HttpGet("types")]
    public async Task<ActionResult<IReadOnlyList<string>>> GetTypes()
    {
        var spec = new TypeListSpecification();
        var types = await unit.Repository<Product>().ListAsync(spec);
        return Ok(types);
    }
    private bool ProductExists(int id)
    {
        return unit.Repository<Product>().Exists(id);
    }

}
