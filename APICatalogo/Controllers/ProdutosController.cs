using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.AspNetCore.Mvc;

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

    [HttpGet("{id:int}")]
    public ActionResult<Produto> Get(int id)
    {
        var produto = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);
        if(produto is null)
        {
            return NotFound("Produto não encontrado...");
        }

        return produto;
    }

}
