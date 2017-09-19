using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientWebApi.ObjectReaders;

namespace ClientWebApi.Registry
{
    public class ObjectReaderRegistry
    {
        private Dictionary<string, IObjectReader> Dictionary = new Dictionary<string, IObjectReader>();

        public void Reg(string key, IObjectReader actionHandler)
        {
            Dictionary.Add(key, actionHandler);
        }

        public IObjectReader Get(string str)
        {
            return Dictionary.SingleOrDefault(x => str.StartsWith(x.Key)).Value;
        }
    }
}
