using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank
{
    class Program
    {
        public void CreateAccount()
        {

            Console.WriteLine("Account type: ");
            string accountType1 = Console.ReadLine();
            Console.WriteLine("Sum: ");
            int basicSum = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Acumulative: ");
            int regular = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Regular :");
            int acumulative = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Please enter your first name:");
            string firstName = Console.ReadLine();
            Console.WriteLine("Please enter your second name: ");
            string secondName = Console.ReadLine();
            Console.WriteLine("Please enter your address: ");
            string address = Console.ReadLine();
            Console.WriteLine("Please enter your phone: ");
            int phoneNumber = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Please enter your accountNumber: ");
            int accountNumber = Convert.ToInt32(Console.ReadLine());



            using (var db = new BankingSystemV3Entities())
            {
                var account = new Account
                {
                    accountType = accountType1,
                    sum = basicSum,
                    acumulativeID = acumulative,
                    regularID = regular
                    
                };

                var customer = new Customer
                {
                    customerName = firstName,
                    customerSurname = secondName,
                    customerAddress = address,
                    customerPhone = phoneNumber,
                    customerAccNo = accountNumber

                };

                db.Accounts.Add(account);
                db.Customers.Add(customer);
                db.SaveChanges();
            }
        }


        public void DeleteAccount()
        {
            using (var db = new BankingSystemV3Entities())
            {
                Console.WriteLine("Write the account ID you want to delete");
                int deleteAccount = Convert.ToInt32(Console.ReadLine());
                Account account = db.Accounts.SingleOrDefault(x => x.accountID == deleteAccount);

            }
        }

        public void ShowAccountDetails()
        {
            using (var db = new BankingSystemV3Entities())
            {
                Console.WriteLine("Customer First name is " );
                Console.WriteLine("Customer Second name is " );
                Console.WriteLine("Customer Address is " );
                Console.WriteLine("Customer Phone number is " );
                Console.WriteLine("Customer Account numbers is " );
                Console.WriteLine("Customer Account type is " );
                Console.WriteLine("Customer Money on accout is " );
                Console.WriteLine("Customer has Acumulative account " );
                Console.WriteLine("Customer has Regular account " );
                Console.WriteLine("Customer has" );
            }
        }


        //public void AddMoney()
        //{
        //    Console.WriteLine("Add a sum to your account");
        //    int d = Convert.ToInt32(Console.ReadLine());
        //    int balance += d;
        //}

        public void WithdrawMoney()
        {

        }
  

        static void Main(string[] args)
        {
            Program p = new Program();
            p.CreateAccount();
            //p.AddMoney();
            //p.DeleteAccount();
            //p.ShowAccountDetails();
            Console.ReadKey();


            //int userChoice;
            //Boolean quit = true;
            //do
            //{
            //    Console.WriteLine("1. Create account");
            //    Console.WriteLine("2. Add Money");
            //    Console.WriteLine("3. Withdraw money");
            //    Console.WriteLine("4. Check Balance");
            //    userChoice = Convert.ToInt32(Console.ReadLine());
            //    switch (userChoice)
            //    {
            //        case 1:
            //        //create account + dep basic money
            //        case 2:
            //        //add money
                      //if (amount <=0) Console.WriteLine("Can't deposit non positive amount");
                      //else{ add function}
            //        case 3:
            //        //withdraw balance
                      //if(amount <=0 || amount > currentBalance) Console.WriteLine("Withdraw can't be completed");
                      //else {withdraw function}
            //        case 4:
            //        //check balance
            //        case 0:
            //            quit = true;
            //            break;
            //        default:
            //            Console.WriteLine("Wrong choice.");
            //            break;
            //    }
            //    Console.WriteLine();
            //} while (!quit);
            Console.WriteLine("Thank you for using our services goodbye. Have a good day!");
          
        }

       
    }
}
