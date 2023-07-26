using SQLite;

namespace WorkoutLog.Model;

[Table("Progressions")]
public class Progression
{
    [PrimaryKey]
    public string name { get; set; }

    public string date { get; set; }
    public DateTime date_sort { get; set; }
}