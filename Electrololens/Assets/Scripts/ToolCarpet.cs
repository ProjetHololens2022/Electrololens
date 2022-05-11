using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolCarpet : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Tool"){
            other.GetComponent<Tool>().isNearToolBox = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.gameObject.tag == "Tool"){
            other.GetComponent<Tool>().isNearToolBox = false;
            other.attachedRigidbody.useGravity = false;
            other.attachedRigidbody.isKinematic = true;
        }
    }
}
