using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PoseTrainerClient.ViewModels
{
    public class BilateralRepsVM
    {
        public int LeftSideReps { get; set; }
        public int RightSideReps { get; set; }
        public string ExerciseId { get; set; }
    }
}
