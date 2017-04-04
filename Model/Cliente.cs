using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public  class Cliente :ElementoBase
    {

        public Cliente()
        {
            Endereco = new Endereco();
        }


        public string CPF { get; set; }

        public string Telefone { get; set; }

        public string Email { get; set; }

        public Endereco Endereco { get; set; }

        public int Idade { get; set; }

    }
}
