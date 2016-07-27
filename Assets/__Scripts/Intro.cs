using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class Intro : MonoBehaviour {

    public static Intro S;
    public GameObject introCanvas;
    public CanvasGroup introGroup;
    public List<GameObject> introImages;
    public Text introText;
    public Button introButton;
    public Text introButtonText;
    public Button introPlayButton;
    public Text introPlayButtonText;
    public int introSlide = 0;

    void Awake() {
        S = this;
        introGroup = introCanvas.GetComponent<CanvasGroup>();

        ConfigureIntro();
    }

    public void ConfigureIntro() {
        //configure canvas
        introGroup.alpha = 0;
        introGroup.interactable = false;
        introCanvas.SetActive(false);

        //configure buttons
        introButton.onClick.RemoveAllListeners();
        introPlayButton.onClick.RemoveAllListeners();

        introButton.onClick.AddListener(TransitionIntro);
        introPlayButton.onClick.AddListener(delegate { StartCoroutine(TransitionIntroCanvas(0)); });
    }

    public void TransitionIntro() {               

        for (int i = 0; i < introImages.Count; i++) { introImages[i].SetActive(false); }

        switch (introSlide) {            
            case 0:
                introText.text = "One night after dinner, your mother tells you that you're going to fly to Arcadia, California where you will learn English, meet new friends and live the dream of being an American. You’re confused. You're a 16-year-old high school student who has lived in China your entire life. The thought of being almost 7,000 miles away from home is exciting and daunting. You've been to California once before and you’ve learned English phrases (mainly from American TV shows), so you tell yourself this will just be an extended vacation.";
                introImages[introSlide].SetActive(true);

                //destroy Title
                GameObject title = GameObject.Find("_Title");
                Destroy(title, 2);
                break;
            case 1:
                introText.text = "A week later, you land in Southern California. You were able to enter the country with a Green Card with the sponsorship of your father, who is an American citizen. You settle into your cramped rental room where your mother says you'll stay until you graduate from public high school and move into a college dorm – that’s the goal, she says, to be accepted into a four-year university in the U.S.";
                introImages[introSlide].SetActive(true);              
                break;
            case 2:
                introText.text = "Your mother is only around for a few weeks, and since both your parents can’t afford not to continue working abroad, you’ll have to take care of yourself. You can't imagine what life will be like without her soon...but onwards you must go!";
                introButtonText.text = "Tutorial";
                introPlayButtonText.text = "Play Game!";
                introButton.onClick.RemoveListener(TransitionIntro);
                introButton.onClick.AddListener(delegate { StartCoroutine(Tutorial.S.TransitionToTutorial(1)); });
                introImages[introSlide].SetActive(true);
                break;
        }
        introSlide += 1;

    }

    public IEnumerator TransitionIntroCanvas(int i) {
        switch (i) {
            case 0:              
                while (introGroup.alpha > 0) {
                    introGroup.alpha -= Time.deltaTime * 2;
                    yield return null;
                }
                introGroup.interactable = false;
                introCanvas.SetActive(false);

                ParachuteKids.S.StartGame();
                break;
            case 1:
                //background
                ArtAssets.S.ControlBackground(1);

                while (introGroup.alpha < 1) {
                    introGroup.alpha += Time.deltaTime * 2;
                    yield return null;
                }            

                introGroup.interactable = true;
                break;
        }
    }

}
