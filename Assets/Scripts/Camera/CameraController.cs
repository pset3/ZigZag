using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CameraController : MonoBehaviour
{
    public float Distance = 10f;

    [Inject] Player player;

    void Start()
    {
        transform.position = player.transform.position + Vector3.back * Distance;
    }

    void LateUpdate()
    {
        if (player.State == Player.StateType.Start || player.State == Player.StateType.Move)
        {
            transform.position = player.transform.position + Vector3.back * Distance;
        }
    }
}
