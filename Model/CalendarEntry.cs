using SQLite;

namespace WorkoutLog.Model;

[Table("CalendarEntries")]
public class CalendarEntry 
{
    [PrimaryKey, AutoIncrement]
    public int id { get; set; }

    public DateTime entry_date { get; set; }

    public Category calendar_category { get; set; }
}