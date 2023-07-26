namespace WorkoutLog.Popup;

public partial class NotesAddPopup
{
	private int max_char_value { get; set; }
	private int current_char_value { get; set; }

	public NotesAddPopup()
	{
		InitializeComponent();
		       
		current_char_value = 0;
        max_char_value = 300;

        current_char_value_display.Text = current_char_value.ToString();
        max_char_value_display.Text = max_char_value.ToString();
    }

	/* refreshes the current char value of the note text box */
	private void Refresh_Word_Count(object sender, EventArgs e)
	{
		current_char_value = text_box_display.Text.Length;
        current_char_value_display.Text = current_char_value.ToString();
    }

    /* saves the note into the database */
    private async void Submit_Record(object sender, EventArgs e)
    {
        string name = note_title_content.Text;
        
        if (name != null && name.Length != 0) /* if name field is not empty */
        {
            name = name.Trim();

            if (name != null && name.Length != 0) /* if name field is not empty after trim */
            {
                string note_content = text_box_display.Text;

                if (note_content == null || note_content.Length == 0) /* if no note content */
                {
                    note_content = "";
                }

                await App.RecordRepo.Add_Note(name, note_content);

                error_prompt.IsVisible = false;
                Close();
            }
            else /* else; name field is empty after trim */
            {
                error_prompt.Text = "Note title cannot be empty";
                error_prompt.IsVisible = true;
            }
        }
        else /* else; name field is empty */
        {
            error_prompt.Text = "Note title cannot be empty";
            error_prompt.IsVisible = true;
        }
    }

    /* closes the note popup */
    private void Cancel_Record(object sender, EventArgs e)
    {
        Close();
    }
}