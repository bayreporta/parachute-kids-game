using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;

public class GeneralCanvas : MonoBehaviour {

    /* CLASS VARIABLES
    ---------------------------------------------------------------*/
    public static GeneralCanvas S;

    //canvas items

    public GameObject generalCanvas;
    public CanvasGroup generalGroup;
    public GameObject generalModalPanel;

    //act panel
    public GameObject generalActPanel;
    public GameObject generalActTextObject;
    public GameObject generalActContextObject;
    public Transform generalActText;
    public Transform generalActContext;

    //results panel
    public GameObject generalResultsPanel;
    public GameObject resultsTitle;
    public GameObject resultsFlavor;
    public GameObject readingResults;
    public GameObject mathResults;
    public GameObject writingResults;
    public GameObject totalResults;
    public GameObject collegeResults;

    public Transform resultsTitleText;
    public Transform resultsFlavorText;
    public Transform readingResultsText;
    public Transform mathResultsText;
    public Transform writingResultsText;
    public Transform totalResultsText;
    public Transform collegeResultsText;

    public Button resultsButton;
    public Transform resultsButtonText;



    /* FUNCTIONS
    ---------------------------------------------------------------*/
    void Awake () {
        S = this;
	}

    public IEnumerator TransitionActCanvas(int i) {
        switch (i) {
            case 0:
                while (generalGroup.alpha > 0) {
                    generalGroup.alpha -= Time.deltaTime / 1;
                    yield return null;
                }      

                generalGroup.interactable = false;
                generalCanvas.SetActive(false);
                break;
            case 1:
                /*while (generalGroup.alpha < 1) {
                    generalGroup.alpha += Time.deltaTime / 1;
                    yield return null;
                }*/
      
                generalGroup.alpha = 1;
                generalGroup.interactable = true;
                break;
        }
        
    }
	
    public void FindGeneralCanvasElems() {
        generalCanvas = GameObject.Find("_GeneralCanvas");
        generalGroup = generalCanvas.GetComponent<CanvasGroup>();
        generalActPanel = GameObject.Find("ActPanel");

        //Act Panel
        generalActTextObject = GameObject.Find("ActText");
        generalActText = generalActTextObject.transform.GetChild(0);

        generalActContextObject = GameObject.Find("ActContext");
        generalActContext = generalActContextObject.transform.GetChild(0);

        //Results Panel
        generalResultsPanel = GameObject.Find("ResultsPanel");
        resultsTitle = GameObject.Find("ResultsTitle");
        resultsFlavor = GameObject.Find("ResultsFlavor");
        readingResults = GameObject.Find("ReadingResults");
        mathResults = GameObject.Find("MathResults");
        writingResults = GameObject.Find("WritingResults");
        totalResults = GameObject.Find("TotalResults");
        collegeResults = GameObject.Find("CollegeResults");

        resultsTitleText = resultsTitle.transform.GetChild(0);
        resultsFlavorText = readingResults.transform.GetChild(0);
        readingResultsText = readingResults.transform.GetChild(0);
        mathResultsText = mathResults.transform.GetChild(0);
        writingResultsText = writingResults.transform.GetChild(0);
        totalResultsText = totalResults.transform.GetChild(0);
        collegeResultsText = collegeResults.transform.GetChild(0);
        resultsButton = GameObject.Find("ResultsButton").GetComponent<Button>();
        resultsButtonText = resultsButton.transform.GetChild(0);

        resultsFlavor.SetActive(false);
        generalResultsPanel.SetActive(false);

        generalCanvas.SetActive(false);
    }

    public void UpdateActCanvas(int act) {
        Text actText = generalActText.GetComponent<Text>();
        Text actContext = generalActContext.GetComponent<Text>();

        switch (act) {
            case 0:
            case 1:
                actText.text = "ACT 1";
                actContext.text = "Junior Year, Fall Semester";
                break;
            case 2:
                actText.text = "ACT 2";
                actContext.text = "Junior Year, Spring Semester";
                break;
            case 3:
                actText.text = "ACT 3";
                actContext.text = "Senior Year";
                break;
        }
    }

    public void UpdateSATResultsPanel() {
        CollegeDefinition college = ParachuteKids.S.GetCollegeDefinition((CollegeType)Player.S.collegeChoice);
        Text readingScore = readingResultsText.GetComponent<Text>();
        Text mathScore = mathResultsText.GetComponent<Text>();
        Text writingScore = writingResultsText.GetComponent<Text>();
        Text totalScore = totalResultsText.GetComponent<Text>();
        Text admittedText = collegeResultsText.GetComponent<Text>();
        Text resultsText = resultsButtonText.GetComponent<Text>();

        int ending = -1; //this will determine the ending of the game
        bool admittance = false;

        //activate Results panel
        generalCanvas.SetActive(true);
        generalActPanel.SetActive(false);
        generalResultsPanel.SetActive(true);
        StartCoroutine(TransitionActCanvas(1));

        //determine if you were admitted into college
        admittance = EndGame.S.DetermineCollegeAdmittance();

        //update panel
        readingScore.text = "Critical Reading: " + EndGame.S.readingSATScore;
        mathScore.text = "Math: " + EndGame.S.mathSATScore;
        writingScore.text = "Writing: " + EndGame.S.writingSATScore;
        totalScore.text = "Total Score: " + (EndGame.S.readingSATScore + EndGame.S.mathSATScore + EndGame.S.writingSATScore);

        if (admittance == true) {
            admittedText.text = college.name + " has accepted your application!";
            resultsText.text = "My family will be proud";
            college.admitted = true;
            ending = 1;
        }
        else if (admittance == false) {
            admittedText.text = "You have not been accepted into " + college.name;
            ending = 2;
        }

        resultsButton.onClick.RemoveAllListeners();
        resultsButton.onClick.AddListener(delegate { StartCoroutine(TransitionActCanvas(0)); });
        resultsButton.onClick.AddListener(delegate { EndGame.S.PopulateEnding(ending); });
    }

}
