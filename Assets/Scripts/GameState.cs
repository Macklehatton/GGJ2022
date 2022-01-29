using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    [SerializeField]
    private Transform startLocation;
    [SerializeField]
    private Transform player;

    public void Start()
    {
        ClassLibrary1.MyClass myClass = new ClassLibrary1.MyClass();
        Debug.Log(myClass.Speak());
    }

    public void Restart()
    {
        player.position = startLocation.position;
    }
}
