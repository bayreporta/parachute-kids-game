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
    public JsonData ConvertJson(string loc) {
        string appPath = Application.streamingAssetsPath;
        //filePath = System.String.Concat("//games.bayreporta.com/hr-test/StreamingAssets/", loc);
        filePath = System.String.Concat(appPath,"/", loc);
        //result = System.IO.File.ReadAllText(filePath);
        StartCoroutine(Example());
        data = JsonMapper.ToObject(result);
        return data;
    }

    IEnumerator Example() {
        if (filePath.Contains("://")) {
            WWW www = new WWW(filePath);
            yield return www;
            result = www.text;
        } else
            result = System.IO.File.ReadAllText(filePath);
    }


}
