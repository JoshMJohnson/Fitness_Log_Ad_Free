using SQLite;

namespace WorkoutLog.Model;

[Table("PRs")]
public class PR
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public DateTime Date_Achieved { get; set; }

    [Unique]
    public string Exercise_Name { get; set; }

    public int Weight { get; set; }  

    public TimeSpan Time { get;set; }
}