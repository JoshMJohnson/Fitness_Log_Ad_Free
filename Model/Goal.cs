using SQLite;

namespace WorkoutLog.Model;

[Table("Goals")]
public class Goal
{
	[PrimaryKey, AutoIncrement]
	public int Id { get; set; }

	public bool is_weight_goal { get; set; }

    public bool is_pr_goal { get; set; }

    public bool is_body_weight_goal { get; set; }

	public int weight_lbs { get; set; }

	public string time { get; set; }
}