using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InfoNetwork : MonoBehaviour
{

    private float coef = 0.7f;
    private int nbIdx = 11;

    [SerializeField]
    private LineRenderer prod;
    [SerializeField]
    private LineRenderer cons;
    [SerializeField]
    private TextMeshPro maxValueDisplay;

    private double[] dataProd = new double[11] {0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0};
    private double[] dataCons = new double[11] {0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0};

    // Start is called before the first frame update
    void Start()
    {
        DrawCurve(prod,dataProd);
        DrawCurve(cons,dataCons);
    }

    // Update is called once per frame
    void Update()
    {
        DrawCurve(prod,dataProd);
        DrawCurve(cons,dataCons);
        maxValueDisplay.SetText(((int)GetMaxValue()).ToString());
    }

    public void SetDatas(double[] prod, double[] cons){
        dataProd = prod;
        dataCons = cons;
    }

    void DrawCurve(LineRenderer curve, double[] data){
        if(GetMaxValue() > GetMinValue()){
            for(int i = 0; i < nbIdx; ++i){
                Debug.Log(i);
                curve.SetPosition(i,new Vector3(    -0.4f+((coef/(float)(nbIdx-1))*(float)i),
                                                    (float)data[i]/((float)GetMaxValue()*coef*2.0f),
                                                    0.0f
                                                ));
            }
        } else {
            for(int i = 0; i < nbIdx; ++i){
                curve.SetPosition(i,new Vector3(-0.4f+((coef/(float)(nbIdx-1))*(float)i),0.0f,0.0f));
            }
        }
    }

    double GetMaxValue(){
        double max = dataProd[0];

        for(int i = 0; i < nbIdx; ++i) {
            double number = dataProd[i];

            if (number > max) {
                max = number;
            }
        }

        for(int i = 0; i < nbIdx; ++i) {
            double number = dataCons[i];

            if (number > max) {
                max = number;
            }
        }

        return max;
    }

    double GetMinValue(){
        double min = dataProd[0];

        for(int i = 0; i < nbIdx; ++i) {
            double number = dataProd[i];

            if (number < min) {
                min = number;
            }
        }

        for(int i = 0; i < nbIdx; ++i) {
            double number = dataCons[i];

            if (number < min) {
                min = number;
            }
        }

        return min;
    }

    void AddData(double newProd, double newCons){
        for(int i = 0; i < nbIdx-1; ++i){
            dataProd[i] = dataProd[i+1];
            dataCons[i] = dataCons[i+1];
        }
        dataProd[nbIdx-1] = newProd;
        dataCons[nbIdx-1] = newCons;
    }
}
