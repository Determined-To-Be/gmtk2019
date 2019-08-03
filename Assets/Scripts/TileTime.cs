using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TileTime : MonoBehaviour
{

	UnityEvent tile_event = new UnityEvent();

	public static TileTime _instance;
	public static TileTime instance {
		get {
			if (_instance != null)
			{
				return _instance;
			}

			GameObject go = new GameObject();
			_instance = go.AddComponent<TileTime>();
			return _instance;
		}

	}

    void Awake()
    {

		if(_instance == null){
			_instance = this.GetComponent<TileTime>();
		}
    }

	void Tick() {
		tile_event.Invoke();
	}

	public void AddListener(UnityAction call) {
		tile_event.AddListener(call);
	}

	public void RemoveListener(UnityAction call){
		tile_event.RemoveListener(call);
	}
}
