using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos
{
    public class UpdateCardRequest
    {
        public long userCreditCardId { get; set; }
        public long userId { get; set; }
        public bool value { get; set; }
    }
}
