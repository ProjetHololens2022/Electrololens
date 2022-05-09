using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other){
        Debug.Log(other.gameObject);
        if(other.gameObject.GetComponent<ProducteurClass>() != null){
            other.gameObject.GetComponent<ProducteurClass>().Delete();
        }
        if(other.gameObject.GetComponent<ConsommateurClass>() != null){
            other.gameObject.GetComponent<ConsommateurClass>().Delete();
        }
    }
}
