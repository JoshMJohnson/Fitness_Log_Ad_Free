namespace WorkoutLog.Popup;

public partial class BodyWeightAddPopup
{
	public BodyWeightAddPopup()
	{
		InitializeComponent();
	}

    /* creates an exercise along with the PR */
    private void Submit_Body_Weight(object sender, EventArgs e)
    {

    }

    /* closes popup for creating an exercise */
    private void Cancel_Body_Weight(object sender, EventArgs e)
    {
        Close();
    }
}