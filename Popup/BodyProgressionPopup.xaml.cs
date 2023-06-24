namespace WorkoutLog.Popup;

public partial class BodyProgressionPopup
{
	public BodyProgressionPopup()
	{
		InitializeComponent();
    }

    /* creates an exercise along with the PR */
    private void Submit_Progression(object sender, EventArgs e)
    {

    }

    /* closes popup for creating an exercise */
    private void Cancel_Progression(object sender, EventArgs e)
    {
        Close();
    }
}