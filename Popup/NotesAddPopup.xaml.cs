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
}