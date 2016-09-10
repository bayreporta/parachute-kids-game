using UnityEngine;
using UnityEngine.UI;
using SVGImporter;
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
    public GameObject challengeFlavor;
    public Transform challengeFlavorText;

    public Button challengeOptionOne;
    public Transform challengeOptionOneText;
    public Button challengeOptionTwo;
    public Transform challengeOptionTwoText;
    public Button challengeOptionThree;
    public Transform challengeOptionThreeText;

    //effect canvas
    public GameObject challengePopup;
    public Text effectOne;
    public Text effectTwo;
    public bool popup = false;

    
    private readonly ChallengeType JohnDoe_Act3_BusStop;

    //this is for the effects popup
    public int challengeID;
    public string[] optionsOne;
    public string[] optionsTwo;

    /* FUNCTIONS
    ---------------------------------------------------------------*/
    void Awake() {
        S = this;          
    }

    public void UpdateResultEffect(int option) {
        ChallengeDefinition chal = ParachuteKids.S.GetChallengeDefinition((ChallengeType)challengeID);
        if (popup == true) {
            popup = false;
            challengePopup.SetActive(false);
            effectTwo.gameObject.SetActive(true);
        }
        else if (popup == false) {
            challengePopup.SetActive(true);
            popup = true;
            switch (option) {
                case 0:
                    effectOne.text = "* " + chal.optionOnePopupA;

                    if (chal.optionOnePopupB != "none") {
                        effectTwo.text = "* " + chal.optionOnePopupB;
                    }
                    else { effectTwo.gameObject.SetActive(false); }
                    
                    break;
                case 1:
                    effectOne.text = "* " + chal.optionTwoPopupA;

                    if (chal.optionTwoPopupB != "none") {
                        effectTwo.text = "* " + chal.optionTwoPopupB;
                    } else { effectTwo.gameObject.SetActive(false); }                    
                    break;
            }
        }       
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

        challengeFlavor = GameObject.Find("ChallengeFlavor");
        challengeFlavorText = challengeFlavor.transform.GetChild(0);

        challengeOptionOne = GameObject.Find("OptionOne").GetComponent<Button>();
        challengeOptionOneText = challengeOptionOne.transform.GetChild(0);

        challengeOptionTwo = GameObject.Find("OptionTwo").GetComponent<Button>();
        challengeOptionTwoText = challengeOptionTwo.transform.GetChild(0);

        challengeOptionThreeText = challengeOptionThree.transform.GetChild(0);

        challengeCanvas.SetActive(false);
    }

    public void UpdateChallengeCanvas(ChallengeDefinition chal) {
        bool hideOption;

        //hide OptionButton 3
        challengeOptionThree.gameObject.SetActive(false);

        //grab vital data
        challengeID = chal.challengeID;
        optionsOne = chal.optionOneResults.Split(',');
        optionsTwo = chal.optionTwoResults.Split(',');

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

        //change image
        for (int i=0; i < Challenges.S.totChallenges; i++) { ArtAssets.S.challengeImages[i].SetActive(false); }
        ArtAssets.S.challengeImages[chal.challengeID].SetActive(true);

        //reset effects popup
        challengePopup.GetComponent<CanvasGroup>().alpha = 1;

        //add event listeners
        challengeOptionOne.onClick.RemoveAllListeners();
        challengeOptionTwo.onClick.RemoveAllListeners();
        if (chal.challengeID != 15) {
            challengeOptionOne.onClick.AddListener(delegate {
                challengePopup.GetComponent<CanvasGroup>().alpha = 0;
                challengePopup.SetActive(false);
                Results.S.RetrieveResult(challengeID, optionsOne);
            });
        }

        //check prereqs for challenge and options
        hideOption = Results.S.ResultPrereqCheck(chal.challengeID, 0);

        //check to see if second button is hidden
        if (chal.optionTwoText == "none" || hideOption == true) { challengeOptionTwo.gameObject.SetActive(false); }
        else {
            challengeOptionTwo.onClick.AddListener(delegate {
                challengePopup.GetComponent<CanvasGroup>().alpha = 0;
                challengePopup.SetActive(false);
                Results.S.RetrieveResult(challengeID, optionsTwo);
            });
        }

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
        Text optionThree = challengeOptionThreeText.GetComponent<Text>();

        //hide first button
        challengeOptionOne.gameObject.SetActive(false);
        challengeOptionTwo.gameObject.SetActive(false);
        challengeOptionThree.gameObject.SetActive(true);


        //change text
        title.text = r.resultTitle;
        flavor.text = r.resultFlavor;
        optionThree.text = "Continue.";

        challengeOptionThree.onClick.RemoveAllListeners();
        challengeOptionThree.onClick.AddListener(delegate { GameResources.S.UpdateResources(r); });
        challengeOptionThree.onClick.AddListener(CloseChallengeCanvas);
        

    }

    public void CloseChallengeCanvas() {
        //check if wellbeing is low enough for run away
        if (Player.S.currAct == 3) Acts.S.ReevaluateActThree();

        Acts.S.challengesDoneForAct += 1;

        Debug.Log("finished " + Acts.S.challengesDoneForAct + "of " + Acts.S.challengeThisAct);

        //hide and disable canvas
        StartCoroutine(TransitionChallengeCanvas(0));

        //check if act is finished
        Acts.S.CheckActStatus();

        //disable Popup
        popup = false;
        
    }

    public IEnumerator TransitionChallengeCanvas(int i) { 
        switch (i) {
            case 0:
                ArtAssets.S.ControlBackground(0);
                GUIControl.S.GUICanvas.SetActive(true);                
                while (challengeGroup.alpha > 0) {
                    challengeGroup.alpha -= Time.deltaTime * 2;
                    GUIControl.S.GUIAlpha.alpha += Time.deltaTime * 2;
                    yield return null;
                }
                challengeCanvas.SetActive(false);
                challengeGroup.interactable = false;                
                break;
            case 1:
                //background
                ArtAssets.S.ControlBackground(1);
                GUIControl.S.GUIAlpha.alpha = 0;
                challengeCanvas.SetActive(true);
                GUIControl.S.GUICanvas.SetActive(false);

                while (challengeGroup.alpha < 1) {
                    challengeGroup.alpha += Time.deltaTime * 2;
                    yield return null;
                }
                challengeGroup.interactable = true;
                break;
        }
    }

}
