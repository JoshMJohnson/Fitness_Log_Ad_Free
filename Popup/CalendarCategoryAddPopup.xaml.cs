using Android.OS;
using Microsoft.Maui.Controls.Shapes;
using WorkoutLog.Model;

namespace WorkoutLog.Popup;

public partial class CalendarCategoryAddPopup
{
    /* color options */
    private Color color0;
    private Color color1;
    private Color color2;
    private Color color3;
    private Color color4;
    private Color color5;
    private Color color6;
    private Color color7;
    private Color color8;
    private Color color9;

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

    /* submits category creation */
    private async void Create_Category(object sender, EventArgs e)
    {
        int max_categories = 7; /* max number of categories allowed for calendar */

        List<Category> category_list = await App.RecordRepo.Get_Calendar_Category_List();

        /* only allow a max of max_categories to be chosen at a time */
        if (category_list.Count < max_categories) /* if category limit not reached with creation; create category */
        {
            string name = category_name.Text;
            int category_index = category_color_picker.SelectedIndex;

            await App.RecordRepo.Add_Calendar_Category(name, category_index);
        }
        else /* todo prevent the creation of category; limit reached */
        {
            
        }

        Close();
    }

    /* returns a list of available colors for a new category */
    public List<Color> Generate_Category_Colors()
    {
        List<Color> colors = new List<Color>();

        /* generate 10 color options and add to list for selection */
        color0 = Color.FromRgb(255,150,0);
        color1 = Color.FromRgb(255,0,0);
        color2 = Color.FromRgb(0,255,0);
        color3 = Color.FromRgb(0,0,255);
        color4 = Color.FromRgb(255,0,255);
        color5 = Color.FromRgb(0,255,255);
        color6 = Color.FromRgb(255,255,0);
        color7 = Color.FromRgb(255,255,255);
        color8 = Color.FromRgb(50,255,50);
        color9 = Color.FromRgb(0,150,255);

        colors.Add(color0);
        colors.Add(color1);
        colors.Add(color2);
        colors.Add(color3);
        colors.Add(color4);
        colors.Add(color5);
        colors.Add(color6);
        colors.Add(color7);
        colors.Add(color8);
        colors.Add(color9);

        category_color_picker.ItemsSource = colors;

        return colors;
    }
}