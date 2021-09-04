using MyCoach.DataHandling;
using MyCoach.DataHandling.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCoach.ViewModel.TrainingGeneration
{
    public static class TrainingGenerator
    {
        public static Training CreateTraining(TrainingSettings trainingSettings)
        {
            var training = new Training();
            var globalSettings = DataInterface.GetInstance().GetData<Settings>().FirstOrDefault();
            return training;
        }

        public static double DetermineMultiplierForRound(
            int round, 
            Settings globalSettings)
        {
            switch (round)
            {
                case 1:
                    return 1.0 * globalSettings.ScoresRound1 / 100;
                case 2:
                    return 1.0 * globalSettings.ScoresRound2 / 100;
                case 3:
                    return 1.0 * globalSettings.ScoresRound3 / 100;
                case 4:
                    return 1.0 * globalSettings.ScoresRound4 / 100;
                default:
                    return 1.0;                    
            }
        }
    }
}
