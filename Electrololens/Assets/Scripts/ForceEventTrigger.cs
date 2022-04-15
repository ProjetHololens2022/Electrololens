using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;

public class ForceEventTrigger : MonoBehaviour
{

    public void getDisplayedEvent()
    {

        GameObject gsoc = gameObject.transform.parent.gameObject;
        ScrollingObjectCollection soc = gsoc.GetComponent<ScrollingObjectCollection>();
        int fvci = soc.FirstVisibleCellIndex;
        Debug.Log("Hello index is :" + fvci);

        GameObject ggoc = GameObject.Find("Container").gameObject.transform.GetChild(0).gameObject;

        GameObject selected = ggoc.transform.GetChild(fvci).gameObject;
        Debug.Log(selected.name);
    }
}
