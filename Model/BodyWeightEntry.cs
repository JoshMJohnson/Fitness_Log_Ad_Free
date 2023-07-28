using SQLite;

namespace WorkoutLog.Model;

[Table("BodyWeight")]
public class BodyWeightEntry
{
    [PrimaryKey]
    public DateTime date { get; set; }

    [NotNull]
    public int weight { get; set; }
}