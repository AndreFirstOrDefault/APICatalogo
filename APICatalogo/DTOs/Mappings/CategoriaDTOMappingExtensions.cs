using APICatalogo.Models;

namespace APICatalogo.DTOs.Mappings;

public static class CategoriaDTOMappingExtensions
{
    public static CategoriaDTO? ToCategoriaDTO(this Categoria categoria)
    {
        if (categoria == null)
            return null;

        var categoriaDTO = new CategoriaDTO
        {
            CategoriaId = categoria.CategoriaId,
            Nome = categoria.Nome,
            ImagemUrl = categoria.ImagemUrl
        };

        return categoriaDTO;
    }

    public static Categoria? ToCategoria(this CategoriaDTO categoriaDTO)
    {
        if(categoriaDTO == null)
        {
            return null;
        }

        return new Categoria
        {
            CategoriaId = categoriaDTO.CategoriaId,
            Nome = categoriaDTO.Nome,
            ImagemUrl = categoriaDTO.ImagemUrl
        };
    }

    public static IEnumerable<CategoriaDTO> ToCategoriaDTOList(this IEnumerable<Categoria> categorias)
    {
        if(categorias is null || !categorias.Any())
        {
            return new List<CategoriaDTO>();
        }

        // Minha solução
        //var categoriasDTO = new List <CategoriaDTO>();
        //foreach (var categoria in categorias)
        //{
        //    var categoriaDTO = new CategoriaDTO
        //    {
        //        CategoriaId = categoria.CategoriaId,
        //        Nome = categoria.Nome,
        //        ImagemUrl = categoria.ImagemUrl
        //    };
        //    categoriasDTO.Add(categoriaDTO);

        //}
        //return categoriasDTO;

        // Solução do Macoratti
        return categorias.Select(categoria => new CategoriaDTO
        {
            CategoriaId = categoria.CategoriaId,
            Nome = categoria.Nome,
            ImagemUrl = categoria.ImagemUrl
        }).ToList();
    }
}
