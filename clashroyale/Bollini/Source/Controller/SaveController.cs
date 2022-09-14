using Panni.Source.Model.Users;

namespace Bollini.Source.Controller
{
/**
 * Class used to save and load User and Deck.
 */
public sealed class SaveController  
{
    private static readonly Gson _GSON = new GsonBuilder().create();
    private static readonly string _USER_DIR_PATH =  System.Environment.SetEnvironmentVariable("user.home") + Path.DirectorySeparatorChar + "royaleData" + Path.DirectorySeparatorChar;
    private const string _FILE_NAME = "user.json";

    private SaveController() 
    {
    }

    private static bool CheckDirectoryExistance()
    {
        return Directory.Exists(_USER_DIR_PATH) || File.Exists(_USER_DIR_PATH);
    }

    private static void CreateDirectory()
    {
        Directory.CreateDirectory(_USER_DIR_PATH);
    }

    private static bool CheckFileExistance()
    {
        return Directory.Exists(_USER_DIR_PATH + Path.DirectorySeparatorChar + _FILE_NAME) || File.Exists(_USER_DIR_PATH + Path.DirectorySeparatorChar + _FILE_NAME);
    }
    
    private static void CreateFile() 
    {
        try {
            (new File(_USER_DIR_PATH + Path.DirectorySeparatorChar + _FILE_NAME)).CreateNewFile();
        } 
        catch (IOException e)
        {
            Console.WriteLine(e.ToString());
            Console.Write(e.StackTrace);
        }
    }

    /// <summary>
    /// Load the user from the Json file.
    /// </summary>
    /// <returns> a <seealso cref="User"/> loaded from file. </returns>

    public static User LoadUser() {
        if (SaveController.CheckDirectoryExistance() && SaveController.CheckFileExistance()) 
        {
            try {
                return _GSON.fromJson(new StreamReader(_USER_DIR_PATH + Path.DirectorySeparatorChar + _FILE_NAME), typeof(User));
            }
            catch (IOException e) 
            {
                Console.WriteLine(e.ToString());
                Console.Write(e.StackTrace);
            }
        }
        else 
        {
            SaveController.CreateDirectory();
            SaveController.CreateFile();
            return new User("Dream");
        }
        return null;
    }

    /// <summary>
    /// Serialize a user in a json file.
    /// </summary>
    /// <param name="user"> the <seealso cref="User"/> to be saved. </param>

    public static void SaveUser(in User user) 
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(_USER_DIR_PATH + Path.DirectorySeparatorChar + _FILE_NAME))
            { 
               _GSON.toJson(user, writer);
            }
        } 
        catch (IOException e)
        {
            Console.WriteLine(e.ToString());
            Console.Write(e.StackTrace);
        }
    }
}
}