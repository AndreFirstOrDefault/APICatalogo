using APICatalogo.Models;
using APICatalogo.Repositories.Implements;
using APICatalogo.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace APICatalogo.Controllers;

[ApiController]
[Route("[controller]")] // produtos
public class ProdutosController : Controller
{
    private readonly UnityOfWork _uof;

    // não precisa implementar pq o repositório genérico ja implementa
    //private readonly IRepository<Produto> _repository;
    public ProdutosController(UnityOfWork uof)
    {
        _uof = uof;
    }

    // 3 motivos para usar IEnumerable:
    // 1 - interface somente leitura
    // 2 - permite adiar a execução (trabalha por demandpra)
    // 3 - não precisa ter toda a coleção na memória

    // Usar ActionResult

    [HttpGet("produtos/{id}")]
    public ActionResult <IEnumerable<Produto>> GetProdutosPorCategoria(int id)
    {
        var produtos = _uof.ProdutoRepository.GetProdutosPorCategoria(id);
        if(produtos is null)
        {
            return NotFound();
        }

        return Ok(produtos);
    }

    [HttpGet]
    public ActionResult<IQueryable<Produto>> Get()
    {
        var produtos = _uof.ProdutoRepository.GetAll().ToList();
        if (produtos is null)
        {
            return NotFound("Produtos não encontrados...");
        }
        return Ok(produtos);
    }

    [HttpGet("{id:int:min(1)}", Name= "ObterProduto")]
    public ActionResult<Produto> Get(int id)
    {
        var produto = _uof.ProdutoRepository.Get(p => p.ProdutoId == id);
        if(produto is null)
        {
            return NotFound("Produto não encontrado...");
        }

        return Ok(produto);
    }

    [HttpPost]
    public ActionResult Post(Produto produto)
    {
        if (produto is null)
            return BadRequest();

        var novoProduto = _uof.ProdutoRepository.Create(produto);
        _uof.Commit();
        return  new CreatedAtRouteResult("ObterProduto",
            new {id=novoProduto.ProdutoId},novoProduto);
    }

    [HttpPut("{id:int}")]
    public ActionResult Put(int id,Produto produto) 
    {
        if(id != produto.ProdutoId)
        {
            return BadRequest();
        }
        
        var produtoAtualizado = _uof.ProdutoRepository.Update(produto);
        _uof.Commit();
        return Ok(produto);


    }

    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        var produto = _uof.ProdutoRepository.Get(p => p.ProdutoId == id);

        if(produto == null)
        {
            return StatusCode(500, $" Falha ao excluir o produto de id: {id}");
            
        }
        _uof.ProdutoRepository.Delete(produto);
        _uof.Commit();
        return Ok(produto);
    }     

}
