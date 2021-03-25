using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnReachNewDistance : UnityEvent<float> { }

public class ScoreTracker : MonoBehaviour
{
    public static ScoreTracker instance;

    public OnReachNewDistance OnReachNewDistanceEvent = new OnReachNewDistance();

    private float maxPassedDistance;
    private float startXPos;

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);
        else
            instance = this;
    }

    private void Start()
    {
        startXPos = transform.position.x;
    }

    void Update()
    {
        if (transform.position.x - startXPos > maxPassedDistance)
        {
            maxPassedDistance = transform.position.x - startXPos;
            OnReachNewDistanceEvent.Invoke(maxPassedDistance);
        }
    }
}
