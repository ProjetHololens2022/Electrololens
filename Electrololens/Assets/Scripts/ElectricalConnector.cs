using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum typeObject
{
    None,
    ElectricalCenter,
    Consumer,
    Producer
}

public class ElectricalConnector : MonoBehaviour
{

    [SerializeField]
    private GameObject otherConnector = null;
    [SerializeField]
    private AudioSource audio;

    private GameObject goConnected = null;

    private typeObject typeGo = typeObject.None;

    private Vector3 startPos;
    private Quaternion startRotation;

    void Start(){
        startPos = this.transform.position;
        startRotation = this.transform.rotation;
    }

    public GameObject GetConnectedObject(){
        return goConnected;
    }

    public typeObject GetTypeObject(){
        return typeGo;
    }

    public void Replace(){
        this.transform.position = startPos;
        this.transform.rotation = startRotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject go = other.gameObject;
        GameObject otherGo = otherConnector.GetComponent<ElectricalConnector>().GetConnectedObject();
        print(go.name);
        if(go.name == "Sphere"){
            go = go.transform.parent.gameObject;
        }
        typeObject otherType = otherConnector.GetComponent<ElectricalConnector>().GetTypeObject();
        if(go.GetComponent<ElectricalNetwork>() != null){
            this.goConnected = go;
            this.typeGo = typeObject.ElectricalCenter;
            if(otherGo != null){
                goConnected.GetComponent<ElectricalNetwork>().addBuilding(otherGo);
                audio.Play();
                //Replace();
                //otherConnector.GetComponent<ElectricalConnector>().Replace();
            }
        }
        if(go.GetComponent<ConsommateurClass>() != null){
            this.goConnected = go;
            this.typeGo = typeObject.Consumer;
            if(otherGo != null){
                if(otherType == typeObject.ElectricalCenter){
                    otherGo.GetComponent<ElectricalNetwork>().addBuilding(goConnected);
                    audio.Play();
                    //Replace();
                    //otherConnector.GetComponent<ElectricalConnector>().Replace();
                }
            }
        }
        if(go.GetComponent<ProducteurClass>() != null){
            this.goConnected = go;
            this.typeGo = typeObject.Producer;
            if(otherGo != null){
                if(otherType == typeObject.ElectricalCenter){
                    otherGo.GetComponent<ElectricalNetwork>().addBuilding(goConnected);
                    audio.Play();
                    //Replace();
                    //otherConnector.GetComponent<ElectricalConnector>().Replace();
                }
            }
        }
    }

    private void OnTriggerExit(Collider other){
        this.goConnected = null;
        this.typeGo = typeObject.None;
    }

}
