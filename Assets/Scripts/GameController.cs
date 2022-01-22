using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameController : MonoBehaviour
{
    public enum State { Start, CoreGameplay, Endgame, Settings};
    UI_Controller uic;
    public Action OnStartNewGame;

    //state
    State previousState;
    State currentState;

    void Start()
    {
        uic = FindObjectOfType<UI_Controller>();
    }

    #region Public Methods

    /// <summary>
    /// Workhorse method of the Game Controller. Allows other classes to switch the state. Privately handles
    /// the entry and exit work needed for any particular state.
    /// </summary>
    /// <param name="newState"></param>
    public void SetNewState(State newState)
    {
        if (newState == currentState)
        {
            Debug.Log("New state is the same as the current state");
            return;
        }
        else
        {
            previousState = currentState;
            currentState = newState;
        }

        EnterState(currentState);
        ExitState(previousState);

        uic.UpdateUIWithNewState(newState);
    }

    /// <summary>
    /// This is needed by the persistent settings button in since buttons can't take in enums within the editor.
    /// </summary>
    public void SetStateAsSettings() 
    {
        SetNewState(State.Settings);
    }

    /// <summary>
    /// This allows the state to rollback to the previous state, like a back button
    /// </summary>
    public void RequestReturnToPreviousState()
    {
        //if (previousState == State.Endgame)
        //{
        //    // Perhaps there are some states you can't return to? Endgame?
        //    return
        //}

        SetNewState(previousState);
    }

    #endregion

    #region State Helpers
    private void EnterState(State newState)
    {
        switch (newState)
        {
            case State.Start:
                // do things related to entering the start state
                break;

            case State.CoreGameplay:
                // Do things to set up the core gameplay state
                // This might include starting a brand new game, but also might be entering mid-game,
                // ...especially if coming back from the settings menu mid-game.
                OnStartNewGame?.Invoke();
                break;

            case State.Endgame:
                // set up the endgame state
                break;

            case State.Settings:

                break;

        }

    }


    private void ExitState(State previousState)
    {
        switch (previousState)
        {
            case State.Start:
                // cleanup anything related to old state
                return;

            case State.CoreGameplay:

                return;

            case State.Endgame:

                return;

            case State.Settings:

                return;

        }
    }

    #endregion

}
