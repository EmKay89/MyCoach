using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MyCoach.DataHandling;
using MyCoach.DataHandling.DataManager;
using MyCoach.Model.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCoachTests.ViewModel
{
    public abstract class DataInterfaceTestBase
    {
        protected ObservableCollection<Category> Categories => TestDtos.Categories;
        protected ObservableCollection<Exercise> Exercises => TestDtos.Exercises;
        protected ObservableCollection<Month> Months => TestDtos.TrainingScores;
        protected ObservableCollection<Settings> SettingsCollection => TestDtos.Settings;
        protected ObservableCollection<TrainingSchedule> TrainingSchedulesCollection => TestDtos.TrainingSchedules;
        protected Settings Settings => this.SettingsCollection.First();
        protected TrainingSchedule TrainingSchedule => this.TrainingSchedulesCollection.First();

        protected IDataManager DataManager;

        protected void Initialize()
        {
            this.SetupDataManager();
        }

        protected void CleanupTestBase()
        {
            DataInterface.SetDataManager(null);
            TestDtos.Reset();
        }

        protected void SetupData<T>(List<T> newElements) where T : IDataTransferObject
        {
            var data = this.DataManager.GetData<T>();
            data.Clear();
            foreach (var element in newElements)
            {
                data.Add(element);
            }
        }

        private void SetupDataManager()
        {
            this.DataManager = Mock.Of<IDataManager>(dm => 
                dm.GetData<Category>() == this.Categories &&
                dm.GetData<Exercise>() == this.Exercises && 
                dm.GetData<Month>() == this.Months &&
                dm.GetData<Settings>() == this.SettingsCollection &&
                dm.GetData<TrainingSchedule>() == this.TrainingSchedulesCollection &&
                dm.SaveData<Category>() == true &&
                dm.SaveData<Exercise>() == true &&
                dm.SaveData<Month>() == true &&
                dm.SaveData<Settings>() == true &&
                dm.SaveData<TrainingSchedule>() == true);
            DataInterface.SetDataManager(this.DataManager);
        }
    }
}
