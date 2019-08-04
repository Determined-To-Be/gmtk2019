using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Switchable : MonoBehaviour
{

	public SwitchBase controlSwitch;

    // Start is called before the first frame update
    void Start(){
		controlSwitch.AddListener(OnSwitchStateChange);
    }

	public abstract void OnSwitchStateChange();
}
