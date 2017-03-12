using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProiectING
{
    public class CursValutar
    {
        private String Currency;
        private double Value;

        public String GetCurrency()
        {
            return this.Currency;
        }

        public void SetCurrency(String currency)
        {
            this.Currency = currency;
        }

        public double GetValue()
        {
            return this.Value;
        }

        public void SetValue(double value)
        {
            this.Value = value;
        }

        public CursValutar()
        {

        }

        public CursValutar(String currency, double value)
        {
            this.Currency = currency;
            this.Value = value;
        }

        public override string ToString()
        {
            return String.Format("{0},{1}", this.Currency, this.Value);
        }
    }
}