using SQLite;

namespace WorkoutLog.Model;

[Table("Categories")]
public class Category
{
	[PrimaryKey]
	public string name { get; set; }

	[Unique]
	public Color color { get; set; }
}