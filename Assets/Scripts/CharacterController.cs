using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{

	[Tooltip("In Seconds")]
	public float moveSpeed = 16f;
	public bool canMove = true;

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		Vector2 moveDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
		if (canMove && moveDirection.sqrMagnitude > .1f) {
			move(moveDirection);
		}
	}

	void move(Vector2 direction) {
		//We need to prevent the character from moving in the direction of a wall or a collision
		if (direction.x > direction.y)
			direction.y = 0;
		else
			direction.x = 0;


		RaycastHit2D hit = Physics2D.Raycast(this.transform.position, direction);
		if (hit){ //I hit something I can't move here
			//Rebound
		}

		//this.transform.position += (Vector3)direction.normalized;
		//this.transform.position = new Vector3(Mathf.RoundToInt(this.transform.position.x) + .5f, Mathf.RoundToInt(this.transform.position.y) + .5f, 0);
		//We need to clamp the position to an int and the plus .5 on the x and -.5 on the y

		StartCoroutine(moveTo(direction.normalized, moveSpeed));
	}

	IEnumerator moveTo(Vector2 goal, float speed) {
		canMove = false;
		while (Vector2.Distance((Vector2)this.transform.position, goal) < .1f) {
			this.transform.position = Vector3.Lerp(this.transform.position, goal, speed * Time.deltaTime);
			yield return null;
		}

		this.transform.position = new Vector3(Mathf.RoundToInt(this.transform.position.x), Mathf.RoundToInt(this.transform.position.y), this.transform.position.z);
		canMove = true;
	}
}
