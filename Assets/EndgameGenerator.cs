using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndgameGenerator : MonoBehaviour
{
    UI_Controller uic;
    ParameterTracker pt;

    private void Start()
    {
        uic = FindObjectOfType<UI_Controller>();
        pt = FindObjectOfType<ParameterTracker>();
    }

    public void GenerateEndGame()
    {
        Debug.Log("endgame generated");

        // TODO generate this into something that looks at the ParameterTracker's 7 params
        // and turns it into a compelling and interesting set of strings for the Endgame panel.

        //For testing:
        string[] endgamePages =
            {"These are the objective results",
            "These are some subjective results",
            "This is final page of outcomes. The end. " };

        uic.UpdateEndgamePanel(endgamePages);
    }

}
