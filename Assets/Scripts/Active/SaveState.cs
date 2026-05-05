using System.IO;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class SaveState : MonoBehaviour
{
    private string filePath; // Path to save/load the game data
    public GameData data; // This will hold the data we want to save/load
    public player Player; // Reference to the player script to sync data
    public SceneReloader screl;

    void Awake()
    {
        // Set the file path for saving/loading
        filePath = Path.Combine(Application.persistentDataPath, "savegame.xml");
        Debug.Log(Application.persistentDataPath); // Log the persistent data path for debugging
        Debug.Log(filePath); // Log the full file path for debugging
        screl = GetComponent<SceneReloader>();
    }

    void Start()
    {
        
        // Find the player
        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
        {
            Player = playerObj.GetComponent<player>();
        }

        // Load the data from the file
        LoadGame();

        // APPLY the loaded data to the actual player script
        ApplyDataToPlayer();
    }

    public void NewGame()
    {
        // Reset data to default values for a new game
        Time.timeScale = 1f;
        data.souls = 0;
        data.exp = 0;
        data.ultimate = 0;
        data.health = 5;
        foreach (Ability a in Player.abilities)
        {
            a.ResetAbility();
        }
        // Create an XML serializer and write the data to the file
        XmlSerializer serializer = new XmlSerializer(typeof(GameData));
        using (FileStream stream = new FileStream(filePath, FileMode.Create))
        {
            serializer.Serialize(stream, data);
        }
        LoadGame(); // Load the new game data to reset the player stats
        Debug.Log("New Game Started!");
        screl.RestartScene();
    }

    public void SaveGame()
    {
        // Sync current player stats to our data container
        data.souls = Player.souls;
        data.exp = Player.exp;
        data.ultimate = Player.ultimate;
        data.health = Player.health;
        //Create an XML serializer and write the data to the file
        XmlSerializer serializer = new XmlSerializer(typeof(GameData));
        using (FileStream stream = new FileStream(filePath, FileMode.Create))
        {
            serializer.Serialize(stream, data);
        }
        Debug.Log("Game Saved!");
    }

    public void LoadGame()
    {
        Time.timeScale = 1f;
        try
        {
        // Check if the file is there, if so read it and apply it to the player stats, if not create a new data object with default values
        if (File.Exists(filePath))
        {
            XmlSerializer serializer = new XmlSerializer(typeof(GameData));
            using (FileStream stream = new FileStream(filePath, FileMode.Open))
            {
                data = (GameData)serializer.Deserialize(stream);
            }
            ApplyDataToPlayer(); // Apply loaded data to player stats
            Debug.Log("Game Loaded!");
        }
        else
        {
            data = new GameData(); // Default values if no file exists
        }
        }
        catch (System.Exception e)
        {
            Debug.LogError("Load System Failed" + e.Message);
        }
    }

    // Push data from the 'File' to the 'player'
    void ApplyDataToPlayer()
    {
        if (Player != null && data != null)
        {
            Player.souls = data.souls;
            Player.exp = data.exp;
            Player.ultimate = data.ultimate;
            Player.health = data.health;
        }
    }
}