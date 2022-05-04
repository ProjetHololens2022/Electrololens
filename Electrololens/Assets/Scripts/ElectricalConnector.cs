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
        GameObject otherGo = otherConnector.GetComponent<ElectricalConnector>().GetConnectedObject();
        typeObject otherType = otherConnector.GetComponent<ElectricalConnector>().GetTypeObject();
        if(other.gameObject.GetComponent<ElectricalNetwork>() != null){
            this.goConnected = other.gameObject;
            this.typeGo = typeObject.ElectricalCenter;
            if(otherGo != null){
                goConnected.GetComponent<ElectricalNetwork>().addBuilding(otherGo);
                //Replace();
                //otherConnector.GetComponent<ElectricalConnector>().Replace();
            }
        }
        if(other.gameObject.GetComponent<ConsommateurClass>() != null){
            this.goConnected = other.gameObject;
            this.typeGo = typeObject.Consumer;
            if(otherGo != null){
                if(otherType == typeObject.ElectricalCenter){
                    otherGo.GetComponent<ElectricalNetwork>().addBuilding(goConnected);
                    //Replace();
                    //otherConnector.GetComponent<ElectricalConnector>().Replace();
                }
            }
        }
        if(other.gameObject.GetComponent<ProducteurClass>() != null){
            this.goConnected = other.gameObject;
            this.typeGo = typeObject.Producer;
            if(otherGo != null){
                if(otherType == typeObject.ElectricalCenter){
                    otherGo.GetComponent<ElectricalNetwork>().addBuilding(goConnected);
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
