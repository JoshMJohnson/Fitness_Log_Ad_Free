using SQLite;

namespace WorkoutLog.Model;

[Table("BodyWeight")]
public class BodyWeight
{
    [PrimaryKey]
    public DateTime entry_date { get; set; }

    [NotNull]
    public int weight { get; set; }
}