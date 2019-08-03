using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileTime : MonoBehaviour
{

	public static TileTime instance;

	public static void create() {
		if (instance == null) {
			return;
		}

		GameObject go = Instantiate(new GameObject());
		go.AddComponent<TileTime>();
	}


    void Awake()
    {
		instance = this.GetComponent<TileTime>();
		create();
    }

}
