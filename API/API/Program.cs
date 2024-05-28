using System.ComponentModel.DataAnnotations;
using API.Models;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

//Registrar o serviço de banco de dados na aplicação
builder.Services.AddDbContext<AppDataContext>();

//Builder que libera as credencias de qualquer origem para conectar com o front
builder.Services.AddCors(
    options =>
    {
        options.AddPolicy("AcessoTotal",
            builder => builder.
                AllowAnyOrigin().
                AllowAnyHeader().
                AllowAnyMethod());
    }
);

var app = builder.Build();

List<Produto> produtos = new List<Produto>();

//End Points = Funcionalidades - JSON

//POST: http://localhost:5076/api/produto/cadastrar
app.MapPost("/api/produto/cadastrar", ([FromBody] Produto produto, [FromServices] AppDataContext context) =>
{
    //Validando os atributos do objeto produto
    List<ValidationResult> erros = new List<ValidationResult>();
    if (!Validator.TryValidateObject(produto, new ValidationContext(produto), erros, true))
    {
        return Results.BadRequest(erros);
    }

    //Adicionando o produto dentro da tabela no banco de dados
    //Verificação de se a tabela possui uma coluna com o mesmo nome, se tiver, não cadastra
    Produto? produtoBuscado = context.Produtos.FirstOrDefault(x => x.Nome == produto.Nome);

    if (produtoBuscado is null)
    {
        //Criar coluna com nome maisculo: produto.Nome = produto.Nome.ToUpper();
        context.Produtos.Add(produto);
        context.SaveChanges();
        return Results.Created($"/api/produto/buscar{produto.Id}", produto);
    }
    return Results.BadRequest("Já existe um produto com o mesmo nome");
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

// PUT: http://localhost:5076/api/produto/alterar/{iddoproduto}
app.MapPut("/api/produto/alterar/{id}", ([FromRoute] string id, [FromBody] Produto produtoAlterado,
[FromServices] AppDataContext context) =>
{
    //Endpoint com varias linhas de código
    Produto? produto = context.Produtos.Find(id);

    if (produto is null)
    {
        return Results.NotFound("Produto não encontrado!");
    }
    produto.Nome = produtoAlterado.Nome;
    produto.Descricao = produtoAlterado.Descricao;
    produto.Preco = produtoAlterado.Preco;

    context.Produtos.Update(produto);
    context.SaveChanges();

    return Results.Ok("Produto alterado com sucesso!");
});

// DELETE: http://localhost:5076/api/produto/deletar/{iddoproduto}
app.MapDelete("/api/produto/deletar{id}", ([FromRoute] string id,
[FromServices] AppDataContext context) =>
{
    //Endpoint com varias linhas de código
    Produto? produto = context.Produtos.Find(id);

    if (produto is null)
    {
        return Results.NotFound("Produto não encontrado!");
    }
    context.Produtos.Remove(produto);
    context.SaveChanges();
    return Results.Ok(produtos);
});

//Utilizar o cors para ter acesso as credencias
app.UseCors("AcessoTotal");

app.Run();

//Exercicios para 0 EF
//1. Cadastrar o objeto de produto no banco
//2. Listar os registros da tabela