using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using LitJson;

public enum CharacterType {
   JohnDoe,
   JaneDoe
}

public class CharacterDefinition {
    public CharacterType type;
    public string name;
    public string characterText;
    public string characterImage;
    public float startingGPA;
    public int startingWellbeing;
    public int startingLanguage;
}

public class Characters : MonoBehaviour {
    
    /* CLASS VARIABLES
    ---------------------------------------------------------------*/
    static public Characters S;
    public int totCharacters = 2;
    public JsonData characterData;
    public TextAsset characterJson;
    public List<CharacterDefinition> charactersDefinitions;
    public List<CharacterType> charactersTypes;

    //private----------------------------//


    /* FUNCTIONS
    ---------------------------------------------------------------*/
    void Awake() {
        S = this;      
    }



    public void GetCharacterDefinitions() {
        characterData = new JsonData();

        charactersDefinitions = new List<CharacterDefinition>();
        charactersTypes = new List<CharacterType>();
        characterData = Utils.S.ConvertJson(characterJson);

        //grab all values from ChallengeType enum
        foreach (CharacterType c in System.Enum.GetValues(typeof(CharacterType))) {
            charactersTypes.Add(c);
        }

        //build challenge data from json
        for (int i = 0; i < Characters.S.totCharacters; i++) {
            CharacterDefinition chara = new CharacterDefinition();

            chara.type = charactersTypes[i];
            chara.name = characterData[0][i]["name"].ToString();
            chara.characterText = characterData[0][i]["charactertxt"].ToString();
            chara.characterImage = characterData[0][i]["characterimg"].ToString();
            chara.startingLanguage = int.Parse(characterData[0][i]["startinglanguage"].ToString());
            chara.startingWellbeing = int.Parse(characterData[0][i]["startingwellbeing"].ToString());
            chara.startingGPA = float.Parse(characterData[0][i]["startinggpa"].ToString());

            charactersDefinitions.Add(chara);
        }
    }


}
