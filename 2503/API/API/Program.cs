using API.Models;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

List<Produto> produtos = new List<Produto>();

//End Points = Funcionalidades - JSON

//Cadastrar um produto na lista
//A. Através das informações na URL
//B. Através das informações no corpo da requisição
//C. Realizar as operações de alteração e remoção da lista

//POST: http://localhost:5076/api/produto/cadastrar
app.MapPost("/api/produto/cadastrar/", ([FromBody] Produto produto) =>
{
    //Adicionando o produto dentro da lista
    produtos.Add(produto);

    return Results.Created("", produto);
});

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
    return Results.NotFound("Produto não encontrado!");
});

// PATCH: http://localhost:5076/api/produto/alterar/{id}
app.MapPatch("/api/produto/alterar/{id}", ([FromRoute] string id, [FromBody] Produto produtoAtualizado) =>
{
    var produtoExistente = produtos.FirstOrDefault(p => p.Id == id);
    if (produtoExistente != null)
    {
        produtoExistente.Nome = produtoAtualizado.Nome;
        produtoExistente.Descricao = produtoAtualizado.Descricao;
        produtoExistente.Preco = produtoAtualizado.Preco;

    }
});

// DELETE: http://localhost:5076/api/produto/remover
app.MapDelete("/api/produto/remover", ([FromBody] Produto produtoParaRemover) =>
{
    var produtoExistente = produtos.FirstOrDefault(p =>
        p.Nome == produtoParaRemover.Nome &&
        p.Descricao == produtoParaRemover.Descricao &&
        p.Preco == produtoParaRemover.Preco);

    if (produtoExistente != null)
    {
        produtos.Remove(produtoExistente);
        return Results.NoContent();
    }

    return Results.NotFound("Produto não encontrado!");
});

app.Run();