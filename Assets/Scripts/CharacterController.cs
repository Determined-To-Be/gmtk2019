using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterController : MonoBehaviour
{
	UnityEvent player_event = new UnityEvent();

	public GameObject grid;
	public SpriteRenderer[] gridRenderer;

	public float moveSpeed = 16f;
	public bool canMove = true;
	Vector2 direction;

	public Item item;

	public static CharacterController instance;

	// Start is called before the first frame update
	void Start()
	{
		TileTime.instance.AddListener(move);
		CharacterController.instance = this.GetComponent<CharacterController>();
	}

	// Update is called once per frame
	void Update()
	{
		direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
		if (canMove && direction.sqrMagnitude > .1f) {
			TileTime.instance.Tick();
		}
	}

	void gridDisplay() {
		if (grid == null)
			return;

		if (gridRenderer.Length == 0)
			return;

		List<RaycastHit2D> hits = new List<RaycastHit2D>();

		//UP -> 0, 1
		hits.Add(Physics2D.Raycast(this.transform.position + Vector3.down * .1f + Vector3.left * .1f, Vector2.up, .8f, ~LayerMask.GetMask("Player", "Enviroment", "Item")));
		hits.Add(Physics2D.Raycast(this.transform.position + Vector3.down * .1f + Vector3.right * .1f, Vector2.up, .8f, ~LayerMask.GetMask("Player", "Enviroment", "Item")));

		//Down -> 2, 3
		hits.Add(Physics2D.Raycast(this.transform.position + Vector3.down * .1f + Vector3.left * .1f, Vector2.down, 1.1f, ~LayerMask.GetMask("Player", "Enviroment", "Item")));
		hits.Add(Physics2D.Raycast(this.transform.position + Vector3.down * .1f + Vector3.right * .1f, Vector2.down, 1.1f, ~LayerMask.GetMask("Player", "Enviroment", "Item")));

		//Left -> 4
		hits.Add(Physics2D.Raycast(this.transform.position + Vector3.down * .1f, Vector2.left, 1.1f, ~LayerMask.GetMask("Player", "Enviroment", "Item")));

		//Right -> 5
		hits.Add(Physics2D.Raycast(this.transform.position + Vector3.down * .1f, Vector2.right, 1.1f, ~LayerMask.GetMask("Player", "Enviroment", "Item")));

		gridRenderer[0].enabled = !(hits[0] || hits[1]);
		gridRenderer[1].enabled = !(hits[2] || hits[3]);
		gridRenderer[2].enabled = !hits[4];
		gridRenderer[3].enabled = !hits[5];
	}

	void move() {
		//We need to prevent the character from moving in the direction of a wall or a collision
		if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
			direction.y = 0;
		else
			direction.x = 0;

		List<RaycastHit2D> hits = new List<RaycastHit2D>();
		if (direction == Vector2.up){
			hits.Add(Physics2D.Raycast(this.transform.position + Vector3.down * .1f + Vector3.left * .1f, direction, .8f, ~LayerMask.GetMask("Player", "Enviroment", "Item")));
			hits.Add(Physics2D.Raycast(this.transform.position + Vector3.down * .1f + Vector3.right * .1f, direction, .8f, ~LayerMask.GetMask("Player", "Enviroment", "Item")));

			Debug.DrawRay(this.transform.position + Vector3.down * .1f + Vector3.left * .1f, direction * .8f);
			Debug.DrawRay(this.transform.position + Vector3.down * .1f + Vector3.right * .1f, direction * .8f);

		} else if (direction == Vector2.down) {
			hits.Add(Physics2D.Raycast(this.transform.position + Vector3.down * .1f + Vector3.left * .1f, direction, 1.1f, ~LayerMask.GetMask("Player", "Enviroment", "Item")));
			hits.Add(Physics2D.Raycast(this.transform.position + Vector3.down * .1f + Vector3.right * .1f, direction, 1.1f, ~LayerMask.GetMask("Player", "Enviroment", "Item")));

			Debug.DrawRay(this.transform.position + Vector3.down * .1f + Vector3.left * .1f, direction * 1.1f);
			Debug.DrawRay(this.transform.position + Vector3.down * .1f + Vector3.right * .1f, direction * 1.1f);
		} else{
			hits.Add(Physics2D.Raycast(this.transform.position + Vector3.down * .1f, direction, 1.1f, ~LayerMask.GetMask("Player", "Enviroment", "Item")));

			Debug.DrawRay(this.transform.position + Vector3.down * .1f, direction * 1.1f);
		}
		bool canMove = true;
		foreach (RaycastHit2D hit in hits) {
			if (hit){ //I hit something I can't move here
				//Rebound
				if (hit.transform.tag == "Interactable") {
					hit.transform.gameObject.GetComponent<PlayerInteractable>().OnPlayerInteration();
				}

				ScreenShake.instance.Shake(.1f, .1f);
				SoundManager.instance.PlaySound(SoundManager.PlayerSound.wall, true);
				canMove = false;
				break;
			}
		}

		//this.transform.position += (Vector3)direction.normalized;
		//this.transform.position = new Vector3(Mathf.RoundToInt(this.transform.position.x) + .5f, Mathf.RoundToInt(this.transform.position.y) + .5f, 0);
		//We need to clamp the position to an int and the plus .5 on the x and -.5 on the y
		SoundManager.instance.PlaySound(SoundManager.PlayerSound.step, true);
		if (canMove)
			StartCoroutine(moveTo(this.transform.position + (Vector3)direction.normalized, moveSpeed));
		else {
			StartCoroutine(cantMoveTo(this.transform.position + (Vector3)direction.normalized, moveSpeed));
		}
	}

	IEnumerator cantMoveTo(Vector2 goal, float speed) {
		canMove = false;
		float val = 1;
		while (val > .05f){
			val = Mathf.Lerp(val, 0, speed * Time.deltaTime);
			yield return null;
		}

		gridDisplay();
		canMove = true;
	}

	IEnumerator moveTo(Vector2 goal, float speed) {
		canMove = false;
		Vector3 lastpos = this.transform.position + Vector3.down * .1f;
		
		while (Vector2.Distance((Vector2)this.transform.position, goal) > .05f) {
			this.transform.position = Vector3.Lerp(this.transform.position, goal, speed * Time.deltaTime);

			if (item != null)
				item.transform.position = Vector3.Lerp(item.transform.position, lastpos, speed * Time.deltaTime);

			yield return null;
		}
		//this.transform.position = new Vector3(Mathf.RoundToInt(this.transform.position.x), Mathf.RoundToInt(this.transform.position.y), this.transform.position.z);
		this.transform.position = goal;
		grid.transform.position = this.transform.position;
		gridDisplay();
		canMove = true;
	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.transform.tag == "Item") {
			if (item != null) {
				if(item.gameObject != coll.gameObject)
					SoundManager.instance.PlaySound(SoundManager.PlayerSound.pickup);
			}
			item = coll.gameObject.GetComponent<Item>() ;
		}
	}
}
