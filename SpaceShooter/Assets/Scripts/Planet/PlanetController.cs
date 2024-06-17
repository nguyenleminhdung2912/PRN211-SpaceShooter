using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetController : MonoBehaviour
{
    public GameObject[] Planets;
    Queue<GameObject> availablesPlanets = new Queue<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        availablesPlanets.Enqueue(Planets[0]);
        availablesPlanets.Enqueue(Planets[1]);
        availablesPlanets.Enqueue(Planets[2]);

        InvokeRepeating("MovePlanetDown", 0, 20f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void MovePlanetDown()
    {
        if(availablesPlanets.Count == 0)
        {
            return;
        }

        GameObject aPlanet = availablesPlanets.Dequeue();
        aPlanet.GetComponent<Planet>().isMoving = true;
    }

    void EnqueuePLanet()
    {
        foreach(GameObject aPlanet in Planets)
        {
            if((aPlanet.transform.position.y < 0) && (!aPlanet.GetComponent<Planet>().isMoving))
            {
                aPlanet.GetComponent<Planet>().ResetPosition();

                availablesPlanets.Enqueue(aPlanet);
            }
        }
    }
}
