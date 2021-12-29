using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public delegate void StateActions();
    public event StateActions InGameEvent;
    public event StateActions EndGameEvent;

    private void Awake()
    {
        // subscribes
        #region InGameEvents
        InGameEvent += UIManager.Instance.InGameEvent;
        #endregion

        #region EndGameEvents
        EndGameEvent += UIManager.Instance.EndGameEvent;
        #endregion


    }


    private void Update()
    {
        switch (StateManager.Instance.state)
        {
            case State.InGame:
                InGameEvent();
                break;
            case State.EndGame:
                EndGameEvent();
                break;
            default:
                break;
        }
    }


}
