using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private int powerupID;
    [SerializeField] private int totalPowerups;
    [SerializeField] private GameObject plusLife;
    [SerializeField] private GameObject minusLife;
    [SerializeField] private GameObject invincible;
    [SerializeField] private GameObject increaseSize;
    [SerializeField] private int delay;
    [SerializeField] private int totalPowerupsPerLevel;

    private GameObject tempPower;
    Vector3 startPosition;

    public int powerupsCalled;

    private void Start()
    {
        startPosition = new Vector3(Random.Range(-37, 37), 21,0);
        powerupID = Random.Range(1, totalPowerups);
        delay = Random.Range(25, 35);
        Invoke("InstantiatePowerup", delay);
        
    }

    private void InstantiatePowerup()
    {
        powerupsCalled++;
        if(powerupID == 1)
        {
            tempPower = Instantiate(plusLife, startPosition, transform.rotation);
        }
        else if(powerupID == 2)
        {
            tempPower = Instantiate(minusLife, startPosition, transform.rotation);
        }
        else if(powerupID == 3)
        {
            tempPower = Instantiate(invincible, startPosition, transform.rotation);
        }
        else if(powerupID == 4)
        {
            tempPower = Instantiate(increaseSize, startPosition, transform.rotation);
        }
        Destroy(tempPower,30f);
        //delay on how quick powerup is called.

        if(powerupsCalled != totalPowerupsPerLevel)
        {
            delay = Random.Range(40, 60);
            powerupID = Random.Range(1, totalPowerups);
            Invoke("InstantiatePowerup", delay);
        }
    }
}
