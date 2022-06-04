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
        public String WhichSmesharik()
        {
            string smesharik = string.Empty;
            Random rnd = new Random();
            int value = rnd.Next(0, 9);
            switch (value)
            {
                case 0:
                    smesharik = "Крош";
                    break;
                case 1:
                    smesharik = "Ёжик";
                    break;
                case 2:
                    smesharik = "Бараш";
                    break;
                case 3:
                    smesharik = "Нюша";
                    break;
                case 4:
                    smesharik = "Кар-Карыч";
                    break;
                case 5:
                    smesharik = "Копатыч";
                    break;
                case 6:
                    smesharik = "Лосяш";
                    break;
                case 7:
                    smesharik = "Пин";
                    break;
                case 8:
                    smesharik = "Совунья";
                    break;
                default:
                    Console.WriteLine("Чёрный Ловелас");
                    break;
            }
            return smesharik;
        }
        public float CelsiusToFahrenheit(float degrees)
        {
            return degrees * 9 / 5 + 32;
        }
        public float FahrenheitToCelsius(float degrees)
        {
            return (degrees - 32) * 5 / 9;
        }
    }
    [ComVisible(true)]
    [Guid("CB6C156B-B9B9-4DCA-B33F-E1F5F9709F13")]
    public interface IStarsurge
    {
        String WhichSmesharik();
        float CelsiusToFahrenheit(float degrees);
        float FahrenheitToCelsius(float degrees);
    }
    [ComVisible(true)]
    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    [Guid("A4B5EFB4-4892-4424-9875-84A838B5E5A4")]
    public interface IStarsurgeEvents
    {

    }
}
