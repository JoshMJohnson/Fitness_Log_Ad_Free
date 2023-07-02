using Android.OS;
using SQLite;
using System.Net;
using System.Xml.Linq;
using WorkoutLog.Model;
using static SQLite.SQLite3;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WorkoutLog;

public class RecordRepository
{
	private static SQLiteAsyncConnection conn;
    private string database_path;
    public string status_message { get; set; }

	/* constructor - creates a database */
	public RecordRepository(string db_path)
	{
        database_path = db_path;
	}

    /* initializes database */
    private async Task Init_Database()
    {
        if (conn != null) { return; }

        conn = new SQLiteAsyncConnection(database_path); /* create database */

        /* create database tables */

        await conn.CreateTablesAsync<Category, PR, GoalPR, GoalBW>();

        /*
         await conn.CreateTableAsync<BodyWeight>();
         await conn.CreateTableAsync<Calendar>();
         await conn.CreateTableAsync<Progression>();*/
    }

    /* * body progression section */
    /* todo adds an entry to the body progression table within the database */
    public async Task Add_Progression()
	{

	}

    /* todo removes an entry in the body progression table within the database */
    public async Task Remove_Progression()
    {

    }

    /* ? returns a list of progressions from the database */
    public async Task<List<Progression>> Get_Progression_List()
    {
        try
        {
            await Init_Database();
            List<Progression> body_progression_list = await conn.Table<Progression>().ToListAsync();
            return body_progression_list;
        }
        catch (Exception e)
        {
            status_message = string.Format("Failed to retrieve data. {0}", e.Message);
        }

        return new List<Progression>();
    }

    /* * body weight section */
    /* todo adds an entry to the body weight table within the database */
    public async Task Add_Body_Weight()
    {

    }

    /* ? edit an entry in the body weight table within the database */
    public async Task Edit_Body_Weight(DateTime date, int updated_weight)
    {
        ArgumentNullException.ThrowIfNull(date, nameof(date));
        ArgumentNullException.ThrowIfNull(updated_weight, nameof(updated_weight));

        try
        {
            await Init_Database();

            BodyWeightEntry updating_body_weight_entry = await conn.FindAsync<BodyWeightEntry>(date);
            updating_body_weight_entry.weight = updated_weight;

            await conn.UpdateAsync(updating_body_weight_entry);
        }
        catch (Exception e)
        {
            status_message = string.Format("Failed to update. Error: {0}", e.Message);
        }
    }

    /* todo removes an entry in the body weight table within the database */
    public async Task Remove_Body_Weight()
    {

    }

    /* ? returns a list of body weight entries from the database */
    public async Task<List<BodyWeight>> Get_Body_Weight_List()
    {
        try
        {
            await Init_Database();
            List<BodyWeight> body_weight_list = await conn.Table<BodyWeight>().ToListAsync();
            return body_weight_list;
        }
        catch (Exception e)
        {
            status_message = string.Format("Failed to retrieve data. {0}", e.Message);
        }

        return new List<BodyWeight>();
    }

    /* * body weight goals section */
    /* adds a body weight entry to the goal table within the database */
    public async Task Add_Goal_Body_Weight(string goal_name, DateTime date, bool has_desired, int goal_weight)
    {
        ArgumentNullException.ThrowIfNull(goal_name, nameof(goal_name));
        ArgumentNullException.ThrowIfNull(date, nameof(date));
        ArgumentNullException.ThrowIfNull(has_desired, nameof(has_desired));
        ArgumentNullException.ThrowIfNull(goal_weight, nameof(goal_weight));

        try
        {
            await Init_Database();

            GoalBW new_goal = new GoalBW
            {
                name = goal_name,
                goal_achieve_by_date = date,
                date_desired = has_desired,
                weight = goal_weight,
            };

            int result = await conn.InsertAsync(new_goal);

            status_message = string.Format("{0} goal added (Goal date: {1})", result, date);
        }
        catch (Exception e)
        {
            status_message = string.Format("Failed to add goal date: {0}. Error: {1}", date, e.Message);
        }
    }

    /* todo updates a body weight entry in the goal table within the database */
    public async Task Update_Goal_Body_Weight()
    {

    }

    /* removes a body weight entry in the goal table within the database */
    public async Task Remove_Goal_Body_Weight(string goal_name)
    {
        ArgumentNullException.ThrowIfNull(goal_name, nameof(goal_name));

        try
        {
            await Init_Database();
            GoalBW removing_bw_goal = await conn.FindAsync<GoalBW>(goal_name);
            await conn.DeleteAsync(removing_bw_goal);
        }
        catch (Exception e)
        {
            status_message = string.Format("Failed to remove {0}. Error: {1}", goal_name, e.Message);
        }
    }

    /* returns a list of body weight goals from the database */
    public async Task<List<GoalBW>> Get_Body_Weight_Goal_List()
    {
        try
        {
            await Init_Database();
            List<GoalBW> body_weight_goal_list = await conn.Table<GoalBW>().ToListAsync();
            return body_weight_goal_list;
        }
        catch (Exception e)
        {
            status_message = string.Format("Failed to retrieve data. {0}", e.Message);
        }

        return new List<GoalBW>();
    }

