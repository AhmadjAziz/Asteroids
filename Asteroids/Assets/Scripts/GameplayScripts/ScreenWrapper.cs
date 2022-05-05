using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenWrapper : MonoBehaviour
{
    [SerializeField] private float upperBoundary;
    [SerializeField] private float lowerBoundary;
    [SerializeField] private float leftBoundary;
    [SerializeField] private float rightBoundary;


    private void Update()
    {
        CheckBounds();
    }
    /**
     * If spaceship goes beyond a boundary, it will pop back from another side.
     **/
    public void CheckBounds()
    {
        Vector2 newPosition = transform.position;
        if (this.transform.position.y > upperBoundary)
        {
            newPosition.y = lowerBoundary;
        }
        if (this.transform.position.y < lowerBoundary)
        {
            newPosition.y = upperBoundary;
        }
        if (this.transform.position.x > rightBoundary)
        {
            newPosition.x = leftBoundary;
        }
        if (this.transform.position.x < leftBoundary)
        {
            newPosition.x = rightBoundary;
        }
        this.transform.position = newPosition;
    }
}
