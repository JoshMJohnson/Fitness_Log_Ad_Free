using SQLite;

namespace WorkoutLog.Model;

[Table("PRs")]
public class PR
{
    [PrimaryKey]
    public string exercise_name { get; set; }

    [NotNull]
    public DateTime date_achieved { get; set; }

    [NotNull]
    public bool is_weight_pr { get; set; }

    public int weight { get; set; }

    public int time_hours { get; set; }
    public int time_min { get;set; }
    public int time_sec { get; set; }
}