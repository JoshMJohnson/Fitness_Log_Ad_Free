using WorkoutLog.Model;

namespace WorkoutLog.Popup;

public partial class BodyProgressionDisplayPopup
{
    private Progression current_progression { get; set; }
    
    public BodyProgressionDisplayPopup(string image_name)
	{
		InitializeComponent();
        View_Progression(image_name);
    }

    /* todo displays the progression image */
    private async void View_Progression(string note_name)
    {


    }

    /* closes popup for viewing a body progression */
    private void Close_Progression_Popup(object sender, EventArgs e)
    {
        Close();
    }

    /* ? delete progression currently being viewed */
    private async void Remove_Body_Progression(object sender, EventArgs e)
    {
        string progression_image_name = current_progression.name;
        await App.RecordRepo.Remove_Progression(progression_image_name);
        Close();
    }
}