using System.Collections.Generic;

using LiveChartsCore.SkiaSharpView;
using LiveChartsCore;

namespace BeFit.ViewModels
{
    public partial class TrendsViewModel(IEnumerable<OneDayGoalsViewModel> days) : ViewModelBase
    {
        public ISeries[] Series { get; set; } = [
            new LineSeries<OneDayGoalsViewModel>
            {
                Values = days,
                Mapping = (sample, index) => new(sample.Day.ToOADate(), sample.Progress)
            }
        ];
    }
}
