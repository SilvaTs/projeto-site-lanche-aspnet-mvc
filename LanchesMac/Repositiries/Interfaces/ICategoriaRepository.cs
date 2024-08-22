using LanchesMac.Models;

namespace LanchesMac.Repositiries.Interfaces;

public interface ICategoriaRepository
{
    IEnumerable<Categoria> Categorias { get; }
}
