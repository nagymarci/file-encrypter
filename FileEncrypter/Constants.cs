using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileEncrypter
{
    static class Constants
    {
        static public string DefaultSalt { get; } = "878353BC595A4E2CA430F3CE12CB3690";
        static public int DefaultIterations { get; } = 1000000;
    }
}
