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
    public CanvasGroup challengeGroup; 
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
    }


   public void FindChallengeCanvasElems() {
        //grab Challenge Canvas GO and deactivate it. Also grab children
        challengeCanvas = GameObject.Find("_ChallengeCanvas");
        challengeGroup = challengeCanvas.GetComponent<CanvasGroup>();
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

        challengeCanvas.SetActive(false);
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
        if (chal.challengeID != 15) challengeOptionOne.onClick.AddListener(delegate { Results.S.RetrieveResult(challengeID, optionsOne); });

        //check prereqs for challenge and options
        hideOption = Results.S.ResultPrereqCheck(chal.challengeID, 0);

        //check to see if second button is hidden
        if (chal.optionTwoText == "none" || hideOption == true) { challengeOptionTwo.gameObject.SetActive(false); }
        else {challengeOptionTwo.onClick.AddListener(delegate { Results.S.RetrieveResult(challengeID, optionsTwo); }); }

        //activate canvas
        chal.clickedFlag = true;
        challengeCanvas.SetActive(true);
        StartCoroutine(TransitionChallengeCanvas(1));

        //special SAT check
        if (chal.challengeID == 15) {
            EndGame.S.ProcessSATScores();
            challengeOptionOne.onClick.AddListener(delegate { EndGame.S.FinalResults(); });
        }
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
		challengeOptionTwo.onClick.AddListener(CloseChallengeCanvas);
        challengeOptionTwo.onClick.AddListener(delegate { Resources.S.UpdateResources(r); });
    }

    public void CloseChallengeCanvas() {
        //check if wellbeing is low enough for run away
        if (Player.S.currAct == 3) LocationControl.S.ActivateLocation();

        Acts.S.challengesDoneForAct += 1;

        //hide and disable canvas
        StartCoroutine(TransitionChallengeCanvas(0));

        //check if act is finished
        Acts.S.CheckActStatus();

    }

    public IEnumerator TransitionChallengeCanvas(int i) {
        switch (i) {
            case 0:
                while (challengeGroup.alpha > 0) {
                    challengeGroup.alpha -= Time.deltaTime * 2;
                    yield return null;
                }
                challengeGroup.interactable = false;
                challengeCanvas.SetActive(false);
                break;
            case 1:                
                while (challengeGroup.alpha < 1) {
                    challengeGroup.alpha += Time.deltaTime * 2;
                    yield return null;
                }
                challengeGroup.interactable = true;
                break;
        }
    }

}
