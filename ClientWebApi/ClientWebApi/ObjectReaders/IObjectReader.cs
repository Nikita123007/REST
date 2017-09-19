using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientWebApi.ObjectReaders
{
    public interface IObjectReader
    {
        object ReadFromConsole(bool needEmbedded);
    }
}
