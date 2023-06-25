namespace WorkoutLog.Popup;

public partial class GoalBodyWeightPopup
{
	public GoalBodyWeightPopup()
	{
		InitializeComponent();
    }

    /* creates an exercise along with the PR */
    private void Submit_Goal(object sender, EventArgs e)
    {

    }

    /* closes popup for creating an exercise */
    private void Cancel_Goal(object sender, EventArgs e)
    {
        Close();
    }

    /* handles event when toggled between goal date and no date */
    private void date_included_change(object sender, EventArgs e)
    {
        if (date_included_toggle.IsToggled) /* if date desired */
        {
            record_date_display.IsVisible = true;
            no_date_display.IsVisible = false;
        }
        else /* else no date desired */
        {
            record_date_display.IsVisible = false;
            no_date_display.IsVisible = true;
        }
    }
}
