using LanchesMac.Context;
using LanchesMac.Models;
using LanchesMac.Repositiries.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LanchesMac.Repositiries;

public class LancheRepository : ILancheRepository
{
    private readonly AppDbContext _appDbContext;

    public LancheRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public IEnumerable<Lanche> Lanches => _appDbContext.Lanches.Include(c => c.Categoria);

    public IEnumerable<Lanche> LanchesPreferido => _appDbContext.Lanches
                                                   .Where(l => l.IsLanchePreferido)
                                                   .Include(c => c.Categoria);

    public Lanche GetLancheById(int lancheId)
    {
        return _appDbContext.Lanches.FirstOrDefault(l => l.LancheId == lancheId);
    }
}
