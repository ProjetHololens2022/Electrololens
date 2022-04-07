using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameArea : MonoBehaviour
{

    [SerializeField]
    private GameObject platform;

    void Start(){
        platform.SetActive(false);
    }

    public void LockPlace(){
        gameObject.GetComponent<Microsoft.MixedReality.Toolkit.UI.ObjectManipulator>().enabled = false;
        gameObject.GetComponent<BoxCollider>().enabled = false;
        platform.SetActive(true);
    }
}
