using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MyCoach.DataHandling;
using MyCoach.DataHandling.DataManager;
using MyCoach.DataHandling.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCoachTests.ViewModel.TrainingGenerationAndEvaluation
{
    [TestClass]
    public class TrainingGeneratorTests : DataManagerTestBase
    {
        private IDataManager dataManager;
        private Settings settings;

        [TestInitialize]
        public void Init()
        {
            base.Initialize();
        }

        [TestCleanup]
        public void Cleanup()
        {
            base.CleanupTestBase();
        }

    }
}
