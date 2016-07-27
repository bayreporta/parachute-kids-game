using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;

public class CollegeCanvas : MonoBehaviour {

    /* CLASS VARIABLES
    ---------------------------------------------------------------*/
    public static CollegeCanvas S;

    //canvas items
    public GameObject collegeCanvas;
    public CanvasGroup collegeGroup;
    public GameObject collegePickPanel;

    public Button collegeOne;
    public Button collegeTwo;
    public Button collegeThree;
    public Button collegeFour;



    /* FUNCTIONS
    ---------------------------------------------------------------*/
    void Awake() {
        S = this;
        collegeCanvas = this.gameObject;
    }

    public void FindCollegeCanvasElems() {
        collegeGroup = collegeCanvas.GetComponent<CanvasGroup>();
        collegePickPanel = GameObject.Find("CollegePickPanel");

        collegeOne = GameObject.Find("CollegeOne").GetComponent<Button>();
        collegeTwo = GameObject.Find("CollegeTwo").GetComponent<Button>();
        collegeThree = GameObject.Find("CollegeThree").GetComponent<Button>();
        collegeFour = GameObject.Find("CollegeFour").GetComponent<Button>();

        collegeCanvas.SetActive(false);
    }

    public IEnumerator TransitionCollegeCanvas(int i) {
        switch (i) {
            case 0:
                while (collegeGroup.alpha > 0) {
                    collegeGroup.alpha -= Time.deltaTime * 2;
                    yield return null;
                }
                //background
                ArtAssets.S.ControlBackground(0);
                GUIControl.S.GUICanvas.SetActive(true);

                collegeGroup.interactable = false;
                collegeCanvas.SetActive(false);
                break;
            case 1:
                //background                   
                while (collegeGroup.alpha < 1) {
                    collegeGroup.alpha += Time.deltaTime * 2;
                    yield return null;
                }              
                
                collegeGroup.interactable = true;
                ConfigureCollegeCanvas();
                break;
        }    
    }

    public void ConfigureCollegeCanvas() {       

        //remove event listeners
        collegeOne.onClick.RemoveAllListeners();
        collegeTwo.onClick.RemoveAllListeners();
        collegeThree.onClick.RemoveAllListeners();
        collegeFour.onClick.RemoveAllListeners();

        //add events
        collegeOne.onClick.AddListener(delegate { ProccessCollegeChoice(0); });
        collegeTwo.onClick.AddListener(delegate { ProccessCollegeChoice(1); });
        collegeThree.onClick.AddListener(delegate { ProccessCollegeChoice(2); });
        collegeFour.onClick.AddListener(delegate { ProccessCollegeChoice(3); });

        //challengeOptionTwoText = challengeOptionTwo.transform.GetChild(0);
    }

    public void ProccessCollegeChoice(int id) {
        Player.S.collegeChoice = id;

        //close canvas
        StartCoroutine(TransitionCollegeCanvas(0));
    }     
}
