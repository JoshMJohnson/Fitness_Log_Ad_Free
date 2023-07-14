using System;
using WorkoutLog.Model;

namespace WorkoutLog.Cells;

public class CalendarDayTemplateSelector : DataTemplateSelector
{
    public DataTemplate display_day_entry_symbol { get; set; } /* display the symbol indicating an entry for that date */

    public DataTemplate no_display_day_entry_symbol { get; set; } /* hide display the symbol indicating an entry for that date */

    private bool display_indicator { get; set; }

    protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
    {
        var day_entry = (CalendarDay) item;

        Day_Fetch(day_entry);

        if (display_indicator) /* if date has at least one entry */
        {
            return display_day_entry_symbol;
        }
        else /* else date has no entry */
        {
            return no_display_day_entry_symbol;
        }
    }

    /* fetches date data from database */
    private async void Day_Fetch(CalendarDay day_entry)
    {
        List<CalendarEntry> entries = await App.RecordRepo.Get_Calendar_Entries_List(day_entry.date);

        if (entries.Count != 0) /* if date has at least one entry */
        {
            display_indicator = true;
        }
        else /* else date has no entry */
        {
            display_indicator = false;
        }
    }
}