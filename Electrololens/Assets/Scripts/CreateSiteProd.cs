using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit.Utilities;
using UnityEngine;

public class CreateSiteProd : MonoBehaviour
{
    [SerializeField]
    private GameObject prefabPanier;
    [SerializeField]
    private GameObject prefabPlatform;
    [SerializeField]
    private GameObject prefabProjection;
    private GameObject platform;
    private Vector3 coordBase = new Vector3(0.0f, 0.0f, 0.0f);

    private GameObject prefabPlatformProjection = null;

    public void Start()
    {
        coordBase = prefabPanier.transform.localPosition;
        platform = GameObject.FindGameObjectsWithTag("Plateforme")[1].gameObject;
    }

    public void holdPrefabPanier()
    {
        //On prend un des �l�ments du panier
        int nbVille = 0;
        for (int i = 0; i < platform.transform.childCount; ++i)
        { 
            if (platform.transform.GetChild(i).GetComponent<ConsommateurClass>() != null)
            {
                nbVille++;
            }
        }
        Debug.Log(prefabPanier);
        GameObject go = (GameObject)Instantiate(prefabPanier, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.Euler(0, platform.transform.eulerAngles.y, 0));
        go.transform.parent = prefabPanier.transform.parent;
        go.transform.localPosition = coordBase;
        Vector3 scale = prefabPanier.transform.localScale;
        go.transform.localScale = scale;
        if (prefabPanier.tag == "Nucleaire" && nbVille < 5)
        {
            Destroy(prefabPanier);
        }
        else
        {
            Debug.Log("En avant la coroutine");
            StartCoroutine("ProjectionPrefabPanier");
        }
    }

    IEnumerator ProjectionPrefabPanier()
    {
        
        while (true)
        {
            float prefabpanierX = prefabPanier.transform.position.x;
            float prefabpanierZ = prefabPanier.transform.position.z;
            if (isInsinePlatform(prefabPanier.transform.position) && prefabProjection.activeSelf == false)
            {
                prefabProjection.transform.position = new Vector3(prefabpanierX, platform.transform.position.y, prefabpanierZ);
                prefabProjection.SetActive(true);
            }
            else if (isInsinePlatform(prefabPanier.transform.position) && prefabProjection.activeSelf == true)
            {
                prefabProjection.transform.position = new Vector3(prefabpanierX, platform.transform.position.y, prefabpanierZ);
            }
            else if (prefabProjection.activeSelf == true)
            {
                prefabProjection.SetActive(false);
            }
            yield return 0;
        }
    }

    public void UnholdPrefabPanier()
    {
        StopCoroutine("ProjectionPrefabPanier");
        float prefabpanierX = prefabPanier.transform.position.x;
        float prefabpanierZ = prefabPanier.transform.position.z;
        Vector3 prefabPanierPoint = prefabPanier.transform.position;
        //Si le prefab est dans la gamezone.platform
        if (isInsinePlatform(prefabPanierPoint))
        {
            GameObject go = (GameObject)Instantiate(prefabPlatform, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
            go.transform.parent = platform.transform;

            Vector3 localDir = prefabPanierPoint-platform.transform.position; //Angle 0
            localDir = Quaternion.Euler(0, platform.transform.eulerAngles.y, 0) * localDir; //Angle X
            localDir = Vector3.Normalize(localDir);
            localDir = Quaternion.Euler(0, -2.0f * platform.transform.eulerAngles.y, 0) * localDir; //Angle -X
            localDir.x *= 0.225f;
            localDir.z *= 0.15f;
            localDir = Quaternion.Euler(0, platform.transform.eulerAngles.y, 0) * localDir; //Angle 0
            localDir.y = 0.0f;

            go.transform.position = platform.transform.position + localDir;
            go.transform.localScale = new Vector3(1.0f,1.0f,1.0f);
            go.transform.rotation = prefabPanier.transform.rotation;
            if(go.GetComponent<ProducteurClass>() != null)
            {
                go.GetComponent<ProducteurClass>().startDegradation();
            }
            
        }
        Destroy(prefabPanier);
    }

    public bool isInsinePlatform(Vector3 pointToTest)
    {
        if (platform.transform == null)
        {
            Debug.Log("Ellipse null");
            return false;
        }
        const float long_diameter = 0.3f;
        const float small_diameter = 0.2f;
        pointToTest = pointToTest - platform.transform.position; //Angle 0
        Debug.Log(pointToTest);
        pointToTest = Quaternion.Euler(0, -platform.transform.eulerAngles.y, 0) * pointToTest; //Angle X
        Debug.Log(pointToTest);
        float equation = Mathf.Pow(pointToTest.x, 2.0f) / Mathf.Pow(long_diameter, 2.0f) + Mathf.Pow(pointToTest.z, 2.0f) / Mathf.Pow(small_diameter, 2.0f);
        if (equation <= 1.0f)
        {
            return true;
        }
        return false;
    }
}
