using GerenciadorTarefasConsoleApp.Enums;
using GerenciadorTarefasConsoleApp.Models;
using GerenciadorTarefasConsoleApp.Repository;
using GerenciadorTarefasConsoleApp.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorTarefasConsoleApp.Helpers
{
    class InterfaceHelper
    {
        public static void Start()
        {


            LogHelper.Info("INICIANDO APLICAÇÃO");

            JsonHelper JsonHelper = new JsonHelper();
            JsonHelper.CreateIfNotExists();
        }

        public static void ShowMenu()
        {
            Console.WriteLine("================== SISTEMA DE TAREFAS ==================");
            Console.WriteLine("Menu");
            Console.WriteLine("----------------------");
            Console.WriteLine("1 - Cadastrar Tarefa");
            Console.WriteLine("2 - Listar Tarefas");
            Console.WriteLine("3 - Editar Tarefa");
            Console.WriteLine("4 - Buscar Tarefa por ID");
            Console.WriteLine("5 - Buscar Tarefa por Status");
            Console.WriteLine("0 - Encerrar programa");
            Console.WriteLine("----------------------");
            Console.WriteLine("Digite a opção desejada: ");
            int.TryParse(Console.ReadLine(), out int op);
            LogHelper.Info($"MENU - O usuário esconlheu a opção: {op}");
            Console.WriteLine("========================================================\n\n");

            ITarefaRepository _repository = new TarefaRepositoryImpl();
            TarefaService service = new TarefaService(_repository);
            
            switch (op)
            {
                case 1:
                    ViewCriaTarefa(ref service);
                    break;
                case 2:
                    ViewExibeListaDeTarefas(ref service);
                    break;
                case 3:
                    ViewEditaTarefaMenu(ref service);
                    break;
                case 4:
                    ViewBuscaTarefaPorId(ref service);
                    break;
                case 5:
                    ViewBuscaTarefaPorStatus(ref service);
                    break;
                case 0:
                    PararPrograma();
                    break;
            }
            Console.WriteLine("========================================================");
        }

        public static void PararPrograma()
        {
            Console.WriteLine("\nPressione qualquer tecla para sair...");
            Console.ReadKey();
            LogHelper.Info("Aplicação está sendo encerrada pelo usuário");
        }

        public static void LimparConsole() {
            Console.Clear();
        }

        public static void ViewExibeListaDeTarefas(ref TarefaService service) {
            Console.WriteLine("\n");
            LogHelper.Debug($"InterfaceHelper - Iniciando Exibição da Lista de Tarefa");
            List<Tarefa> lista = service.CarregaListaDeTarefa();
            if (lista.Count > 0)
            {
                Console.WriteLine("Lista de Tarefas:");
                foreach (var tarefa in lista)
                {
                    ViewExibeTarefa(tarefa);
                }
                Console.WriteLine("\n\n");
            }
            else
            {
                Console.WriteLine("Lista de tarefas vazia\n\n");
            }
            ShowMenu();
        }

        public static void ViewExibeListaDeTarefasEdit(ref TarefaService service)
        {
            Console.WriteLine("\n");
            LogHelper.Debug($"InterfaceHelper - Iniciando Exibição da Lista de Tarefa");
            List<Tarefa> lista = service.CarregaListaDeTarefa();
            if (lista.Count > 0)
            {
                Console.WriteLine("Lista de Tarefas:");
                Console.WriteLine();
                foreach (var tarefa in lista)
                {
                    ViewExibeTarefa(tarefa);
                }
                Console.WriteLine("\n\n");
            }
            else
            {
                Console.WriteLine("Lista de tarefas vazia\n\n");
            }
        }

        public static void ViewExibeTarefa(Tarefa tarefa) {
            Console.WriteLine($"ID: {tarefa.Id}");
            Console.WriteLine($"Título: {tarefa.Titulo}");
            Console.WriteLine($"Descrição: {tarefa.Descricao}");
            Console.WriteLine($"Data de Criação: {tarefa.DataCriacao}");
            Console.WriteLine($"Data de Conclusão: {tarefa.DataConclusao}");
            Console.WriteLine($"Status: {EnumHelper.GetDescription(tarefa.Status)}");
            Console.WriteLine("-----------------------------");
        }

        public static void ViewExibeListaTarefasAux(List<Tarefa> tarefas)
        {
            Console.WriteLine("Lista de Tarefas:");
            Console.WriteLine();
            foreach (var tarefa in tarefas)
            {
                ViewExibeTarefa(tarefa);
            }
            Console.WriteLine("\n\n");
        }

        public static void ViewCriaTarefa(ref TarefaService service)
        {
            Console.WriteLine("Digite o título da tarefa:");
            var titulo = Console.ReadLine();
            Console.WriteLine("Digite o descrição da tarefa:");
            var descricao = Console.ReadLine();
            try {
                service.CriarTarefa(titulo, descricao);
                Console.WriteLine("\n\nTarefa criada com sucesso!");
                Console.WriteLine("\n\n");
                ShowMenu();
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Ocorreu um erro ao criar a Tarefa: {titulo} Erro: {ex.Message}");
                Console.WriteLine("\n\n");
                ShowMenu();
            }
        }

        public static Boolean validaSimNaoEntrada(String entrada)
        {
            LogHelper.Debug($"ValidaEntrada - Entrada: {entrada}");
            // Lista de respostas positivas
            var respostasSim = new List<string> { "s", "sim", "yes", "y" };

            // Lista de respostas negativas
            var respostasNao = new List<string> { "n", "nao", "não", "no" };

            if (respostasSim.Contains(entrada))
            {
                return true;
            }
            else if (respostasNao.Contains(entrada))
            {
                return false;
            }
            else
            {
                Console.WriteLine("Entrada inválida");
                LogHelper.Warn("Entrada inválida");
                return false;
            }
        }
        public static void ViewBuscaTarefaPorId(ref TarefaService service) {

            Console.WriteLine("Digite o ID da tarefa: ");
            int.TryParse(Console.ReadLine(), out int id);

            var tarefa = service.BuscaTarefaPorId(id);

            ViewExibeTarefa(tarefa);

            Console.WriteLine($"Deseja editar a tarefa ?");
            string op = Console.ReadLine().Trim().ToLower();

            try
            {
                if (validaSimNaoEntrada(op))
                {
                    //service.ExcluirTarefa(tarefa);
                    Console.WriteLine("\n\nTarefa editada com sucesso!");
                }
                else
                {
                    Console.WriteLine("\n\nRetornando ao menu principal");
                    ShowMenu();
                }
                Console.WriteLine("\n\n");
                ShowMenu();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocorreu um erro ao editar a Tarefa: {tarefa.Titulo} Erro: {ex.Message}");
                Console.WriteLine("\n\n");
                ShowMenu();
            }
        }

        public static void ViewBuscaTarefaPorStatus(ref TarefaService service)
        {
            Console.WriteLine("BUSCA POR STATUS\n");
            Console.WriteLine("STATUS:");
            Console.WriteLine($"{EnumHelper.GetIdInt(StatusEnum.PENDENTE)} - {EnumHelper.GetDescription(StatusEnum.PENDENTE)}");
            Console.WriteLine($"{EnumHelper.GetIdInt(StatusEnum.INICIADA)} - {EnumHelper.GetDescription(StatusEnum.INICIADA)}");
            Console.WriteLine($"{EnumHelper.GetIdInt(StatusEnum.CONCLUIDA)} - {EnumHelper.GetDescription(StatusEnum.CONCLUIDA)}");
            Console.WriteLine($"{EnumHelper.GetIdInt(StatusEnum.CANCELADA)} - {EnumHelper.GetDescription(StatusEnum.CANCELADA)}");
            Console.WriteLine($"{EnumHelper.GetIdInt(StatusEnum.EXCLUIDA)} - {EnumHelper.GetDescription(StatusEnum.EXCLUIDA)}");

            Console.WriteLine("\nDigite o código de Status da tarefa: ");
            int.TryParse(Console.ReadLine(), out int statusId);
            Console.WriteLine();

            StatusEnum StatusEnumID = EnumHelper.GetIdEnum<StatusEnum>(statusId);

            var tarefas = service.BuscaTarefaPorStatus(StatusEnumID);

            if (tarefas.Count > 0)
            {
                ViewExibeListaTarefasAux(tarefas);
            }
            else {
                Console.WriteLine($"\nNenhuma tarefa encontrada com o Status \"{EnumHelper.GetDescription(StatusEnumID)}\"");
                Console.WriteLine("\n\n");
            }
            ShowMenu();
        }

        public static int ViewShowActionListEdicaoReturnAction() {
            Console.WriteLine("\nAÇÕES");
            Console.WriteLine($"{EnumHelper.GetIdInt(AcaoEnum.ALTERAR_STATUS)} - {EnumHelper.GetDescription(AcaoEnum.ALTERAR_STATUS)}");
            Console.WriteLine($"{EnumHelper.GetIdInt(AcaoEnum.ALTERAR_NOME)} - {EnumHelper.GetDescription(AcaoEnum.ALTERAR_NOME)}");
            Console.WriteLine($"{EnumHelper.GetIdInt(AcaoEnum.ALTERAR_DESC)} - {EnumHelper.GetDescription(AcaoEnum.ALTERAR_DESC)}");
            Console.WriteLine("-------------------------");
            Console.WriteLine("333 - Retornar ao Menu Principal");

            Console.WriteLine("\nEscolha uma ação :");
            int.TryParse(Console.ReadLine(), out int acao);
            return acao;
        }

        public static void ViewEditaTarefaMenu(ref TarefaService service)
        {
            List<Tarefa> tarefas = service.CarregaListaDeTarefa();
            ViewExibeListaTarefasAux(tarefas);
            Console.WriteLine("Escolha uma tarefa para ser editada: ");
            int.TryParse(Console.ReadLine(), out int tarefaId);

            var tarefa = service.BuscaTarefaPorId(tarefaId);
            Console.WriteLine();
            ViewEditaTarefa(ref service, tarefa);
        }



        public static void ViewEditaTarefa(ref TarefaService service, Tarefa tarefa)
        {
            ViewExibeTarefa(tarefa);
            Console.WriteLine("---------------");
            Console.WriteLine("EDITAR");
            var acao = ViewShowActionListEdicaoReturnAction();
            switch (acao)
            {
                case 333:
                    ShowMenu();
                    break;
                case 0:
                    ViewAlteraStatusTarefa(ref service, tarefa);
                    break;
            }
        }

        public static void ViewAlteraStatusTarefa(ref TarefaService service,Tarefa tarefa) {
            Console.WriteLine("\nSTATUS:");
            Console.WriteLine($"{EnumHelper.GetIdInt(StatusEnum.PENDENTE)} - {EnumHelper.GetDescription(StatusEnum.PENDENTE)}");
            Console.WriteLine($"{EnumHelper.GetIdInt(StatusEnum.INICIADA)} - {EnumHelper.GetDescription(StatusEnum.INICIADA)}");
            Console.WriteLine($"{EnumHelper.GetIdInt(StatusEnum.CONCLUIDA)} - {EnumHelper.GetDescription(StatusEnum.CONCLUIDA)}");
            Console.WriteLine($"{EnumHelper.GetIdInt(StatusEnum.CANCELADA)} - {EnumHelper.GetDescription(StatusEnum.CANCELADA)}");
            Console.WriteLine($"{EnumHelper.GetIdInt(StatusEnum.EXCLUIDA)} - {EnumHelper.GetDescription(StatusEnum.EXCLUIDA)}");
            Console.WriteLine("-------------------------");
            Console.WriteLine("333 - Retornar ao Menu Principal");

            Console.WriteLine("\nEscolha um status:");
            int.TryParse(Console.ReadLine(), out int novoStatus);
            if (!novoStatus.Equals(333))
            {
                StatusEnum status = EnumHelper.GetIdEnum<StatusEnum>(novoStatus);

                Console.WriteLine($"\nDeseja alterar o status da tarefa {tarefa.Id} - {tarefa.Titulo} para {EnumHelper.GetDescription(status)}?");
                string op = Console.ReadLine().Trim().ToLower();
                try
                {
                    if (validaSimNaoEntrada(op))
                    {
                        var listaTrefas = service.CarregaListaDeTarefa();
                        service.AlterarStatus(tarefa, listaTrefas, status);
                        Console.WriteLine("\n\nStatus alterado com sucesso!");
                    }
                    else
                    {
                        Console.WriteLine($"Não foi possível alterar status da Tarefa: {tarefa.Id} - {tarefa.Titulo}");
                    }
                    Console.WriteLine("\n\n");
                    ShowMenu();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ocorreu um erro ao alterar status da Tarefa: {tarefa.Id} - {tarefa.Titulo} Erro: {ex.Message}");
                    Console.WriteLine("\n\n");
                    ShowMenu();
                }
            }
            else {
                LimparConsole();
                ShowMenu();
            }
        }
    }
}
