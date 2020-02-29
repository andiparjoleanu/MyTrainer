using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PoseTrainerClient.ViewModels
{
    public class HistoryVM
    {
        public string UserId { get; set; }
        public string ExerciseId { get; set; }
        public DateTime Date { get; set; }
        public int NoRepsLeft { get; set; }
        public int NoRepsRight { get; set; }
    }
}
