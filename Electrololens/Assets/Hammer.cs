using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : MonoBehaviour
{
    [SerializeField]
    private AudioSource audio;

    private void OnTriggerEnter(Collider other){
        Debug.Log(other.gameObject);
        if(other.gameObject.GetComponent<ProducteurClass>() != null){
            other.gameObject.GetComponent<ProducteurClass>().Delete();
            audio.Play();
        }
        if(other.gameObject.GetComponent<ConsommateurClass>() != null){
            other.gameObject.GetComponent<ConsommateurClass>().Delete();
            audio.Play();
        }
    }
}
