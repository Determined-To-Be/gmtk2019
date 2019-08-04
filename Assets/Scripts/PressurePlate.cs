using UnityEngine;


public class PressurePlate : SwitchBase
{
    private SpriteRenderer plate;
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
}
