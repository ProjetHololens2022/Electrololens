using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;

public class ForceEventTrigger : MonoBehaviour
{

    private static int index;
    public int nbItems;

    private void Start()
    {
        index = 0;
        nbItems = nbItems - 1;
        Debug.Log("Index initialized : " + index);
    }

    public string getDisplayedEvent()
    {
        Debug.Log("display index : " + index);
        GameObject gsoc = gameObject.transform.parent.gameObject;
        ScrollingObjectCollection soc = gsoc.GetComponent<ScrollingObjectCollection>();
        GameObject ggoc = GameObject.Find("Container").gameObject.transform.GetChild(0).gameObject;
        GameObject selected = ggoc.transform.GetChild(index).gameObject;

        return selected.name;
    }

    public void fireEvent()
    {
        GameObject[] cities = GameObject.FindGameObjectsWithTag("City");
        print(cities.Length);
        foreach (var city in cities)
        {
            city.SendMessage("ApplyEvent", getDisplayedEvent());
        }
    }

    public void NextItem()
    {
        GameObject gsoc = gameObject.transform.parent.gameObject;
        ScrollingObjectCollection soc = gsoc.GetComponent<ScrollingObjectCollection>();
        index = index < nbItems ? ++index : 0;
        soc.MoveToIndex(index);
        Debug.Log("next index : " + index);
    }

    public void PrevItem()
    {
        GameObject gsoc = gameObject.transform.parent.gameObject;
        ScrollingObjectCollection soc = gsoc.GetComponent<ScrollingObjectCollection>();
        index = index > 0 ? --index : nbItems;
        soc.MoveToIndex(index);
        Debug.Log("prev index : " + index);
    }
}
