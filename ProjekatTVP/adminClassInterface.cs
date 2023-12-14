using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatTVP
{
    // C# 7.3 DOES NOT SUPPORT PUBLIC INTERFACE PROPERTIES AAAAAAAAAAAAAAAAAAAAAAAAA and ofc i cant change the version
    // Bataljujem zato sto ne radi lepo a ne mogu da promenim c# verziju jer mi Crashuje ceo kompjuter slomio bih nesto
    public interface adminClassInterface
    {
        string[] textBoxesNames { get; set; }
    }
}
