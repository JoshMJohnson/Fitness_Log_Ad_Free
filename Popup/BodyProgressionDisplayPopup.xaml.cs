using WorkoutLog.Model;

namespace WorkoutLog.Popup;

public partial class BodyProgressionDisplayPopup
{
    private Progression current_progression { get; set; }
    
    public BodyProgressionDisplayPopup(string image_name)
	{
		InitializeComponent();
        View_Progression_Data(image_name);
    }

    /* displays the progression image */
    private async void View_Progression_Data(string image_name)
    {
        current_progression = await App.RecordRepo.Get_Progression(image_name);

        image_date_selected_display.Text = current_progression.date;
        load_progression_view.Source = current_progression.image_full_path;
    }

    /* closes popup for viewing a body progression */
    private void Close_Progression_Popup(object sender, EventArgs e)
    {
        Close();
    }

    /* delete progression currently being viewed */
    private async void Remove_Body_Progression(object sender, EventArgs e)
    {
        string progression_image_name = current_progression.image_full_path;
        await App.RecordRepo.Remove_Progression(progression_image_name);
        Close();
    }
}