using CarRental.Business.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SM = System.ServiceModel;

namespace CarRental.ServiceHost
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting up services...");
            Console.WriteLine("");

            SM.ServiceHost host = new SM.ServiceHost(typeof(InventoryManager));
            host.Open();

            Console.WriteLine("");
            Console.WriteLine("Press [Enter] to exit");
            Console.ReadLine();
        }
    }
}
