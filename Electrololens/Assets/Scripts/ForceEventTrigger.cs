using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;

public class ForceEventTrigger : MonoBehaviour
{

    public void getDisplayedEvent()
    {

        GameObject gsoc = gameObject.transform.parent.gameObject;
        ScrollingObjectCollection soc = gsoc.GetComponent<ScrollingObjectCollection>();
        for (int i = 0; i < 4; i++)
        {
            Debug.Log("loop " + i + ": " + soc.FirstVisibleCellIndex + " visible : " + soc.IsCellVisible(i));
        }
/*        int fvci = soc.FirstVisibleCellIndex;

        GameObject ggoc = GameObject.Find("Container").gameObject.transform.GetChild(0).gameObject;
        GameObject selected = ggoc.transform.GetChild(fvci).gameObject;
        Debug.Log(selected.name);*/
    }

    public void NextItem()
    {
        GameObject gsoc = gameObject.transform.parent.gameObject;
        ScrollingObjectCollection soc = gsoc.GetComponent<ScrollingObjectCollection>();
        soc.MoveByTiers(1);
    }

    public void PrevItem()
    {
        GameObject gsoc = gameObject.transform.parent.gameObject;
        ScrollingObjectCollection soc = gsoc.GetComponent<ScrollingObjectCollection>();
        soc.MoveByTiers(-1);
    }
}
