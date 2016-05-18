using UnityEngine;
using System.Collections;
using System.IO;
using LitJson;

public class ReadJson : MonoBehaviour {
    private static string jsonString;
    public static JsonData data;

    // Use this for initialization
    public static JsonData Process_Data (string loc) {
        jsonString = File.ReadAllText(Application.dataPath + loc);
        data = JsonMapper.ToObject(jsonString); 
        return data;
	}
}
