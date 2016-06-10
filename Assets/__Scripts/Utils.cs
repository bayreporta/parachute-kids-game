using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

    // Use this to read JSON
    public static JsonData ConvertJson(string loc) {
        jsonString = File.ReadAllText(Application.dataPath + loc);
        data = JsonMapper.ToObject(jsonString);
        return data;
    }
 
}
