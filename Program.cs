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
        /// 
        public static Form1 FormInstace;
        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        private static extern bool AllocConsole();
        [STAThread]
        static void Main()
        {
            AllocConsole();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var form = new Form1();
            Program.FormInstace = form;
            Application.Run(form);
        }

        public static readonly object[] garfos = new object[5];
        public static readonly Thread[] filosofos = new Thread[5];

        public static void Iniciar()
        {
            string[] nomesFilosofos = { "Dijkstra", "Ada Lovelace", "Djikstra", "filosofo2", "filoso3" };
            for (int i = 0; i < garfos.Length; i++)
            {
                garfos[i] = new object();
            }

            for (int i = 0; i < filosofos.Length; i++)
            {
                int id = i;
                filosofos[i] = new Thread(() => Filosofos(id))
                { Name = nomesFilosofos[i] };
                filosofos[i].Start();
            }
        }

        private static void Filosofos(int id)
        {

            while (true)
            {
                FormInstace?.AtualizarEstadoBotao(id, "pensando");
                Console.WriteLine($"Filosofo {Thread.CurrentThread.Name} está pensando.");
                Thread.Sleep(new Random().Next(1000, 3000));

                FormInstace?.AtualizarEstadoBotao(id, "quer comer");
                Console.WriteLine($"Filosofo {Thread.CurrentThread.Name} quer comer.");
                 
                int garfoEsquerda = id;
                int garfoDireita = (id + 1) % garfos.Length;

                if(id == garfos.Length - 1)
                {
                    int temp = garfoEsquerda;
                    garfoEsquerda = garfoDireita;
                    garfoDireita = temp;
                }

                lock (garfos[garfoEsquerda])
                {
                    lock (garfos[garfoDireita])
                    {
                        FormInstace?.AtualizarEstadoBotao(id, "comendo");
                        Console.WriteLine($"Filosofo {Thread.CurrentThread.Name} está comendo.");
                        Thread.Sleep(new Random().Next(1000, 3000));
                    }
                }
            }
        }

    }
}
