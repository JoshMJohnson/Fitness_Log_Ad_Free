using SQLite;

namespace WorkoutLog.Model;

[Table("Categories")]
public class Category
{
	[PrimaryKey, AutoIncrement]
	public int id { get; set; }

	[Unique]
	public string name { get; set; }

	public string color { get; set; }
}