    /* * pr goals section */
    /* adds a pr entry to the goal table within the database */
    public async Task Add_Goal_PR(string goal_name, DateTime date, bool has_desired, bool is_weight, 
                                    int goal_weight, int hours, int mins, int sec)
    {
        ArgumentNullException.ThrowIfNull(goal_name, nameof(goal_name));
        ArgumentNullException.ThrowIfNull(date, nameof(date));
        ArgumentNullException.ThrowIfNull(has_desired, nameof(has_desired));
        ArgumentNullException.ThrowIfNull(is_weight, nameof(is_weight));
        ArgumentNullException.ThrowIfNull(goal_weight, nameof(goal_weight));
        ArgumentNullException.ThrowIfNull(hours, nameof(hours));
        ArgumentNullException.ThrowIfNull(mins, nameof(mins));
        ArgumentNullException.ThrowIfNull(sec, nameof(sec));

        if (is_weight) /* if weight pr type */
        {
            hours = -1;
            mins = -1;
            sec = -1;
        }
        else /* if time pr type */
        {
            goal_weight = -1;
        }

        try
        {
            await Init_Database();

            GoalPR new_goal = new GoalPR
            {
                name = goal_name,
                goal_achieve_by_date = date,
                date_desired = has_desired,
                is_weight_goal = is_weight,
                weight = goal_weight,
                time_hours = hours,
                time_min = mins,
                time_sec = sec
            };

            int result = await conn.InsertAsync(new_goal);

            status_message = string.Format("{0} PR goal added (Goal date: {1})", result, date);
        }
        catch (Exception e)
        {
            status_message = string.Format("Failed to add PR goal date: {0}. Error: {1}", date, e.Message);
        }
    }

    /* todo updates a pr goal entry in the GoalPR table within the database */
    public async Task Update_Goal_PR()
    {

    }

    /* removes a pr goal entry in the GoalPR table within the database */
    public async Task Remove_Goal_PR(string goal_name)
    {
        ArgumentNullException.ThrowIfNull(goal_name, nameof(goal_name));

        try
        {
            await Init_Database();
            GoalPR removing_pr_goal = await conn.FindAsync<GoalPR>(goal_name);
            await conn.DeleteAsync(removing_pr_goal);
        }
        catch (Exception e)
        {
            status_message = string.Format("Failed to remove {0}. Error: {1}", goal_name, e.Message);
        }
    }

    /* returns a list of pr goals from the database */
    public async Task<List<GoalPR>> Get_Goal_PR_List()
    {
        try
        {
            await Init_Database();
            List<GoalPR> pr_goal_list = await conn.Table<GoalPR>().ToListAsync();
            return pr_goal_list;
        }
        catch (Exception e)
        {
            status_message = string.Format("Failed to retrieve data. {0}", e.Message);
        }

        return new List<GoalPR>();
    }

    /* * personal records section */
    /* adds a pr entry to the pr table within the database */
    public async Task Add_PR(string name, DateTime date, bool is_weight_pr_type,
                                int weight_pr, int hours, int min, int sec)
    {
        ArgumentNullException.ThrowIfNull(name, nameof(name));
        ArgumentNullException.ThrowIfNull(date, nameof(date));
        ArgumentNullException.ThrowIfNull(is_weight_pr_type, nameof(is_weight_pr_type));
        ArgumentNullException.ThrowIfNull(weight_pr, nameof(weight_pr));
        ArgumentNullException.ThrowIfNull(hours, nameof(hours));
        ArgumentNullException.ThrowIfNull(min, nameof(min));
        ArgumentNullException.ThrowIfNull(sec, nameof(sec));

        try
        {
            await Init_Database();

            PR new_pr = new PR
            {
                exercise_name = name,
                date_achieved = date,
                is_weight_pr = is_weight_pr_type,
                weight = weight_pr,
                time_hours = hours,
                time_min = min,
                time_sec = sec
            };

            int result = await conn.InsertAsync(new_pr);

            status_message = string.Format("{0} PR added (PR Name: {1})", result, name);
        }
        catch (Exception e)
        {
            status_message = string.Format("Failed to add {0}. Error: {1}", name, e.Message);
        }
    }

    /* ? updates a pr entry in the pr table within the database */
    public async Task Update_PR(string exercise_name, DateTime date, int updated_weight, int hours, int mins, int sec)
    {
        ArgumentNullException.ThrowIfNull(exercise_name, nameof(exercise_name));
        ArgumentNullException.ThrowIfNull(date, nameof(date));
        ArgumentNullException.ThrowIfNull(updated_weight, nameof(updated_weight));
        ArgumentNullException.ThrowIfNull(hours, nameof(hours));
        ArgumentNullException.ThrowIfNull(mins, nameof(mins));
        ArgumentNullException.ThrowIfNull(sec, nameof(sec));
        
        try
        {
            await Init_Database();
            PR updating_pr = await conn.FindAsync<PR>(exercise_name);

            updating_pr.date_achieved = date;

            if (updating_pr.is_weight_pr) /* if updating pr is a weight pr */
            {
                updating_pr.weight = updated_weight;
            }
            else /* else updating pr is a timed pr */
            {
                updating_pr.time_hours = hours;
                updating_pr.time_min = mins;
                updating_pr.time_sec = sec;
            }

            await conn.UpdateAsync(updating_pr);
        }
        catch (Exception e)
        {
            status_message = string.Format("Failed to update. Error: {0}", e.Message);
        }
    }

