using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RockPaperScissor.Models
{
    public class HistoricalScore
    {
        public int SCORE_ID { get; set; }
        public string PLAYER { get; set; }
        public string WON_AGGAINST { get; set; }
        public DateTime CREATED_ON { get; set; }
    }
}