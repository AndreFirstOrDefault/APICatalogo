using APICatalogo.Context;
using APICatalogo.Models;
using APICatalogo.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers;

[ApiController]
[Route("[controller]")] // produtos
public class ProdutosController : Controller
{
    private readonly IProdutoRepository _repository;
    
    public ProdutosController(IProdutoRepository repository)
    {
        _repository = repository;
    }

    // 3 motivos para usar IEnumerable:
    // 1 - interface somente leitura
    // 2 - permite adiar a execução (trabalha por demandpra)
    // 3 - não precisa ter toda a coleção na memória

    // Usar ActionResult

    [HttpGet]
    public ActionResult<IQueryable<Produto>> Get()
    {
        var produtos = _repository.GetProdutos().ToList();
        if (produtos is null)
        {
            return NotFound("Produtos não encontrados...");
        }
        return Ok(produtos);
    }

    [HttpGet("{id:int:min(1)}", Name= "ObterProduto")]
    public ActionResult<Produto> Get(int id)
    {
        var produto = _repository.GetProduto(id);
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

        var novoProduto = _repository.Create(produto);
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
        
        bool atualizado = _repository.Update(produto);

        if (atualizado)
        {
            return Ok(produto);
        }
        else
        {
            return StatusCode(500, $"Falha ao atualizar o produto de id {id}");
        }

        
    }

    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        bool deletado = _repository.Delete(id);

        if(deletado)
        {
            return Ok($"Produdo de id: {id} foi excluido");
        }
        else
        {
            return StatusCode(500,$" Falha ao excluir o produto de id: {id}");
        }
    }     

}
