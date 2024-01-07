using System;

namespace Bank
{
    class Transaction
    {
        //Projekt wykonali: Michał Pieczyński i Mateusz Pawlak
        public decimal Amount { get; }
        public DateTime Date { get; }
        public string Note { get; }

        public Transaction(decimal amount, DateTime date, string note)
        {
            this.Amount = amount;
            this.Date = date;
            this.Note = note;
        }

    }
}