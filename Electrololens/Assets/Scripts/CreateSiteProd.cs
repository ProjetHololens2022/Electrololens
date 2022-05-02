using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateSiteProd : MonoBehaviour
{
    [SerializeField]
    private GameObject prefabPanier;
    private GameObject prefabPlatform;
    private Vector3 coordBase = new Vector3(0.0f, 0.0f, 0.0f);
    private GameObject platform;
    //private Transform ellipse = null;
    Camera camera;

    public void Start()
    {
        coordBase = prefabPanier.transform.localPosition;
        platform = GameObject.FindGameObjectsWithTag("Plateforme")[0].transform.GetChild(1).gameObject;
        prefabPlatform = GameObject.FindGameObjectsWithTag("CentraleNucleaire")[0];
    }

    public void UnholdPrefabPanier()
    {
        float prefabpanierX = prefabPanier.transform.position.x;
        float prefabpanierZ = prefabPanier.transform.position.z;
        //Si le prefab est dans la gamezone.platform
        if (isInsinePlatform(prefabpanierX, prefabpanierZ))
        {
/*            Vector3 camVect;
            Debug.Log(Camera.current);
            Vector3 camForward = Camera.main.transform.forward;
            camVect = Camera.main.transform.position + new Vector3(Vector3.Dot(camForward, new Vector3(1f, 0f, 0f)), 0f, Vector3.Dot(camForward, new Vector3(0f, 0f, 1f))) * 1.2f;*/
            GameObject go = (GameObject)Instantiate(prefabPlatform, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
            go.transform.parent = platform.transform;
            go.transform.localPosition = new Vector3(prefabpanierX*50.0f, 0.0f, prefabpanierX*50.0f);
            Vector3 scale = platform.transform.GetChild(0).localScale;
            go.transform.localScale = scale;
        }
        prefabPanier.transform.localPosition = coordBase;
    }

    public bool isInsinePlatform(float X, float Z)
    {
        
        if (platform.transform.position.x - 0.3f < X & X < platform.transform.position.x + 0.3f & platform.transform.position.z - 0.2f < Z & Z < platform.transform.position.z + 0.2f)
        {
            return true;
        }
        return false;
        /*if (ellipse == null)
        {
            return;
        }
        const float long_diameter = 0.28f;
        const float small_diameter = 0.18f;
        Vector3 localDir = transform.Position - ellipse.position; //Angle 0
        localDir = Quaternion.Euler(0, ellipse.eulerAngles.y, 0) * localDir; //Angle X
        float equation = Mathf.Pow(localDir.x, 2.0f) / Mathf.Pow(long_diameter, 2.0f) + Mathf.Pow(localDir.z, 2.0f) / Mathf.Pow(small_diameter, 2.0f);
        if (equation > 1.0f)
        {
            localDir = Vector3.Normalize(localDir);
            localDir = Quaternion.Euler(0, -2.0f * ellipse.eulerAngles.y, 0) * localDir; //Angle -X
            localDir.x *= long_diameter;
            localDir.z *= small_diameter;
            localDir = Quaternion.Euler(0, ellipse.eulerAngles.y, 0) * localDir; //Angle 0
            transform.Position = ellipse.position + localDir;
        }*/
    }
}
