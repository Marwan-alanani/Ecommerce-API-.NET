using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace C43_G04_API01.Web.Controllers;

[Route("api/[controller]")] // baseUrl/api/Products/
[ApiController] // must specify the verb for the http request

public class ProductsController : ControllerBase
{
    [HttpGet("{id:int}")]
    public ActionResult<Product> Get(int id ) // GET baseUrl/api/Products/{id}
    {
        return new Product() { Id = id };
    }

    [HttpGet]
    public ActionResult<Product> GetAll(int id ) // GET baseUrl/api/Products/
    {
        return new Product() { Id = id };
    }

    [HttpPost]
    public ActionResult<Product> Add(Product product) // Post baseUrl/api/Products/
    {

        return  new Product() { Id = 100 };
    }

    [HttpPut]
    public ActionResult<Product> Update(Product product) // Put baseUrl/api/Products/
    {
        return  new Product() { Id = 100 };
    }

    [HttpDelete]
    public ActionResult<Product> Delete(int id ) // Delete baseUrl/api/Products/
    {
        return  new Product() { Id = 100 };
    }


}

public class Product
{
    [Range(1,2)]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
}