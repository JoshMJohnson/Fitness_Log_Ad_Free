using SQLite;
using System.ComponentModel;

namespace WorkoutLog.Model;

[Table("GoalsBW")]
public class GoalBW
{
    [PrimaryKey]
    public string name { get; set; }

    [DefaultValue(false)]
    public bool has_accomplished_goal { get; set; }

    public string goal_achieve_by_date { get; set; }
    public bool date_desired { get; set; }

    public int weight { get; set; }
}