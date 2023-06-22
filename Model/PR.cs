using SQLite;

namespace WorkoutLog.Model;

[Table("PRs")]
public class PR
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }


}