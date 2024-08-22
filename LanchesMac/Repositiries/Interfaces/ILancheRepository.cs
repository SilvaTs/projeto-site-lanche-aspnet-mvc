using LanchesMac.Models;

namespace LanchesMac.Repositiries.Interfaces;

public interface ILancheRepository
{
    IEnumerable<Lanche> Lanches { get; }
    IEnumerable<Lanche> LanchesPreferido { get; }
    Lanche GetLancheById(int lancheId);
}
