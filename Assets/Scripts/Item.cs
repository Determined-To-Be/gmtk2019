using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{

	public abstract void UseItem();

	void OnTriggerEnter2D(Collider2D collision)
	{
		
	}
}
