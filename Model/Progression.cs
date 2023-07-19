using SQLite;

namespace WorkoutLog.Model;

[Table("Progressions")]
public class Progression
{
    [PrimaryKey, AutoIncrement]
    public int id { get; set; }

    public string image_date { get; set; }
    public DateTime image_sort { get; set; }

    public string image_full_path { get; set; }
}