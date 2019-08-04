using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int wpIdx = 0;
    Transform[] waypoints;

    public float moveSpeed;
    public enum Enemy { lurkie, weepie, doppie };
    public Enemy enemy;
    public GameObject path;

    void Start()
    {
        TileTime.instance.AddListener(move);
        if (enemy == Enemy.lurkie)
        {
            waypoints = path.GetComponentsInChildren<Transform>();
            gameObject.transform.position = waypoints[wpIdx].position;
        }
    }

    void move()
    {
        switch (enemy)
        {
            case Enemy.weepie:

                break;
            case Enemy.doppie:

                break;
            case Enemy.lurkie:
            default:
                lurk();
                break;
        }
    }

    IEnumerator moveTo(Vector3 next)
    {
        while (Vector2.Distance(transform.position, next) > .05f)
        {
            transform.position = Vector3.Lerp(transform.position, next, moveSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = next;
    }

    void lurk()
    {
        Vector2 direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
            direction.y = 0;
        else
            direction.x = 0;

        List<RaycastHit2D> hits = new List<RaycastHit2D>();
        if (direction == Vector2.up)
        {
            hits.Add(Physics2D.Raycast(this.transform.position + Vector3.down * .1f + Vector3.left * .1f, direction, .8f, ~LayerMask.GetMask("Player", "Enviroment", "Item")));
            hits.Add(Physics2D.Raycast(this.transform.position + Vector3.down * .1f + Vector3.right * .1f, direction, .8f, ~LayerMask.GetMask("Player", "Enviroment", "Item")));

            Debug.DrawRay(this.transform.position + Vector3.down * .1f + Vector3.left * .1f, direction * .8f);
            Debug.DrawRay(this.transform.position + Vector3.down * .1f + Vector3.right * .1f, direction * .8f);

        }
        else if (direction == Vector2.down)
        {
            hits.Add(Physics2D.Raycast(this.transform.position + Vector3.down * .1f + Vector3.left * .1f, direction, 1.1f, ~LayerMask.GetMask("Player", "Enviroment", "Item")));
            hits.Add(Physics2D.Raycast(this.transform.position + Vector3.down * .1f + Vector3.right * .1f, direction, 1.1f, ~LayerMask.GetMask("Player", "Enviroment", "Item")));

            Debug.DrawRay(this.transform.position + Vector3.down * .1f + Vector3.left * .1f, direction * 1.1f);
            Debug.DrawRay(this.transform.position + Vector3.down * .1f + Vector3.right * .1f, direction * 1.1f);
        }
        else
        {
            hits.Add(Physics2D.Raycast(this.transform.position + Vector3.down * .1f, direction, 1.1f, ~LayerMask.GetMask("Player", "Enviroment", "Item")));

            Debug.DrawRay(this.transform.position + Vector3.down * .1f, direction * 1.1f);
        }

        foreach (RaycastHit2D hit in hits)
        {
            if (hit)
            {
                if (hit.transform.tag == "Interactable")
                {
                    hit.transform.gameObject.GetComponent<PlayerInteractable>().OnPlayerInteration();
                }
                return;
            }
        }

        Vector3 destination = transform.position + (Vector3)direction.normalized;

        StartCoroutine(moveTo(destination));
    }
}
