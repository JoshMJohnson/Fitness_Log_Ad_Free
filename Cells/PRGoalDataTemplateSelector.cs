using System;
using System.Collections.Generic;
using System.Text;
using WorkoutLog.Model;

namespace WorkoutLog.Cells;

public class PRGoalDataTemplateSelector : DataTemplateSelector
{
    public DataTemplate Is_Weight_PR_Goal { get; set; }
    public DataTemplate Is_Time_PR_Goal { get; set; }

    protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
    {
        var pr_goal_entry = (GoalPR) item;

        return pr_goal_entry.is_weight_goal == true ? Is_Weight_PR_Goal : Is_Time_PR_Goal;
    }
}