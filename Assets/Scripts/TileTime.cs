using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileTime : MonoBehaviour
{


	public static TileTime _instance;
	public static TileTime instance {
		get {
			if (_instance != null)
			{
				return _instance;
			}

			GameObject go = Instantiate(new GameObject());
			go.AddComponent<TileTime>();
			return _instance;
		}

	}

    void Awake()
    {
		_instance = this.GetComponent<TileTime>();
    }
}
