using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace Jantar_dos_Filosofos
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Program.FormInstace = this;
            Program.Iniciar();
        }

        public void AtualizarEstadoBotao(int id, string estado) {
            if (InvokeRequired)
            {
                if (this.IsHandleCreated && !this.IsDisposed && !this.Disposing)
                {
                    Invoke(new Action(() => AtualizarEstadoBotao(id, estado)));
                }
                return;
            }

            Button botao = null;


            switch (id)
            {
                case 0: botao = button1; break;
                case 1: botao = button2; break;
                case 2: botao = button3; break;
                case 3: botao = button4; break;
                case 4: botao = button5; break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(id), "Invalid button ID provided.");
            }

            if (botao == null)
            {
                throw new InvalidOperationException($"Button with ID {id} is not initialized.");
            }
            botao.FlatStyle = FlatStyle.Flat;
            botao.FlatAppearance.BorderSize = 2;

            if (botao != null)
            {
                string nomeFilosofo = Program.filosofos[id].Name;
                switch(estado)
                {
                    case "pensando":
                        botao.BackColor = Color.Yellow;
                        botao.FlatAppearance.BorderColor = Color.Yellow;
                        switch(nomeFilosofo)
                        {
                            case "Alan Turing": botao.BackgroundImage = Properties.Resources.AlanTuringPensando; break;
                            case "Ada Lovelace": botao.BackgroundImage = Properties.Resources.AdaPensando; break;
                            case "Filosofo 2": botao.BackgroundImage = Properties.Resources.Filosofo2Pensando; break;
                            case "Djikstra": botao.BackgroundImage = Properties.Resources.DjikstraPensando; break;
                            default: botao.BackgroundImage = Properties.Resources.computer_science_philosopher_320x320; break;
                        }
                        break;
                    case "comendo":
                        botao.BackColor = Color.LightGreen;
                        botao.FlatAppearance.BorderColor = Color.LightGreen;
                        switch(nomeFilosofo)
                        {
                            case "Alan Turing": botao.BackgroundImage = Properties.Resources.AlanTuringComendo; break;
                            case "Ada Lovelace": botao.BackgroundImage = Properties.Resources.AdaComendo; break;
                            case "Filosofo 2": botao.BackgroundImage = Properties.Resources.Filosofo2Comendo; break;
                            case "Djikstra": botao.BackgroundImage = Properties.Resources.DjikstraComendo; break;
                            default: botao.BackgroundImage = Properties.Resources.computer_science_philosopher_320x320; break;
                        }
                        break;
                    case "quer comer":
                        botao.BackColor = Color.Red;
                        botao.FlatAppearance.BorderColor = Color.Red;
                        switch(nomeFilosofo)
                        {
                            case "Alan Turing": botao.BackgroundImage = Properties.Resources.AlanTuringFaminto; break;
                            case "Ada Lovelace": botao.BackgroundImage = Properties.Resources.AdaFaminta; break;
                            case "Filosofo 2": botao.BackgroundImage = Properties.Resources.Filosofo2Faminto; break;
                            case "Djikstra": botao.BackgroundImage = Properties.Resources.DjikstraFaminto; break;
                            default: botao.BackgroundImage = Properties.Resources.computer_science_philosopher_320x320; break;
                        }
                        break;
                    default:
                        botao.BackColor = SystemColors.Control;
                        botao.BackgroundImage = Properties.Resources.computer_science_philosopher_320x320;
                        break;
                }
                botao.Text = $"{Program.filosofos[id].Name} - {estado}";
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            StartThread(0);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            StartThread(1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StartThread(2);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            StartThread(3);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            StartThread(4);
        }
        private void StartThread(int id)
        {
            if (Program.filosofos[id].ThreadState == ThreadState.Unstarted)
            {
                Program.filosofos[id].Start();
                MessageBox.Show($"Thread {Program.filosofos[id].Name} iniciada.");
            }
            else
            {
                MessageBox.Show($"Thread {Program.filosofos[id].Name} já está em execução.");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }
    }
}
