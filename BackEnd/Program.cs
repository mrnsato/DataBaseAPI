using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using AplicacaoTecnologiaAPI.Data;
using TecnologiaAPI;
using AplicacaoAPI;
using AplicacaoTecnologiaAPI.Entities;
using OpenAIService;



var builder = WebApplication.CreateBuilder(args);

// Configurar conexão com o banco de dados
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IAService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirTudo",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

var app = builder.Build();

app.UseCors("PermitirTudo");

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

app.MapPut("/tecnologia/{id}", async (int id, Tecnologia inputTecnologia, AppDbContext db) =>
{
    var tecnologia = await db.Tecnologia.FindAsync(id);

    if (tecnologia == null)
        return Results.NotFound();

    tecnologia.Nome = inputTecnologia.Nome;

    await db.SaveChangesAsync();
    return Results.Ok(tecnologia);
});

app.MapDelete("/tecnologia/{id}", async (int id, AppDbContext db) =>
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


// Definição das rotas
app.MapGet("/", () => "API rodando!");
app.MapPost("/ia/perguntar", async (HttpContext context, IAService iaService) =>
{
    var request = await context.Request.ReadFromJsonAsync<PerguntaRequest>();
    if (request == null || string.IsNullOrWhiteSpace(request.Pergunta))
    {
        return Results.BadRequest("Erro: A pergunta está vazia ou inválida.");
    }

    var resposta = await iaService.GerarResposta(request.Pergunta);
    return Results.Ok(resposta);
});


app.Run();

// Classe para deserializar o JSON recebido
public class PerguntaRequest
{
    public required string Pergunta { get; set; }
}



