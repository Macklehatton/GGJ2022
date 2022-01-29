using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    [SerializeField]
    private Transform startLocation;
    [SerializeField]
    private Transform player;

    public void Restart()
    {
        player.position = startLocation.position;
    }
}
