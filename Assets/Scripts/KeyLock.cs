using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyLock : PlayerInteractable
{

	public Behaviour normalBehavior;
	public bool isLocked = true;
	public GameObject lockSprite;


	public override void OnPlayerInteration()
	{
		if (CharacterController.instance.item.itemType == Item.ItemType.Key){
			DestroyImmediate(CharacterController.instance.item.gameObject);
			isLocked = false;
			lockSprite.SetActive(false);
			SoundManager.instance.PlaySound(SoundManager.PlayerSound.drop);
			normalBehavior.enabled = true;
			DestroyImmediate(this);
		}
	}

	// Start is called before the first frame update
	void Start()
    {
		base.Start();
		normalBehavior.enabled = false;

	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
