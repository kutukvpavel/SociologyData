using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ScottPlot;
using ScottPlot.Avalonia;
using System;
using System.Drawing;

namespace SociologyData.Views
{
    public partial class MainWindow : ReactiveWindow<ViewModels.MainWindowViewModel>
    { 
        private AvaPlot _AvaPlot;
        private Plot _Plot { get => _AvaPlot.Plot; }

        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            _AvaPlot = this.FindControl<AvaPlot>("MainPlot");
            _Plot.Legend(location: Alignment.UpperLeft).FillColor = ColorTranslator.FromHtml("#5DFFFFFF");
            
            _Plot.YAxis2.Ticks(true);
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public void Plot()
        {
            PlotHelper((points) => _Plot.AddScatter(points.Item1, points.Item2, 
                label: $"{ViewModel.CurrentTable}-{ViewModel.CurrentRegion}-{ViewModel.CurrentColumn}",
                markerShape: MarkerShape.filledCircle, markerSize: 10));
        }

        public void PlotSecondary()
        {
            PlotHelper((points) =>
            {
                var p = _Plot.AddScatter(points.Item1, points.Item2,
                    label: $"{ViewModel.CurrentTable}-{ViewModel.CurrentRegion}-{ViewModel.CurrentColumn}",
                    markerShape: MarkerShape.eks, markerSize: 10);
                p.YAxisIndex = 1;
            });
        }

        private void PlotHelper(Action<Tuple<double[], double[]>> plotter)
        {
            var points = ViewModel.GetPoints();
            if (points.Item1.Length == 0 || points.Item2.Length == 0) return;
            plotter(points);
            _Plot.AxisAuto();
            _Plot.AxisAuto(null, null, 0, 1);
            _AvaPlot.Refresh();
        }

        public void Clear()
        {
            _Plot.Clear();
            _AvaPlot.Refresh();
        }

        public void RemoveLast()
        {
            int l = _Plot.GetPlottables().Length;
            if (l == 0) return;
            _Plot.RemoveAt(l - 1);
            _Plot.AxisAuto();
            _Plot.AxisAuto(null, null, 0, 1);
            _AvaPlot.Refresh();
        }
    }
}
