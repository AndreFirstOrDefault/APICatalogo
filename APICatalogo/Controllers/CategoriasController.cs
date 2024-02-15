using APICatalogo.Models;
using APICatalogo.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace APICatalogo.Controllers;

[Route("[controller]")]
public class CategoriasController : Controller
{
    private readonly IUnityOfWork _uof;

    public CategoriasController(IUnityOfWork uof)
    {
        _uof = uof;
    }


    [HttpGet]
    public ActionResult<IEnumerable<Categoria>> Get()
    {
        var categorias = _uof.CategoriaRepository.GetAll();
        return Ok(categorias);
               
    }

    [HttpGet("{id:int}",Name ="ObterCategoria")]
    public ActionResult<Categoria> Get(int id)
    {
        var categoria = _uof.CategoriaRepository.Get(c => c.CategoriaId == id);

        if (categoria is null)
        {
            return NotFound("Categoria não encontrada");
        }
        return Ok(categoria);
                
    }

    [HttpPost]
    public ActionResult Post(Categoria categoria)
    {
        if(categoria is null)
        {
            return BadRequest("Dados inválidos");
        }

        var categoriaCriada = _uof.CategoriaRepository.Create(categoria);
        _uof.Commit();
        return new CreatedAtRouteResult("ObterCategoria", new { id = categoriaCriada.CategoriaId,nome = categoriaCriada.Nome, imagemUrl = categoriaCriada.ImagemUrl });
    }

    [HttpPut("{id:int}")]
    public ActionResult Put(int id,Categoria categoria)
    {
        if(id != categoria.CategoriaId)
        {
            return BadRequest();
        }

        var categoriaAtualizada = _uof.CategoriaRepository.Update(categoria);
        _uof.Commit();
        return Ok(categoriaAtualizada);
            

    }

    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        var categoria = _uof.CategoriaRepository.Get(c => c.CategoriaId == id);
        if(categoria is null)
        {
            return NotFound($"Categoria com id: {id} não encontrada...");
        }

        _uof.CategoriaRepository.Delete(categoria);
        _uof.Commit();
        return Ok(categoria);
    }
}
