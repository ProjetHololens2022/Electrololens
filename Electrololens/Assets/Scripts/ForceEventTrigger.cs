using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;

public class ForceEventTrigger : MonoBehaviour
{

    private static int index;
    public int nbItems;

    private void Start()
    {
        index = 0;
        Debug.Log("Index initialized : " + index);
    }

    public void getDisplayedEvent()
    {
        Debug.Log("display index : " + index);
        GameObject gsoc = gameObject.transform.parent.gameObject;
        ScrollingObjectCollection soc = gsoc.GetComponent<ScrollingObjectCollection>();
        GameObject ggoc = GameObject.Find("Container").gameObject.transform.GetChild(0).gameObject;
        GameObject selected = ggoc.transform.GetChild(index).gameObject;
        Debug.Log(selected.name);
    }

    public void fireEvent()
    {
        // fire event
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
        index = index > 0 ? --index : 0;
        soc.MoveToIndex(index);
        Debug.Log("prev index : " + index);
    }
}
