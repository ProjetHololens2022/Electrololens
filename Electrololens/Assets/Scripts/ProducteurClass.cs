using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;


public enum Type
{
    Nucléaire,
    Charbon,
    Eolien,
    Solaire,
    Hydroélectrique
}

public class ProducteurClass : MonoBehaviour
{


    private string nom;
    private Type type;
    private double production;
    private int emissionCO2;


    public string getNom()
    {
        return nom;
    }

    public void setNom(string nom)
    {
        this.nom = nom;
    }

    public Type getType()
    {
        return type;
    }

    public double getProduction()
    {
        return production;
    }

    public void setProduction(double production)
    {
        this.production = production;
    }

    public int getEmissionCO2()
    {
        return emissionCO2;
    }

}
