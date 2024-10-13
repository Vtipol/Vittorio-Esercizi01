using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CarMovement : MonoBehaviour
{
    public float speed = 5f; 
    private bool shouldMove = false; 

    public static UnityEvent<string> onRaceFinish = new UnityEvent<string>(); 
    private static bool raceEnded = false; 

    public void StartMoving()
    {
        shouldMove = true;
    }

    void Update()
    {
        if (shouldMove && !raceEnded)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Finish") && !raceEnded)
        {
            raceEnded = true; 
            shouldMove = false; 
            string carName = gameObject.name; 
            onRaceFinish.Invoke(carName); 
        }
    }

    public void StopMoving()
    {
        shouldMove = false;
    }
}
