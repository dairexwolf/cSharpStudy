using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class ClientsTB
    {
        public int Client_id { get; }
        public string Fam { get; }
        public string Name { get; }
        public string Otch { get; }
        public string Phone { get; }
        public string E_mail { get; }
        public bool Search { get; set; }


        public ClientsTB(int client_id, string fam, string name, string otch, string phone, string e_mail, bool search = false)
        {
            Client_id = client_id;
            Fam = fam;
            Name = name;
            Otch = otch;
            Phone = phone;
            E_mail = e_mail;
            Search = search;
        }
    }
}
