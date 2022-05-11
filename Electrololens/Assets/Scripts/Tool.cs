using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool : MonoBehaviour
{
    public bool isNearToolBox = false;
    public void CheckPlace(){
        if(isNearToolBox){
            this.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            this.gameObject.GetComponent<Rigidbody>().useGravity = true;
            isNearToolBox = false;
        } else {
            this.gameObject.GetComponent<Rigidbody>().isKinematic = true;  
            this.gameObject.GetComponent<Rigidbody>().useGravity = false;
        }
    }
}
