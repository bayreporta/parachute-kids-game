using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

public class Menus : MonoBehaviour {
    public static Menus S;

    //title screen UI
    public GameObject titleCanvas;
    public CanvasGroup titleGroup;
    public Button playButton;
    public Button aboutButton;
    public Button creditsButton;

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
        creditsButton = GameObject.Find("CreditsButton").GetComponent<Button>();

        //clear button events
        playButton.onClick.RemoveAllListeners();
        aboutButton.onClick.RemoveAllListeners();
        creditsButton.onClick.RemoveAllListeners();

        //initalize button events
        playButton.onClick.AddListener(TitleTransition);

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
        StartCoroutine(Intro.S.TransitionIntroCanvas(1));
    }

    /* public IEnumerator TransitionToAbout() {
         while (titleGroup.alpha > 0) {
             titleGroup.alpha -= Time.deltaTime * 2;
             yield return null;
         }
     }

     public IEnumerator TransitionToCredits() {
         while (titleGroup.alpha > 0) {
             titleGroup.alpha -= Time.deltaTime * 2;
             yield return null;
         }
     }*/
}
