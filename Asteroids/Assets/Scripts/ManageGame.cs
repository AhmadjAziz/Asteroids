using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageGame : MonoBehaviour
{
    [SerializeField] private int numAsteroids; // total number of asteroids in the scene.



    public void UpdateNumAsteroids(int change)
    {
        numAsteroids += change;
    }
}
