using APICatalogo.DTOs;
using APICatalogo.DTOs.Mappings;
using APICatalogo.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace APICatalogo.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoriasController : Controller
{
    private readonly IUnityOfWork _uof;

    public CategoriasController(IUnityOfWork uof)
    {
        _uof = uof;
    }


    [HttpGet]
    public ActionResult<IEnumerable<CategoriaDTO>> Get()
    {
        var categorias = _uof.CategoriaRepository.GetAll();
        if(categorias is null)
        {
            return NotFound("Não existem categorias...");
        }
        var categoriasDTO = categorias.ToCategoriaDTOList();
        return Ok(categoriasDTO);
               
    }

    [HttpGet("{id:int}",Name ="ObterCategoria")]
    public ActionResult<CategoriaDTO> Get(int id)
    {
        var categoria = _uof.CategoriaRepository.Get(c => c.CategoriaId == id);

        if (categoria is null)
        {
            return NotFound("Categoria não encontrada");
        }

        var categoriaDTO = categoria.ToCategoriaDTO();
        return Ok(categoriaDTO);
                
    }

    [HttpPost]
    public ActionResult <CategoriaDTO> Post(CategoriaDTO categoriaDTO)
    {
        if(categoriaDTO is null)
        {
            return BadRequest("Dados inválidos");
        }

        var categoria = categoriaDTO.ToCategoria();
        var categoriaCriada = _uof.CategoriaRepository.Create(categoria);
        _uof.Commit();

        var novaCategoriaDTO = categoriaCriada.ToCategoriaDTO();
        return new CreatedAtRouteResult("ObterCategoria", new { id = novaCategoriaDTO.CategoriaId,novaCategoriaDTO });
    }

    [HttpPut("{id:int}")]
    public ActionResult<CategoriaDTO> Put(int id,CategoriaDTO categoriaDTO)
    {
        if(id != categoriaDTO.CategoriaId)
        {
            return BadRequest();
        }

        var categoria = categoriaDTO.ToCategoria();

        var categoriaAtualizada = _uof.CategoriaRepository.Update(categoria);
        _uof.Commit();

        var categoriaAtualizadaDTO = categoriaAtualizada.ToCategoriaDTO();
        return Ok(categoriaAtualizadaDTO);
    }

    [HttpDelete("{id:int}")]
    public ActionResult<CategoriaDTO> Delete(int id)
    {
        var categoriaExcluida = _uof.CategoriaRepository.Get(c => c.CategoriaId == id);
        if(categoriaExcluida is null)
        {
            return NotFound($"Categoria com id: {id} não encontrada...");
        }

        _uof.CategoriaRepository.Delete(categoriaExcluida);
        _uof.Commit();

        var categoriaExcluidaDTO = categoriaExcluida.ToCategoriaDTO();
        return Ok(categoriaExcluidaDTO);
    }
}
