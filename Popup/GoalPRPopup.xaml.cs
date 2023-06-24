namespace WorkoutLog.Popup;

public partial class GoalPRPopup
{
	public GoalPRPopup()
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

    /* handles event when toggled between weight and time PR type */
    private void exercise_type_change(object sender, EventArgs e)
    {
        
    }

    /* handles event when toggled between goal date and no date */
    private void date_included_change(object sender, EventArgs e)
    {

    }
}
