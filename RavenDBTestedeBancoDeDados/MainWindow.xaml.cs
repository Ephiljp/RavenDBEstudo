using Model;
using Raven.Client;
using Repositorio;
using System;
using System.Collections.Generic;
using System.Windows;



namespace RavenDBTestedeBancoDeDados
{
   
    public partial class MainWindow 
    {
        public RepositorioDeCliente Repositorio { get; set; }
        public string idDoClienteSalvo { get; set; }

       const int Quantidade_PorPagina = 10;

        public int paginaAtual { get; set; } = 1;

        public int QuantidadeTotalDePaginas { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            Repositorio = new RepositorioDeCliente();
            CarregueelementosNoBancoDeDados();

        }


        private void btnCadastrar_Click(object sender, RoutedEventArgs e)
        {

            var cliente = ChamaEditorDeCliente(new Cliente());
            


        }

        private void btnConsulta_Click(object sender, RoutedEventArgs e)
        {

            var cliente = Repositorio.Consulte(idDoClienteSalvo);

            MessageBox.Show($"Consultado o cliente {cliente.Nome} ");

        }

        private void btnRemover_Click(object sender, RoutedEventArgs e)
        {
            if (lstClientes.SelectedItem == null)
            {
                MessageBox.Show("Selecione um item na lista");
                return;
            }

            var cliente = ((Cliente)(lstClientes.SelectedItem));
            var repositorio = new RepositorioDeCliente();

            repositorio.Remover(cliente.Id);
            CarregueelementosNoBancoDeDados();


        }

        public void CarregueelementosNoBancoDeDados()
        {

            RavenQueryStatistics estatistica;
            lstClientes.DataContext = Repositorio.Liste(paginaAtual,Quantidade_PorPagina, out estatistica);
            QuantidadeTotalDePaginas = (int)Math.Ceiling((decimal) estatistica.TotalResults / (decimal)Quantidade_PorPagina);
            txtPaginaAtual.Text = $"Pagina Atual: {paginaAtual} de {QuantidadeTotalDePaginas}; total de elementos: {estatistica.TotalResults};Duração de consulta: {estatistica.DurationMilliseconds} ms";




        }

        private void btnListar_Click(object sender, RoutedEventArgs e)
        {
            btnListar.Visibility = Visibility.Hidden;
            fly.IsOpen = true;
            lstClientes.Margin = new Thickness(205, 79, 10, 10);

             CarregueelementosNoBancoDeDados();
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            if (lstClientes.SelectedItem == null)
            {
                MessageBox.Show("Selecione um item na lista");
                return;
            }
            var cliente = (Cliente)lstClientes.SelectedItem;
            ChamaEditorDeCliente(cliente);

            Repositorio.Atualizar(cliente);
            CarregueelementosNoBancoDeDados();

        }

        private Cliente ChamaEditorDeCliente(Cliente cliente)
        {



            var formCadastro = new FormCadastroDeUsuarios(cliente);
            formCadastro.ShowDialog();

            return formCadastro.Cliente;

        }

        private void btnMenu_Click(object sender, RoutedEventArgs e)
        {
            fly.IsOpen = false;
            btnListar.Visibility = Visibility.Visible;
            lstClientes.Margin = new Thickness(10, 79, 10, 10);

            btnPaginaAnterior.Margin = new Thickness(0, 346, 272, 11);

            CarregueelementosNoBancoDeDados();
        }

        private void txtConsulta_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
       
            
                


            if (string.IsNullOrWhiteSpace(txtConsulta.Text))
            {
                CarregueelementosNoBancoDeDados();
                return;
            }
            else
            {
                lstClientes.DataContext = Repositorio.ConsultePorTermo(txtConsulta.Text);
            }
            



        }

        private void txtIdade_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
           

           
            int idade;

            if (int.TryParse(txtIdade.Text, out idade))
            {
                lstClientes.DataContext = Repositorio.ConsultePorIdade(idade);
             
            }
            else
            {
               
                lstClientes.DataContext = new List<Cliente>();
               
            }
            
            
        }

        private void btnPaginaAnterior_Click(object sender, RoutedEventArgs e)
        {
            if (paginaAtual > 1)
            {
                paginaAtual--;
                CarregueelementosNoBancoDeDados();
                
            }
           
            
        }

        private void btnProximaPagina_Click(object sender, RoutedEventArgs e)
        {
            if (paginaAtual< QuantidadeTotalDePaginas)
            {
                paginaAtual++;
                 
            }

            CarregueelementosNoBancoDeDados();

        }
    }
}
