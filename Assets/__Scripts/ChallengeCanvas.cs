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

    int challengeID;
    string[] optionsOne;
    string[] optionsTwo;
    private readonly ChallengeType JohnDoe_Act3_BusStop;



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
        bool hideOption;

        //grab vital data
        int challengeID = chal.challengeID;
        string[] optionsOne = chal.optionOneResults.Split(',');
        string[] optionsTwo = chal.optionTwoResults.Split(',');

        //turn on all buttons
        challengeOptionOne.gameObject.SetActive(true);
        challengeOptionTwo.gameObject.SetActive(true);


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
        challengeOptionOne.onClick.AddListener(delegate { Results.S.RetrieveResult(challengeID, optionsOne); });

        //check prereqs for challenge and options
        hideOption = Results.S.ResultPrereqCheck(chal.challengeID, 0);

        //check to see if second button is hidden
        if (chal.optionTwoText == "none" || hideOption == true) { challengeOptionTwo.gameObject.SetActive(false); }
        else {challengeOptionTwo.onClick.AddListener(delegate { Results.S.RetrieveResult(challengeID, optionsTwo); }); }

        challengeModalPanel.SetActive(true);
    }

    public void UpdateResultCanvas(ResultDefinition r) {
        //grab text
        Text title = challengeTitleText.GetComponent<Text>();
        Text flavor = challengeFlavorText.GetComponent<Text>();
        Text optionTwo = challengeOptionTwoText.GetComponent<Text>();

        //hide first button
        challengeOptionOne.gameObject.SetActive(false);
        challengeOptionTwo.gameObject.SetActive(true);

        //change text
        title.text = r.resultTitle;
        flavor.text = r.resultFlavor;
        optionTwo.text = "Continue.";

        challengeOptionTwo.onClick.RemoveAllListeners();
        challengeOptionTwo.onClick.AddListener(delegate { Resources.S.UpdateResources(r); });
        challengeOptionTwo.onClick.AddListener(CloseChallengeCanvas);
    }

    public void CloseChallengeCanvas() {
        //check if wellbeing is low enough for run away
        if (Player.S.currAct == 3) Challenges.S.BusStopCheck();

        Acts.S.challengesDoneForAct += 1;
        challengeModalPanel.SetActive(false);
        //Locations.S.blockLocationClick = false;
    }


}
