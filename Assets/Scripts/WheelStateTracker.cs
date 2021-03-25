using UnityEngine;

public enum Wheel {  Forward }
public class WheelStateTracker : MonoBehaviour
{
    [SerializeField]
    private Wheel wheelType;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Rideable"))
            UIManager.instance.SetWheelState(wheelType, false);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Rideable"))
            UIManager.instance.SetWheelState(wheelType, true);
    }
}
