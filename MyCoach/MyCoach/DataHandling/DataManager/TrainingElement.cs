using MyCoach.Model.DataTransferObjects;

namespace MyCoach.DataHandling.DataManager
{
    public class TrainingElement
    {
        public TrainingElementType Type { get; set; }

        public Exercise Exercise { get; set; }

        public string Headline { get; set; }
    }
}
