using Avalonia.Data.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Szakdolgozat.Classes
{
    public class Converters
    {
        //Minden FuncValueConverter csak a VM felé konvertál.
        public static FuncMultiValueConverter<string, string> LeaveTypeNameChange { get; } =
            new FuncMultiValueConverter<string, string>((IEnumerable<string?> a) =>
            {
                return a.FirstOrDefault();
            });
    }
}
