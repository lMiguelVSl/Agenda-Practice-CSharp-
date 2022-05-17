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
        public ContactoDAO contactoDAO { get; set; }
        public Contacto(int contactoId, string nombreContacto, string numeroTelefonico, string correoElectronico, double saldoDolares)
        {

            ContactoId = contactoId;
            NombreContacto = nombreContacto;
            NumeroTelefonico = numeroTelefonico;
            CorreoElectronico = correoElectronico;
            SaldoDolares = saldoDolares;
            contactoDAO = new ContactoDAO();

        }
        public Contacto()
        {

        }
        public void getAll(string correoElectronico)
        {
            Contacto contacto = new();
           if(correoElectronico == contacto.CorreoElectronico)
            {
                Console.WriteLine($"Id de contacto: {ContactoId}, \n" +
                 $"Nombre del contacto: {NombreContacto} \n" +
                 $"Numero de contacto: {NumeroTelefonico} \n" +
                 $"Correo electronico: {CorreoElectronico} \n" +
                 $"Saldo: {SaldoDolares}");
            }
         
        }
    }
    
}
