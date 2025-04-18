using System;
using System.ComponentModel;
using System.Reflection;

namespace GerenciadorTarefasConsoleApp.Helpers
{
    public static class EnumHelper
    {
        // Retorna a descrição (se houver) ou o nome do enum
        public static string GetDescription<TEnum>(TEnum enumValue) where TEnum : Enum
        {
            var field = enumValue.GetType().GetField(enumValue.ToString());
            var attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
            return attribute?.Description ?? enumValue.ToString();
        }

        // Converte o enum em int
        public static int GetIdInt<TEnum>(TEnum enumValue) where TEnum : Enum
        {
            return Convert.ToInt32(enumValue);
        }

        // Converte int para enum (com verificação opcional)
        public static TEnum GetIdEnum<TEnum>(int intValue) where TEnum : Enum
        {
            if (!Enum.IsDefined(typeof(TEnum), intValue))
            {
                throw new ArgumentException($"Valor inválido para o enum {typeof(TEnum).Name}: {intValue}");
            }

            return (TEnum)Enum.ToObject(typeof(TEnum), intValue);
        }
    }
}
