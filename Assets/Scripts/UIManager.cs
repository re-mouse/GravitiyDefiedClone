using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField]
    private Text gameStateText;
    [SerializeField]
    private Text distanceText;

    private bool isForwardWheelInAir;

    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;
    }

    private void Start()
    {
        ScoreTracker.instance.OnReachNewDistanceEvent.AddListener(UpdateReachedDistanceText);
    }

    public void SetWheelState(Wheel type, bool isInAir)
    {
        switch (type)
        {
            case Wheel.Forward:
                isForwardWheelInAir = isInAir;
                break;
        }

        UpdateTextState();
    }

    private void UpdateTextState()
    {
        string stateText;
        if (isForwardWheelInAir)
            stateText = "Wheelie!";
        else
            stateText = "";

        gameStateText.text = stateText;
    }

    private void UpdateReachedDistanceText(float distance)
    {
        distanceText.text = Mathf.RoundToInt(distance).ToString();
    }
}
