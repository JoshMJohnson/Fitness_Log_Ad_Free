using System;
using WorkoutLog.Model;

namespace WorkoutLog.Cells;

public class CalendarDayTemplateSelector : DataTemplateSelector
{
    public DataTemplate display_day_entry_symbol { get; set; } /* display the symbol indicating an entry for that date */

    public DataTemplate no_display_day_entry_symbol { get; set; } /* hide display the symbol indicating an entry for that date */

    protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
    {
        var day_entry = (CalendarDay) item;

        if (day_entry.has_entry) /* if date has at least one entry */
        {
            return display_day_entry_symbol;
        }
        else /* else date has no entry */
        {
            return no_display_day_entry_symbol;
        }
    }
}