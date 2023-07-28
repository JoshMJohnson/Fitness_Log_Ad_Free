using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Xml.Linq;

namespace WorkoutLog.Popup;

public partial class BodyWeightAddPopup
{
	public BodyWeightAddPopup()
	{
		InitializeComponent();
	}

    /* records body weight entry */
    private async void Submit_Body_Weight(object sender, EventArgs e)
    {
        string weight_update_string = weight_entry.Text;

        if (weight_update_string != null && weight_update_string.Length != 0) /* if weight field is not empty */
        {
            weight_update_string = weight_update_string.ToString();
            int weight_update = int.Parse(weight_update_string);

            DateTime date = record_date.Date;

            await App.RecordRepo.Add_Body_Weight(date, weight_update);

            error_prompt.IsVisible = false;
            Close(true);
        }
        else /* if weight field is empty */
        {
            error_prompt.Text = "Body weight value cannot be empty";
            error_prompt.IsVisible = true;
        }
    }

    /* closes popup for recording a body weight entry */
    private void Cancel_Body_Weight(object sender, EventArgs e)
    {
        Close();
    }
}