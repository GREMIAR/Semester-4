using System;
using System.Runtime.InteropServices;
namespace COMbine
{
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.None)]
    [ComSourceInterfaces(typeof(IStarsurgeEvents))]
    [Guid("0EAE5E5D-EA09-42DA-A2C6-B3E2C40E0A95")]
    public class Starsurge : IStarsurge
    {
        public String PingPong(string str)
        {
            if (str == "Пинг")
            {
                return "Понг";
            }
            else if ( str == "Понг")
            {
                return "ПИН";
            }
            return "ОТ ВИН ТА";
        }
    }
    [ComVisible(true)]
    [Guid("CB6C156B-B9B9-4DCA-B33F-E1F5F9709F13")]
    public interface IStarsurge
    {

    }
    [ComVisible(true)]
    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    [Guid("A4B5EFB4-4892-4424-9875-84A838B5E5A4")]
    public interface IStarsurgeEvents
    {

    }
}
