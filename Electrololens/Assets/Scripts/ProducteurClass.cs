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
    [SerializeField]
    private Type type;
    private double production;
    private double emissionCO2;
    private double etat;

    [SerializeField]
    private GameObject infoProducteurGO;
    private InfoProducteur infoProducteur;

    private double pollution = 0;

    private double beforeEmissionC02;
    private double beforeetat;
    private double beforeprod;

    public void Start()
    {
        etat = 100;
        production = 1;
        emissionCO2 = 0;
        beforeetat = etat;
        beforeprod = production;
        beforeEmissionC02 = emissionCO2;

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
        while (true)
        {
            double randomDegrad = Random.Range(0, 10);
            etat -= randomDegrad;
            calculPollution();
            if (etat <= 50)
            {
                //Attention, jaune, reduction de production
            }
            if (etat <= 20)
            {
                //Danger, arret du systéme 
            }
            yield return new WaitForSeconds(10);
        }
    }

    public void OnSliderUpdated(SliderEventData eventData)
    {
        print(eventData.NewValue);
        production = eventData.NewValue*100;
    }

    public void reparationEtat()
    {
        etat += 10;
        if (etat > 100)
        {
            etat = 100;
        }
        calculPollution();
        if (etat > 50)
        {
            //Tout redevient vert, tout est ok
        }
        if (etat > 20 && etat <= 50)
        {
            //Attention, jaune, reduction de production
        }
    }

    public void calculPollution()
    {
        Type typeProd = type;
        switch (typeProd)
        {
            case Type.Nucléaire:
                if (etat != 100)
                {
                    pollution = ((100 - etat) * 12) + 12;

                }
                else
                {
                    pollution = 12;
                }
                break;
            case Type.Eolien:
                if (etat != 100)
                {
                    pollution = ((100 - etat) * 11) + 11;

                }
                else
                {
                    pollution = 11;
                }
                break;
            case Type.Charbon:
                if (etat != 100)
                {
                    pollution = ((100 - etat) * 852) + 852;

                }
                else
                {
                    pollution = 852;
                }
                break;
            case Type.Solaire:
                if (etat != 100)
                {
                    pollution = ((100 - etat) * 44) + 44;

                }
                else
                {
                    pollution = 44;
                }
                break;
        }
    }

    public void setProduction()
    {

        // Debug.Log("Hello");
        // Debug.Log(production);
        // Debug.Log(pollution);
        emissionCO2 = (production / 100) * pollution;
        // Regler la production
    }

    public void updateProgessValues()
    {
        setProduction();
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
        Debug.Log(te);

        beforeEmissionC02 = emissionCO2;
        beforeetat = etat;
        beforeprod = production;

        if (type == Type.Charbon)
        {
            if (te == TypeEvent.PENURIECHARBON)
            {
                production -= 15;
                emissionCO2 = 0;
            }
            if (te == TypeEvent.CENTRALEHS)
            {
                production -= 15;
                emissionCO2 += 5;
            }
        }
        
        if (type == Type.Nucléaire)
        {
            if (te == TypeEvent.CENTRALEHS)
            {
                    production -= 15;
                    emissionCO2 += 5;
            }
            if (te == TypeEvent.PENURIEURANIUM)
            {
                    production = 0;
                    etat -= 25;
                    emissionCO2 = 0;
            }
            if (te == TypeEvent.CANICULE)
            {
                    production -= 45;
                    emissionCO2 = 0;
                    etat -= 20;
            }
        }
        if (type == Type.Solaire && te == TypeEvent.PENURIESOLEIL)
        {
            production -= 50;
        }
        if (type == Type.Eolien && te == TypeEvent.PENURIEVENT)
        {
            production -= 50;
            emissionCO2 = 0;
            etat -= 10;
        }

        if(te == TypeEvent.TDT)
        {
            etat -= 25;
        }
    }


    public void RemoveEvent(NoRotationDockable e)
    {
        TypeEvent te = e.GetComponent<EventDockable>().typeEvent;

        if (type == Type.Charbon)
        {
            if (te == TypeEvent.PENURIECHARBON)
            {
                production += 15;
                emissionCO2 = beforeEmissionC02;
            }
            if (te == TypeEvent.CENTRALEHS)
            {
                production += 15;
                    emissionCO2 -= 5;
            }
        }
        if(type == Type.Nucléaire)
        {
            if (te == TypeEvent.CENTRALEHS)
            {
                production += 15;
                emissionCO2 -= 5;
            }
            if (te == TypeEvent.PENURIEURANIUM)
            {
                production = beforeprod;
                etat += 25;
                emissionCO2 = 0;

            }
            if (te == TypeEvent.CANICULE)
            {
                production += 45;
                emissionCO2 = beforeEmissionC02;
                etat += 20;
            }
        }
        if(type == Type.Solaire && te == TypeEvent.PENURIESOLEIL)
        {
            production += 50;
        }
        if(type == Type.Eolien && te == TypeEvent.PENURIEVENT)
        {
            production += 50;
            emissionCO2 = beforeEmissionC02;
            etat += 10;
        }
    }

}