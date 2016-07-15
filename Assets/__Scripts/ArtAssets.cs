using UnityEngine;
using UnityEngine.UI;
using SVGImporter;
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
    public GameObject worldContainer;
    public GameObject gameTitle;
    public GameObject titleBackground;

	//challenge images
    public List<GameObject> challengeImages;

    void Awake() {
        S = this;

        //title screen
        gameTitle = GameObject.Find("GameTitle");
        titleBackground = GameObject.Find("TitleBackground");

        //tiles
        worldContainer = GameObject.Find("_World");
        worldContainer.SetActive(false);

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
