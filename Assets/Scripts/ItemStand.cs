using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStand : PlayerInteractable
{
	Item item = null;

	public override void OnPlayerInteration(){

		Item tmp = item;
		Vector3 ttmp = CharacterController.instance.transform.position;

		if (CharacterController.instance.item != null) {
			item = CharacterController.instance.item;
			item.transform.position = this.transform.position;
			ttmp = CharacterController.instance.item.transform.position;
		}
		else {
			item = null;
		}

		if (tmp != null)
		{
			
			CharacterController.instance.item = tmp;
			CharacterController.instance.item.transform.position = ttmp;
		} else {
			CharacterController.instance.item = null;
		}
		
	}

	// Start is called before the first frame update
	void Start()
    {
		base.Start();
    }
}
