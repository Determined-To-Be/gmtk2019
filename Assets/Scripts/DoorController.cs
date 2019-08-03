using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    bool isOpen = false;
    bool isLocked = false;

    bool canGoThroughDoor()
    {
        return !isLocked;
    }

    //if open, disable collider
    void Start()
    {
        //TileTime.instance.AddListener();
    }

}
