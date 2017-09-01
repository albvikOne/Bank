using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Bank
{
    class Program

    {
        private void CreateAccount()
        {

            var dbcontext = new BankingSystemV3Entities();

            try
            {
                Console.WriteLine("Please enter the bank where you want to open account : ");
                string bname = Console.ReadLine();
                Console.WriteLine("Please enter the bank city location: ");
                string blocation = Console.ReadLine();
                Console.WriteLine("Account type: ");
                string accountType1 = Console.ReadLine();
                Console.WriteLine("Sum: ");
                int basicDeposit = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Please enter your first name:");
                string firstName = Console.ReadLine();
                Console.WriteLine("Please enter your second name: ");
                string secondName = Console.ReadLine();
                Console.WriteLine("Please enter your address: ");
                string address = Console.ReadLine();
                Console.WriteLine("Please enter your phone: ");
                int phoneNumber = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Please enter your accountNumber: ");
                int customerAccNumber = Convert.ToInt32(Console.ReadLine());



                using (dbcontext)
                {

                    var bank = new Bank
                    {
                        bankName = bname,
                        bankLocation = blocation
                    };

                    var account = new Account
                    {
                        accountType = accountType1,
                        sum = basicDeposit
                    };

                    var customer = new Customer
                    {

                        customerName = firstName,
                        customerSurname = secondName,
                        customerAddress = address,
                        customerPhone = phoneNumber,
                        customerAccNo = customerAccNumber,
                        Bank = bank,
                        Account = account
                    };



                    dbcontext.Customers.Add(customer);
                    dbcontext.SaveChanges();
                    Console.WriteLine("Customer account was created. Thank you for using our services. Have a nice day.");
                    Console.ReadKey();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
            }

        }

        private void ShowAccountDetails()
        {
            using (var db = new BankingSystemV3Entities())
            {

                var generalinformation = from c in db.Customers
                                         join b in db.Banks on c.bankID equals b.bankID
                                         join a in db.Accounts on c.accountID equals a.accountID
                                         select new
                                         {
                                             c.customerID,
                                             c.accountID,
                                             b.bankName,
                                             b.bankID,
                                             c.customerName,
                                             c.customerSurname,
                                             c.customerAddress,
                                             c.customerPhone,
                                             a.sum,
                                             a.accountType,
                                             b.bankLocation
                                         };



                Console.WriteLine("\nList all users data");

                foreach (var result in generalinformation)
                {
                    Console.WriteLine("Customer ID :" + result.customerID +
                        " \nAccountID " + result.accountID +
                        " \nBankID " + result.bankID +
                        " \nName : " + result.customerName +
                        " \nSecond name: " + result.customerSurname +
                        " \nAddress : " + result.customerAddress +
                        " \nPhone : " + result.customerPhone +
                        " \nCurrent balance : " + result.sum +
                        " \nAccount type : " + result.accountType +
                        " \nBank location : " + result.bankLocation +
                        " \nBank city :" + result.bankName +
                        " \n\n"
                        );
                }

                Console.WriteLine();
                Console.ReadLine();



            }
        }

        private void DeleteAccount()
        {
            var dbcontext = new BankingSystemV3Entities();

            using (dbcontext)
            {
                try
                {
                    Console.WriteLine("Type the account ID which u want to delete");
                    int input = Convert.ToInt32(Console.ReadLine());
                    Account account = dbcontext.Accounts.First(i => i.accountID == input);
                    dbcontext.Accounts.Remove(account);
                    dbcontext.SaveChanges();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.InnerException);
                }
            }
        }

        private void DeleteCustomer()
        {
            var dbcontext = new BankingSystemV3Entities();

            using (dbcontext)
            {
                try
                {
                    Console.WriteLine("Type the customer id which u want to delete");
                    int input = Convert.ToInt32(Console.ReadLine());
                    Customer customer = dbcontext.Customers.First(i => i.customerID == input);
                    dbcontext.Customers.Remove(customer);
                    dbcontext.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.InnerException);
                }
            }
        }

        private void AddMoney()
        {
            try
            {
                using (var dbcontext = new BankingSystemV3Entities())
                {
                    Console.WriteLine("Please select the account ID which u want to add money to");
                    int id = Convert.ToInt32(Console.ReadLine());
                    Account account = dbcontext.Accounts
                                        .First(i => i.accountID == id);
                    Console.WriteLine("Input the sum which you want to add to account");
                    int add = Convert.ToInt32(Console.ReadLine());
                    if (add <= 0)
                    {
                        Console.WriteLine("Can't deposit non positive amount");
                    }
                    else
                    {
                        account.sum += add;
                    }

                    dbcontext.SaveChanges();
                    Console.WriteLine("You have added " + add + " to your account. Now you have " + account.sum);
                }
            }
            catch
            (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
            }
        }

        private void WithdrawMoney()
        {
            try
            {
                using (var dbcontext = new BankingSystemV3Entities())
                {
                    Console.WriteLine("Please select the account ID which u want to withdraw money ");
                    int id = Convert.ToInt32(Console.ReadLine());
                    Account account = dbcontext.Accounts
                                        .First(i => i.accountID == id);

                    Console.WriteLine("Please enter the sum which you want to withdraw ");
                    int withdraw = Convert.ToInt32(Console.ReadLine());
                    if (withdraw <=  0 || withdraw > account.sum)
                    {
                        Console.WriteLine("Withdraw can't be completed, try again.");
                    }
                    else
                    {
                        account.sum -= withdraw;
                        Console.WriteLine("You have withdrawed " + withdraw + " from your account. Now you have " + account.sum);
                    }
                    dbcontext.SaveChanges();                    
                }
            }
            catch
            (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
            }
        }

        private void UserChanges()
        {
            try
            {
                using (var dbcontext = new BankingSystemV3Entities())
                {
                    Console.WriteLine("Please select the account ID : ");
                    int id = Convert.ToInt32(Console.ReadLine());
                    Customer c = dbcontext.Customers
                        .First(i => i.customerID == id);

                    Console.WriteLine("Please enter the new name  ");
                    string newFirstName = Console.ReadLine();
                    c.customerName = newFirstName;

                    Console.Write("Please enther the second name \n");
                    string newSecondName = Console.ReadLine();
                    c.customerSurname = newSecondName;

                    dbcontext.SaveChanges();
                    Console.WriteLine("Customer personal data" +
                        " modified");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
            }
        }

        public void CheckBalance()
        {

            try
            {
                using (var dbcontext = new BankingSystemV3Entities())
                {
                    Console.WriteLine("Please select the account ID : ");
                    int id = Convert.ToInt32(Console.ReadLine());
                    Account a = dbcontext.Accounts
                        .First(i => i.accountID == id);

                    Console.WriteLine("Your current balance is " + a.sum);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
            }
        }

        public void ChangeAccountType()
        {

        }

        public void ChangeAddress()
        {
            try
            {
                using (var dbcontext = new BankingSystemV3Entities())
                {
                    Console.WriteLine("Please select the account ID : ");
                    int id = Convert.ToInt32(Console.ReadLine());
                    Customer c = dbcontext.Customers
                        .First(i => i.customerID == id);

                    Console.WriteLine("Please enter the new address  ");
                    string newAddress = Console.ReadLine();
                    c.customerAddress = newAddress;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
            }
        }

        public void ChangePhoneNumber()
        {
            try
            {
                using (var dbcontext = new BankingSystemV3Entities())
                {
                    Console.WriteLine("Please select the account ID : ");
                    int id = Convert.ToInt32(Console.ReadLine());
                    Customer c = dbcontext.Customers
                        .First(i => i.customerID == id);

                    Console.WriteLine("Please enter the new Phone Number  ");
                    int newPhoneNumber = Convert.ToInt32(Console.ReadLine());
                    c.customerPhone = newPhoneNumber;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
            }
        }

        public void ChangeBankLocation()
        {
            try
            {
                using (var dbcontext = new BankingSystemV3Entities())
                {
                    Console.WriteLine("Please select the bamk ID : ");
                    int id = Convert.ToInt32(Console.ReadLine());
                    Bank b = dbcontext.Banks
                        .First(i => i.bankID == id);

                    Console.WriteLine("Please enter the new Bank Location ");
                    string newBankLocation = Console.ReadLine();
                    b.bankLocation = newBankLocation;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
            }
        }

        private void ChangeBankName()
        {
            try
            {
                using (var dbcontext = new BankingSystemV3Entities())
                {
                    Console.WriteLine("Please select the bank ID : ");
                    int id = Convert.ToInt32(Console.ReadLine());
                    Bank b = dbcontext.Banks
                        .First(i => i.bankID == id);

                    Console.WriteLine("Please enter the new Bank Name ");
                    string newBankName = Console.ReadLine();
                    b.bankName = newBankName;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
            }
        }

        private void Menu()
        {
            int userChoice;
            Boolean quit = true;
            do
            {
                Console.WriteLine("1. Create Account");
                Console.WriteLine("2. Delete Customer");
                Console.WriteLine("3. Delete Account");
                Console.WriteLine("4. Show All Accounts Details");
                Console.WriteLine("5. Add Money to account");
                Console.WriteLine("6. Withdraw money");
                Console.WriteLine("7. User  changes");
                Console.WriteLine("8. Check Balance");
                Console.WriteLine("9. Change account type");
                Console.WriteLine("10. Change Address");
                Console.WriteLine("11. Change Phone number");
                Console.WriteLine("12. Change Bank Location");
                Console.WriteLine("13. Change Bank Name");
                Console.WriteLine("0. Press 0 to Exit");

                userChoice = Convert.ToInt32(Console.ReadLine());
                switch (userChoice)
                {
                    case 1:
                        CreateAccount();
                        break;
                    case 2:
                        DeleteCustomer();
                        break;
                    case 3:
                        DeleteAccount();
                        break;
                    case 4:
                        ShowAccountDetails();
                        break;
                    case 5:
                        AddMoney();
                        break;
                    case 6:
                        WithdrawMoney();
                        break;
                    case 7:
                        UserChanges();
                        break;
                    case 8:
                        CheckBalance();
                        break;
                    case 9:
                        ChangeAccountType();
                        break;
                    case 10:
                        ChangeAddress();
                        break;
                    case 11:
                        ChangePhoneNumber();
                        break;
                    case 12:
                        ChangeBankLocation();
                        break;
                    case 13:
                        ChangeBankName();
                        break;
                    //case :
                    //    free case, for future changes
                    //    
                    case 0:
                        Console.WriteLine("\nThe application will close in 5 seconds!" +
                            "\nThank you for using our services have a great day.");
                        Thread.Sleep(5000);
                        System.Environment.Exit(1);  
                        break;
                    default:
                        Console.WriteLine("\nWrong choice. Choose again.");
                        break;
                }
                Console.WriteLine();
            } while (quit);           
        }



        static void Main(string[] args)
        {
            Program p = new Program();
            p.Menu();

        }

    }
}
