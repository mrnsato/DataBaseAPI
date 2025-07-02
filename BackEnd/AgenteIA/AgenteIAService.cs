namespace AgenteIAService;

using System.Text;
using System.Text.Json;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

public class AgenteIAService
{
    private readonly HttpClient _httpClient;
    private readonly string _ollamaUrl = "http://localhost:11434/api/generate";
    private readonly string _connectionString;

    public AgenteIAService(IConfiguration config, HttpClient httpClient)
    {
        _httpClient = httpClient;
        _connectionString = config.GetConnectionString("DefaultConnection")!;
    }

    public async Task<string> ProcessarPerguntaAsync(string pergunta)
    {
        var prompt = $"Você é um especialista SQL Server. Gere apenas a instrução SQL baseada nessa pergunta: \"{pergunta}\"";

        var request = new
        {
            model = "llama3",
            prompt = prompt,
            stream = false
        };

        var response = await _httpClient.PostAsync(_ollamaUrl,
            new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json"));

        var respostaString = await response.Content.ReadAsStringAsync();
        using var json = JsonDocument.Parse(respostaString);
        var sqlQuery = json.RootElement.GetProperty("response").GetString();

        if (string.IsNullOrWhiteSpace(sqlQuery))
            return "Não foi possível gerar uma query SQL para essa pergunta.";

        var resultado = await ExecutarQueryAsync(sqlQuery!);
        return resultado;
    }

    private async Task<string> ExecutarQueryAsync(string sql)
    {
        using var conexao = new SqlConnection(_connectionString);
        var dados = await conexao.QueryAsync(sql);
        return JsonSerializer.Serialize(dados);
    }
}
