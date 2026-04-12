using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class AbilityLoader : MonoBehaviour
{
    
    void Start()
    {
        string path = Application.dataPath + "/abilities.json";
        string json = File.ReadAllText(path);

        Debug.Log(json);
    }
}
