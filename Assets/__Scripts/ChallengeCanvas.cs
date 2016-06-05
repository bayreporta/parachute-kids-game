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
    public GameObject challengeOptionOne;
    public Transform challengeOptionOneText;
    public GameObject challengeOptionTwo;
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

        challengeOptionOne = GameObject.Find("OptionOne");
        challengeOptionOneText = challengeOptionOne.transform.GetChild(0);

        challengeOptionTwo = GameObject.Find("OptionTwo");
        challengeOptionTwoText = challengeOptionTwo.transform.GetChild(0);

        challengeCanvas.SetActive(false);
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
        optionOne.text = chal.OptionOneText;
        optionTwo.text = chal.OptionTwoText;

        chal.clickedFlag = true;
        challengeCanvas.SetActive(true);
    }


}
