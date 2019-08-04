using UnityEngine;
using UnityEngine.Events;

public class LightBox : MonoBehaviour
{
    bool wasState = false;

    public bool state = false;
    public UnityEvent switch_enable = new UnityEvent();
    public UnityEvent switch_disable = new UnityEvent();

    void Start()
    {
        TileTime.instance.AddListener(OnTick);
    }

    void OnTick()
    {
        if (state && !wasState)
        {
            switch_enable.Invoke();
            wasState = true;
        }
        else if (!state && wasState)
        {
            switch_disable.Invoke();
            wasState = false;
        }
        state = false;
    }
}
