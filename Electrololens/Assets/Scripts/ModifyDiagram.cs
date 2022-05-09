using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ModifyDiagram : MonoBehaviour
{

    public LineRenderer foreground;
    public GameObject value;

    public void updateValue(double val, string unit)
    {
        value.GetComponent<TextMeshPro>().text = val + unit;
    }

    public void updateForegroud(double perc)
    {
        foreground.SetPosition(1, new Vector3((float) perc, 0.0f, 0.0f));
    }
}
