using Model;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Embedded;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio
{
    public class RepositorioGenerico<T> where T : ElementoBase, new()
    {
        public DocumentStore store { get; set; }

        public RepositorioGenerico()

        {

            store = new DocumentStore

            {
                Url = "http://localhost:8080",

                DefaultDatabase = "RavenDBTestedeBancoDeDados"
            };

            store.Initialize();



        }

        public virtual string Cadastrar(T Item)
        {

            Item.Id = null;
            return Salvar(Item);

        }

        public virtual string Salvar(T Item)
        {

            using (IDocumentSession session = store.OpenSession())
            {
                session.Store(Item);
                session.SaveChanges();

            }
            return Item.Id;

        }

        public virtual string Atualizar(T Item)
        {

                    return Salvar(Item);

        }

        public virtual T Consulte(string IdItem)
        {
            using (IDocumentSession session = store.OpenSession())
            {
                return session.Load<T>(IdItem);


            }

        }

        public virtual List<T> Liste()
        {
            
            using (IDocumentSession session = store.OpenSession())
            {
                return session
                    .Query<T>()
                    .ToList();


            }

        }

        public virtual List<T> Liste(int pagina, int elementosPorPagina, out RavenQueryStatistics estatistica)
        {
            var quantidadeAPular = (pagina - 1) * elementosPorPagina;
            using (IDocumentSession session = store.OpenSession())
            {
                return session
                    .Query<T>()
                    .Statistics(out estatistica)
                    .OrderBy(x => x.Nome)
                    .Skip(quantidadeAPular)
                    .Take(elementosPorPagina)                    
                    .ToList();


            }

        }

        public virtual void Remover(string IdDoItemSalvo)
        {
            using (IDocumentSession session = store.OpenSession())
            {
                session.Delete(IdDoItemSalvo);
                session.SaveChanges();


            }

        }

        public List<T> Procure()
        {
            using (IDocumentSession session = store.OpenSession())
            {
                return session.Load<T>().ToList();


            }


        }
    }
}
