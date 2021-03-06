﻿using Bugger.Applications.Services;
using Bugger.Applications.ViewModels;
using Bugger.Applications.Views;
using Bugger.Base.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Bugger.Applications.Test.ViewModels
{
    [TestClass]
    public class TeamBugsViewModelTest : TestClassBase
    {
        [TestMethod]
        public void GenaralTeamBugsViewModelTest()
        {
            ITeamBugsView view = Container.GetExportedValue<ITeamBugsView>();
            IDataService dataService = Container.GetExportedValue<IDataService>();

            TeamBugsViewModel viewModel = new TeamBugsViewModel(view, dataService);
            Assert.AreEqual(0, viewModel.Bugs.Count);

            dataService.TeamBugs.Add(
                new Bug()
                {
                    ID = 5,
                    Title = "Bug5",
                    Description = "Description for Bug5.",
                    Type = BugType.Red,
                    AssignedTo = "BigEgg",
                    State = "Implement",
                    ChangedDate = new DateTime(2013, 4, 11),
                    CreatedBy = "Pupil",
                    Priority = "High",
                    Severity = "High"
                }
            );
            Assert.AreEqual(1, viewModel.Bugs.Count);
        }
    }
}
