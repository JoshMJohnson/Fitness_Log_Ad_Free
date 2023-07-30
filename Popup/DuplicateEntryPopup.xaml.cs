using Android.Media.TV;

namespace WorkoutLog.Popup;

public partial class DuplicateEntryPopup
{
	public DuplicateEntryPopup(string table_name)
	{
		InitializeComponent();

		/* if duplicate entry is a body progression entry */
		if (table_name == "Body Progression")
		{
			Display_Body_Progression(table_name);
        }
		else if (table_name == "Body Weight") /* todo else if duplicate entry is a body weight entry */
        {

		}
        else if (table_name == "Body Weight Goal") /* todo else if duplicate entry is a body weight goal entry */
        {

        }
        else if (table_name == "PR Goal") /* todo else if duplicate entry is a pr goal entry */
        {

        }
        else if (table_name == "PR") /* todo else if duplicate entry is a PR entry */
        {

        }
        else if (table_name == "Category") /* todo else if duplicate entry is a calendar category entry */
        {

        }
        else /* todo else duplicate entry is a notes entry */
        {

        }
    }

	/* displays body progressin duplicate info */
	private void Display_Body_Progression(string table_name)
	{
        duplicate_table_display.Text = table_name;

        string prompt = $"The {table_name} entry is a duplicate.";
        string prompt2 = $"The {table_name} entry was not added as a duplicate.";

        duplicate_table_prompt_display.Text = prompt;
        duplicate_table_prompt2_display.Text = prompt2;
    }


	/* closes the popup alert for duplicate */
	private void Close_Popup(object sender, EventArgs e)
	{
		Close();
	}
}