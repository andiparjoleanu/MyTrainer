using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PoseTrainerClient.Models
{
    public class History
    {
        public string UserId { get; set; }
        public virtual User User { get; set; }
        public string ExerciseId { get; set; }
        public virtual Exercise Exercise { get; set; }
        public DateTime Date { get; set; }
        public int NoRepsLeft { get; set; }
        public int NoRepsRight { get; set; }
    }
}
