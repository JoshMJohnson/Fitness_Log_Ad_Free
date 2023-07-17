using SQLite;
using System.ComponentModel;

namespace WorkoutLog.Model;

[Table("GoalsBW")]
public class GoalBW
{
    [PrimaryKey]
    public string name { get; set; }

    public string goal_achieve_by_date { get; set; }
    public bool date_desired { get; set; }

    public int weight { get; set; }

    public DateTime date_sort { get; set; }
}