using APICatalogo.Context;
using APICatalogo.Filters;
using APICatalogo.Models;
using APICatalogo.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers;

[Route("[controller]")]
public class CategoriasController : Controller
{
    private readonly ICategoriaRepository _repository;
    private readonly IConfiguration _configuration;
    private readonly ILogger _logger;

    public CategoriasController(ICategoriaRepository repository, IConfiguration configuration, ILogger<CategoriasController> logger)
    {
        _repository = repository;
        _configuration = configuration;
        _logger = logger;

    }

    [HttpGet("LerArquivoConfiguracao")]
    public string GetValores()
    {
        var valor1 = _configuration ["chave1"];
        var valor2 = _configuration ["chave2"];

        var secao1 = _configuration ["secao1:chave2"];

        return $"Chave1 = {valor1} \nChave2 = {valor2} \nSeção1 => Chave2 = {secao1}";
    }

    //[HttpGet("produtos")]
    //public ActionResult<IEnumerable<Categoria>> GetCategoriasProdutos()
    //{
    //    _logger.LogInformation(" =============== ========== GET api/categorias/produtos ============= =====");

    //    //return _context.Categorias.AsNoTracking().Include(p => p.Produtos).ToList();
    //    var categorias =  _repository.GetCategorias().;
    //}

    [HttpGet]
    //[ServiceFilter(typeof(ApiLoggingFilter))]
    public ActionResult<IEnumerable<Categoria>> Get()
    {
        _logger.LogInformation(" =============== ========== GET api/categorias ============= =====");

        var categorias = _repository.GetCategorias();
        return Ok(categorias);
               
    }

    [HttpGet("{id:int}",Name ="ObterCategoria")]
    public ActionResult<Categoria> Get(int id)
    {
        var categoria = _repository.GetCategoria(id);

        _logger.LogInformation($" =============== ========== GET api/categorias/id = {id} ============= =====");

        if (categoria is null)
        {
            _logger.LogInformation($" =============== ========== GET api/categorias/id = {id} NOT FOUND============= =====");
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

        var categoriaCriada = _repository.Create(categoria);
        return new CreatedAtRouteResult("ObterCategoria", new { id = categoriaCriada.CategoriaId,nome = categoriaCriada.Nome, imagemUrl = categoriaCriada.ImagemUrl });
    }

    [HttpPut("{id:int}")]
    public ActionResult Put(int id,Categoria categoria)
    {
        if(id != categoria.CategoriaId)
        {
            return BadRequest();
        }

        var categoriaAtualizada = _repository.Update(categoria);
        return Ok(categoriaAtualizada);
            

    }

    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        var categoria = _repository.GetCategoria(id);
        if(categoria is null)
        {
            return NotFound($"Categoria com id: {id} não encontrada...");
        }

        _repository.Delete(id);
        return Ok(categoria);
    }
}
