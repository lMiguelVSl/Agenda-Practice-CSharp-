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
        private static SqlConnection connection = new SqlConnection(connectionString);
        public static void AddContact(string Nombre, string cellphone, string correoElectronico, double SaldoDolares)
        {
            if (ValidContact(cellphone) == 0)
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
            Agenda.Start();
        }
        public static int ValidContact(string cellphone)
        {
            List<Contacto> contactos = new();
            using (connection)
            {
                SqlCommand queryGetContact = new SqlCommand($"select * from Contacto where numeroTelefonico = '{cellphone}' or correoElectronico = '{cellphone}'", connection);
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
            Agenda.Start();
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
        public static void changeAmount(string number, double newValue)
        {
            if (ValidContact(number) >= 1)
            {
                using (connection)
                {
                    SqlCommand command = new SqlCommand($"update Contacto set saldoDolares='{newValue}' where numeroTelefonico = '{number}';", connection);
                    connection.Open();
                    int numberRowsAfected = command.ExecuteNonQuery();
                    Console.WriteLine("The amount changed \n" +
                        $"{numberRowsAfected} Rows afected");
                }
            }
            else
            {
                Console.WriteLine("The contact dosen't exist");
            }

            Agenda.Start();
        }

        public static void Transaction(string contactBy, string contactFor, double amount)
        {
            double amountFrom;
            double amountFor=0;
            List<Contacto> contactos = new();
            List<Contacto> contactosBy = new();
            if (ValidAmount(contactFor, amount))
            {
                using (connection)
                {
                    SqlCommand queryGetContact = new SqlCommand($"select * from Contacto where numeroTelefonico = '{contactFor}' or correoElectronico = '{contactFor}'", connection);
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
                    SqlCommand queryGetContactBy = new SqlCommand($"select * from Contacto where numeroTelefonico = '{contactBy}' or correoElectronico = '{contactBy}'", connection);
                    SqlDataReader readerBy = queryGetContactBy.ExecuteReader();
                    while (readerBy.Read())
                    {
                        Contacto contactoBy = new(
                            Convert.ToInt32(readerBy["contactoId"]),
                            Convert.ToString(readerBy["nombreContacto"]),
                            Convert.ToString(readerBy["numeroTelefonico"]),
                            Convert.ToString(readerBy["correoElectronico"]),
                            Convert.ToDouble(readerBy["saldoDolares"]));
                        contactosBy.Add(contactoBy);
                    }
                    foreach (var item in contactosBy)
                    {
                        if (item.CorreoElectronico == contactBy || item.NumeroTelefonico == contactBy && item.SaldoDolares-amount >= 0)
                        {
                            amountFrom = item.SaldoDolares - amount;                           
                        } else if (item.CorreoElectronico == contactFor || item.NumeroTelefonico == contactFor)
                        {
                            amountFor = item.SaldoDolares + amount;
                        }
                    }

                    SqlCommand sqlCommand = new SqlCommand($"update Contacto set saldoDolares='{amountFor}' where numeroTelefonico='{contactFor}' or correoElectronico='{contactFor}'", connection);
                    int numberRowsAfected = sqlCommand.ExecuteNonQuery();
                    Console.WriteLine("The amount changed \n" +
                        $"{numberRowsAfected} Rows afected");
                }
            }

        }
        public static bool ValidAmount(string contactFor,double amount)
        {
            bool flag = true;
            List<Contacto> contactos = new();
            using (connection)
            {               
                SqlCommand queryGetContact = new SqlCommand($"select * from Contacto where numeroTelefonico = '{contactFor}' or correoElectronico = '{contactFor}'", connection);
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
            foreach (var item in contactos)
            {
                if (item.CorreoElectronico == contactFor || item.NumeroTelefonico == contactFor && item.SaldoDolares - amount < 0)
                {
                    Console.WriteLine("You're exceeding the amount possible ");
                    flag = false;
                }
                else
                {
                    flag = true;
                    return true;
                }
            }
            return flag;

           
        }
    }
}
