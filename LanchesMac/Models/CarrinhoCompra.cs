using LanchesMac.Context;

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
}
