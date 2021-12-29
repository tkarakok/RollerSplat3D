using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum State
{
    MainMenu,
    InGame,
    EndGame
}

public class StateManager : Singleton<StateManager>
{
    public State state;

    private void Awake()
    {
        state = State.MainMenu;
    }

}
