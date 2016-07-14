using UnityEngine;
using System.Collections;

public class AnimationControl : MonoBehaviour {

    public static AnimationControl S;

    //title animations
    public Animator planeController;
    public AnimationClip planeHover;
    public AnimationClip planeTitleOff;

    void Awake() {
        S = this;
        ConfigureAnimations();
    }

    public void ConfigureAnimations() {
        //title screen
        planeController = GameObject.Find("Plane").GetComponent<Animator>();
    }


}
