using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PoseTrainerClient.Models
{
    public class Exercise
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Difficulty { get; set; }
        public string Script { get; set; }
        public ICollection<History> Histories { get; set; }
    }
}
