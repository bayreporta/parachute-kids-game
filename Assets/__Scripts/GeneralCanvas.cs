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

}
