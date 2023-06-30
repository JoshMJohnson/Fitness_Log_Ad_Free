using Microsoft.Maui.Controls.Shapes;
using WorkoutLog.Model;

namespace WorkoutLog.Popup;

public partial class CalendarCategoryAddPopup
{
    public CalendarCategoryAddPopup()
	{
		InitializeComponent();
        Generate_Category_Colors();

    }

    /* closes popup for creating a category */
    private void Cancel_Create_Category(object sender, EventArgs e)
    {
        Close();
    }

    /* todo submits category creation */
    private async void Create_Category(object sender, EventArgs e)
    {
        int max_categories = 6;

        List<Category> category_list = await App.RecordRepo.Get_Calendar_Category_List();

        /* only allow a max of 6 to be chosen at a time */
        if (category_list.Count < max_categories) /* if category limit not reached with creation; create category */
        {

        }
        else /* prevent the creation of category; limit reached */
        {
            
        }
    }

    /* todo displays a list of available colors for a new category */
    private void Generate_Category_Colors()
    {
        List<Rectangle> colors = new List<Rectangle>();

        /* generate 10 color options and add to list for selection */
        Color color0 = Color.FromRgb(255,150,0);
        Color color1 = Color.FromRgb(255,0,0);
        Color color2 = Color.FromRgb(0,255,0);
        Color color3 = Color.FromRgb(0,0,255);
        Color color4 = Color.FromRgb(255,0,255);
        Color color5 = Color.FromRgb(0,255,255);
        Color color6 = Color.FromRgb(255,255,0);
        Color color7 = Color.FromRgb(255,255,255);
        Color color8 = Color.FromRgb(50,255,50);
        Color color9 = Color.FromRgb(0,150,255);

       

        category_color_picker.ItemsSource = colors; 
    }
}