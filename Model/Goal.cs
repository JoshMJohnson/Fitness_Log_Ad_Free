using SQLite;

namespace WorkoutLog.Model;

[Table("Goals")]
public class Goal
{
	[PrimaryKey, AutoIncrement]
	public int id { get; set; }

	public bool has_accomplished_goal { get; set; }

	public bool is_weight_goal { get; set; }

    public bool is_pr_goal { get; set; }

    public bool is_body_weight_goal { get; set; }

	public int weight_lbs { get; set; }

	public string time { get; set; }

	public DateTime goal_archieve_by_date { get; set; }
}