using SQLite;

namespace WorkoutLog.Model;

[Table("Goals")]
public class Goal
{
	[PrimaryKey, AutoIncrement]
	public int Id { get; set; }
	

}