using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

public class ChallengeCanvas : MonoBehaviour {

    /* CLASS VARIABLES
    ---------------------------------------------------------------*/
    public static ChallengeCanvas S;

    //all Challenge Canvas elems
    public int canvasElements = 9;
    public GameObject challengeCanvas;
    public GameObject challengeModalPanel; 
    public GameObject challengeQuestionPanel; 
    public GameObject challengeAnswerPanel; 
    public GameObject challengeTitle;
    public Transform challengeTitleText;
    public GameObject challengeImage; 
    public GameObject challengeFlavor;
    public Transform challengeFlavorText;
    public Button challengeOptionOne;
    public Transform challengeOptionOneText;
    public Button challengeOptionTwo;
    public Transform challengeOptionTwoText;



    /* FUNCTIONS
    ---------------------------------------------------------------*/
    void Awake() {
        S = this;

        FindChallengeCanvasElems();        
    }


   void FindChallengeCanvasElems() {
        //grab Challenge Canvas GO and deactivate it. Also grab children
        challengeCanvas = GameObject.Find("_ChallengeCanvas");
        challengeModalPanel = GameObject.Find("ModalPanel");
        challengeQuestionPanel = GameObject.Find("QuestionPanel");
        challengeAnswerPanel = GameObject.Find("AnswerPanel");

        challengeTitle = GameObject.Find("ChallengeTitle");
        challengeTitleText = challengeTitle.transform.GetChild(0);

        challengeImage = GameObject.Find("ChallengeImage");

        challengeFlavor = GameObject.Find("ChallengeFlavor");
        challengeFlavorText = challengeFlavor.transform.GetChild(0);

        challengeOptionOne = GameObject.Find("OptionOne").GetComponent<Button>();
        challengeOptionOneText = challengeOptionOne.transform.GetChild(0);

        challengeOptionTwo = GameObject.Find("OptionTwo").GetComponent<Button>();
        challengeOptionTwoText = challengeOptionTwo.transform.GetChild(0);

        challengeModalPanel.SetActive(false);
    }

    public void UpdateChallengeCanvas(ChallengeDefinition chal) {
        //grab text
        Text title = challengeTitleText.GetComponent<Text>();
        Text flavor = challengeFlavorText.GetComponent<Text>();
        Text optionOne = challengeOptionOneText.GetComponent<Text>();
        Text optionTwo = challengeOptionTwoText.GetComponent<Text>();

        //change text
        title.text = chal.title;
        flavor.text = chal.flavorText;
        optionOne.text = chal.optionOneText;
        optionTwo.text = chal.optionTwoText;

        //add event listeners
        challengeOptionOne.onClick.RemoveAllListeners();
        challengeOptionTwo.onClick.RemoveAllListeners();

        challengeOptionOne.onClick.AddListener(CloseChallengeCanvas);
        challengeOptionTwo.onClick.AddListener(CloseChallengeCanvas);

        chal.clickedFlag = true;
        //Locations.S.blockLocationClick = true;
        challengeModalPanel.SetActive(true);
    }

    public void CloseChallengeCanvas() {
        challengeModalPanel.SetActive(false);
        //Locations.S.blockLocationClick = false;
    }


}
