using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBox : SwitchBase
{
    // Start is called before the first frame update
    void Start()
    {
		base.Start();
    }

    // Update is called once per frame
    void onTick()
    {
		this.state = false;
    }
}
