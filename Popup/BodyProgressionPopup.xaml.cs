using Microsoft.Maui.Storage;

namespace WorkoutLog.Popup;

public partial class BodyProgressionPopup
{
    private string full_path;

	public BodyProgressionPopup()
	{
		InitializeComponent();
    }

    /* closes popup for creating an exercise */
    private void Cancel_Progression(object sender, EventArgs e)
    {
        Close();
    }

    /* submits body progression image */
    private void Save_Progression(object sender, EventArgs e)
    {
        
    }

    /* load local image from device */
    private async void Load_Image(object sender, EventArgs e)
    {
        var result = await FilePicker.PickAsync(new PickOptions
        {
            PickerTitle = "Load Body Progression",
            FileTypes = FilePickerFileType.Images
        });

        if (result == null) /* if image returned is null */
        {
            return;
        }

        full_path = result.FullPath;

        var stream = await result.OpenReadAsync();

        load_progression_preview.Source = ImageSource.FromStream(() => stream);
    }
}