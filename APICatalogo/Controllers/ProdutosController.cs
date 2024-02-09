using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers;

[ApiController]
[Route("[controller]")] // produtos
public class ProdutosController : Controller
{
    private readonly AppDbContext _context;

    public ProdutosController(AppDbContext context)
    {
        _context = context;
    }

    // 3 motivos para usar IEnumerable:
    // 1 - interface somente leitura
    // 2 - permite adiar a execução (trabalha por demandpra)
    // 3 - não precisa ter toda a coleção na memória

    // Usar ActionResult

    [HttpGet] 
    public async Task<ActionResult<IEnumerable<Produto>>> GetAsync()
    {
        var produtos = _context.Produtos.AsNoTracking();
        if(produtos is null)
        {
            return NotFound("Produtos não encontrados...");
        }
        return await produtos.Take(10).ToListAsync();
    }

    [HttpGet("{id:int:min(1)}", Name= "ObterProduto")]
    //public async Task<ActionResult<Produto>> GetAsync(int id, [BindRequired] string nome)
    // public async Task<ActionResult<Produto>> GetAsync([FromQuery]int id) // = https://localhost:7128/produtos/1/?id=2
    public async Task<ActionResult<Produto>> GetAsync(int id)
    {
        //var nomeProduto = nome;

        var produto = await _context.Produtos.AsNoTracking().FirstOrDefaultAsync(p => p.ProdutoId == id);
        if(produto is null)
        {
            return NotFound("Produto não encontrado...");
        }

        return  produto;
    }

    [HttpPost]
    public async Task<ActionResult> PostAsync(Produto produto)
    {
        if (produto is null)
            return BadRequest();

       await _context.Produtos.AddAsync(produto);
        _context.SaveChanges();
        return  new CreatedAtRouteResult("ObterProduto",
            new {id=produto.ProdutoId},produto);
    }

    [HttpPut("{id:int}")]
    public async Task <ActionResult> PutAsync(int id,Produto produto) 
    {
        if(id != produto.ProdutoId)
        {
            return BadRequest();
        }

        _context.Entry(produto).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return Ok(produto);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteAsync(int id)
    {
        var produto = await _context.Produtos.FirstOrDefaultAsync(p => p.ProdutoId == id);
        //var produto = _context.Produtos.Find(id);

        if(produto is null)
        {
            return NotFound("Produto não localizado...");
        }
        _context.Produtos.Remove(produto);
        _context.SaveChanges();

        return Ok(produto);
    }

}
