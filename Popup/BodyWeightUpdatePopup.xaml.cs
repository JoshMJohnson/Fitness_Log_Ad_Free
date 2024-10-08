using WorkoutLog.Model;

namespace WorkoutLog.Popup;

public partial class BodyWeightUpdatePopup
{
    private BodyWeightEntry current_entry { get; set; }
    
    public BodyWeightUpdatePopup(DateTime entry_date)
	{
		InitializeComponent();
        View_Entry(entry_date);
    }

    /* displays the notes content */
    private async void View_Entry(DateTime entry_date)
    {
        current_entry = await App.RecordRepo.Get_Body_Weight(entry_date);

        string full_date = current_entry.date.ToString();
        string[] split_date = full_date.Split(' ');
        string date_only = split_date[0];

        record_date.Text = date_only;
        weight_entry.Placeholder = current_entry.weight.ToString();
    }

    /* executes when edit body weight entry button clicked */
    private async void Edit_Body_Weight_Entry(object sender, EventArgs e)
    {
        DateTime date = current_entry.date;
        string weight_update_string = weight_entry.Text;

        if (weight_update_string != null && weight_update_string.Length != 0) /* if weight field is not empty */
        {
            weight_update_string = weight_update_string.ToString();
            double weight_update = double.Parse(weight_update_string);

            await App.RecordRepo.Edit_Body_Weight(date, weight_update);

            error_prompt.IsVisible = false;
            Close();
        }
        else /* if weight field is empty */
        {
            error_prompt.Text = "Body weight value cannot be empty";
            error_prompt.IsVisible = true;
        }
    }

    /* executes when delete body weight entry button is clicked */
    private async void Delete_Body_Weight_Entry(object sender, EventArgs e)
    {
        DateTime entry_date = current_entry.date;
        await App.RecordRepo.Remove_Body_Weight(entry_date);
        Close();
    }

    /* closes the note view */
    private void Close_Edit_Popup(object sender, EventArgs e)
    {
        Close();
    }
}