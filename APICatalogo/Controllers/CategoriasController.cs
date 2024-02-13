using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers;

[Route("[controller]")]
public class CategoriasController : Controller
{
    private readonly AppDbContext _context;
    private readonly IConfiguration _configuration;

    public CategoriasController(AppDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;

    }

    [HttpGet("LerArquivoConfiguracao")]
    public string GetValores()
    {
        var valor1 = _configuration ["chave1"];
        var valor2 = _configuration ["chave2"];

        var secao1 = _configuration ["secao1:chave2"];

        return $"Chave1 = {valor1} \nChave2 = {valor2} \nSeção1 => Chave2 = {secao1}";
    }

    [HttpGet("produtos")]
    public async Task<ActionResult<IEnumerable<Categoria>>> GetCategoriasProdutosAsync()
    {
        //return _context.Categorias.AsNoTracking().Include(p => p.Produtos).ToList();
        return _context.Categorias.AsNoTracking().Include(p => p.Produtos).Where(p => p.CategoriaId <= 5).ToList();
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Categoria>>> GetAsync()
    {
        try
        {
            //throw new DataMisalignedException();
            return await _context.Categorias.AsNoTracking().ToListAsync();
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao tratar a sua solicitação...");           
        }
        
        
        
    }

    [HttpGet("{id:int}",Name ="ObterCategoria")]
    public async Task<ActionResult<Categoria>> GetAsync(int id)
    {
        //throw new Exception("Exceção ao retornar a categoria pelo Id");

        //string[] teste = null;
        //if(teste.Length > 0)
        //{

        //}

        try
        {
            //throw new DataMisalignedException();
            var categoria = await _context.Categorias.AsNoTracking().FirstOrDefaultAsync(c => c.CategoriaId == id);
            if (categoria is null)
            {
                return NotFound("Categoria não encontrada");
            }

            return Ok(categoria);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao tratar a sua solicitação...");
        }
        
    }

    [HttpPost("{id:int}")]
    public async Task<ActionResult> PostAsync(int id, Categoria categoria)
    {
        if(categoria is null)
        {
            return BadRequest("Dados inválidos");
        }

       await _context.AddAsync(categoria);
        _context.SaveChangesAsync();

        return new CreatedAtRouteResult("ObterCategoria", new { id = categoria.CategoriaId }, categoria);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> PutAsync(int id, Categoria categoria)
    {
        if(id != categoria.CategoriaId)
        {
            return BadRequest();
        }

         _context.Entry(categoria).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return Ok(categoria);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteAsync(int id)
    {
        var categoria = await _context.Categorias.FirstOrDefaultAsync(c => c.CategoriaId == id);
        if(categoria is null)
        {
            return NotFound("Categoria com  não encontrada...");
        }

        _context.Remove(categoria);
        _context.SaveChangesAsync();
        return Ok(categoria);
    }
}
