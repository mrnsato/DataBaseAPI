using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using AplicacaoTecnologiaAPI.Data;
using TecnologiaAPI;
using AplicacaoAPI;
using AplicacaoTecnologiaAPI.Entities;
using System.Net;


var builder = WebApplication.CreateBuilder(args);

// Configurar conexão com o banco de dados
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDbContext<AppDbContext>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirTudo",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});



var app = builder.Build();
var tecnologias = new List<Tecnologia>();
var aplicacoes = new List<Aplicacoes>();

app.UseCors("PermitirTudo");


// Frontend - Endpoints Minimal API
app.MapPost("/aplicacao", (Aplicacoes aplicacao) => {
    aplicacoes.Add(aplicacao); // 🔥 Salvando aplicação
    return Results.Ok("Aplicação cadastrada!");
});

app.MapPost("/tecnologia/inmemory", (Tecnologia tecnologia) =>
{
    tecnologias.Add(tecnologia); // 🔥 Salvando tecnologia
    return Results.Ok("Tecnologia cadastrada!");
});

//Frotend - CRUD Minimal API
app.MapPut("/tecnologia/{id}", (int id, Tecnologia tecnologia) => {
    var item = tecnologias.FirstOrDefault(t => t.Id == id);
    if (item == null) return Results.NotFound();
    item.Nome = tecnologia.Nome;
    return Results.Ok(item);
});

app.MapDelete("/tecnologia/{id}", (int id) => {
    var item = tecnologias.FirstOrDefault(t => t.Id == id);
    if (item == null) return Results.NotFound();
    tecnologias.Remove(item);
    return Results.Ok("Tecnologia deletada!");
});

app.MapPut("/aplicacao/{id}", (int id, Aplicacoes aplicacao) => {
    var item = aplicacoes.FirstOrDefault(a => a.Id == id);
    if (item == null) return Results.NotFound();
    item.Nome = aplicacao.Nome;
    return Results.Ok(item);
});

app.MapDelete("/aplicacao/{id}", (int id) => {
    var item = aplicacoes.FirstOrDefault(a => a.Id == id);
    if (item == null) return Results.NotFound();
    aplicacoes.Remove(item);
    return Results.Ok("Aplicação deletada!");
});




// Endpoints Minimal API para Aplicacao
app.MapPost("/aplicacoes", async (Aplicacoes aplicacao, AppDbContext db) =>
{
    db.Aplicacoes.Add(aplicacao);
    await db.SaveChangesAsync();
    return Results.Created($"/aplicacoes/{aplicacao.Id}", aplicacao);
});

app.MapGet("/aplicacoes", async (AppDbContext db) => await db.Aplicacoes.ToListAsync());

app.MapPut("/aplicacoes/{id}", async (int id, Aplicacoes inputAplicacao, AppDbContext db) =>
{
    var aplicacao = await db.Aplicacoes.FindAsync(id);

    if (aplicacao == null)
        return Results.NotFound();

    aplicacao.Nome = inputAplicacao.Nome;

    await db.SaveChangesAsync();
    return Results.Ok(aplicacao);
});

app.MapDelete("/aplicacoes/{id}", async (int id, AppDbContext db) =>
{
    var aplicacao = await db.Aplicacoes.FindAsync(id);

    if (aplicacao == null)
        return Results.NotFound();

    db.Aplicacoes.Remove(aplicacao);
    await db.SaveChangesAsync();
    return Results.Ok();
});


// Endpoints Minimal API para Tecnologia
app.MapGet("/tecnologia", async (AppDbContext db) => await db.Tecnologia.ToListAsync());

app.MapPost("/tecnologia", async (Tecnologia tecnologia, AppDbContext db) =>
{
    db.Tecnologia.Add(tecnologia);
    await db.SaveChangesAsync();
    return Results.Created($"/tecnologia/{tecnologia.Id}", tecnologia);
});

app.MapPut("/tecnologia/update/{id}", async (int id, Tecnologia inputTecnologia, AppDbContext db) =>
{
    var tecnologia = await db.Tecnologia.FindAsync(id);

    if (tecnologia == null)
        return Results.NotFound();

    tecnologia.Nome = inputTecnologia.Nome;

    await db.SaveChangesAsync();
    return Results.Ok(tecnologia);
});

app.MapDelete("/tecnologia/remove/{id}", async (int id, AppDbContext db) =>
{
    var tecnologia = await db.Tecnologia.FindAsync(id);

    if (tecnologia == null)
        return Results.NotFound();

    db.Tecnologia.Remove(tecnologia);
    await db.SaveChangesAsync();
    return Results.Ok();
});

// Endpoints Minimal API para AplicacaoTecnologia (Relacionamento Muitos-para-Muitos)
app.MapGet("/aplicacao-tecnologia", async (AppDbContext db) => await db.Aplicacao_Tecnologia.ToListAsync());

app.MapPost("/aplicacao-tecnologia", async (AplicacaoTecnologia aplicacaoTecnologia, AppDbContext db) =>
{
    db.Aplicacao_Tecnologia.Add(aplicacaoTecnologia);
    await db.SaveChangesAsync();
    return Results.Created("/aplicacao-tecnologia", aplicacaoTecnologia);
});

app.MapDelete("/aplicacao-tecnologia/{aplicacaoId}/{tecnologiaId}", async (int aplicacaoId, int tecnologiaId, AppDbContext db) =>
{
    var aplicacaoTecnologia = await db.Aplicacao_Tecnologia.FindAsync(aplicacaoId, tecnologiaId);

    if (aplicacaoTecnologia == null)
        return Results.NotFound();

    db.Aplicacao_Tecnologia.Remove(aplicacaoTecnologia);
    await db.SaveChangesAsync();
    return Results.Ok();
});


// Definição das rotas AI API
app.MapGet("/", () => "API rodando!");


//Endpoints para exibir Aplicacao_Tecnologia
app.MapGet("/aplicacoes-com-tecnologias", async (AppDbContext db) =>
{
    var resultado = await db.Aplicacoes
        .OrderBy(a => a.Nome)
        .Select(a => new
        {
            Aplicacao = a.Nome,
            Tecnologias = db.Aplicacao_Tecnologia
                .Where(at => at.AplicacaoId == a.Id)
                .Select(at => at.Tecnologia.Nome)
                .OrderBy(t => t)
                .ToList()
        })
        .ToListAsync();

    return Results.Ok(resultado);
});


app.Run();



