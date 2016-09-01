using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

public class Menus : MonoBehaviour {
    public static Menus S;

    //title screen UI
    public GameObject title;
    public GameObject titleCanvas;
    public CanvasGroup titleGroup;
    public Button playButton;
    public Button aboutButton;

    void Awake() {
        S = this;

        //initalize menus
        ConfigureTitleMenu();
    }

    public void ConfigureTitleMenu() {
        titleCanvas = GameObject.Find("_TitleCanvas");
        titleGroup = titleCanvas.GetComponent<CanvasGroup>();
        playButton = GameObject.Find("PlayButton").GetComponent<Button>();
        aboutButton = GameObject.Find("AboutButton").GetComponent<Button>();

        //clear button events
        playButton.onClick.RemoveAllListeners();
        aboutButton.onClick.RemoveAllListeners();

        //initalize button events
        playButton.onClick.AddListener(TitleTransition);
        aboutButton.onClick.AddListener(delegate {
            AboutScr.S.canvas.SetActive(true);
            ArtAssets.S.background.SetActive(true);
            title.SetActive(false);
        });

    }

    public void TitleTransition() {
        //transition title animation
        AnimationControl.S.planeController.SetBool("PlayPressed", true);
        titleCanvas.SetActive(false);
        Invoke("TransitionToPlay", 1.3f);
    }

    public void TransitionToPlay() {     

        //transition and kill title
        ArtAssets.S.gameTitle.SetActive(false);
        ArtAssets.S.titleBackground.SetActive(false);
        titleGroup.interactable = false;

        //load intro
        Intro.S.introCanvas.SetActive(true);
        Intro.S.TransitionIntro();
        StartCoroutine(Intro.S.TransitionIntroCanvas(1));
    }
  
}
