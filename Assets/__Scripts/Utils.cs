using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LitJson;

public class Utils : MonoBehaviour {

    /* CLASS VARIABLES
    ---------------------------------------------------------------*/
    public static Utils S;

    //dynamic
    public JsonData data;
    public string filePath;
    public string result = "";
    public string jsonString;

    /* FUNCTIONS
    ---------------------------------------------------------------*/

    void Awake() {
        S = this;
    }

    // Use this to read JSON
    public JsonData ConvertJson(TextAsset json) {
        data = new JsonData();
        data = JsonMapper.ToObject(json.text);
        return data;
    }

   


}
