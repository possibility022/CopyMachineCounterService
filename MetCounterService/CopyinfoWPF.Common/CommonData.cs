using System.Collections.Generic;
using System.Windows;

namespace CopyinfoWPF.Common
{
    public class CommonData
    {
        public static string DateTimeFormat = "dd.MM.yyyy";

        public const string AllStandardCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        public static IReadOnlyDictionary<Enums.PageSizes, Size> PageSizes = new Dictionary<Enums.PageSizes, Size>()
        {
            { Enums.PageSizes.A4, new Size(1654, 2339) },
            { Enums.PageSizes.A5, new Size(1165, 1654) }
        };
    }
}
