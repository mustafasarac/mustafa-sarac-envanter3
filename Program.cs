using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    class Program
    {
        static void Main(string[] args)
        {
            int müşteriNumarası = 15000000;
            ÇalıştırmaMotoru.KomutÇalıştır("MuhasebeModulu", "MaaşYatır", new object[] { müşteriNumarası });
            ÇalıştırmaMotoru.KomutÇalıştır("MuhasebeModulu", "YıllıkÜcretTahsilEt", new object[] { müşteriNumarası });
            ÇalıştırmaMotoru.BekleyenİşlemleriGerçekleştir();
        }
    }

    public class ÇalıştırmaMotoru
    {
        public static List<Thread> threads { get; set; }

        public static object[] KomutÇalıştır(string modülSınıfAdı, string methodAdı, object[] inputs)
        {
            if(threads.Where(a => a.IsAlive == true).Count() > 4)
            {
                Thread.Sleep(5000);
            }

            Assembly assembly = Assembly.LoadFile("C:\\Users\\mustafa\\source\\repos\\ConsoleApp3\\ConsoleApp3\\bin\\Debug\\ConsoleApp3.exe");
            Type type = assembly.GetType("ConsoleApp3." + modülSınıfAdı);
            object classInstance = Activator.CreateInstance(type, null);
            MethodInfo method = type.GetMethod(methodAdı, BindingFlags.NonPublic | BindingFlags.Instance);
            var result = method.Invoke(classInstance, inputs);
            return new object[] { result };
        }

        public static void BekleyenİşlemleriGerçekleştir()
        {
            int müşteriNumarası = 15000000;
            var thread = new Thread(() =>
            {
                KomutÇalıştır("MuhasebeModulu", "MaaşYatır", new object[] { müşteriNumarası });
                KomutÇalıştır("MuhasebeModulu", "YıllıkÜcretTahsilEt", new object[] { müşteriNumarası });
            });

            thread.Start();
            threads.Add(thread);
        }
    }

    public class MuhasebeModülü
    {
        public MuhasebeModülü()
        {

        }

        private void MaaşYatır(int müşteriNumarası)
        {
            // gerekli işlemler gerçekleştirilir.
            Console.WriteLine(string.Format("{0} numaralı müşterinin maaşı yatırıldı.", müşteriNumarası));
        }

        private void YıllıkÜcretTahsilEt(int müşteriNumarası)
        {
            // gerekli işlemler gerçekleştirilir.
            Console.WriteLine("{0} numaralı müşteriden yıllık kart ücreti tahsil edildi.", müşteriNumarası);
        }

        private void OtomatikÖdemeleriGerçekleştir(int müşteriNumarası)
        {
            // gerekli işlemler gerçekleştirilir.
            Console.WriteLine("{0} numaralı müşterinin otomatik ödemeleri gerçekleştirildi.", müşteriNumarası);
        }
    }

    public class Veritabanıİşlemleri
    {

    }
}
