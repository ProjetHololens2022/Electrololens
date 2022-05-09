using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrenchHitter : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<ProducteurClass>() != null)
        {
            other.gameObject.GetComponent<ProducteurClass>().reparationEtat();
        }
    }
}
