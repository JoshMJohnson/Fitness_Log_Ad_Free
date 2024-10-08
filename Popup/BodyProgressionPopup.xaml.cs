using Microsoft.Maui.Storage;
using WorkoutLog.Model;

namespace WorkoutLog.Popup;

public partial class BodyProgressionPopup
{
    private string image_full_path_cache = "";
    private string image_file_name;
    private Stream stream;

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

            string local_storage_location_prev = Path.Combine(FileSystem.AppDataDirectory, image_file_name);

            if (local_storage_location_prev != image_full_path_cache) /* if stream already being used by image */
            {
                /* save image from cache to local storage */
                string local_storage_location = Path.Combine(FileSystem.AppDataDirectory, image_file_name);
                FileStream local_file_stream = File.OpenWrite(local_storage_location);
                await stream.CopyToAsync(local_file_stream);

                /* save progression to database */
                List<Progression> progression_list_before_entry = await App.RecordRepo.Get_Progression_List();
                await App.RecordRepo.Add_Progression(local_storage_location, image_date);
                List<Progression> progression_list_after_entry = await App.RecordRepo.Get_Progression_List();

                if (progression_list_before_entry.Count == progression_list_after_entry.Count) /* if duplicate */
                {
                    Close(false);
                }
                else
                {
                    Close(true);
                }
            }
            else
            {
                Close(false);
            }
        }
    }

    /* load local image from device */
    private async void Load_Image(object sender, EventArgs e)
    {
        FileResult result = await FilePicker.PickAsync(new PickOptions
        {
            PickerTitle = "Load Body Progression",
            FileTypes = FilePickerFileType.Images
        });

        if (result == null) /* if image returned is null */
        {
            return;
        }

        stream = await result.OpenReadAsync();

        
        image_full_path_cache = result.FullPath;
        image_file_name = result.FileName;

        load_progression_preview.Source = image_full_path_cache;
        error_prompt.IsVisible = false;
    }
}