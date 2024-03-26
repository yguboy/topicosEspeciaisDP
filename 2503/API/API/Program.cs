using API.Models;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

List<Produto> produtos =
[
    new Produto("Celular", "Android", 6500),
    new Produto("Celular", "IOS", 12000),
    new Produto("Televisão", "LG", 18000),
    new Produto("Cafeteira", "Oaster", 1200)
];

//End Points = Funcionalidades - JSON

//Cadastrar um produto na lista
//A. Através das informações na URL
//B. Através das informações no corpo da requisição
//C. Realizar as operações de alteração e remoção da lista

//POST: http://localhost:5076/api/produto/cadastrar
app.MapPost("/api/produto/cadastrar", () => "Cadastro de produtos feito!");

//GET: http://localhost:5076/api/produto/listar
app.MapGet("/api/produto/listar", () => produtos);

//GET: http://localhost:5076/api/produto/buscar/{nomedoproduto}
app.MapGet("/api/produto/buscar/{nome}", ([FromRoute] string nome) =>
{
    //EndPoint com várias linhas de código 
    for (int i = 0; i < produtos.Count; i++)
    {
        if (produtos[i].Nome == nome)
        {
            return Results.Ok(produtos[i]);
        }
    }
    return Results.NotFound();
});

app.Run();