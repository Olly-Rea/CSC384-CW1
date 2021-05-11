using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveLoadController {

    // Method to save the game
    public static void SaveGame(int saveSlot, SaveData data) {
        // Initialise an instance of the BindaryFormatter to serialize the SaveData class
        BinaryFormatter formatter = new BinaryFormatter();
        string path = GetPath(saveSlot);
        // Create the filestream to write a new file
        FileStream stream = new FileStream(path, FileMode.Create);
        // Write the data to the file
        formatter.Serialize(stream, data);
        // Close the file stream
        stream.Close();
    }

    // Method to load 
    public static SaveData LoadGame(int saveSlot) {
        // Get the path of the file
        string path = GetPath(saveSlot);
        // Double-Check the file exists (checked already in SaveLoadSlot too)
        if (File.Exists(path)) {
            // Initialise an instance of the BindaryFormatter to deserialize data from the file
            BinaryFormatter formatter = new BinaryFormatter();
            // Open the filestream and read in the data from the file using the formatter 
            FileStream stream = new FileStream(path, FileMode.Open);
            SaveData data = formatter.Deserialize(stream) as SaveData;
            // Close the FileStream
            stream.Close();
            // Return the data from the filestream
            return data;
        } else {
            // Output error and return null
            Debug.LogError("File not found in" + path);
            return null;
        }
    }

    // Method to construct the Save/Load file path
    public static string GetPath(int saveSlot) {
        return Application.persistentDataPath + Path.AltDirectorySeparatorChar + "game_"+saveSlot+".astrd";
    }

    // Method to check how many save games exist
    public static int SaveGamesCount() {
        // Get the path of the persitent data directory
        string path = Application.persistentDataPath;
        // Count the number of existing files
        int count = Directory.GetFiles(path).Length;
        // Return the number of files present
        return count;
    }

    // Method to get the date a save game file was created
    public static string getSaveCreatedDate(int saveSlot) {
        // Get the path of the file
        string path = GetPath(saveSlot);
        // Double-Check the file exists (checked already in SaveLoadSlot too)
        if (File.Exists(path)) {
            return File.GetCreationTime(path).ToString();
        } else {
            // Output error and return null
            Debug.LogError("File not found in" + path);
            return null;
        }
    }

}
