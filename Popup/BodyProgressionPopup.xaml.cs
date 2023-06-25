using Microsoft.Maui.Storage;

namespace WorkoutLog.Popup;

public partial class BodyProgressionPopup
{
	public BodyProgressionPopup()
	{
		InitializeComponent();
    }

    /* closes popup for creating an exercise */
    private void Cancel_Progression(object sender, EventArgs e)
    {
        Close();
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

        var stream = result.FullPath;
        Console.WriteLine($"******{stream}******");

        Close();
    }
}