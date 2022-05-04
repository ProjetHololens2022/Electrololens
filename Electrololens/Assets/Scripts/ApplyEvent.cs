using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyEvent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void fireEvent()
    {
        GameObject[] cities = GameObject.FindGameObjectsWithTag("City");
        print(cities.Length);
        foreach (var city in cities)
        {
            city.SendMessage("ApplyEvent", "");
        }
    }
}
