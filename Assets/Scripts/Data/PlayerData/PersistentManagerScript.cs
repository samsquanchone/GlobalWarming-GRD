using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentManagerScript : MonoBehaviour
{
    public static PersistentManagerScript instance { get; private set; }
    private double playerWealth = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else
        {
            Destroy(gameObject);
        }

    }

    public void AddWealth(double wealth)
    {

        playerWealth += wealth;
    }

    public double GetWealth()
    {
        return playerWealth;
    }

}
