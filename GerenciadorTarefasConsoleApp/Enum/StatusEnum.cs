using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorTarefasConsoleApp.Enum
{
    public enum StatusEnum
    {
        [Description("Pendente")]
        PENDENTE,
        [Description("Iniciada")]
        INICIADA,
        [Description("Concluída")]
        CONCLUIDA,
        [Description("Cancelada")]
        CANCELADA,
        [Description("Excluída")]
        EXCLUIDA
    }
}
