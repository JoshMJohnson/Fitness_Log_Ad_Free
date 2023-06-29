using SQLite;

namespace WorkoutLog.Model;

[Table("PRs")]
public class PR
{
    [PrimaryKey]
    public string exercise_name { get; set; }

    public DateTime date_achieved { get; set; }

    public int weight { get; set; }

    public int time_hours { get; set; }
    public int time_min { get;set; }
    public int time_sec { get; set; }

    public bool weight_pr_type { get; set; } /* true = Weight; else false = Time */
}