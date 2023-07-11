using WorkoutLog.Model;

namespace WorkoutLog.Popup;

public partial class UpdatePR
{
    private PR updating_pr;

	public UpdatePR(string pr_name)
	{
		InitializeComponent();
        Get_PR_Data(pr_name);
    }

    /* creates an exercise along with the PR */
    private async void Submit_PR_Update(object sender, EventArgs e)
    {
        string name = updating_pr.exercise_name;
        DateTime date = record_date.Date;

        if (updating_pr.is_weight_pr == false) /* if time pr */
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

                await App.RecordRepo.Update_PR(name, date, -1, hr_update, min_update, sec_update);

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
                await App.RecordRepo.Update_PR(name, date, weight_update, -1, -1, -1);

                error_prompt.IsVisible = false;
                Close();
            }
            else /* if weight field is empty */
            {
                error_prompt.Text = "Weight value cannot be empty";
                error_prompt.IsVisible = true;
            }
        }
    }

    /* closes popup for creating an exercise */
    private void Cancel_PR_Update(object sender, EventArgs e)
    {
        Close();
    }

    /* gets pr data from database */
    private async void Get_PR_Data(string pr_name)
    {
        update_pr_exercise_name.Text = pr_name;
        updating_pr = await App.RecordRepo.Get_PR(pr_name);

        if (updating_pr.is_weight_pr) /* if updating pr is a weight pr */
        {
            time_display.IsVisible = false;
            weight_display.IsVisible = true;
        }
        else /* else updating pr is a time pr */
        {
            time_display.IsVisible = true;
            weight_display.IsVisible = false;
        }
    }
}