    /* removes a pr entry in the pr table within the database */
    public async Task Remove_PR(string exercise_name)
    {
        ArgumentNullException.ThrowIfNull(exercise_name, nameof(exercise_name));

        try
        {
            await Init_Database();
            PR removing_pr = await conn.FindAsync<PR>(exercise_name);
            await conn.DeleteAsync(removing_pr);
        }
        catch (Exception e)
        {
            status_message = string.Format("Failed to remove {0}. Error: {1}", exercise_name, e.Message);
        }
    }

    /* returns a list of PR's from the database */
    public async Task<List<PR>> Get_PR_List()
    {
        try
        {
            await Init_Database();
            List<PR> pr_list = await conn.Table<PR>().ToListAsync();
            return pr_list;
        }
        catch (Exception e)
        {
            status_message = string.Format("Failed to retrieve data. {0}", e.Message);
        }

        return new List<PR>();
    }

    /* * workout calendar section*/
    /* ? adds an entry to the workout calendar table within the database */
    public async Task Add_Calendar_Entry(DateTime date, Category category)
    {
        ArgumentNullException.ThrowIfNull(date, nameof(date));
        ArgumentNullException.ThrowIfNull(category, nameof(category));

        try
        {
            await Init_Database();

            Calendar calendar_entry = new Calendar
            {
                entry_date = date,
                calendar_category = category
            };

            int result = await conn.InsertAsync(calendar_entry);

            status_message = string.Format("{0} calendar entry made for {1})", result, date);
        }
        catch (Exception e)
        {
            status_message = string.Format("Failed to add calendar entry on {0}. Error: {1}", date, e.Message);
        }
    }

    /* todo removes an entry in the workout calendar table within the database */
    public async Task Remove_Calendar_Entry(DateTime date, Category category)
    {
        ArgumentNullException.ThrowIfNull(date, nameof(date));
        ArgumentNullException.ThrowIfNull(category, nameof(category));

        try
        {
            await Init_Database();





            int result = 1;

            status_message = string.Format("{0} calendar entry removed (Date: {1})", result, date);
        }
        catch (Exception e)
        {
            status_message = string.Format("Failed to remove calendar entry on {0}. Error: {1}", date, e.Message);
        }
    }

    /* ? returns a list of calendar entries from the database */
    public async Task<List<Calendar>> Get_Calendar_Entries_List()
    {
        try
        {
            await Init_Database();
            List<Calendar> calendar_entry_list = await conn.Table<Calendar>().ToListAsync();
            return calendar_entry_list;
        }
        catch (Exception e)
        {
            status_message = string.Format("Failed to retrieve data. {0}", e.Message);
        }

        return new List<Calendar>();
    }

    /* adds a category to the categories table within the database */
    public async Task Add_Calendar_Category(string category_name, int category_color_index)
    {
        ArgumentNullException.ThrowIfNull(category_name, nameof(category_name));
        ArgumentNullException.ThrowIfNull(category_color_index, nameof(category_color_index));

        try
        {
            await Init_Database();

            Category new_category = new Category
            {
                name = category_name,
                color = category_color_index
            };

            int result = await conn.InsertAsync(new_category);

            status_message = string.Format("{0} category added (Category Name: {1})", result, category_name);
        }
        catch (Exception e)
        {
            status_message = string.Format("Failed to add category {0}. Error: {1}", category_name, e.Message);
        }
    }
    
    /* removes a category in the categories table within the database */
    public async Task Remove_Calendar_Category(string category_name)
    {
        ArgumentNullException.ThrowIfNull(category_name, nameof(category_name));

        try
        {
            await Init_Database();

            Category removing_category = await conn.FindAsync<Category>(category_name);
            int result = await conn.DeleteAsync(removing_category);

            status_message = string.Format("{0} category removed (Category Name: {1})", result, category_name);
        }
        catch (Exception e)
        {
            status_message = string.Format("Failed to remove category {0}. Error: {1}", category_name, e.Message);
        }
    }

    /* returns a list of calendar categories from the database */
    public async Task<List<Category>> Get_Calendar_Category_List()
    {
        try
        {
            await Init_Database();
            List<Category> category_list = await conn.Table<Category>().ToListAsync();
            return category_list;
        }
        catch (Exception e)
        {
            status_message = string.Format("Failed to retrieve data. {0}", e.Message);
        }

        return new List<Category>();
    }
}