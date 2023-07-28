using Microsoft.Maui.Storage;

namespace WorkoutLog.Popup;

public partial class BodyProgressionPopup
{
    private string image_full_path_cache = "";

    public BodyProgressionPopup()
	{
		InitializeComponent();
    }

    /* closes popup for adding body progression */
    private void Cancel_Progression(object sender, EventArgs e)
    {
        Close();
    }

    /* submits body progression image */
    private async void Save_Progression(object sender, EventArgs e)
    {
        if (image_full_path_cache == "") /* if trying to save with no image selected */
        {
            error_prompt.IsVisible = true;
        }
        else /* else; saving a body progression image */
        {
            DateTime image_date = image_date_selected_display.Date;

            /* todo save image from cache to local storage */
            Console.WriteLine($"******image_full_path: {image_full_path_cache}******");

            


            await App.RecordRepo.Add_Progression(image_full_path_cache, image_date);

            Close();
        }
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

        error_prompt.IsVisible = false;
        image_full_path_cache = result.FullPath;
        load_progression_preview.Source = image_full_path_cache;
    }
}