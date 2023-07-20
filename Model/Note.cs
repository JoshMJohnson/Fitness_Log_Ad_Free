using Java.Util;
using SQLite;


namespace WorkoutLog.Model;

[Table("Notes")]
public class Note
{
	[PrimaryKey]
	public string name { get; set; }

	public string content { get; set; }

	public DateTime last_edited { get; set; }
}