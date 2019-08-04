using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBox : PushableBox
{
	public bool isLocked = true;
	public GameObject lockSprite;



	public override void OnPlayerInteration()
	{
		if (CharacterController.instance.item.itemType == Item.ItemType.Key) {
			isLocked = false;
			lockSprite.SetActive(false);
			SoundManager.Instance.PlaySound(SoundManager.PlayerSound.drop);
		}

		Vector3 dir = this.transform.position - CharacterController.instance.transform.position;
		dir = dir.normalized;

		if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
			dir.y = 0;
		else
			dir.x = 0;

		if (isLocked == false) {
			move(dir);
		}
	}

	// Start is called before the first frame update
	void Start()
    {
		base.Start();
    }

}
