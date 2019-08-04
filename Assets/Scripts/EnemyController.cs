using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(SpriteRenderer))]

public class EnemyController : MonoBehaviour
{
    int wpIdx = 0;
    Transform[] waypoints;
    bool wasLit = false;
    SpriteRenderer weepieSpriteRenderer;

    public float moveSpeed = 16f;
    public enum Enemy { lurkie, weepie, doppie };
    public Enemy enemy;
    public GameObject path;
    public bool isLit = false, isMirroredX = true, isMirroredY = true;
    public Sprite[] weepieSprites;

    void Start()
    {
        TileTime.instance.AddListener(Move);
        if (enemy == Enemy.lurkie)
        {
            waypoints = path.GetComponentsInChildren<Transform>();
            transform.position = waypoints[wpIdx].position;
        }
        else if (enemy == Enemy.weepie)
        {
            weepieSpriteRenderer = GetComponent<SpriteRenderer>();
        }
    }

    void Move()
    {
        switch (enemy)
        {
            case Enemy.weepie:
                Weep();
                break;
            case Enemy.doppie:
                Dopple();
                break;
            case Enemy.lurkie:
            default:
                Lurk();
                break;
        }
    }

    IEnumerator MoveTo(Vector3 next)
    {
        while (Vector2.Distance(transform.position, next) > .05f)
        {
            transform.position = Vector3.Lerp(transform.position, next, moveSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = next;
    }

    void Weep()
    {
        if (isLit)
        {
            if (!wasLit)
            {
                weepieSpriteRenderer.sprite = weepieSprites[1];
                SoundManager.Instance.PlaySound(SoundManager.PlayerSound.weep, true);
                wasLit = true;
            }
            return;
        }
        if (wasLit)
        {
            weepieSpriteRenderer.sprite = weepieSprites[0];
            wasLit = false;
        }

        Vector2 direction = CharacterController.instance.transform.position - transform.position;
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
            direction.y = 0;
        else
            direction.x = 0;
        direction = direction.normalized;

        List<RaycastHit2D> hits = new List<RaycastHit2D>();
        if (direction == Vector2.up)
        {
            hits.Add(Physics2D.Raycast(transform.position + Vector3.down * .1f + Vector3.left * .1f, direction, .8f, ~LayerMask.GetMask("Player", "Enviroment", "Item")));
            hits.Add(Physics2D.Raycast(transform.position + Vector3.down * .1f + Vector3.right * .1f, direction, .8f, ~LayerMask.GetMask("Player", "Enviroment", "Item")));

            Debug.DrawRay(transform.position + Vector3.down * .1f + Vector3.left * .1f, direction * .8f);
            Debug.DrawRay(transform.position + Vector3.down * .1f + Vector3.right * .1f, direction * .8f);
        }
        else if (direction == Vector2.down)
        {
            hits.Add(Physics2D.Raycast(transform.position + Vector3.down * .1f + Vector3.left * .1f, direction, 1.1f, ~LayerMask.GetMask("Player", "Enviroment", "Item")));
            hits.Add(Physics2D.Raycast(transform.position + Vector3.down * .1f + Vector3.right * .1f, direction, 1.1f, ~LayerMask.GetMask("Player", "Enviroment", "Item")));

            Debug.DrawRay(transform.position + Vector3.down * .1f + Vector3.left * .1f, direction * 1.1f);
            Debug.DrawRay(transform.position + Vector3.down * .1f + Vector3.right * .1f, direction * 1.1f);
        }
        else
        {
            hits.Add(Physics2D.Raycast(transform.position + Vector3.down * .1f, direction, 1.1f, ~LayerMask.GetMask("Player", "Enviroment", "Item")));

            Debug.DrawRay(transform.position + Vector3.down * .1f, direction * 1.1f);
        }
        foreach (RaycastHit2D hit in hits)
        {
            if (hit)
            {
                return;
            }
        }

        StartCoroutine(MoveTo(transform.position + (Vector3)direction.normalized));
    }

    void Dopple()
    {
        Vector2 direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (isMirroredX)
            direction.x *= -1f;
        if (isMirroredY)
            direction.y *= -1f;
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
            direction.y = 0;
        else
            direction.x = 0;

        List<RaycastHit2D> hits = new List<RaycastHit2D>();
        if (direction == Vector2.up)
        {
            hits.Add(Physics2D.Raycast(transform.position + Vector3.down * .1f + Vector3.left * .1f, direction, .8f, ~LayerMask.GetMask("Player", "Enviroment", "Item")));
            hits.Add(Physics2D.Raycast(transform.position + Vector3.down * .1f + Vector3.right * .1f, direction, .8f, ~LayerMask.GetMask("Player", "Enviroment", "Item")));

            Debug.DrawRay(transform.position + Vector3.down * .1f + Vector3.left * .1f, direction * .8f);
            Debug.DrawRay(transform.position + Vector3.down * .1f + Vector3.right * .1f, direction * .8f);
        }
        else if (direction == Vector2.down)
        {
            hits.Add(Physics2D.Raycast(transform.position + Vector3.down * .1f + Vector3.left * .1f, direction, 1.1f, ~LayerMask.GetMask("Player", "Enviroment", "Item")));
            hits.Add(Physics2D.Raycast(transform.position + Vector3.down * .1f + Vector3.right * .1f, direction, 1.1f, ~LayerMask.GetMask("Player", "Enviroment", "Item")));

            Debug.DrawRay(transform.position + Vector3.down * .1f + Vector3.left * .1f, direction * 1.1f);
            Debug.DrawRay(transform.position + Vector3.down * .1f + Vector3.right * .1f, direction * 1.1f);
        }
        else
        {
            hits.Add(Physics2D.Raycast(transform.position + Vector3.down * .1f, direction, 1.1f, ~LayerMask.GetMask("Player", "Enviroment", "Item")));

            Debug.DrawRay(transform.position + Vector3.down * .1f, direction * 1.1f);
        }
        foreach (RaycastHit2D hit in hits)
        {
            if (hit)
            {
                SoundManager.Instance.PlaySound(SoundManager.PlayerSound.dopple, true);
                return;
            }
        }

        StartCoroutine(MoveTo(transform.position + (Vector3)direction.normalized));
    }

    void Lurk()
    {
        Vector2 direction = waypoints[wpIdx].position - transform.position;
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
            direction.y = 0;
        else
            direction.x = 0;
        direction = direction.normalized;

        List<RaycastHit2D> hits = new List<RaycastHit2D>();
        if (direction == Vector2.up)
        {
            hits.Add(Physics2D.Raycast(transform.position + Vector3.down * .1f + Vector3.left * .1f, direction, .8f, ~LayerMask.GetMask("Player", "Enviroment", "Item")));
            hits.Add(Physics2D.Raycast(transform.position + Vector3.down * .1f + Vector3.right * .1f, direction, .8f, ~LayerMask.GetMask("Player", "Enviroment", "Item")));

            Debug.DrawRay(transform.position + Vector3.down * .1f + Vector3.left * .1f, direction * .8f);
            Debug.DrawRay(transform.position + Vector3.down * .1f + Vector3.right * .1f, direction * .8f);
        }
        else if (direction == Vector2.down)
        {
            hits.Add(Physics2D.Raycast(transform.position + Vector3.down * .1f + Vector3.left * .1f, direction, 1.1f, ~LayerMask.GetMask("Player", "Enviroment", "Item")));
            hits.Add(Physics2D.Raycast(transform.position + Vector3.down * .1f + Vector3.right * .1f, direction, 1.1f, ~LayerMask.GetMask("Player", "Enviroment", "Item")));

            Debug.DrawRay(transform.position + Vector3.down * .1f + Vector3.left * .1f, direction * 1.1f);
            Debug.DrawRay(transform.position + Vector3.down * .1f + Vector3.right * .1f, direction * 1.1f);
        }
        else
        {
            hits.Add(Physics2D.Raycast(transform.position + Vector3.down * .1f, direction, 1.1f, ~LayerMask.GetMask("Player", "Enviroment", "Item")));

            Debug.DrawRay(transform.position + Vector3.down * .1f, direction * 1.1f);
        }
        foreach (RaycastHit2D hit in hits)
        {
            if (hit)
            {
                return;
            }
        }

        Vector3 destination = transform.position + (Vector3)direction.normalized;
        if (destination == waypoints[wpIdx].position)
        {
            if (++wpIdx >= waypoints.Length)
            {
                SoundManager.Instance.PlaySound(SoundManager.PlayerSound.lurk, true);
                wpIdx = 0;
            }
        }

        StartCoroutine(MoveTo(destination));
    }
}
