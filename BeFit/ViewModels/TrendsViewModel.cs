using System.Collections.Generic;

using LiveChartsCore.SkiaSharpView;
using LiveChartsCore;

using System;

using CommunityToolkit.Mvvm.Input;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;

namespace BeFit.ViewModels
{
    public partial class TrendsViewModel(INavigator navigator, IEnumerable<OneDayGoalsViewModel> days) : ViewModelBase
    {
        public ISeries[] Series { get; } =
        [
            new LineSeries<OneDayGoalsViewModel>
            {
                Values = days,
                Mapping = (sample, index) => new(sample.Day.ToOADate(), sample.Progress),
                Fill = new SolidColorPaint(
                    new SKColor(
                        SKColors.DeepSkyBlue.Red,
                        SKColors.DeepSkyBlue.Green,
                        SKColors.DeepSkyBlue.Blue,
                        32)),
                XToolTipLabelFormatter = (p) => p.Model!.Day.ToString("dd MMM"),
                YToolTipLabelFormatter = (p) => $"{p.Model!.Progress:00.0}%",
            }
        ];

        public Axis[] XAxes { get; } =
        [
            new Axis
            {
                Labeler = (value) => DateTime.FromOADate(value).ToString("dd.MM"),
                MinLimit = 0,
                MaxLimit = 10
            }
        ];

        public Axis[] YAxes { get; } =
        [
            new Axis
            {
                Labeler = (value) => $"{value:##0}%",
                MinLimit = 0,
                MaxLimit = 100
            }
        ];

        [RelayCommand]
        private void Back()
            => navigator.NavigateBackward();
    }
}
