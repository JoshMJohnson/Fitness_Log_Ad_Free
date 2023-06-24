namespace WorkoutLog.Popup;

public partial class PersonalRecordAddPopup
{
	public PersonalRecordAddPopup()
	{
		InitializeComponent();

        time_display.IsVisible = false;
    }

    /* creates an exercise along with the PR */
    private void Submit_PR(object sender, EventArgs e)
    {

    }

    /* closes popup for creating an exercise */
    private void Cancel_PR(object sender, EventArgs e)
    {
        Close();
    }

    /* handles event when toggled between weight and time PR type */
    private void exercise_type_change(object sender, EventArgs e)
    {
        /* if changed to toggle on weight PR */
        if (time_display.IsVisible)
        {
            weight_display.IsVisible = true;
            time_display.IsVisible = false;
        }
        else /* else changed to toggle on time PR */
        {
            weight_display.IsVisible = false;
            time_display.IsVisible = true;
        }
    }
}
