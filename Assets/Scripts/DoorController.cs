using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : PlayerInteractable
{
    Sprite Open;
    Sprite Closed;

    BoxCollider2D coll;
    SpriteRenderer rend;
    bool isLocked = false;

    protected void Start()
    {
        base.Start();
        coll = this.GetComponent<BoxCollider2D>();
        rend = this.GetComponent<SpriteRenderer>();
    }

    protected override void OnPlayerInteration()
    {
        if (!isLocked)
        {
            coll.enabled = false;
            rend.sprite = Closed;
        }
    }
}