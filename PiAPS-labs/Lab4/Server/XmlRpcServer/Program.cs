using System;
using CookComputing.XmlRpc;

namespace XmlRpcServer
{
    class Program
    {
        public class StateNameService : XmlRpcService
        {
            [XmlRpcMethod("examples.getStateName",
              Description = "Return name of state given its number")]
            public string getStateName(int stateNum)
            {
                if (stateNum == 41)
                    return "South Dakota";
                else
                    return "Don't know";
            }
        }
    }
}
