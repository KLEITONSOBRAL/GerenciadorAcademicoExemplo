using GerenciadorAcademicoExemplo.Models;
using GerenciadorAcademicoExemplo.Services;

// O arquivo é copiado para a pasta de saída do projeto na compilação.
string caminho = Path.Combine(AppContext.BaseDirectory, "Dados", "projetos.json");
var servico = new ProjetoArquivoService(caminho);

try
{
    List<Projeto> projetos = await servico.CarregarAsync();

    Console.WriteLine("Projetos encontrados no arquivo JSON:");
    foreach (Projeto projeto in projetos)
        Console.WriteLine($"{projeto.Id} - {projeto.Titulo} ({projeto.CargaHoraria} horas)");

    // Demonstra a persistência: inclui um projeto somente se ainda não existir.
    if (!projetos.Any(p => p.Id == 4))
    {
        projetos.Add(new Projeto
        {
            Id = 4,
            Titulo = "Feira de Ciências",
            CargaHoraria = 25
        });

        await servico.SalvarAsync(projetos);
        Console.WriteLine("\nNovo projeto salvo no JSON.");
    }
    else
    {
        Console.WriteLine("\nO projeto de exemplo já estava cadastrado.");
    }

    Console.WriteLine("\nTotal de projetos: " + projetos.Count);
    Console.WriteLine("Arquivo utilizado: " + caminho);
}
catch (IOException erro)
{
    Console.WriteLine("Não foi possível acessar o arquivo: " + erro.Message);
}
catch (System.Text.Json.JsonException erro)
{
    Console.WriteLine("O arquivo não contém um JSON válido: " + erro.Message);
}
