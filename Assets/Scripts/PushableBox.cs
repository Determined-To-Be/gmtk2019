using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushableBox : PlayerInteractable
{
	public override void OnPlayerInteration()
	{
		Vector3 dir = this.transform.position - CharacterController.instance.transform.position;
		dir = dir.normalized;

		if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
			dir.y = 0;
		else
			dir.x = 0;

		//SoundManager.instance.PlaySound(SoundManager.PlayerSound.wall);
		move(dir);
		//StartCoroutine(moveTo(this.transform.position + dir, 16f));
	}

	protected void move(Vector2 direction)
	{
		//We need to prevent the character from moving in the direction of a wall or a collision
		if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
			direction.y = 0;
		else
			direction.x = 0;

		List<RaycastHit2D> hits = new List<RaycastHit2D>();
		if (direction == Vector2.up) {

			hits.Add(Physics2D.Raycast(this.transform.position + Vector3.down * .1f + Vector3.left * .1f, direction, .8f, ~LayerMask.GetMask("Player", "Enviroment", "Item")));
			hits.Add(Physics2D.Raycast(this.transform.position + Vector3.down * .1f + Vector3.right * .1f, direction, .8f, ~LayerMask.GetMask("Player", "Enviroment", "Item")));

			Debug.DrawRay(this.transform.position + Vector3.down * .1f + Vector3.left * .1f, direction * .8f);
			Debug.DrawRay(this.transform.position + Vector3.down * .1f + Vector3.right * .1f, direction * .8f);

		}
		else if (direction == Vector2.down){

			hits.Add(Physics2D.Raycast(this.transform.position + Vector3.down * .1f + Vector3.left * .1f, direction, 1.1f, ~LayerMask.GetMask("Player", "Enviroment", "Item")));
			hits.Add(Physics2D.Raycast(this.transform.position + Vector3.down * .1f + Vector3.right * .1f, direction, 1.1f, ~LayerMask.GetMask("Player", "Enviroment", "Item")));

			Debug.DrawRay(this.transform.position + Vector3.down * .1f + Vector3.left * .1f, direction * 1.1f);
			Debug.DrawRay(this.transform.position + Vector3.down * .1f + Vector3.right * .1f, direction * 1.1f);
		} else {
			hits.Add(Physics2D.Raycast(this.transform.position + Vector3.down * .1f, direction, 1.1f, ~LayerMask.GetMask("Player", "Enviroment", "Item")));
			Debug.DrawRay(this.transform.position + Vector3.down * .1f, direction * 1.1f);
		}

		foreach (RaycastHit2D hit in hits){
			if (hit)
			{
				//Debug.Log("I hit a wall");
				return;
			}
		}

		StartCoroutine(moveTo(this.transform.position + (Vector3)direction.normalized, 16f));
	}


	IEnumerator moveTo(Vector2 goal, float speed)
	{
		Vector3 lastpos = this.transform.position + Vector3.down * .1f;

		while (Vector2.Distance((Vector2)this.transform.position, goal) > .05f)
		{
			this.transform.position = Vector3.Lerp(this.transform.position, goal, speed * Time.deltaTime);
			yield return null;
		}
		this.transform.position = new Vector3(Mathf.RoundToInt(this.transform.position.x), Mathf.RoundToInt(this.transform.position.y), this.transform.position.z);
	}

	// Start is called before the first frame update
	void Start()
    {
		base.Start();   
    }
}
