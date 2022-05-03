using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateSiteProd : MonoBehaviour
{
    [SerializeField]
    private GameObject prefabPanier;
    [SerializeField]
    private GameObject prefabPlatform;
    private Vector3 coordBase = new Vector3(0.0f, 0.0f, 0.0f);
    private GameObject platform;
    //private Transform ellipse = null;

    public void Start()
    {
        coordBase = prefabPanier.transform.localPosition;
        platform = GameObject.FindGameObjectsWithTag("Plateforme")[0].transform.GetChild(1).gameObject;
    }

    public void holdPrefabPanier()
    {
        GameObject go = (GameObject)Instantiate(prefabPanier, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
        go.transform.parent = prefabPanier.transform.parent;
        go.transform.localPosition = coordBase;
        Vector3 scale = platform.transform.GetChild(0).localScale;
        go.transform.localScale = scale;
    }

    public void UnholdPrefabPanier()
    {
        float prefabpanierX = prefabPanier.transform.position.x;
        float prefabpanierZ = prefabPanier.transform.position.z;
        //Si le prefab est dans la gamezone.platform
        if (isInsinePlatform(prefabpanierX, prefabpanierZ))
        {
            GameObject go = (GameObject)Instantiate(prefabPlatform, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
            go.transform.parent = platform.transform;
            go.transform.localPosition = new Vector3((prefabpanierX-platform.transform.position.x)*50.0f, 0.0f, (prefabpanierZ-platform.transform.position.z)*50.0f);
            Vector3 scale = platform.transform.GetChild(0).localScale;
            go.transform.localScale = scale;
        }
        Destroy(prefabPanier);
    }

    public bool isInsinePlatform(float X, float Z)
    {
        float platformX = platform.transform.position.x;
        float platformZ = platform.transform.position.z;
        if (platformX - 0.3f < X & X < platformX + 0.3f & platformZ - 0.2f < Z & Z < platformZ + 0.2f)
        {
            return true;
        }
        return false;
    }
}
