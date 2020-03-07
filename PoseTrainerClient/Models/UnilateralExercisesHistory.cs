﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PoseTrainerClient.Models
{
    public class UnilateralExercisesHistory
    {
        public string UserId { get; set; }
        public string ExerciseId { get; set; }
        public History History { get; set; }
        public int Reps { get; set; }
    }
}
