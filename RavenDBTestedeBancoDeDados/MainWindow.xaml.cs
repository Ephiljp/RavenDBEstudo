using Model;
using Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RavenDBTestedeBancoDeDados
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string idDoClienteSalvo { get; set; }

        public MainWindow()
        {
            InitializeComponent();
        }

        
        private void btnCadastrar_Click(object sender, RoutedEventArgs e)
        {
            var cliente = new Cliente
            {
                Nome = txtCliente.Text,
                CPF = "3251651",
                Email = "asdasdas@asdas",
                Endereco = new Endereco
                              
                {
                    Cidade = "Sao Paulo",
                    Complemento = "Casa Verde",
                    Estado = "SP",
                    Logradouro = "Rua 01",
                    Numero = 123
                }

            };

            var repositorio = new RepositorioGenerico();

            var idCliente =  repositorio.Cadastrar(cliente);

            idDoClienteSalvo = idCliente;

            MessageBox.Show($"Cadastrado com sucesso ({ idCliente})");

          
    }

        private void btnConsulta_Click(object sender, RoutedEventArgs e)
        {
            var repositorio = new RepositorioGenerico();

            var cliente = repositorio.Consulte(idDoClienteSalvo);

            MessageBox.Show($"Consultado o cliente {cliente.Nome} ");

        }

        private void btnRemover_Click(object sender, RoutedEventArgs e)
        {
            if (idDoClienteSalvo == null)
            {
                MessageBox.Show("Não há o que remover");
                return;
            }
            var repositorio = new RepositorioGenerico();

            repositorio.Remover(idDoClienteSalvo);

            MessageBox.Show("Removido com sucesso");
            return;
        }
    }
}
