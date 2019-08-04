using UnityEngine;


public class PressurePlate : SwitchBase
{
    private SpriteRenderer plate;
    public float dim = .75f;

    private void Start()
    {
        plate = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        plate.color = new Color(dim, dim, dim);
		state = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        plate.color = new Color(1f, 1f, 1f);
		state = false;
	}
}
