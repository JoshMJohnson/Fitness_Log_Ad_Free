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