using SofkaPractice.Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SofkaPractice.DAO
{

    //sql connection
    public class ContactoDAO
    {
        public static string connectionString = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString; //por medio del nuget manager manipulo mi config
        public static void AddContact(string Nombre, string cellphone, string correoElectronico, double SaldoDolares)
        {
            if (ValidContact(cellphone, correoElectronico) == 0)
            {

                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    SqlCommand query = new SqlCommand($"insert into Contacto(nombreContacto,numeroTelefonico,correoElectronico,saldoDolares) values('{Nombre}','{cellphone}','{correoElectronico}',{SaldoDolares})", connection);
                    connection.Open();
                    int numberRowsAfected = query.ExecuteNonQuery();
                    Console.WriteLine("The contact has been added \n" +
                    $"{numberRowsAfected} Rows afected");
                }
            }
            else
            {
                Console.WriteLine("The user already exist");
                Agenda.Start();
            }

        }
        public static int ValidContact(string cellphone, string correoElectronico)
        {
            List<Contacto> contactos = new();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand queryGetContact = new SqlCommand($"select * from Contacto where numeroTelefonico = '{cellphone}' or correoElectronico = '{correoElectronico}'", connection);
                connection.Open();
                SqlDataReader reader = queryGetContact.ExecuteReader();
                while (reader.Read())
                {
                    Contacto contacto = new(
                        Convert.ToInt32(reader["contactoId"]),
                        Convert.ToString(reader["nombreContacto"]),
                        Convert.ToString(reader["numeroTelefonico"]),
                        Convert.ToString(reader["correoElectronico"]),
                        Convert.ToDouble(reader["saldoDolares"]));
                    contactos.Add(contacto);
                }
            }
            if (contactos.Count >= 1)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        public static void GetAll()
        {
            List<Contacto> contactos = new();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand($"select * from Contacto;", connection);
                connection.Open();
                SqlDataReader reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                {

                    Contacto contacto = new(
                    Convert.ToInt32(reader["contactoId"]),
                    Convert.ToString(reader["nombreContacto"]),
                    Convert.ToString(reader["numeroTelefonico"]),
                    Convert.ToString(reader["correoElectronico"]),
                    Convert.ToDouble(reader["saldoDolares"]));
                    contactos.Add(contacto);
                }
            }
            foreach (var item in contactos)
            {
                Console.WriteLine(
                    $"Id: {item.ContactoId} \n" +
                    $"Nombre: {item.NombreContacto} \n" +
                    $"Numero Telefonico: {item.NumeroTelefonico} \n" +
                    $"Correo Electronico: {item.CorreoElectronico} \n" +
                    $"Saldo: {item.SaldoDolares}");
                Console.WriteLine(" ");
            }
            Console.ReadKey();
        }
        public static void GetOne(string correoElectronico)
        {
            List<Contacto> contactos = new();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand($"select * from Contacto where correoElectronico='{correoElectronico}' or numeroTelefonico='{correoElectronico}';", connection);
                connection.Open();
                SqlDataReader reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                {

                    Contacto contacto = new(
                    Convert.ToInt32(reader["contactoId"]),
                    Convert.ToString(reader["nombreContacto"]),
                    Convert.ToString(reader["numeroTelefonico"]),
                    Convert.ToString(reader["correoElectronico"]),
                    Convert.ToDouble(reader["saldoDolares"]));
                    contactos.Add(contacto);
                }
            }
            foreach (var item in contactos)
            {
                Console.WriteLine(
                    $"Id: {item.ContactoId} \n" +
                    $"Nombre: {item.NombreContacto} \n" +
                    $"Numero Telefonico: {item.NumeroTelefonico} \n" +
                    $"Correo Electronico: {item.CorreoElectronico} \n" +
                    $"Saldo: {item.SaldoDolares}");
                Console.WriteLine(" ");
            }
            Console.ReadKey();
        }
    }
}
