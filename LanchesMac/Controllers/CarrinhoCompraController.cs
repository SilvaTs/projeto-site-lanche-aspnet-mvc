﻿using LanchesMac.Models;
using LanchesMac.Repositiries.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LanchesMac.Controllers;

public class CarrinhoCompraController : Controller
{
    private readonly ILancheRepository _lancheRepository;
    private readonly CarrinhoCompra _carrinhoCompra;

    public CarrinhoCompraController(ILancheRepository lancheRepository, CarrinhoCompra carrinhoCompra)
    {
        _lancheRepository = lancheRepository;
        _carrinhoCompra = carrinhoCompra;
    }

    public IActionResult Index()
    {
        return View();
    }
}
