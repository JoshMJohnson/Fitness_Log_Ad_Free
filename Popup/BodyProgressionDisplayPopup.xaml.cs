namespace WorkoutLog.Popup;

public partial class BodyProgressionDisplayPopup
{
	public BodyProgressionDisplayPopup(string image_name)
	{
		InitializeComponent();
	}

    /* closes popup for viewing a body progression */
    private void Close_Progression_Popup(object sender, EventArgs e)
    {
        Close();
    }

    /* todo delete progression currently being viewed */
    private async void Remove_Body_Progression(object sender, EventArgs e)
    {


    }
}