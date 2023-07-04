using SQLite;

namespace WorkoutLog.Model;

[Table("CalendarEntries")]
public class CalendarEntry
{
    [PrimaryKey, AutoIncrement]
    public int id { get; set; }

    [NotNull]
    public DateTime entry_date { get; set; }

    [NotNull]
    public Category calendar_category { get; set; }
}