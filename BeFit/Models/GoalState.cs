using System;

namespace BeFit.Models;

public sealed record class GoalState(Goal Goal, bool Done, DateTime Created);
