using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos
{
    public class CardList
    {
        public long UserCreditCardId { get; set; }
        public string CardType { get; set; }
        public long Month { get; set; }
        public long Year { get; set; }
        public string Last4 { get; set; }
        public string Name { get; set; }
        public bool? IsPrimary { get; set; }
    }
}
