using LanchesMac.Context;
using LanchesMac.Models;
using LanchesMac.Repositiries.Interfaces;

namespace LanchesMac.Repositiries;

public class CategoriaRepository : ICategoriaRepository
{
    private readonly AppDbContext _appDbContext;

    public CategoriaRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public IEnumerable<Categoria> Categorias => _appDbContext.Categorias;
}
