using UnityEngine;
using System.Collections;
using LitJson;

public class ParachuteKids : MonoBehaviour {
    private JsonData data;

	// Use this for initialization
	void Start () {
        //data = ReadJson.Process_Data("/Resources/test.json");
        data = ProcessMath.Add_Nums(34, 23, 235, 7756, 112, -2323, -44);
        Debug.Log(data);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
