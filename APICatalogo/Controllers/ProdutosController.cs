using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers;

[ApiController]
[Route("[controller]")]
public class ProdutosController : Controller
{
    private readonly AppDbContext _context;

    public ProdutosController(AppDbContext context)
    {
        _context = context;
    }

    // 3 motivos para usar IEnumerable:
    // 1 - interface somente leitura
    // 2 - permite adiar a execução (trabalha por demanda)
    // 3 - não precisa ter toda a coleção na memória

    // Usar ActionResult

    [HttpGet]
    public ActionResult<IEnumerable<Produto>> Get()
    {
        var produtos = _context.Produtos;
        if(produtos is null)
        {
            return NotFound("Produtos não encontrados...");
        }
        return produtos;
    }

    [HttpGet("{id:int}", Name= "ObterProduto")]
    public ActionResult<Produto> Get(int id)
    {
        var produto = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);
        if(produto is null)
        {
            return NotFound("Produto não encontrado...");
        }

        return produto;
    }

    [HttpPost]
    public ActionResult Post(Produto produto)
    {
        if (produto is null)
            return BadRequest();

        _context.Produtos.Add(produto);
        _context.SaveChanges();
        return new CreatedAtRouteResult("ObterProduto",
            new {id=produto.ProdutoId},produto);
    }

    [HttpPut("{id:int}")]
    public ActionResult Put(int id,Produto produto) 
    {
        if(id != produto.ProdutoId)
        {
            return BadRequest();
        }

        _context.Entry(produto).State = EntityState.Modified;
        _context.SaveChanges();

        return Ok(produto);
    }

    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        var produto = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);
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
