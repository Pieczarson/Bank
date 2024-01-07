using System;
using System.Collections.Generic;

namespace Bank
{
    class BankAccount
    {
        //Projekt wykonali: Michał Pieczyński i Mateusz Pawlak
        private List<Transaction> AllTransactions = new List<Transaction>();

        public static UInt32 accountNumberSeed = 2453453;
        public UInt32 Number { get; }
        public string Owner { get; }
        public decimal balance;
        public decimal MaxLoanValue { get; }

        public Loan Loan = new Loan(0);

        public decimal Balance
        {
            get
            {
                decimal transactionsSum = 0;
                foreach (var transaction in AllTransactions)
                {
                    transactionsSum += transaction.Amount;
                }
                return transactionsSum + balance;
            }
            set
            {
                balance = value;
            }
        }

        public BankAccount(string name, decimal initialBalance, decimal maxLoanValue)
        {
            this.Owner = name;
            this.Balance = initialBalance;
            this.Number = accountNumberSeed++;
            this.MaxLoanValue = maxLoanValue;
            Console.WriteLine($"Utworzono nowe konto z saldem: {initialBalance} i maksymalnym kredytem: {maxLoanValue}");
        }

        public static void PrimaryAmountController(decimal amount, string communicate = "Operacja niedozwolona - wpisana kwota nie jest dodatnia")
        {
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), communicate);
            }
        }

        public static void AdditionalAmountController(decimal amount, decimal maxNumber, string communicate = "Operacja niedozwolona - wpisana kwota jest za duża")
        {
            if (amount > maxNumber)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), communicate);
            }
        }

        public void AddTransactionToList(decimal amount, DateTime date, string note)
        {
            Transaction transactionToAdd = new Transaction(amount, date, note);
            AllTransactions.Add(transactionToAdd);
        }

        public void MakeDeposit(decimal amount, DateTime date, string note)
        {
            Console.WriteLine($"Operacja wpłaty kwoty: {amount}");
            PrimaryAmountController(amount);

            AddTransactionToList(amount, date, note);
            Console.WriteLine();
        }

        public void MakeWithdraw(decimal amount, DateTime date, string note)
        {
            Console.WriteLine($"Operacja wypłaty kwoty: {amount}");
            PrimaryAmountController(amount);

            AddTransactionToList(-amount, date, note);
            Console.WriteLine();
        }

        public void GiveLoan(decimal amount, DateTime date, string note)
        {
            Console.WriteLine($"Operacja udzielenia kredytu w kwocie: {amount}");

            PrimaryAmountController(amount);
            AdditionalAmountController(amount, MaxLoanValue, "Przekroczyłeś maksymalną kwotę kredytu dla tego konta");

            Loan.LoanState += amount;

            AddTransactionToList(-amount, date, note);
            Console.WriteLine();
        }

        public void PayLoan(decimal amount, DateTime date, string note)
        {
            Console.WriteLine($"Operacja spłaty kredytu w kwocie: {amount}");

            PrimaryAmountController(amount);
            AdditionalAmountController(amount, Loan.LoanState, "Podana kwota przekracza wysokość zadłużenia");

            Loan.LoanState -= amount;

            AddTransactionToList(amount, date, note);
            Console.WriteLine();
        }

        public void ListTransactions()
        {
            Console.WriteLine("Historia rachunku");
            int transactionCounter = 0;
            foreach (Transaction singleTransaction in AllTransactions)
            {
                Console.WriteLine($"Operacja numer {transactionCounter} wykonana w dniu {singleTransaction.Date} na kwotę {singleTransaction.Amount} tytułem {singleTransaction.Note}");
                Console.WriteLine("");
                transactionCounter++;
            }
            Console.WriteLine();
        }

        public void DisplayInfo()
        {
            Console.WriteLine("Podstawowe informacje o koncie");
            Console.WriteLine($"Właściciel rachunku: {Owner}");
            Console.WriteLine($"Stan konta (po odliczeniu kredytów): {Balance}");
            Console.WriteLine($"Stan kredytu: {Loan.LoanState}");
            Console.WriteLine();
        }
        public void Exit() 
        {
            Console.WriteLine("Czy chcesz kontynuować? Jeżeli tak, napisz t. Jeżeli nie - wybierz dowolny klawisz, aby zakończyć operacje w koncie bankowym");
            Console.Write("Twoja decyzja: ");
            if (Console.ReadLine() != "t")
            {
                Console.WriteLine("Dziękujemy za skorzystanie z programu.");
            }
        }
    }
}