using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterController : MonoBehaviour
{
	UnityEvent player_event = new UnityEvent();
	
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
			move();
		}
	}

	void move() {
		//We need to prevent the character from moving in the direction of a wall or a collision
		if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
			direction.y = 0;
		else
			direction.x = 0;

		List<RaycastHit2D> hits = new List<RaycastHit2D>();
		if (direction == Vector2.up){
			hits.Add(Physics2D.Raycast(this.transform.position + Vector3.down * .1f, direction, .8f));
		} else {
			hits.Add(Physics2D.Raycast(this.transform.position + Vector3.down * .1f, direction, 1.1f));
		}

		Debug.DrawRay(this.transform.position, direction, Color.green);

		foreach(RaycastHit2D hit in hits) {
			if (hit){ //I hit something I can't move here
				//Rebound
				if (hit.transform.tag == "Interactable") {
					hit.transform.gameObject.GetComponent<PlayerInteractable>().OnPlayerInteration();
				}
				return;
			}
		}

		//this.transform.position += (Vector3)direction.normalized;
		//this.transform.position = new Vector3(Mathf.RoundToInt(this.transform.position.x) + .5f, Mathf.RoundToInt(this.transform.position.y) + .5f, 0);
		//We need to clamp the position to an int and the plus .5 on the x and -.5 on the y
		ScreenShake.instance.Shake(.1f, .1f);
		SoundManager.instance.PlaySound(SoundManager.PlayerSound.step, true);
		StartCoroutine(moveTo(this.transform.position + (Vector3)direction.normalized, moveSpeed));
	}

	IEnumerator moveTo(Vector2 goal, float speed) {
		canMove = false;
		Vector3 lastpos = this.transform.position + Vector3.down * .1f;
		
		while (Vector2.Distance((Vector2)this.transform.position, goal) > .05f) {
			this.transform.position = Vector3.Lerp(this.transform.position, goal, speed * Time.deltaTime);
			yield return null;
		}		
		this.transform.position = new Vector3(Mathf.RoundToInt(this.transform.position.x), Mathf.RoundToInt(this.transform.position.y), this.transform.position.z);

		if (item != null)
			item.transform.position = lastpos;

		canMove = true;
	}
}
