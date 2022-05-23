using SofkaPractice.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SofkaPractice.Domain
{
    public class Contacto
    {
        public int ContactoId { get; set; }
        public string NombreContacto { get; set; }
        public string  NumeroTelefonico { get; set; }
        public string CorreoElectronico { get; set; }
        public double SaldoDolares { get; set; }
       
        public Contacto(int contactoId, string nombreContacto, string numeroTelefonico, string correoElectronico, double saldoDolares)
        {

            ContactoId = contactoId;
            NombreContacto = nombreContacto;
            NumeroTelefonico = numeroTelefonico;
            CorreoElectronico = correoElectronico;
            SaldoDolares = saldoDolares;
            

        }
        public Contacto()
        {

        }
        public static void getAll()
        {
            ContactoDAO.GetAll();
        }
        public static void changeAmount(string number,double newValue)
        {
            ContactoDAO.changeAmount(number,newValue);
        }
        public static void TransactionAmount(string contactBy, string contactFor, double amount)
        {
            Console.WriteLine("Contact to be debited before the transaction");
            ContactoDAO.GetOne(contactBy);
            Console.WriteLine("Contact to deposit the amount befor the transaction");
            ContactoDAO.GetOne(contactFor);
            ContactoDAO.Transaction(contactBy,contactFor,amount);
            Console.WriteLine("Contact to be debited after the transaction");
            ContactoDAO.GetOne(contactBy);
            Console.WriteLine("Contact to deposit after the transaction");
            ContactoDAO.GetOne(contactFor);
        }
    }
    
}
