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
        // Dicionários para armazenar as imagens de cada filósofo em cada estado
        private Dictionary<int, Dictionary<string, Image>> imagensFilosofos;

        public Form1()
        {
            InitializeComponent();
            CarregarImagens();
        }

        private void CarregarImagens()
        {
            // Inicializar o dicionário de imagens
            imagensFilosofos = new Dictionary<int, Dictionary<string, Image>>();

            try
            {
                // Para os filósofos que você tem imagens (0, 1, 2)
                for (int i = 0; i < 3; i++)
                {
                    imagensFilosofos[i] = new Dictionary<string, Image>
                    {
                        { "pensando", Properties.Resources.ResourceManager.GetObject($"filosofo{i+1}_pensando") as Image },
                        { "quer comer", Properties.Resources.ResourceManager.GetObject($"filosofo{i+1}_faminto") as Image },
                        { "comendo", Properties.Resources.ResourceManager.GetObject($"filosofo{i+1}_comendo") as Image }
                    };
                }

                // Para os filósofos que não têm imagens (3, 4), você pode usar um placeholder ou deixar sem imagem
                for (int i = 3; i < 5; i++)
                {
                    imagensFilosofos[i] = new Dictionary<string, Image>
                    {
                        { "pensando", null },
                        { "quer comer", null },
                        { "comendo", null }
                    };
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar imagens: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Program.FormInstace = this;
            
            // Configuração inicial dos botões
            ConfigurarBotoes();
            
            // Iniciar o programa
            Program.Iniciar();
        }

        private void ConfigurarBotoes()
        {
            // Configurar os botões com as imagens iniciais (pensando)
            string[] nomesFilosofos = { "Dijkstra", "Alexandre", "Tatiane", "Jacilane", "Patricia" };
            Button[] botoes = { button1, button2, button3, button4, button5 };

            for (int i = 0; i < botoes.Length; i++)
            {
                botoes[i].Text = $"{nomesFilosofos[i]} - pensando";
                botoes[i].TextAlign = ContentAlignment.BottomCenter;
                botoes[i].FlatStyle = FlatStyle.Flat;
                botoes[i].FlatAppearance.BorderSize = 2;
                botoes[i].FlatAppearance.BorderColor = Color.Yellow;
                botoes[i].BackColor = Color.Yellow;
                
                // Configurar imagem se disponível
                if (imagensFilosofos.ContainsKey(i) && imagensFilosofos[i]["pensando"] != null)
                {
                    botoes[i].Image = imagensFilosofos[i]["pensando"];
                    botoes[i].ImageAlign = ContentAlignment.TopCenter;
                }
            }
        }

        public void AtualizarEstadoBotao(int id, string estado)
        {
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

            // Configurar estilo do botão
            botao.FlatStyle = FlatStyle.Flat;
            botao.FlatAppearance.BorderSize = 2;

            // Atualizar cor do botão baseado no estado
            switch(estado)
            {
                case "pensando":
                    botao.BackColor = Color.Yellow;
                    botao.FlatAppearance.BorderColor = Color.Yellow;
                    break;
                case "comendo":
                    botao.BackColor = Color.LightGreen;
                    botao.FlatAppearance.BorderColor = Color.LightGreen;
                    break;
                case "quer comer":
                    botao.BackColor = Color.Red;
                    botao.FlatAppearance.BorderColor = Color.Red;
                    break;
                default:
                    botao.BackColor = SystemColors.Control;
                    break;
            }

            // Atualizar texto do botão
            botao.Text = $"{Program.filosofos[id].Name} - {estado}";

            // Atualizar imagem do botão se disponível
            if (imagensFilosofos.ContainsKey(id) && imagensFilosofos[id].ContainsKey(estado) && imagensFilosofos[id][estado] != null)
            {
                botao.Image = imagensFilosofos[id][estado];
            }
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

        private void button1_Click(object sender, EventArgs e)
        {
            StartThread(0);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            StartThread(1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            StartThread(2);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            StartThread(3);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            StartThread(4);
        }
    }
}