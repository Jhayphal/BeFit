using System;
using System.Collections.Generic;

namespace BeFit.Models;

public sealed record class Schedule(int Id, IEnumerable<DayOfWeek> Days)
{
    public static Schedule CreateNew() => new(0, Enum.GetValues<DayOfWeek>());
}
