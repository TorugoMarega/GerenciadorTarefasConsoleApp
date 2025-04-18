using GerenciadorTarefasConsoleApp.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorTarefasConsoleApp.Helpers
{
    public static class EnumHelper
    {
        public static string GetDescription(StatusEnum statusEnum)
        {
            var field = statusEnum.GetType().GetField(statusEnum.ToString());
            var attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
            return attribute?.Description ?? statusEnum.ToString();
        }

        public static int GetIdInt(StatusEnum status)
        {
            int statusAsInt = (int)status;
            return statusAsInt;
        }

        public static StatusEnum GetIdEnum(int statusInt)
        {
            StatusEnum status = (StatusEnum)statusInt;
            return status;
        }
    }
}
