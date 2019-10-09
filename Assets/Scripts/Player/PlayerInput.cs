using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerInput : MonoBehaviour
{
    [Inject] private Game game;
    [Inject] Player player;

    void Awake()
    {
        player = GetComponent<Player>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (game.State == Game.StateType.Play)
            {
                switch (player.State)
                {
                    case Player.StateType.Start:
                        player.StartMove();
                        break;

                    case Player.StateType.Move:
                        if (player.Direct == Player.DirectType.Right)
                            player.Direct = Player.DirectType.Up;
                        else
                            player.Direct = Player.DirectType.Right;
                        break;
                }
            }
            else if (game.State == Game.StateType.Fail)
            {
                game.Restart();
            }
        }
    }
}
