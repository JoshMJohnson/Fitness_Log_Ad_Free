namespace WorkoutLog;

public partial class WorkoutCalendar : ContentPage
{
	public WorkoutCalendar()
	{
		InitializeComponent();
    }

    /* button clicked to create a workout category */
    private async void Create_Category(object sender, EventArgs e)
    {
        await DisplayAlert("Create Category", "ya", "Create");
    }

    /* button clicked to remove a workout category */
    private async void Remove_Category(object sender, EventArgs e)
    {
        await DisplayAlert("Remove Category", "ya", "Remove");
    }
}