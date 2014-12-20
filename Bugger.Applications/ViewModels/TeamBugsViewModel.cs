﻿using BigEgg.Framework.Applications.ViewModels;
using Bugger.Applications.Services;
using Bugger.Applications.Views;
using Bugger.Base.Models;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;

namespace Bugger.Applications.ViewModels
{
    [Export]
    public class TeamBugsViewModel : ViewModel<ITeamBugsView>
    {
        #region Fields
        private readonly IDataService dataService;
        #endregion

        [ImportingConstructor]
        public TeamBugsViewModel(ITeamBugsView view, IDataService dataService)
            : base(view)
        {
            this.dataService = dataService;
        }

        #region Properties
        public ObservableCollection<Bug> Bugs { get { return this.dataService.TeamBugs; } }
        #endregion
    }
}
