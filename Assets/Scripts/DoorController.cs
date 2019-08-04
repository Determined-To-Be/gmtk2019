using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : PlayerInteractable
{
    public Sprite Open;
    public Sprite Closed;
    public Sprite Locked;
    public string NextLevel;

    BoxCollider2D coll;
    SpriteRenderer rend;

    public bool isLocked = false;

    protected void Start()
    {
        base.Start();
        coll = this.GetComponent<BoxCollider2D>();
        rend = this.GetComponent<SpriteRenderer>();

        if(isLocked)
        {
            rend.sprite = Locked;
        }
    }

    public void LockDoor()
    {
        isLocked = true;
        rend.sprite = Locked;
    }

    public void UnlockDoor()
    {
        isLocked = false;
        rend.sprite = Closed;
    }

    public override void OnPlayerInteration()
    {
        if (!isLocked)
        {
            coll.enabled = false;
            rend.sprite = Open;

            //Wait for 1 sec?

            SceneManager.LoadScene(NextLevel);
        }
    }

}