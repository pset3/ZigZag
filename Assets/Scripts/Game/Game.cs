using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public partial class Game : MonoBehaviour, IRestartable
{
    [Inject] Board board;
    [Inject] Player player;
    [Inject] GameSettings settings;

    public enum StateType
    {
        Play,
        Fail
    }

    StateType state;

    public StateType State
    {
        get { return state; }
    }

    public GameSettings Settings
    {
        get { return settings; }
    }

    private List<IRestartable> restartableObjects = new List<IRestartable>();

    void Start()
    {
        restartableObjects.Add(player);
        restartableObjects.Add(board);
    }

    public void Restart()
    {
        state = StateType.Play;

        foreach (IRestartable restartableObject in restartableObjects)
        {
            restartableObject.Restart();
        }
    }

    public void Fail()
    {
        state = StateType.Fail;
    }

}
