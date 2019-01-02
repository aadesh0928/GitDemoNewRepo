using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole
{
    class Program
    {
        public class Galaxies
        {

            public System.Collections.Generic.IEnumerable<Galaxy> NextGalaxy
            {
                get
                {
                    yield return new Galaxy { Name = "Tadpole", MegaLightYears = 400 };
                    yield return new Galaxy { Name = "Pinwheel", MegaLightYears = 25 };
                    yield return new Galaxy { Name = "Milky Way", MegaLightYears = 0 };
                    yield return new Galaxy { Name = "Andromeda", MegaLightYears = 3 };
                }
            }

        }

        public class Galaxy
        {
            public String Name { get; set; }
            public int MegaLightYears { get; set; }
        }
        static IEnumerable<int> GetNumbers(int n)
        {
            for(int x=0; x < n; x++)
            {

                yield return x*x;

                
            }
        }
        static void Main(string[] args)
        {
            //foreach(var x in GetNumbers(10))
            //{
            //    Console.WriteLine($"{x}");
            //}

            Galaxies galaxies = new Galaxies();
            foreach(Galaxy galaxy in galaxies.NextGalaxy)
            {
                Console.WriteLine($"Galaxy name is {galaxy.Name}, and it's light years is {galaxy.MegaLightYears}");
            }
            //foreach()
            Console.ReadKey();
        }
    }
}
