using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineConnector : MonoBehaviour
{
    private LineRenderer lr;
    [SerializeField]
    private Transform start;
    [SerializeField]
    private Transform end;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.AddComponent<LineRenderer>();
        lr = gameObject.GetComponent<LineRenderer>();
        lr.alignment = LineAlignment.View;
        lr.loop = false;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 pos1 = start.position;
        Vector3 pos3 = end.position;
        Vector3 pos2 = pos1 + (pos3-pos1)/2.0f;
        pos2.y -= 0.02f;
        lr.positionCount = 10;
        Vector3[] positions = new Vector3[10];
        for(int i = 0; i < 10; ++i){
            float t = ((float) i) / 9.0f;
            Vector3 lerp12 = Vector3.Lerp(pos1,pos2,t);
            Vector3 lerp23 = Vector3.Lerp(pos2,pos3,t);
            positions[i] = Vector3.Lerp(lerp12,lerp23,t);
        }
        lr.SetPositions(positions);
        lr.Simplify(0.0001f);
    }

    public void SetStart(Transform start){
        this.start = start;
    }

    public void SetEnd(Transform end){
        this.end = end;
    }


    public Transform getStart()
    {
        return this.start;
    }


    public Transform getEnd()
    {
        return this.end;
    }
}
