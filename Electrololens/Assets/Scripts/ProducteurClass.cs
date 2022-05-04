using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit.Experimental.UI;

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

    public void Start()
    {
        etat = 100;
        production = 50;
        emissionCO2 = 0;
        infoProducteur = infoProducteurGO.GetComponent<InfoProducteur>();
        updateProgessValues();
        closeProgressBar();
    }

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

    public void startDegradation()
    {
        StartCoroutine("degradationEtat");

    }

    IEnumerator degradationEtat()
    {
        while(true)
        {
            double randomDegrad = Random.Range(0, 10);
            etat -= randomDegrad;
            Debug.Log(etat);
            if(etat <= 50)
            {
                //Attention, jaune, reduction de production
            }
            if(etat <= 20)
            {
                //Danger, arret du systéme 
            }
            yield return new WaitForSeconds(10);
        }
        
    }

    public void reparationEtat()
    {

    }

    public void updateProgessValues()
    {
        Debug.Log(infoProducteur);
        Debug.Log(infoProducteur.getProgressEtatLoadingBar());
        if (infoProducteur != null 
            && infoProducteur.getProgressEtatLoadingBar() != null
                && infoProducteur.getProgressConsoLoadingBar() != null
                    && infoProducteur.getProgressEmissionLoadingBar() != null)
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

    public void ApplyEvent(NoRotationDockable e)
    {
        TypeEvent te = e.GetComponent<EventDockable>().typeEvent;

        switch (te)
        {
            case TypeEvent.CENTRALEHS:
                production -= 15;
                emissionCO2 += 5;
                break;
        }
    }

    public void RemoveEvent(NoRotationDockable e)
    {
        TypeEvent te = e.GetComponent<EventDockable>().typeEvent;

        switch (te)
        {
            case TypeEvent.CENTRALEHS:
                production += 15;
                emissionCO2 -= 5;
                break;
        }
    }

}
