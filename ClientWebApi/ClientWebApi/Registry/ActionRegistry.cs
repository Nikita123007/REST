using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientWebApi.ActionHandlers;

namespace ClientWebApi.Registry
{
    public class ActionRegistry
    {
        private Dictionary<string, IActionHandler> Dictionary = new Dictionary<string, IActionHandler>();

        public void Reg(string key, IActionHandler actionHandler)
        {
            Dictionary.Add(key, actionHandler);
        }

        public IActionHandler Get(string str)
        {
            return Dictionary.SingleOrDefault(x => str.EndsWith(x.Key)).Value;
        }
    }
}
