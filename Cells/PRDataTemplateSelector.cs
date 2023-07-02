using System;
using System.Collections.Generic;
using System.Text;
using WorkoutLog.Model;

namespace WorkoutLog.Cells;

/* Determines which data template to return based on cell data */
public class PRDataTemplateSelector : DataTemplateSelector
{
    public DataTemplate Is_Weight_PR { get; set; }
    public DataTemplate Is_Time_PR { get; set; }

    protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
    {
        var pr_entry = (PR) item;

        return pr_entry.is_weight_pr == true ? Is_Weight_PR : Is_Time_PR;
    }
}