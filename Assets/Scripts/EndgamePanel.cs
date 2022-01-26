using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class EndgamePanel : UI_Panel
{
    [SerializeField] TextMeshProUGUI mainTMP = null;
    [SerializeField] TextMeshProUGUI buttonTMP = null;
    [SerializeField] Image planetImage = null;

    //settings
    string next = "Next Page";
    string accept = "Accept Outcome";

    //state
    string[] outcomePages;
    int currentPage;


    #region Button Handlers
    public void HandleButtonPress()
    {
        planetImage.gameObject.SetActive(false);
        if (currentPage < outcomePages.Length - 1)
        {
            currentPage++;
            mainTMP.text = outcomePages[currentPage];
            if (currentPage == outcomePages.Length - 2)
            {
                buttonTMP.text = next;
            }
            else
            {
                buttonTMP.text = accept;
            }

        }
        else
        {
            gcRef.SetNewState(GameController.State.Start);
        }
    }


    #endregion;

    #region Public Methods
    public void UpdateUIWithOutcome(string[] outcomePages, Sprite planetSprite)
    {
        currentPage = 0;
        planetImage.sprite = planetSprite;
        planetImage.gameObject.SetActive(true);
        this.outcomePages = outcomePages;
        mainTMP.text = outcomePages[0];
        if (outcomePages.Length > 1)
        {
            buttonTMP.text = next;
        }
        else
        {
            buttonTMP.text = accept;
        }
    }

    #endregion


}
