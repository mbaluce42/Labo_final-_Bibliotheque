using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace CL_bibliotheque
{
    public class Abonne : Personne
    {
        private static int nbrAbonne = 0;
        private int _numeroAbonne;


        public int NumeroAbonne
        {
            get { return _numeroAbonne; }
            set { _numeroAbonne = value; }
        }

        public Abonne() : this("rien","rien","rien","01/01/1999")    
        {

        }
        public Abonne(string nom, string prenom, string adresse, string dateNaissance) : base(nom, prenom, adresse, dateNaissance)
        {
            NumeroAbonne = ++nbrAbonne;
        }

        public override string ToString()
        {
            return $"Numéro Abonné : {NumeroAbonne}\n" + $"Nom : {Nom}\n" + $"Prénom : {Prenom}\n" + $"Adresse : {Adresse}\n" + $"Date de Naissance : {DateNaissance.ToString("dd/MM/yyyy")}";
        }

    }
}
