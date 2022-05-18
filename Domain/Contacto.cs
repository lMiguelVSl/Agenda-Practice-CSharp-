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
    }
    
}
