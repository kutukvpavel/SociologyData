using System;
using System.Collections.Generic;
using System.Text;
using ReactiveUI;
using Avalonia;
using Avalonia.Interactivity;
using System.Linq;

namespace SociologyData.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel() : base()
        {
            CurrentRegion = Regions[0];
            CurrentTable = _tables.First();
        }

        private Data.Repository _Repo;
        public Data.Repository Repo 
        {
            get => _Repo;
            set
            {
                _Repo = value;
                AvailableTables = _Repo.GetTables();
            }
        }


        public string CurrentTable { get; set; }
        public string CurrentRegion { get; set; }
        public string[] Regions { get; set; } = new string[] 
        { 
            "Российская Федерация", 
            "г. Москва", 
            "г. Санкт-Петербург",
            "Республика Татарстан"
        };
        public int CurrentColumn { get; set; } = 1;

        private IEnumerable<string> _tables = new string[] { "Ex 1", "Ex 2" };
        public IEnumerable<string> AvailableTables
        {
            get => _tables;
            set
            {
                this.RaiseAndSetIfChanged(ref _tables, value);
            }
        }

        public Tuple<double[], double[]> GetPoints()
        {
            return Repo.GetTimePlot(Data.AliasProvider.GetAlias(Data.AliasProvider.TableAliasGroups, CurrentTable),
                Data.AliasProvider.GetAlias(Data.AliasProvider.RegionAliasGroups, CurrentRegion), CurrentColumn);
        }
    }
}
