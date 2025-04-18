using GerenciadorTarefasConsoleApp.Enums;
using GerenciadorTarefasConsoleApp.Helpers;
using GerenciadorTarefasConsoleApp.Models;
using GerenciadorTarefasConsoleApp.Services;
using log4net;
using log4net.Config;
using System.Reflection;

class Program
{
    static void Main(string[] args)
    {
        InterfaceHelper.Start();
        InterfaceHelper.ShowMenu();
    }
}