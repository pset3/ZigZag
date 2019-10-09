using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Zenject;

public partial class Player : MonoBehaviour, IRestartable
{
    [Inject] Game game;
    [Inject] Board board;
    [Inject] PlayerSettings settings;

    PlayerData data = new PlayerData();

    public PlayerData Data
    {
        get { return data; }
    }

    Vector3 startPosition = new Vector3(0f, 0f, -0.75f);
    Rigidbody rigidbody;

    public enum StateType
    {
        Start,
        Move,
        Fall
    }

    public enum DirectType
    {
        Right,
        Up
    }

    StateType state;
    DirectType direct;

    public StateType State
    {
        get { return state; }
    }

    public DirectType Direct
    {
        get { return direct; }
        set { direct = value; }
    }


    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        Restart();
    }

    public void Restart()
    {
        state = StateType.Start;
        direct = DirectType.Right;
        data.Score = 0;
        transform.position = startPosition;
        rigidbody.isKinematic = true;
    }

    public void StartMove()
    {
        state = StateType.Move;
    }

    void Update()
    {
        if (state == StateType.Move || state == StateType.Fall)
        {
            switch (direct)
            {
                case DirectType.Right:
                    transform.position += Vector3.right * settings.speed * Time.deltaTime;
                    break;
                case DirectType.Up:
                    transform.position += Vector3.up * settings.speed * Time.deltaTime;
                    break;
            }
        }

        if (state == StateType.Move)
        {
            board.CheckToScroll(transform.position);

            Tile tile = board.GetTile(transform.position);

            if (tile == null)
            {
                Fall();
                game.Fail();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
       Bonus bonus = other.GetComponent<Bonus>();

       if (bonus != null)
       {
           data.Score++;
           bonus.Despawn();
       }
    }

    void Fall()
    {
        state = StateType.Fall;
        rigidbody.isKinematic = false;
    }

}