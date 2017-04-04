using Model;
using Repositorio;
using System.Windows;


namespace RavenDBTestedeBancoDeDados
{

    public partial class FormCadastroDeUsuarios : Window
    {
        public Cliente Cliente { get; set; }

        public FormCadastroDeUsuarios()
        {
            InitializeComponent();

            Cliente = new Cliente();

            this.DataContext = Cliente;

        }

        public FormCadastroDeUsuarios(Cliente cliente)
        {
            InitializeComponent();

            this.DataContext = cliente;

            Cliente = cliente;

        }


        private void btnSalvar_Click(object sender, RoutedEventArgs e)
        {
            var repositorio = new RepositorioDeCliente();

            Cliente = (Cliente)this.DataContext;

            repositorio.Cadastrar(Cliente);
         
            this.Close();
        }

       
    }
}
