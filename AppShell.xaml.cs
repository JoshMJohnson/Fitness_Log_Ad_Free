namespace WorkoutLog;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		Prepare_Footer();
    }

	/* todo prepare footer */
	private void Prepare_Footer()
	{
		/* time stamp */
		DateTime current_date = DateTime.Now;
		string[] date_string = current_date.ToString().Split(' ');
		string date_display = date_string[0];
		footer_date.Text = date_display;
    }
}
