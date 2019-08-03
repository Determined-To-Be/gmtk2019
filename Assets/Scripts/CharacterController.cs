using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{

	public float maxSpeed = 1f;

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	void move(Vector2 direction) {
		//We need to prevent the character from moving in the direction of a wall or a collision
		RaycastHit2D hit = Physics2D.Raycast(this.transform.position, direction);
		if (hit){ //I hit something I can't move here

		}
	}
}
