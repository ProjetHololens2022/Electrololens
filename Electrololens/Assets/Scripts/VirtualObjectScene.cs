using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualObjectScene : MonoBehaviour
{
    protected List<GameObject> children = new List<GameObject>();

    public void AddVirtualChild(GameObject go){
        if(children.Contains(go)) {
            Debug.Log("GameObject added in VirtualObjectScene");
            return;
        }
        if(go.GetComponent<VirtualObject>() == null) {
            Debug.Log("Cannot add a child that isn't a VirtualObject in the scene.");
            return;
        }
        children.Add(go);
        Debug.LogWarning("GameObject added in VirtualObjectScene");
    }

    public void RemoveVirtualChild(GameObject go){
        children.Remove(go);
    }

    public string GetVirtualTreeText(){
        string strTree = "Root {";
        if(children.Count > 0){
            strTree += "\n";
        }
        foreach(GameObject child in children){
            strTree += child.GetComponent<VirtualObject>().GetTreeText("\t") + "\n";
        }
        strTree += "}";
        Debug.Log(strTree);
        return strTree;
    }
}
