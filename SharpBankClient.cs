using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using BankLib;
using System.Collections;
using System.Data.SqlClient;

namespace BankClients
{
    public class EnumNotFoundException : Exception
    {
        public EnumNotFoundException(string message) : base(message)
        {

        }
    }
    public enum AType
    {
        savings = 1,
        current
    }

    public class SharpBankClient
    {
        SharpBankClient()
        {

        }
        public static void Main()
        {
            Console.WriteLine("Hi! Welcome to Sharp Bank.");
            Thread.Sleep(200);
            Console.WriteLine("I am Mr.Sharp, your personal assistant to help you with Sharp Bank related queries.");
            Thread.Sleep(200);
            int choice;
            SBAccount acc = new SBAccount();
            acc.LoadAccountsFromDatabase();
            do
            {
                Console.WriteLine("You can select from the options below.");
                Thread.Sleep(100);
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("  ____________________________________\n");
                Console.WriteLine("|\t1.  Create Instant Account    |");
                Thread.Sleep(100);
                Console.WriteLine("|\t2.  Deposit Amount            |");
                Thread.Sleep(100);
                Console.WriteLine("|\t3.  Withdraw Amount           |");
                Thread.Sleep(100);
                Console.WriteLine("|\t4.  Balance Enquiry           |");
                Thread.Sleep(100);
                Console.WriteLine("|\t5.  Mini - statement          |");
                Thread.Sleep(100);
                Console.WriteLine("|\t6.  Block Debit Card          |");
                Thread.Sleep(100);
                Console.WriteLine("|\t7.  Make Fixed Deposit        |");
                Thread.Sleep(100);
                Console.WriteLine("|\t8.  Take Loan                 |");
                Thread.Sleep(100);
                Console.WriteLine("|\t9.  Apply Credit Card         |");
                Thread.Sleep(100);
                Console.WriteLine("|\t10. Apply Check Book          |");
                Thread.Sleep(100);
                Console.WriteLine("|\t11. See All Account Details   |");
                Thread.Sleep(100);
                Console.WriteLine("|\t12. Exit                      |");
                Console.WriteLine("  ____________________________________\n");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("---Your Choice---");
                choice = Convert.ToInt32(Console.ReadLine());
                try
                {
                    switch (choice)
                    {
                        case 1:
                            Console.WriteLine("Account Creation Request Initiating...");
                            int countdown = 1;
                            while (countdown <= 10)
                            {
                                Console.Write($"{countdown}|");
                                for (int i = 0; i < countdown; i++)
                                    Console.Write("#");
                                for (int i = 0; i < 10 - countdown; i++)
                                    Console.Write(" ");
                                Console.Write("|10\r");
                                countdown = countdown + 1;
                                Thread.Sleep(50);
                            }
                            Console.WriteLine("Initiated Successfully");
                            Console.WriteLine("Enter Account Holder Name");
                            string acchname = Console.ReadLine();
                            string acctype = "";
                            try
                            {
                                Console.WriteLine("Select Type of Account:\n1. Savings\n2. Current");
                                string atype = Console.ReadLine().ToLower();
                                string enumparse = Enum.Parse(typeof(AType), atype).ToString();
                                acctype = enumparse;

                            }
                            catch (Exception)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("\nEntered Account Type is not valid, Try Savings or Currrent.\n");
                                Console.ForegroundColor = ConsoleColor.White;
                                break;
                            }

                            try
                            {

                                Console.WriteLine("How much money you want to deposit in your account now:");
                                float startamt = float.Parse(Console.ReadLine());
                                acc = new SBAccount();
                                SBAccount accountDetails = acc.AppendAccountDetailsInDatabase(acchname, acctype, startamt);
                                Console.WriteLine("********************************\n");
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine($"Account Number:\t\t {accountDetails.AccountNumber}");
                                Console.WriteLine($"Account Holder Name:\t {accountDetails.AccountHolderName}");
                                Console.WriteLine($"Account Type:\t\t {accountDetails.TypeofAccount}");
                                Console.WriteLine($"Account Created Date:\t {accountDetails.DateofCreation}");
                                Console.WriteLine($"Current Balance:\t {accountDetails.Balance}");
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.WriteLine("********************************\n");
                            }
                            catch (Exception e)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("\nTry entering valid amount\n" + e);
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                            break;

                        case 2:
                            //Deposit Amount
                            Console.WriteLine("Enter Bank Account Number:");
                            int accnum = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Enter Deposit Amount:");
                            float amt = float.Parse(Console.ReadLine());
                            SBAccount useracc = acc.GetAccountDetails(accnum);
                            SBTransaction transaction = new SBTransaction();
                            bool status = transaction.CreditAmount(accnum, amt);
                            if (status)
                            {
                                SBAccount currentAccount = acc.GetAccountDetails(accnum);
                                Console.WriteLine("********************************\n");
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine($"Amount Credited. Now, Your {accnum}'s Account Balance is {currentAccount.Balance}");
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.WriteLine("********************************\n");
                            }
                            else
                            {
                                Console.WriteLine("********************************\n");
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Sorry, Transaction Failed...");
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.WriteLine("********************************\n");
                            }
                            break;

                        case 3:
                            //Withdraw Amount
                            Console.WriteLine("Enter Bank Account Number:");
                            int anum = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Enter Withdraw Amount:");
                            float withdrawamt = float.Parse(Console.ReadLine());
                            SBAccount withdrawuseracc = acc.GetAccountDetails(anum);
                            SBTransaction withdrawtransaction = new SBTransaction();
                            bool withdrawstatus = withdrawtransaction.WithdrawAmount(anum, withdrawamt);
                            if (withdrawstatus)
                            {
                                SBAccount currentAccount = acc.GetAccountDetails(anum);
                                Console.WriteLine("********************************\n");
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine($"Please Collect the cash. Now, Your Account Balance is {currentAccount.Balance}");
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.WriteLine("********************************\n");
                            }
                            else
                            {
                                Console.WriteLine("********************************\n");
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Sorry, Transaction Failed...");
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.WriteLine("********************************\n");
                            }
                            break;

                        case 4:
                            Console.WriteLine("Enter Account Number");
                            int balenquiryaccnum = Convert.ToInt32(Console.ReadLine());
                            SBAccount obj = acc.GetAccountDetails(balenquiryaccnum);
                            Console.WriteLine("********************************\n");
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"Your Account Balance is {obj.Balance}");
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("********************************\n");
                            break;

                        case 5:

                            Console.WriteLine("Enter Account Number");
                            int ministmtaccnum = Convert.ToInt32(Console.ReadLine());
                            countdown = 1;
                            while (countdown < 6)
                            {
                                Console.Write("Scanning all Transactions");
                                for (int i = 0; i < countdown % 6 + 1; i++)
                                    Console.Write(".");
                                Console.Write("\r");
                                countdown++;
                                Thread.Sleep(500);
                            }
                            SBTransaction msattransaction = new SBTransaction();
                            List<SBTransaction> msat = msattransaction.LoadAllTransactionsFromDatabase(ministmtaccnum);
                            if (msat.Count == 0)
                            {
                                Console.WriteLine("********************************\n");
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine("Sorry, No Transactions made on this account.");
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.WriteLine("********************************\n");
                            }
                            else
                            {
                                foreach (SBTransaction a in msat)
                                {
                                    Console.WriteLine("********************************\n");
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine($"Transaction ID:\t\t\t {a.TransactionID}");
                                    Console.WriteLine($"Transaction Account Number:\t {a.TAccountNumber}");
                                    Console.WriteLine($"Transaction Date:\t\t {a.TransactionDate}");
                                    Console.WriteLine($"Tranasction Amount:\t\t {a.TransactionAmt}");
                                    Console.WriteLine($"Balance after Transaction:\t {a.NewBalance}");
                                    Console.ForegroundColor = ConsoleColor.White;
                                    Console.WriteLine("********************************\n");
                                }
                            }
                            break;

                        case 6:
                            Console.WriteLine("********************************\n");
                            Console.WriteLine("Enter Account Number");
                            Console.WriteLine($"Don't Worry, Your Debit Card Linked to Account Number {Console.ReadLine()} is blocked...");
                            Console.WriteLine("********************************\n");
                            break;

                        case 7:
                            Console.WriteLine("********************************\n");
                            Console.WriteLine($"Regret to Say,\nThis Feature is Under Construction.");
                            Console.WriteLine("********************************\n");
                            break;

                        case 8:

                            Console.WriteLine("********************************\n");
                            Console.WriteLine($"Regret to Say,\nThis feature is Under Construction.");
                            Console.WriteLine("********************************\n");
                            break;

                        case 9:

                            Console.WriteLine("********************************\n");
                            Console.WriteLine("Enter Account Number");
                            Console.WriteLine($"Credit Card is applied for Account Number {Console.ReadLine()} successfully.");
                            Console.WriteLine("********************************\n");
                            break;

                        case 10:

                            Console.WriteLine("********************************\n");
                            Console.WriteLine("Enter Account Number");
                            Console.WriteLine($"Check Book is applied for Account Number {Console.ReadLine()} successfully.");
                            Console.WriteLine("********************************\n");
                            break;

                        case 11:

                            acc.LoadAccountsFromDatabase();
                            List<SBAccount> allaccountslist = acc.AllAccounts();
                            if (allaccountslist.Count == 0)
                            {
                                Console.WriteLine("********************************\n");
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine("Sorry, No Accounts Found.");
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.WriteLine("********************************\n");
                            }
                            else
                            {
                                foreach (SBAccount a in allaccountslist)
                                {
                                    Console.WriteLine("********************************\n");
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine($"Account Number:\t\t {a.AccountNumber}");
                                    Console.WriteLine($"Account Holder Name:\t {a.AccountHolderName}");
                                    Console.WriteLine($"Account Type:\t\t {a.TypeofAccount}");
                                    Console.WriteLine($"Account Created Date:\t {a.DateofCreation}");
                                    Console.WriteLine($"Current Balance:\t {a.Balance}");
                                    Console.ForegroundColor = ConsoleColor.White;
                                    Console.WriteLine("********************************\n");
                                }
                            }
                            break;

                        case 12:
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Thank you for using Sharp bank. Signing off...:)");
                            Console.ForegroundColor = ConsoleColor.White;
                            break;

                        default:
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Wrong Choice\n");
                            Console.ForegroundColor = ConsoleColor.White;
                            break;
                    }
                }
                catch(Exception e)
                {
                    Console.WriteLine(e);
                }
            } while (choice != 12);

        }
    }
}
