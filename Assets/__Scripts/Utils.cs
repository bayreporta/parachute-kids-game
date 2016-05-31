using UnityEngine;
using System.Collections;
using System.IO;
using LitJson;

public class Utils : MonoBehaviour {

    /* CLASS VARIABLES
    ---------------------------------------------------------------*/
    //dynamic
    public static JsonData data;

    //private
    private static string jsonString;

    /* FUNCTIONS
    ---------------------------------------------------------------*/

    // Use this for initialization
    public static JsonData ConvertJson(string loc) {
        jsonString = File.ReadAllText(Application.dataPath + loc);
        data = JsonMapper.ToObject(jsonString);
        return data;
    }

}
