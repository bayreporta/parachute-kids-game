using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AnimationControl : MonoBehaviour {

    public static AnimationControl S;

    //title animations
    public Animator planeController;
    public AnimationClip planeHover;
    public AnimationClip planeTitleOff;

    //world animations
    public Animator locationController;
    public List<Animator> locationAnimators;
    
    void Awake() {
        S = this;
        ConfigureAnimations();
    }

    public void ConfigureAnimations() {
        //title screen
        planeController = GameObject.Find("Plane").GetComponent<Animator>();

    }


}
