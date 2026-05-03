using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class AbilityLoader : MonoBehaviour
{
    void Start()
{
    string path = System.IO.Path.Combine(Application.dataPath, "Abilities", "abilities.json");
    Debug.Log("Attempting to load: [" + path + "]");

    if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            Debug.Log("Success:" + json);
        }
    else
        {
            Debug.LogError("File not found at " + path);   
        }    
}
    
}
