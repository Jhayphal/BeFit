using System;
using System.Collections.Generic;

using BeFit.Models;
using BeFit.ViewModels;

namespace BeFit
{
    public interface IGoalsStorage
    {
        IEnumerable<Goal> GetGoals();

        IEnumerable<GoalState> GetGoalsStateOn(DateTime date);

        IEnumerable<IEnumerable<GoalState>> GetHistory(DateTime from, DateTime to);
    }
}
