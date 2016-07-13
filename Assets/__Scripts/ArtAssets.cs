using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class ArtAssets : MonoBehaviour {
	public static ArtAssets S;

    //screen dimensions
    Vector3 bottomLeft;
    Vector3 bottomRight;
    Vector3 topLeft;
    Vector3 topRight;

    //tile vars
    public GameObject tileContainer;   

	//challenge images
	public List<Texture> challengeImages;

    void Awake() {
        S = this;

        //tiles
        tileContainer = GameObject.Find("_Tiles");
        tileContainer.SetActive(false);

        //grab screen dims
        CalculateScreenDimensions();
    }

    public void CalculateScreenDimensions() {
        var cam = Camera.main;
        bottomLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
        bottomRight = cam.ViewportToWorldPoint(new Vector3(1, 0, cam.nearClipPlane));
        topRight = cam.ViewportToWorldPoint(new Vector3(1, 1, cam.nearClipPlane));       
        topLeft = cam.ViewportToWorldPoint(new Vector3(0, 1, cam.nearClipPlane));
    }
}
