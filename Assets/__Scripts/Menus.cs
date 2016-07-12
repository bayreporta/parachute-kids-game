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
        titleCanvas = GameObject.Find("TitleCanvas");
        titleGroup = titleCanvas.GetComponent<CanvasGroup>();
        playButton = GameObject.Find("PlayButton").GetComponent<Button>();
        aboutButton = GameObject.Find("AboutButton").GetComponent<Button>();
        creditsButton = GameObject.Find("CreditsButton").GetComponent<Button>();

        //clear button events
        playButton.onClick.RemoveAllListeners();
        aboutButton.onClick.RemoveAllListeners();
        creditsButton.onClick.RemoveAllListeners();

        //initalize button events
        playButton.onClick.AddListener(delegate { StartCoroutine(TransitionToPlay()); });
        
    }

    public IEnumerator TransitionToPlay() {
        while (titleGroup.alpha > 0) {
            titleGroup.alpha -= Time.deltaTime * 2;
            yield return null;
        }
        titleGroup.interactable = false;
        titleCanvas.SetActive(false);
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
