using SQLite;

namespace WorkoutLog.Model;

[Table("BodyWeight")]
public class BodyWeightEntry
{
    [PrimaryKey, AutoIncrement]
    public int id { get; set; }

    [NotNull]
    public DateTime entry_date { get; set; }

    [NotNull]
    public int weight { get; set; }
}