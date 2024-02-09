using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers;

[Route("[controller]")]
public class CategoriasController : Controller
{
    private readonly AppDbContext _context;

    public CategoriasController(AppDbContext context)
    {
        _context = context;
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
