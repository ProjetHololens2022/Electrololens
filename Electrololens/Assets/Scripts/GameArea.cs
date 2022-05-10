using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameArea : MonoBehaviour
{

    [SerializeField]
    private GameObject platform;
    [SerializeField]
    private GameObject panier;
    [SerializeField]
    private GameObject tools;
    [SerializeField]
    private GameObject dock;
    [SerializeField]
    private GameObject buttonSave;
    [SerializeField]
    private GameObject buttonLoad;
    [SerializeField]
    private GameObject infopanel;

    void Start(){
        platform.SetActive(false);
        panier.SetActive(false);
        tools.SetActive(false);
        dock.SetActive(false);
        buttonSave.SetActive(false);
        buttonLoad.SetActive(false);
    }

    public void LockPlace(){
        gameObject.GetComponent<Microsoft.MixedReality.Toolkit.UI.ObjectManipulator>().enabled = false;
        gameObject.GetComponent<BoxCollider>().enabled = false;
        platform.SetActive(true);
        panier.SetActive(true);
        tools.SetActive(true);
        dock.SetActive(true);
        buttonSave.SetActive(true);
        buttonLoad.SetActive(true);
        infopanel.SetActive(true);

        for(int i = 0; i < platform.transform.childCount; ++i){
            StartCoroutine(popPunsPlatform(platform.transform.GetChild(i)));
            if(platform.transform.GetChild(i).GetComponent<ProducteurClass>() != null)
            {
                platform.transform.GetChild(i).GetComponent<ProducteurClass>().startDegradation();
            }
        }
        for(int i = 0; i < tools.transform.childCount; ++i){
            StartCoroutine(popPunsPlatform(tools.transform.GetChild(i)));
        }
        for (int i = 0; i < panier.transform.childCount; ++i)
        {
            StartCoroutine(popPunsPanier(panier.transform.GetChild(i)));
        }
    }

    IEnumerator popPunsPlatform(Transform child){
        float scale = 0.0f;
        float time = 0.5f;
        float rotation = 0.0f;
        float baseAngle = child.eulerAngles.y;

        while(scale < 1.0f)
        {
            child.localScale = new Vector3(scale,scale,scale);
            child.rotation = Quaternion.Euler(0,rotation + baseAngle,0);
            yield return 0;
            scale += (1.0f/time) * Time.deltaTime;
            rotation += (360.0f/time) * Time.deltaTime;
            Mathf.Clamp(scale,0.0f,1.0f);
            Mathf.Clamp(rotation,0.0f,360.0f);
        }
    }

    IEnumerator popPunsPanier(Transform child)
    {
        float scalex = 0.0f;
        float scaley = 0.0f;
        float scalez = 0.0f;
        float time = 0.5f;
        float rotation = 0.0f;
        float baseAngle = child.eulerAngles.y;
        while (scalex < 0.1334f && scaley < 0.4 && scalez < 0.25)
        {
            child.localScale = new Vector3(scalex, scaley, scalez);
            child.rotation = Quaternion.Euler(0, rotation + baseAngle, 0);
            yield return 0;
            scalex += (0.1334f / time) * Time.deltaTime;
            scaley += (0.4f / time) * Time.deltaTime;
            scalez += (0.25f / time) * Time.deltaTime;
            rotation += (360.0f / time) * Time.deltaTime;
            Mathf.Clamp(scalex, 0.0f, 0.16675f);
            Mathf.Clamp(scaley, 0.0f, 0.4f);
            Mathf.Clamp(scalez, 0.0f, 0.25f);
            Mathf.Clamp(rotation, 0.0f, 360.0f);
        }
    }
}
