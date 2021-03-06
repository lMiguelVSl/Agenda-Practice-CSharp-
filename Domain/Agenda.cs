
using SofkaPractice.DAO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SofkaPractice.Domain
{
    public class Agenda
    {
        public Contacto Contacto { get; }

        public static void Start()
        {
            try
            {
                int option;
                Console.WriteLine("Welcome to your adress book");
                Console.WriteLine("Select the option number you need \n" +
                    "1. Add contact \n" +
                    "2. Select all the contacts \n" +
                    "3. Select a contact \n" +
                    "4. Update account value \n" +
                    "5. Transfer money \n" +
                    "6. Update contact \n" +
                    "7. Delete contact \n" +
                    "8. Exit");
                option = GetIntegerData(Console.ReadLine());
                cases(option);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }



        public static void cases(int option)
        {
            switch (option)
            {
                case 1:
                    Console.WriteLine("Write the name");
                    string nombre = Console.ReadLine();
                    Console.WriteLine("Write the cellphone");
                    string cellphone = Console.ReadLine();
                    Console.WriteLine("Write the email");
                    string correoElectronico = Console.ReadLine();
                    Console.WriteLine("Write the dollar amount");
                    double SaldoDolares = Convert.ToDouble(Console.ReadLine());
                    ContactoDAO.AddContact(nombre, cellphone, correoElectronico, SaldoDolares);
                    break;
                case 2:
                    Contacto.getAll();
                    break;
                case 3:
                    Console.WriteLine("write the email or the cellphone");
                    string stringSelect = Console.ReadLine();
                    ContactoDAO.GetOne(stringSelect);
                    break;
                case 4:
                    Console.WriteLine("Write the Number or the email:");
                    string number = Console.ReadLine();
                    Console.WriteLine("Write the new amount: ");
                    double newValue = GetDoubleData(Console.ReadLine());
                    if (ValidateTheAmount(newValue))
                    {
                        Contacto.changeAmount(number, newValue);
                    }
                    else
                    {
                        Console.WriteLine("The value cannot be negative");
                    }
                    
                    break;
                case 5:
                    //transfer money
                    Console.WriteLine("Write the number or email of the contact to take the value: ");
                    string dataTake = Console.ReadLine();
                    Console.WriteLine("Write the number or email of the contact to put the money in");
                    string dataPut = Console.ReadLine();
                    Console.WriteLine("Write the amount to transfer");
                    double amount = GetDoubleData(Console.ReadLine());
                    Console.WriteLine($"Base contact: {dataTake} \n" +
                        $"Destiny contact: {dataPut} \n" +
                        $"Amount: {amount} \n" +
                        $"If all the data is correct write 1");
                    int validation = GetIntegerData(Console.ReadLine());
                   while(validation != 1)
                    {
                        Console.WriteLine("Select the correct option");
                        Console.WriteLine($"Base contact: {dataTake} \n" +
                        $"Destiny contact: {dataPut} \n" +
                        $"Amount: {amount} \n" +
                        $"If al the data if correct write 1");
                        validation = GetIntegerData(Console.ReadLine());
                    }
                    Contacto.TransactionAmount(dataTake,dataPut,amount);
                    break;
                case 6:

                    break;
                case 7:

                    break;
                case 8:
                    Environment.Exit(0);
                    break;
            }
        }
        private static int GetIntegerData(string v) //metodo para verificar el valor ingresado 
        {
            if (!int.TryParse(v, out int result))
            {
                throw new ApplicationException("The enter value is incorrect");
            }
            else
            {
                return result;

            }
        }
        private static double GetDoubleData(string p)
        {
            if (!double.TryParse(p, out double result))
            {
                throw new ApplicationException("The enter value is incorrect");
            }
            else
            {
                return result;
            }

        }
        private static bool ValidateTheAmount(double value)
        {
            if (value < 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }




    }
}
