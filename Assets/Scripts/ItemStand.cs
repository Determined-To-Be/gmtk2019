using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStand : PlayerInteractable
{
	Item item = null;

	public override void OnPlayerInteration(){

		Item tmp = item;
		Vector3 ttmp = CharacterController.instance.item.transform.position;

		item = CharacterController.instance.item;
		item.transform.position = this.transform.position;

		CharacterController.instance.item = tmp;
		CharacterController.instance.item.transform.position = ttmp;
	}

	// Start is called before the first frame update
	void Start()
    {
		base.Start();
    }
}
