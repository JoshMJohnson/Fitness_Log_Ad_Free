using SQLite;
using System.ComponentModel;

namespace WorkoutLog.Model;

[Table("GoalsPR")]
public class GoalPR
{
    [PrimaryKey]
    public string name { get; set; }

    [DefaultValue(false)]
    public bool has_accomplished_goal { get; set; }

    public DateTime goal_achieve_by_date { get; set; }
    public bool date_desired { get; set; }

    public bool is_weight_goal { get; set; }

    public int weight { get; set; }

    public int time_hours { get; set; }
    public int time_min { get; set; }
    public int time_sec { get; set; }
}