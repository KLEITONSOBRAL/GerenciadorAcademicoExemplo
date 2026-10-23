using System.Text.Json;
using GerenciadorAcademicoExemplo.Models;

namespace GerenciadorAcademicoExemplo.Services;

public class ProjetoArquivoService
{
    private readonly string _caminhoArquivo;
    private readonly JsonSerializerOptions _opcoes = new() { WriteIndented = true };

    public ProjetoArquivoService(string caminhoArquivo)
    {
        _caminhoArquivo = caminhoArquivo;
    }

    public async Task<List<Projeto>> CarregarAsync()
    {
        if (!File.Exists(_caminhoArquivo))
            return new List<Projeto>();

        string json = await File.ReadAllTextAsync(_caminhoArquivo);
        return JsonSerializer.Deserialize<List<Projeto>>(json) ?? new List<Projeto>();
    }

    public async Task SalvarAsync(List<Projeto> projetos)
    {
        string? pasta = Path.GetDirectoryName(_caminhoArquivo);
        if (!string.IsNullOrEmpty(pasta))
            Directory.CreateDirectory(pasta);

        string json = JsonSerializer.Serialize(projetos, _opcoes);
        await File.WriteAllTextAsync(_caminhoArquivo, json);
    }
}
