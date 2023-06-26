using SQLite;

namespace WorkoutLog.Model;

[Table("PRs")]
public class PR
{
    [PrimaryKey, AutoIncrement]
    public int ID { get; set; }

    public DateTime Date_Achieved { get; set; }

    [Unique]
    public string Exercise_Name { get; set; }

    public int Weight { get; set; }

    public int Time_Hours { get; set; }
    public int Time_Min { get;set; }
    public int Time_Sec { get; set; }

    public bool Weight_PR_Type { get; set; } /* true = Weight; else false = Time */
}