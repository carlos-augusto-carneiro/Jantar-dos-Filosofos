using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Jantar_dos_Filosofos
{
    internal static class Program
    {
        /// <summary>
        /// Ponto de entrada principal para o aplicativo.
        /// </summary>
        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        private static extern bool AllocConsole();
        [STAThread]
        static void Main()
        {
            AllocConsole();
            Iniciar();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        private static readonly object[] garfos = new object[5];
        private static readonly Thread[] filosofos = new Thread[5];

        public static void Iniciar()
        {
            for (int i = 0; i < garfos.Length; i++)
            {
                garfos[i] = new object();
            }

            for (int i = 0; i < filosofos.Length; i++)
            {
                int id = i;
                filosofos[i] = new Thread(() => Filosofos(id));
                filosofos[i].Start();

            }
        }

        private static void Filosofos(int id)
        {
            while (true)
            {
                Console.WriteLine($"Filosofo {id} está pensando.");
                Thread.Sleep(new Random().Next(1000, 3000));

                Console.WriteLine($"Filosofo {id} quer comer.");
                 
                int garfoEsquerda = id;
                int garfoDireita = (id + 1) % garfos.Length;

                if(id == garfos.Length - 1)
                {
                    int temp = garfoEsquerda;
                    garfoEsquerda = garfoDireita;
                    garfoDireita = temp;
                }

                lock (garfos[id])
                {
                    lock (garfos[garfoDireita])
                    {
                        Console.WriteLine($"Filosofo {id} está comendo.");
                        Thread.Sleep(new Random().Next(1000, 3000));
                    }
                }
            }
        }

    }
}
