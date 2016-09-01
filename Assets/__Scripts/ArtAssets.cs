using UnityEngine;
using UnityEngine.UI;
using SVGImporter;
using System.Collections;
using System.Collections.Generic;


public class ArtAssets : MonoBehaviour {
	public static ArtAssets S;
    public GameObject background;

    //character assets
    public List<GameObject> maleCharacterImages;

    //tile vars
    public GameObject worldContainer;
    public GameObject gameTitle;
    public GameObject titleBackground;

	//challenge images
    public List<GameObject> challengeImages;

    public void ControlBackground(int i) {
        if (i == 0) {
            background.SetActive(false);
        }
        else if (i == 1) {
            background.SetActive(true);
        }
    }

    void Awake() {
        S = this;

        //title screen
        gameTitle = GameObject.Find("GameTitle");
        titleBackground = GameObject.Find("TitleBackground");

        //tiles
        worldContainer = GameObject.Find("_World");
        worldContainer.SetActive(false);

     
    }
}
