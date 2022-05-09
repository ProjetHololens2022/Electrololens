using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ModifyDiagram : MonoBehaviour
{

    public GameObject foreground;
    public GameObject value;

    public void updateValue(double val)
    {
        value.GetComponent<TextMeshPro>().text = val.ToString() + "%";
    }
}
