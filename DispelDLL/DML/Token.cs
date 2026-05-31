using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DispelDLL.DML
{
    public class Token : Usuario
    {
        public string UrlBase { get; set; }
        public string StrToken { get; set; }
        public DateTime DataAcesso { get; set; }
    }
}
