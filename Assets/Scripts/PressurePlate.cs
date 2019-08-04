using UnityEngine;
using UnityEngine.Events;

public class PressurePlate : MonoBehaviour
{
    private SpriteRenderer plate;
    bool wasState = false;

    public float dim = .75f;
    public bool state = false;
    public UnityEvent switch_enable = new UnityEvent();
    public UnityEvent switch_disable = new UnityEvent();

    void Start()
    {
        TileTime.instance.AddListener(OnTick);
        plate = GetComponent<SpriteRenderer>();
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
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        plate.color = new Color(dim, dim, dim);
        state = true;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        plate.color = new Color(1f, 1f, 1f);
        state = false;
    }
}
