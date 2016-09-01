using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class AboutScr : MonoBehaviour {
    public static AboutScr S;

    public GameObject canvas;
    public CanvasGroup canvasGroup;
    public Button leave;
    public Button hrLink;
    public Button readMoreLink;


    void Awake() {
        S = this;
        ConfigureButtons();
    }

    public void ConfigureButtons() {
        leave.onClick.AddListener(delegate {
            canvas.SetActive(false);
            ArtAssets.S.background.SetActive(false);
            Menus.S.title.SetActive(true);
        });
        hrLink.onClick.AddListener(delegate {
            Application.OpenURL("http://hechingerreport.org/");
        });
        readMoreLink.onClick.AddListener(delegate {
            Application.OpenURL("http://hechingerreport.org/special-reports/immigration/");
        });
    }
}
