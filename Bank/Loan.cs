using System;

namespace Bank
{
    class Loan
    {
        //Projekt wykonali: Michał Pieczyński i Mateusz Pawlak
        private decimal loanState;
        public decimal LoanState
        {
            get { return loanState; }
            set { loanState = value; }
        }
        public Loan(decimal loan)
        {
            this.loanState = loan;
        }
    }
}