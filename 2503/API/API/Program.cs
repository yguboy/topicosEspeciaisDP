using API.Models;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

//Registrar o serviço de banco de dados na aplicação
builder.Services.AddDbContext<AppDataContext>();

var app = builder.Build();

List<Produto> produtos = new List<Produto>();

//End Points = Funcionalidades - JSON

//POST: http://localhost:5076/api/produto/cadastrar
app.MapPost("/api/produto/cadastrar/", ([FromBody] Produto produto, [FromServices] AppDataContext context) =>
{
    //Adicionando o produto dentro da tabela no banco de dados
    context.Produtos.Add(produto);
    context.SaveChanges();
    produtos.Add(produto);
    return Results.Created("", produto);
});

//GET: http://localhost:5076/api/produto/listar
app.MapGet("/api/produto/listar", ([FromServices] AppDataContext context) =>
{
    if (context.Produtos.Any())
    {
        return Results.Ok(context.Produtos.ToList());
    }
    return Results.NotFound("Produto não encontrado!");
});

//GET: http://localhost:5076/api/produto/buscar/{iddoproduto}
app.MapGet("/api/produto/buscar/{id}", ([FromRoute] string id, [FromServices] AppDataContext context) =>
{
    //EndPoint com várias linhas de código 
    //x é apenas um apelido qualquer
    Produto? produto = context.Produtos.FirstOrDefault(x => x.Id == id);

    if (produto is null)
    {
        return Results.NotFound("Produto não encontrado!");
    }
    return Results.Ok(produtos);
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

//Exercicios para 0 EF
//1. Cadastrar o objeto de produto no banco
//2. Listar os registros da tabela