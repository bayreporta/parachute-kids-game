using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

public class Tutorial : MonoBehaviour {
    public static Tutorial S;
    public GameObject tutorialCanvas;
    public Button returnButton;


    void Awake() {
        S = this;

        //configure button
        returnButton.onClick.RemoveAllListeners();
        returnButton.onClick.AddListener(delegate { StartCoroutine(TransitionToTutorial(0)); });
    }

    public IEnumerator TransitionToTutorial(int i) {
        CanvasGroup tutorialGroup = tutorialCanvas.GetComponent<CanvasGroup>();
        switch (i) {
            case 0:
                while (tutorialGroup.alpha > 0) {
                    tutorialGroup.alpha -= Time.deltaTime / 1;
                    yield return null;
                }
                tutorialGroup.interactable = false;
                tutorialCanvas.SetActive(false);
                break;
            case 1:
                tutorialCanvas.SetActive(true);
                while (tutorialGroup.alpha < 1) {
                    tutorialGroup.alpha += Time.deltaTime / 1;
                    yield return null;
                }
                tutorialGroup.alpha = 1;
                tutorialGroup.interactable = true;
                break;
        }

    }

}
