using UnityEngine;
using UnityEngine.Events;

public class PressurePlate : MonoBehaviour
{
    private SpriteRenderer plate;
    private UnityEvent stepOn = new UnityEvent();

    public float dim = .75f;

    private void Start()
    {
        plate = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        plate.color = new Color(dim, dim, dim);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        plate.color = new Color(1f, 1f, 1f);
    }

    public void AddListener(UnityAction call)
    {
        stepOn.AddListener(call);
    }

    public void RemoveListener(UnityAction call)
    {
        stepOn.RemoveListener(call);
    }
}
