using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileEncrypter
{
    interface ICryptography
    {
        byte[] HashPassword(string password);
    }
}
