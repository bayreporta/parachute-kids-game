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

    public GameObject generalActPanel;
    public GameObject generalActTextObject;
    public GameObject generalActContextObject;
    public Transform generalActText;
    public Transform generalActContext;

    public GameObject generalResultsPanel;
    public GameObject readingResults;
    public GameObject mathResults;
    public GameObject writingResults;
    public GameObject totalResults;
    public Transform readingResultsText;
    public Transform mathResultsText;
    public Transform writingResultsText;
    public Transform totalResultsText;
    public Button resultsButton;



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
                while (generalGroup.alpha < 1) {
                    generalGroup.alpha += Time.deltaTime / 1;
                    yield return null;
                }
                generalGroup.interactable = true;
                break;
        }
        
    }
	
    public void FindGeneralCanvasElems() {
        generalCanvas = GameObject.Find("_GeneralCanvas");
        generalGroup = generalCanvas.GetComponent<CanvasGroup>();
        generalActPanel = GameObject.Find("ActPanel");

        generalActTextObject = GameObject.Find("ActText");
        generalActText = generalActTextObject.transform.GetChild(0);

        generalActContextObject = GameObject.Find("ActContext");
        generalActContext = generalActContextObject.transform.GetChild(0);

        generalResultsPanel = GameObject.Find("ResultsPanel");
        readingResults = GameObject.Find("ReadingResults");
        mathResults = GameObject.Find("MathResults");
        writingResults = GameObject.Find("WritingResults");
        totalResults = GameObject.Find("TotalResults");
        readingResultsText = readingResults.transform.GetChild(0);
        mathResultsText = mathResults.transform.GetChild(0);
        writingResultsText = writingResults.transform.GetChild(0);
        totalResultsText = totalResults.transform.GetChild(0);
        resultsButton = GameObject.Find("ResultsButton").GetComponent<Button>();

        generalResultsPanel.SetActive(false);

        //temp make active
        generalGroup.alpha = 1;
        generalGroup.interactable = false;

    }

    public void UpdateActCanvas(int act) {
        Text actText = generalActText.GetComponent<Text>();
        Text actContext = generalActContext.GetComponent<Text>();

        switch (act) {
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
        Text readingScore = readingResultsText.GetComponent<Text>();
        Text mathScore = mathResultsText.GetComponent<Text>();
        Text writingScore = writingResultsText.GetComponent<Text>();
        Text totalScore = totalResultsText.GetComponent<Text>();

        //activate Results panel
        generalCanvas.SetActive(true);
        generalActPanel.SetActive(false);
        generalResultsPanel.SetActive(true);
        StartCoroutine(TransitionActCanvas(1));

        //update panel
        readingScore.text = "Critical Reading: " + EndGame.S.readingSATScore;
        mathScore.text = "Math: " + EndGame.S.mathSATScore;
        writingScore.text = "Writing: " + EndGame.S.writingSATScore;
        totalScore.text = "Total Score: " + (EndGame.S.readingSATScore + EndGame.S.mathSATScore + EndGame.S.writingSATScore);

    }

}
