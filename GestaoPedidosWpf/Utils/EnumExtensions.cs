using System;
using System.ComponentModel;
using System.Reflection;

namespace GestaoPedidosWpf.Utils
{
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum valor)
        {
            var fi = valor.GetType().GetField(valor.ToString());
            var attr = fi.GetCustomAttribute<DescriptionAttribute>();
            return attr?.Description ?? valor.ToString();
        }
    }
}
