﻿using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Vector2 direction;
    private int pointNum = 0;

    public enum Enemy { lurkie, weepie, doppie };
    public Enemy enemy;
    public float moveSpeed = 16f;
    public GameObject item = null;
    public bool isMirrorX = true, isLit = false;
    public Transform[] waypoints;

    void Start()
    {
        TileTime.instance.AddListener(move);
        gameObject.transform.position = waypoints[0].position;
    }

    void move()
    {
        switch (enemy)
        {
            case Enemy.weepie:
                doWeepie();
                break;
            case Enemy.doppie:
                doDoppie();
                break;
            case Enemy.lurkie:
            default:
                doLurkie();
                break;
        }
    }

    private void doDoppie()
    {
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (isMirrorX)
        {
            direction.x *= -1f;
        }
        else
        {
            direction.y *= -1f;
        }

        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
            direction.y = 0;
        else
            direction.x = 0;

        RaycastHit2D hit;
        if (direction == Vector2.up)
            hit = Physics2D.Raycast(transform.position + Vector3.down * .1f, direction, .5f);
        else
            hit = Physics2D.Raycast(transform.position + Vector3.down * .1f, direction, 1);

        Debug.DrawRay(transform.position, direction, Color.green);

        if (hit)
        {
            return;
        }

        StartCoroutine(moveTo(transform.position + (Vector3)direction.normalized, moveSpeed));
    }

    private void doWeepie()
    {
        direction = new Vector2();
        if (!isLit)
        {
            Vector2 pos = gameObject.transform.position;
            Vector2 place = CharacterController.instance.transform.position;
            direction.x = pos.x > place.x ? Vector2.left.x : (pos.x < place.x ? Vector2.right.x : Input.GetAxisRaw("Horizontal"));
            direction.y = pos.y > place.y ? Vector2.down.y : (pos.y < place.y ? Vector2.up.y : Input.GetAxisRaw("Vertical"));
        }
        else
        {
            isLit = false;
        }

        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
            direction.y = 0f;
        else
            direction.x = 0f;

        RaycastHit2D hit;
        if (direction == Vector2.up)
            hit = Physics2D.Raycast(transform.position + Vector3.down * .1f, direction, .5f);
        else
            hit = Physics2D.Raycast(transform.position + Vector3.down * .1f, direction, 1);

        Debug.DrawRay(transform.position, direction, Color.green);

        if (hit)
        {
            return;
        }

        StartCoroutine(moveTo(transform.position + (Vector3)direction.normalized, moveSpeed));
    }

    private void doLurkie()
    {
        direction = new Vector2();
        Vector2 pos = gameObject.transform.position;
        Vector2 place = waypoints[pointNum].position;
        if (pos == place)
        {
            if (pointNum >= waypoints.Length)
                pointNum = -1;
            place = waypoints[++pointNum].position;
        }
        direction.x = pos.x > place.x ? Vector2.left.x : (pos.x < place.x ? Vector2.right.x : 0f);
        direction.y = pos.y > place.y ? Vector2.down.y : (pos.y < place.y ? Vector2.up.y : 0f);

        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
            direction.y = 0f;
        else
            direction.x = 0f;

        RaycastHit2D hit;
        if (direction == Vector2.up)
            hit = Physics2D.Raycast(transform.position + Vector3.down * .1f, direction, .5f);
        else
            hit = Physics2D.Raycast(transform.position + Vector3.down * .1f, direction, 1);

        Debug.DrawRay(transform.position, direction, Color.green);

        if (hit)
        {
            return;
        }

        StartCoroutine(moveTo(transform.position + (Vector3)direction.normalized, moveSpeed));
    }

    IEnumerator moveTo(Vector2 goal, float speed)
    {
        Vector3 lastpos = transform.position;

        while (Vector2.Distance(transform.position, goal) > .05f)
        {
            transform.position = Vector3.Lerp(transform.position, goal, speed * Time.deltaTime);
            yield return null;
        }

        if (item != null)
            item.transform.position = lastpos;

        transform.position = new Vector3(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y), transform.position.z);
    }
}
