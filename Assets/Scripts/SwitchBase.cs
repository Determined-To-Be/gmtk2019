using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SwitchBase : MonoBehaviour
{
	public bool state = false;
	public UnityEvent switch_enable = new UnityEvent();
	public UnityEvent switch_disable = new UnityEvent();
	


	// Start is called before the first frame update
	void Start()
    {
		TileTime.instance.AddListener(OnTick);
    }

	void OnTick() {
		if (state) {
			switch_enable.Invoke();
		} else {
			switch_disable.Invoke();
		}
	}
}
