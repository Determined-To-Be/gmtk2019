using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : PlayerInteractable
{
    public Sprite Open;
    public Sprite Closed;
    public string NextLevel;

    BoxCollider2D coll;
    SpriteRenderer rend;

    bool isLocked = false;

    protected void Start()
    {
        base.Start();
        coll = this.GetComponent<BoxCollider2D>();
        rend = this.GetComponent<SpriteRenderer>();
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