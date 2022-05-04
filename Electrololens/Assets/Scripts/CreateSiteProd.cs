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
    private Transform ellipse;
    //private Transform ellipse = null;

    public void Start()
    {
        coordBase = prefabPanier.transform.localPosition;
        platform = GameObject.FindGameObjectsWithTag("Plateforme")[0].transform.GetChild(1).gameObject;
        ellipse = transform.parent.parent.gameObject.transform.GetChild(1);
    }

    public void holdPrefabPanier()
    {
        GameObject go = (GameObject)Instantiate(prefabPanier, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.Euler(0, ellipse.eulerAngles.y, 0));
        go.transform.parent = prefabPanier.transform.parent;
        go.transform.localPosition = coordBase;
        Vector3 scale = prefabPanier.transform.localScale;
        go.transform.localScale = scale;
    }

    public void UnholdPrefabPanier()
    {
        float prefabpanierX = prefabPanier.transform.position.x;
        float prefabpanierZ = prefabPanier.transform.position.z;
        //Si le prefab est dans la gamezone.platform
        if (isInsinePlatform(prefabPanier.transform.position))
        {
            Debug.Log("Instantiation");
            GameObject go = (GameObject)Instantiate(prefabPlatform, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
            go.transform.parent = platform.transform;
            //go.transform.position = Quaternion.Euler(0, ellipse.eulerAngles.y, 0) * go.transform.position;
            go.transform.localPosition = new Vector3((prefabpanierX-platform.transform.position.x)*50.0f, 0.0f, (prefabpanierZ-platform.transform.position.z)*50.0f);
            Debug.Log(go.transform.localPosition);
            go.transform.localPosition = Quaternion.Euler(0, ellipse.eulerAngles.y, 0) * go.transform.localPosition;
            Debug.Log(go.transform.localPosition);
            Vector3 scale = platform.transform.GetChild(0).localScale;
            go.transform.localScale = scale;
        }
        Destroy(prefabPanier);
    }

    public bool isInsinePlatform(Vector3 pointToTest)
    {
        Debug.Log("Debut du test");
        if (ellipse == null)
        {
            Debug.Log("Ellipse null");
            return false;
        }
        const float long_diameter = 0.3f;
        const float small_diameter = 0.2f;
        pointToTest = pointToTest - ellipse.position; //Angle 0
        Debug.Log(pointToTest);
        Debug.Log(ellipse.eulerAngles.y);
        pointToTest = Quaternion.Euler(0, ellipse.eulerAngles.y, 0) * pointToTest; //Angle X
        Debug.Log(pointToTest);
        float equation = Mathf.Pow(pointToTest.x, 2.0f) / Mathf.Pow(long_diameter, 2.0f) + Mathf.Pow(pointToTest.z, 2.0f) / Mathf.Pow(small_diameter, 2.0f);
        Debug.Log(equation);
        if (equation <= 1.0f)
        {
            Debug.Log("True");
            return true;
        }
        Debug.Log("False");
        return false;
    }
}
