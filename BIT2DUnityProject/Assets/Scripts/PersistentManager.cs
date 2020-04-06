using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// This class will remain persistant through the game
// Add tracked variables in here
public class PersistentManager : MonoBehaviour
{
   public static PersistentManager Instance { get; private set; }

    public int testValue;
    public int playerHealth;

    private void Awake()
    {
        // If object instance is empty, fill it
        if (Instance == null)
        {
            Instance = this;
            // This function keeps this instance on scene load
            DontDestroyOnLoad(gameObject);
        }
        // If a new instance is generated that isn't empty, destroy it
        else
        {
            Destroy(gameObject);
        }
    }

}
