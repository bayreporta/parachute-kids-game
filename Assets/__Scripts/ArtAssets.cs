using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class ArtAssets : MonoBehaviour {
	public static ArtAssets S;

	//challenge images
	public List<Texture> challengeImages;

	void Awake(){
		S = this;
	}

}
