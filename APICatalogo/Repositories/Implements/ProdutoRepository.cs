﻿using APICatalogo.Context;
using APICatalogo.Models;
using APICatalogo.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Repositories.Implements;

public class ProdutoRepository : IProdutoRepository
{
    private readonly AppDbContext _context;

    public ProdutoRepository(AppDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Produto> GetProdutos()
    {
        return _context.Produtos.ToList();
    }

    public Produto GetProduto(int id)
    {
        return _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);
        
    }

    public Produto Create(Produto produto)
    {
        if(produto is null) 
            throw new ArgumentNullException(nameof(produto));
         _context.Produtos.Add(produto);
        return produto;
    }

    public Produto Update(Produto produto)
    {
        if( produto is null)
            throw new ArgumentNullException(nameof(produto));
        _context.Entry(produto).State = EntityState.Modified;
        _context.SaveChanges();
        return produto;
    }

    public Produto Delete(int id)
    {
        var produto = _context.Produtos.Find(id);
        if(produto is null ) 
            throw new ArgumentNullException(nameof(produto));
        _context.Produtos.Remove(produto);
        return produto;

    }
    
    
}
