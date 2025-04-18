using GerenciadorTarefasConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GerenciadorTarefasConsoleApp.Helpers
{
    public class JsonHelper
    {
        private readonly string _caminhoArquivo;

        private readonly string _nomeArquivo = "minhaListaTarefas.json";

        public JsonHelper()
        {
            var pasta = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"..\..\..\DB"));
            Directory.CreateDirectory(pasta); //Cria pasta caso n exista
            _caminhoArquivo = Path.Combine(pasta, _nomeArquivo);
        }

        public List<Tarefa> ReadJson<Tarefa>()
        {
            LogHelper.Debug($"JSON_HELPER - Lendo arquivo {_nomeArquivo}");
            if (!File.Exists(_caminhoArquivo))
            {
                return new List<Tarefa>();
            }
            var json = File.ReadAllText(_caminhoArquivo);
            var deserialize = JsonSerializer.Deserialize<List<Tarefa>>(json) ?? new List<Tarefa>();
            LogHelper.Debug($"Quantidade de tarefas da lista: {deserialize.Count}");
            return deserialize;
        }
        public void SaveJson<T>(List<T> dados)
        {
            LogHelper.Debug($"JSON_HELPER - Persistindo dados no arquivo: {_nomeArquivo}");
            var json = JsonSerializer.Serialize(dados, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_caminhoArquivo, json);
        }

        public void CreateIfNotExists()
        {
            LogHelper.Debug($"JSON_HELPER - Caminho do Arquivo JSON: {_caminhoArquivo}");
            if (!File.Exists(_caminhoArquivo))
            {
                // Se o arquivo não existir, cria o arquivo com uma lista vazia
                LogHelper.Debug($"JSON_HELPER - Criando Arquivo JSON de armazenamento no diretório: {_caminhoArquivo}");
                var listaVazia = new List<Tarefa>(); // Ou o tipo correto da sua lista
                var json = JsonSerializer.Serialize(listaVazia, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(_caminhoArquivo, json);
            }
            else {
                LogHelper.Debug($"JSON_HELPER - Arquivo JSON já existe");
            }
               
        }
    }
}
