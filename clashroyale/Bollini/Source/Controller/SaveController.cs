﻿using Panni.Source.Model.Users;

namespace Bollini.Source.Controller
{
/**
 * Class used to save and load User and Deck.
 */
public sealed class SaveController  
{

    private static readonly Gson _GSON = new GsonBuilder().create();
    private static readonly String _USER_DIR_PATH = System.getProperty("user.home") + File.separator + "royaleData" + File.separator;
    private static readonly String _FILE_NAME = "user.json";

    private SaveController() 
    {
    }

    private static boolean CheckDirectoryExistance() 
    {
        return new File(USER_DIR_PATH).exists();
    }

    private static void CreateDirectory() 
    {
        new File(USER_DIR_PATH).mkdir();
    }

    private static boolean CheckFileExistance() 
    {
        return new File(USER_DIR_PATH + File.separator + FILE_NAME).exists();
    }

    private static void CreateFile() 
    {
        try {
            new File(USER_DIR_PATH + File.separator + FILE_NAME).createNewFile();
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    /**
   * Load the user from the Json file.
   * 
   * @return a {@link User} loaded from file.
   */
    public static User LoadUser() {
        if (SaveController.checkDirectoryExistance() && SaveController.checkFileExistance()) 
        {
            try {
                return GSON.fromJson(new FileReader(new File(USER_DIR_PATH + File.separator + FILE_NAME)), User.class);
            } catch (IOException e) {
                e.printStackTrace();
            }
        } else {
            SaveController.createDirectory();
            SaveController.createFile();
            return new User("Dream");
        }
        return null;
    }

    /**
   * Serialize a user in a json file.
   * 
   * @param user the {@link User} to be saved.
   */
    public static void SaveUser(readonly User user) 
    {
        try (FileWriter writer = new FileWriter(new File(USER_DIR_PATH + File.separator + FILE_NAME))) {
            GSON.toJson(user, writer);
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

}
}