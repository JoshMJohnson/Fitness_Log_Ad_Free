using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Xml.Linq;
using WorkoutLog.Model;

namespace WorkoutLog.Popup;

public partial class BodyWeightAddPopup
{
	public BodyWeightAddPopup()
	{
		InitializeComponent();
     
        record_date.MaximumDate = DateTime.Now;
    }

    /* records body weight entry */
    private async void Submit_Body_Weight(object sender, EventArgs e)
    {
        string weight_update_string = weight_entry.Text;

        if (weight_update_string != null && weight_update_string.Length != 0) /* if weight field is not empty */
        {
            weight_update_string = weight_update_string.ToString();
            double weight_update = double.Parse(weight_update_string);

            DateTime date = record_date.Date;

            List<BodyWeightEntry> body_weight_list_before = await App.RecordRepo.Get_Body_Weight_List();
            await App.RecordRepo.Add_Body_Weight(date, weight_update);
            List<BodyWeightEntry> body_weight_list_after = await App.RecordRepo.Get_Body_Weight_List();

            error_prompt.IsVisible = false;

            if (body_weight_list_before.Count == body_weight_list_after.Count) /* if duplicate entry */
            {
                Close(false);
            }
            else /* else; valid entry; not a duplicate */
            {
                Close(true);
            }
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