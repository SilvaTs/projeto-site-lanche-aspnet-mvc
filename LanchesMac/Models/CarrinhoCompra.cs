using LanchesMac.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LanchesMac.Models;

public class CarrinhoCompra
{
    private readonly AppDbContext _appDbContext;

    public CarrinhoCompra(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public string CarrinhoCompraId { get; set; }
    public List<CarrinhoCompraItem> CarrinhoCompraItens { get; set; }

    public static CarrinhoCompra GetCarrinho(IServiceProvider services)
    {
        // define uma sessão
        ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

        // obtem um serviço do tipo do nosso contexto
        var context = services.GetService<AppDbContext>();

        // obtem ou gera o Id do carrinho
        var carrinhoId = session.GetString("CarrinhoId") ?? Guid.NewGuid().ToString();

        // atribui o id do carrinho na Sessão
        session.SetString("CarrinhoId", carrinhoId);

        // retorna o carrinho com o contexto e o Id atribuido ou obtido
        return new CarrinhoCompra(context)
        {
            CarrinhoCompraId = carrinhoId
        };
    }

    public void AdicionarAoCarrinho(Lanche lanche)
    {
        var carrinhoCompraItem = _appDbContext.CarrinhoCompraItens.SingleOrDefault(
            s => s.Lanche.LancheId == lanche.LancheId &&
            s.CarrinhoCompraId == CarrinhoCompraId
            );

        if (carrinhoCompraItem == null)
        {
            carrinhoCompraItem = new CarrinhoCompraItem
            {
                CarrinhoCompraId = CarrinhoCompraId,
                Lanche = lanche,
                Quantidade = 1
            };

            _appDbContext.CarrinhoCompraItens.Add(carrinhoCompraItem);
        }
        else
        {
            carrinhoCompraItem.Quantidade++;
        }
        _appDbContext.SaveChanges();
    }

    public int RemoverDoCarrinho(Lanche lanche)
    {
        var carrinhoCompraItem = _appDbContext.CarrinhoCompraItens.SingleOrDefault(
                s => s.Lanche.LancheId == lanche.LancheId &&
                s.CarrinhoCompraId == CarrinhoCompraId
                );

        var quantidadeLocal = 0;

        if (carrinhoCompraItem is not null)
        {
            if (carrinhoCompraItem.Quantidade > 1)
            {
                carrinhoCompraItem.Quantidade--;
                quantidadeLocal = carrinhoCompraItem.Quantidade;
            }
            else
            {
                _appDbContext.CarrinhoCompraItens.Remove(carrinhoCompraItem);
            }

            _appDbContext.SaveChanges();
        }

        return quantidadeLocal;
    }

    public List<CarrinhoCompraItem> GetCarrinhoCompraItems()
    {
        return CarrinhoCompraItens ??
               (CarrinhoCompraItens =
               _appDbContext.CarrinhoCompraItens
               .Where(c => c.CarrinhoCompraId == CarrinhoCompraId)
               .Include(s => s.Lanche)
               .ToList());
    }

    public void LimparCarrinho()
    {
        var carrinhoItens = _appDbContext.CarrinhoCompraItens
                            .Where(r => r.CarrinhoCompraId == CarrinhoCompraId);

        _appDbContext.CarrinhoCompraItens.RemoveRange(carrinhoItens);
        _appDbContext.SaveChanges();
    }

    public decimal GetCarrinhoCompraTotal()
    {
        var total = _appDbContext.CarrinhoCompraItens
                            .Where(r => r.CarrinhoCompraId == CarrinhoCompraId)
                            .Select(s => s.Lanche.Preco * s.Quantidade).Sum();

        return total;
    }
}
