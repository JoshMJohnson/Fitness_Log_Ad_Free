using GoogleGson;

namespace WorkoutLog.Popup;

public partial class PersonalRecordAddPopup
{
    public PersonalRecordAddPopup()
	{
		InitializeComponent();

        time_display.IsVisible = false;
        record_date.MaximumDate = DateTime.Now;
    }

    /* creates an exercise along with the PR */
    private async void Submit_PR(object sender, EventArgs e)
    {
        string name = exercise_name_entry.Text;

        if (name != null) /* if name field is not empty */
        {
            name = name.Trim(); /* removes leading and trailing whitespace */

            DateTime date = record_date.Date;

            if (exercise_type_toggle.IsToggled) /* if time pr */
            {
                string hr_update_string = hr_pr.Text;
                string min_update_string = min_pr.Text;
                string sec_update_string = sec_pr.Text;

                if ((hr_update_string != null && hr_update_string.Length != 0) || (min_update_string != null && min_update_string.Length != 0) 
                        || (sec_update_string != null && sec_update_string.Length != 0)) /* if at least one time field is not empty */
                {
                    int hr_update;
                    int min_update;
                    int sec_update;

                    if (hr_update_string != null && hr_update_string.Length != 0) /* if hour field is not empty */
                    {
                        hr_update_string = hr_update_string.ToString();
                        hr_update = int.Parse(hr_update_string);
                    }
                    else /* else; hr field is empty */
                    {
                        hr_update = 0;
                    }

                    if (min_update_string != null && min_update_string.Length != 0) /* if min field is not empty */
                    {
                        min_update_string = min_update_string.ToString();
                        min_update = int.Parse(min_update_string);
                    }
                    else /* else; min field is empty */
                    {
                        min_update = 0;
                    }

                    if (sec_update_string != null && sec_update_string.Length != 0) /* if sec field is not empty */
                    {
                        sec_update_string = sec_update_string.ToString();
                        sec_update = int.Parse(sec_update_string);
                    }
                    else /* else; sec field is empty */
                    {
                        sec_update = 0;
                    }

                    await App.RecordRepo.Add_PR(name, date, false, -1, hr_update, min_update, sec_update);

                    error_prompt.IsVisible = false;
                    Close();
                } 
                else /* else; time field is empty */
                {
                    error_prompt.Text = "At least one time field must have a value";
                    error_prompt.IsVisible = true;
                }
            }
            else /* else weight pr */
            {
                string weight_update_string = weight_pr.Text;

                if (weight_update_string != null && weight_update_string.Length != 0) /* if weight field is not empty */
                {
                    weight_update_string = weight_update_string.ToString();

                    int weight_update = int.Parse(weight_update_string);
                    await App.RecordRepo.Add_PR(name, date, true, weight_update, -1, -1, -1);

                    error_prompt.IsVisible = false;
                    Close();
                }
                else /* if weight field is empty */
                {
                    error_prompt.Text = "PR weight value cannot be empty";
                    error_prompt.IsVisible = true;
                }
            }
        }
        else /* else; name field is empty */
        {
            error_prompt.Text = "PR exercise name cannot be empty";
            error_prompt.IsVisible = true;
        }
    }

    /* closes popup for creating an exercise */
    private void Cancel_PR(object sender, EventArgs e)
    {
        Close();
    }

    /* handles event when toggled between weight and time PR type */
    private void exercise_type_change(object sender, EventArgs e)
    {
        /* if changed to toggle on weight PR */
        if (time_display.IsVisible)
        {
            weight_display.IsVisible = true;
            time_display.IsVisible = false;
        }
        else /* else changed to toggle on time PR */
        {
            weight_display.IsVisible = false;
            time_display.IsVisible = true;
        }
    }
}
