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


    private String nom;
    private Type type;
    private int production;
    private int emissionCO2;


    public String getNom()
    {
        return nom;
    }

    public void setNom(String nom)
    {
        this.nom = nom;
    }

    public Type getType()
    {
        return type;
    }

    public int getProduction()
    {
        return production;
    }

    public void setProduction(int production)
    {
        this.production = production;
    }

    public int getEmissionC02()
    {
        return emissionC02;
    }

}
