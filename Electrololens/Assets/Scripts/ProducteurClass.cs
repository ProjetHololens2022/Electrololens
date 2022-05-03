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
    private double emissionCO2;
    private double etat; 

    [SerializeField]
    private GameObject infoProducteurGO;
    private InfoProducteur infoProducteur;

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

    public double getEmissionCO2()
    {
        return emissionCO2;
    }

    public void updateProgessValues()
    {
        if (infoProducteur != null 
            && infoProducteur.getProgressEtatLoadingBar() != null
                && infoProducteur.getProgressConsoLoadingBar() != null
                    && infoProducteur.getProgressConsoLoadingBar() != null)
        {
            infoProducteur.updateLoadingBar(infoProducteur.getProgressConsoLoadingBar(), production / 100.0);
            infoProducteur.updateLoadingBar(infoProducteur.getProgressEtatLoadingBar(), etat / 100.0);
            infoProducteur.updateLoadingBar(infoProducteur.getProgressEmissionLoadingBar(), emissionCO2 / 100.0);
        }
    }


    public void closeProgressBar()
    {
        if (infoProducteur != null 
            && infoProducteur.getProgressEtatLoadingBar() != null
                && infoProducteur.getProgressConsoLoadingBar() != null
                    && infoProducteur.getProgressConsoLoadingBar() != null)
        {
            infoProducteur.closeProgressAsync(infoProducteur.getProgressConsoLoadingBar());
            infoProducteur.closeProgressAsync(infoProducteur.getProgressEtatLoadingBar());
            infoProducteur.closeProgressAsync(infoProducteur.getProgressEmissionLoadingBar());
        }
    }

    public void showInfo()
    {
        updateProgessValues();
        infoProducteurGO.SetActive(true);
    }

    public void hideInfo()
    {
        infoProducteurGO.SetActive(false);
        closeProgressBar();
    }


}
