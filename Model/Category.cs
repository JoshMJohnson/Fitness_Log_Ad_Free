using SQLite;
using System.ComponentModel;

namespace WorkoutLog.Model;

[Table("Categories")]
public class Category
{
	[PrimaryKey]
	public string name { get; set; }

	public bool still_available { get; set; }
}