using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SwitchBase : MonoBehaviour
{
	protected UnityEvent switch_event = new UnityEvent();

	// Start is called before the first frame update
	void Start()
    {
		TileTime.instance.AddListener(Tick);    
    }

	void Tick() {

	}

	public void AddListener(UnityAction call)
	{
		switch_event.AddListener(call);
	}

	public void RemoveListener(UnityAction call)
	{
		switch_event.RemoveListener(call);
	}
}
