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
		StartCoroutine(moveTo(this.transform.position + dir, 16f));
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

	protected override void OnTick()
	{
		//throw new System.NotImplementedException();
	}

	// Start is called before the first frame update
	void Start()
    {
		base.Start();   
    }
}
