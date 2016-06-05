using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LitJson;

public class ParachuteKids : MonoBehaviour {

    /* CLASS VARIABLES
    ---------------------------------------------------------------*/
    static public ParachuteKids S;

    //dictionaries-----------------------//
    static public Dictionary<LocationType, LocationDefinition> LOC_DEFS;
    static public Dictionary<ChallengeType, ChallengeDefinition> CHAL_DEFS;
    static public Dictionary<CharacterType, CharacterDefinition> CHARS_DEFS;

    //dynamic----------------------------//

    //private----------------------------//
    private JsonData data;

    /* FUNCTIONS
    ---------------------------------------------------------------*/
    void Awake() {
        S = this;

        //init game---------------------------//
        Locations.S.CreateLocations();

        Locations.S.GetLocationDefinitions();
        Challenges.S.GetChallengeDefinitions();
        Characters.S.GetCharacterDefinitions();
        BuildDictionaries();
    }

    public void BuildDictionaries() {

        //build location dictionary---------------//
        LOC_DEFS = new Dictionary<LocationType, LocationDefinition>();
        foreach (LocationDefinition loc in Locations.S.locationDefinitions) {
            LOC_DEFS[loc.type] = loc;
        }

        //build challenge dictionary---------------//
        CHAL_DEFS = new Dictionary<ChallengeType, ChallengeDefinition>();
        foreach (ChallengeDefinition chal in Challenges.S.challengeDefinitions) {
            CHAL_DEFS[chal.type] = chal;
        }

        //build characters dictionary---------------//
        CHARS_DEFS = new Dictionary<CharacterType, CharacterDefinition>();
        foreach (CharacterDefinition chara in Characters.S.charactersDefinitions) {
            CHARS_DEFS[chara.type] = chara;
        }

        print(CHARS_DEFS[CharacterType.JohnDoe].startingGPA);
    }

    public ChallengeDefinition GetChallengeDefinition(ChallengeType ct) {
        if (CHAL_DEFS.ContainsKey(ct)) {
            return(CHAL_DEFS[ct]);
        }
        return (new ChallengeDefinition());
    }

}
