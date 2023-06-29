using SQLite;

namespace WorkoutLog.Model;

[Table("Calendar")]
public class CalendarEntry 
{
    [PrimaryKey, AutoIncrement]
    public int id { get; set; }

    [NotNull]
    public DateTime entry_date { get; set; }

    [NotNull]
    public Category calendar_category { get; set; }
}