﻿using BigEgg.Framework.UnitTesting;
using Bugger.Applications.Services;
using Bugger.Domain.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Bugger.Applications.Test.Services
{
    [TestClass]
    public class DataServiceTest : TestClassBase
    {
        [TestMethod]
        public void GeneralDataServiceTest()
        {
            IDataService dataService = Container.GetExportedValue<IDataService>();

            Assert.AreEqual(dataService.UserBugs.Count, 0);
            Assert.AreEqual(dataService.TeamBugs.Count, 0);

            dataService.UserBugs.Add(
                new Bug()
                {
                    ID = 1,
                    Title = "Bug1",
                    Description = "Description for Bug1.",
                    Type = BugType.Red,
                    AssignedTo = "BigEgg",
                    State = "Implement",
                    ChangedDate = new DateTime(2013, 4, 10),
                    CreatedBy = "BigEgg",
                    Priority = "High",
                    Severity = ""
                }
            );

            Assert.AreEqual(dataService.UserBugs.Count, 1);
            Assert.AreEqual(dataService.TeamBugs.Count, 0);

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

            Assert.AreEqual(dataService.UserBugs.Count, 1);
            Assert.AreEqual(dataService.TeamBugs.Count, 1);

            DateTime time = new DateTime(2013, 5, 8, 11, 00, 00);
            dataService.RefreshTime = time;
            Assert.AreEqual(time, dataService.RefreshTime);
        }

        [TestMethod]
        public void PropertiesWithNotification()
        {
            IDataService dataService = Container.GetExportedValue<IDataService>();
            DateTime time = new DateTime(2013, 5, 8, 11, 00, 00);

            AssertHelper.PropertyChangedEvent(dataService, x => x.RefreshTime, () =>
                dataService.RefreshTime = time
            );

            Assert.AreEqual(time, dataService.RefreshTime);
        }
    }
}
