using System;
namespace Hilti_parser
{
    public class PriceStandard
    {
        public string currencyIso;
        public decimal value;
        public string formattedValue;
        public string quantity;
        public string priceUnit;
        public int priceUnitOriginal;
        public string symbol;

        public PriceStandard()
        {
        }
    }
}
