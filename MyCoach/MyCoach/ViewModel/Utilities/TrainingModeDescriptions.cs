using MyCoach.DataHandling;
using MyCoach.Model.DataTransferObjects;
using MyCoach.Model.Defines;
using MyCoach.ViewModel.TrainingGenerationAndEvaluation;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyCoach.ViewModel.Utilities
{
    /// <summary>
    ///     This static class can be used to build the description for a selected <see cref="TrainingMode"/>.
    /// </summary>
    public static class TrainingModeDescriptions
    {
        public const string DESCRIPTION_CIRCLETRAINING = "In diesem Modus wird pro Runde je eine Übung aus allen aktiven Kategorien in das Training eingeplant.";
        public const string DESCRIPTION_RONDOMTRAINING = "In diesem Modus werden die Übungen für eine Trainingsrunde völlig zufällig aus allen aktiven Kategorien ausgewählt.";
        public const string DESCRIPTION_FOCUSTRAINING = "In diesem Modus werden nur Übungen der ausgewählten Kategorie in das Training eingeplant.";
        public const string DESCRIPTION_USERDEFINEDTRAINING = "In diesem Modus kann ein Training frei erstellt werden." +
            "Nach dem Hinzufügen kannst du eine Übung oder Überschrift durch Anklicken bearbeiten." +
            "Ein frei erstelltes Training kann gespeichert und später wieder geladen werden.";

        public static string GetTrainingModeDescription(TrainingMode trainingMode)
        {
            switch (trainingMode)
            {
                case TrainingMode.CircleTraining:
                case TrainingMode.RandomTraining:
                    var warmUp = DataInterface.GetInstance().GetData<Category>().FirstOrDefault(c => c.Type == ExerciseType.WarmUp);
                    var coolDown = DataInterface.GetInstance().GetData<Category>().FirstOrDefault(c => c.Type == ExerciseType.CoolDown);

                    var sb = new StringBuilder(GetInitialStringForDescriptionOfCircleOrRandomTraining(trainingMode));

                    if (warmUp?.Active == true)
                    {
                        sb.Append($" Vor dem eigentlichen Training steht ein Block {warmUp}");
                    }

                    if (warmUp?.Active == true && coolDown?.Active == false)
                    {
                        sb.Append($".");
                    }

                    if (coolDown?.Active == true && warmUp?.Active == true)
                    {
                        sb.Append(" und nach");
                    }

                    if (coolDown?.Active == true && warmUp?.Active != true)
                    {
                        sb.Append(" Nach");
                    }

                    if (coolDown?.Active == true)
                    {
                        sb.Append($" dem Training folgt ein Block {coolDown}.");
                    }

                    return sb.ToString();
                case TrainingMode.FocusTraining:
                    return DESCRIPTION_FOCUSTRAINING;
                case TrainingMode.UserDefinedTraining:
                    return DESCRIPTION_USERDEFINEDTRAINING;
                default:
                    return string.Empty;
            }
        }

        private static string GetInitialStringForDescriptionOfCircleOrRandomTraining(TrainingMode trainingMode)
        {
            switch (trainingMode)
            {
                case TrainingMode.CircleTraining:
                    return DESCRIPTION_CIRCLETRAINING;
                case TrainingMode.RandomTraining:
                    return DESCRIPTION_RONDOMTRAINING;
                default:
                    return string.Empty;
            }
        }
    }
}
