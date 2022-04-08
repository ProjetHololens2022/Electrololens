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
        for(int i = 0; i < platform.transform.childCount; ++i){
            StartCoroutine(popPuns(platform.transform.GetChild(i)));
        }
    }

    IEnumerator popPuns(Transform child){
        float scale = 0.0f;
        float time = 0.5f;
        while(scale < 1.0f){
            child.localScale = new Vector3(scale,scale,scale);
            yield return 0;
            scale += (1.0f/time) * Time.deltaTime;
            Mathf.Clamp(scale,0.0f,1.0f);
        }
    }
}
