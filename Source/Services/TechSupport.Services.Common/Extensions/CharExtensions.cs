using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace TechSupport.Services.Common.Extensions
{
    public static class CharExtensions
    {
        public static bool IsEnglishLetter(this char symbol)
        {
            return !((symbol < 65 || 90 < symbol) && (symbol < 97 || 122 < symbol));
        }
    }
}
    