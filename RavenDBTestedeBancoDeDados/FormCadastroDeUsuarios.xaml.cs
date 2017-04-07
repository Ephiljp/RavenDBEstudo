using Model;
using Repositorio;
using System.Windows;


namespace RavenDBTestedeBancoDeDados
{

    public partial class FormCadastroDeUsuarios : Window
    {
        public Cliente Cliente { get; set; }

        RepositorioDeCliente repositorio;

        public FormCadastroDeUsuarios()
        {
            InitializeComponent();

            Cliente = new Cliente();

            this.DataContext = Cliente;

            repositorio = new RepositorioDeCliente();

        }

        public FormCadastroDeUsuarios(Cliente cliente)
        {
            InitializeComponent();

            repositorio = new RepositorioDeCliente();

            this.DataContext = cliente;

            Cliente = cliente;

            if (cliente.IndicadorId != null)
            {
                cmbIndicador.SelectedValue = cliente.IndicadorId;

            }



        }


        private void btnSalvar_Click(object sender, RoutedEventArgs e)
        {
           
            Cliente = (Cliente)this.DataContext;

            Cliente.ClienteIndicador = (Cliente)cmbIndicador.SelectedItem;

            Cliente.IndicadorId = ((Cliente)cmbIndicador.SelectedItem).Id;

            repositorio.Cadastrar(Cliente);
         
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cmbIndicador.ItemsSource = repositorio.Liste();

        }
    }
}
