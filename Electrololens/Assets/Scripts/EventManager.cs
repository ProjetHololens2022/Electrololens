using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit.Experimental.UI;

public class EventManager : MonoBehaviour
{
    private List<NoRotationDockable> events = new List<NoRotationDockable>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddEvent(NoRotationDockable e)
    {
        events.Add(e);
        if(e.GetComponent<EventDockable>().type.Equals(TypeAgent.CONSUMER))
        {
            GameObject[] consumer = GameObject.FindGameObjectsWithTag("Consumer");
            foreach (var cons in consumer)
            {
                print("eventmanager");
                cons.SendMessage("ApplyEvent", e);
            }
        }
        else
        {
            GameObject[] producers = GameObject.FindGameObjectsWithTag("Producer");
            foreach (var prod in producers)
            {
                prod.SendMessage("ApplyEvent", e);
            }
        }
    }

    public void RemoveEvent(NoRotationDockable e)
    {
         if (e.GetComponent<EventDockable>().type.Equals(TypeAgent.CONSUMER))
        {
            GameObject[] consumer = GameObject.FindGameObjectsWithTag("Consumer");
            foreach (var cons in consumer)
            {
                cons.SendMessage("RemoveEvent", e);
            }
        }
        else
        {
            GameObject[] producers = GameObject.FindGameObjectsWithTag("Producer");
            foreach (var prod in producers)
            {
                prod.SendMessage("RemoveEvent", e);
            }
        }
    }

    public List<NoRotationDockable> GetEvents()
    {
        return events;
    }
}
