using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Controller : MonoBehaviour
{

    [SerializeField] List<UI_Panel> allPanels = null;

    StartPanel startPanel;
    CoreGameplayPanel coreGameplayPanel;
    EndgamePanel endgamePanel;
    SettingsPanel settingsPanel;

    private void Start()
    {
        FindPanels();
        UpdateUIWithNewState(GameController.State.Start); //Force UI to start in Start state
    }

    private void FindPanels()
    {
        foreach (var panel in allPanels)
        {
            panel.gameObject.SetActive(true); //This to ensure that all panels are active when leaving the editor
        }

        startPanel = FindObjectOfType<StartPanel>();
        coreGameplayPanel = FindObjectOfType<CoreGameplayPanel>();
        endgamePanel = FindObjectOfType<EndgamePanel>();
        settingsPanel = FindObjectOfType<SettingsPanel>();
    }

    private void HideAllPanels()
    {
        foreach (var panel in allPanels)
        {
            panel.ShowHideElements(false);
        }
    }

    public void UpdateUIWithNewState(GameController.State newState)
    {
        HideAllPanels();

        switch (newState)
        {
            case GameController.State.Start:
                startPanel.ShowHideElements(true);
                return;

            case GameController.State.CoreGameplay:
                coreGameplayPanel.ShowHideElements(true);
                return;

            case GameController.State.Endgame:
                endgamePanel.ShowHideElements(true);
                return;

            case GameController.State.Settings:
                settingsPanel.ShowHideElements(true);
                return;
        }
    }
}