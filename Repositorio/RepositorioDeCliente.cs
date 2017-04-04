using Model;
using Raven.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio
{
    public class RepositorioDeCliente : RepositorioGenerico<Cliente>
    {
        public List<Cliente>  ConsultePorTermo(string termo)
        {
            using (IDocumentSession session = store.OpenSession())
            {
                return session.Query<Cliente>().Where(x => x.Nome == termo).ToList();


            }
        }

        public List<Cliente> ConsultePorIdade(int idade)
        {
            using (IDocumentSession session = store.OpenSession())
            {
                return session.Query<Cliente>().Where(x => x.Idade >= idade).ToList();


            }
        }
    }
}
