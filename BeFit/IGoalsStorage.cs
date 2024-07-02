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

    public sealed class TestGoalsStorage : IGoalsStorage
    {
        public IEnumerable<Goal> GetGoals()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<GoalState> GetGoalsStateOn(DateTime date)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IEnumerable<GoalState>> GetHistory(DateTime from, DateTime to)
        {
            throw new NotImplementedException();
        }
    }
}
