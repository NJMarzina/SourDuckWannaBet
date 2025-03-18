using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class BetWithUsernames
    {
        public Bet Bet { get; set; }
        public string UserName_Sender { get; set; }
        public string UserName_Receiver { get; set; }
    }

}
