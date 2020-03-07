using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PoseTrainerClient.ViewModels
{
    public class UnilateralExercisesHistoryVM
    {
        public string UserId { get; set; }
        public string ExerciseId { get; set; }
        public int Reps { get; set; }
    }
}
