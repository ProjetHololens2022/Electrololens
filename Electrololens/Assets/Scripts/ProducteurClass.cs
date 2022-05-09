using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit.Experimental.UI;
using System.IO;

public enum Type
{
    Nucléaire,
    Charbon,
    Eolien,
    Solaire,
    Hydroélectrique
}

[System.Serializable]
public struct donneesEmissionCo2
{
    public string name;
    public string emission;
}

[System.Serializable]
public struct ensembleDonnees
{
    public List<donneesEmissionCo2> emissionCO2;
}

public class ProducteurClass : MonoBehaviour
{


    private string nom;
    [SerializeField]
    private Type type;
    private double production = 75.0;
    private double emissionCO2 = 0.0;
    private double etat = 100.0;

    public bool isConnected;
    public GameObject electricalNetwork;

    [SerializeField]
    private GameObject infoProducteurGO;
    private InfoProducteur infoProducteur;

    private double pollution = 0;

    private double productionEvent = 0.0;
    private double emissionCO2Event = 0.0;
    private double etatEvent = 0.0;

    public string jsonString;

    public void Start()
    {
        infoProducteur = infoProducteurGO.GetComponent<InfoProducteur>();
        calculPollution();
        setEmissionCO2();
        Debug.Log("start : " + getEmissionCO2());
}

void Update()
    {
        if(getEtat() > 50){
            this.transform.Find("Sphere").GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(0.0f,1.0f,0.0f,1.0f)*10.0f);
            production = MaxProduction();
        } else if(getEtat() <= 50 && getEtat() > 20) {
            this.transform.Find("Sphere").GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(0.742f,0.742f,0.0f,1.0f)*10.0f);
            production = MaxProduction()/2.0;
        } else if(getEtat() <= 20) {
            this.transform.Find("Sphere").GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(1.0f,0.0f,0.0f,1.0f)*10.0f);
            production = 0.0;
        }

        if(!isConnected){
            this.transform.Find("Sphere").GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(0.5f,0.5f,0.5f,1.0f)*10.0f);
        }
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

    public double getEtat()
    {
        return etat + etatEvent > 0.0 ? etat + etatEvent : 0.0;
        
    }

    public double MaxProduction(){
        switch (type)
        {
            case Type.Nucléaire:
                return 150.0;
            case Type.Eolien:
                return 80.0;
            case Type.Charbon:
                return 100.0;
            case Type.Solaire:
                return 60.0;
        }
        return 0.0;
    }

    public double getProduction()
    {
        return production + productionEvent > 0.0 ? production + productionEvent : 0.0;
    }

    public void setProduction(double production)
    {
        this.production = production;
    }

    public double getEmissionCO2()
    {
        return emissionCO2 + emissionCO2Event > 0.0 ? emissionCO2 + emissionCO2Event : 0.0;
    }

    public void startDegradation()
    {
        StartCoroutine("degradationEtat");
    }

    IEnumerator degradationEtat()
    {
        while (true)
        {
            if(isConnected){
                double randomDegrad = Random.Range(0, 5);
                etat -= randomDegrad;
                if (getEtat() < 0)
                {
                    etat = 0;
                }
            }
            calculPollution();
            yield return new WaitForSeconds(5.0f);
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
        if (getEtat() > 100)
        {
            etat = 100;
        }
        calculPollution();
    }

    public void calculPollution()
    {
        Type typeProd = type;
        int pollutionSiteProd = 0;
        switch (typeProd)
        {
            case Type.Nucléaire:
                pollutionSiteProd = getEmissionCo2("Centrale nucléaire");
                if (getEtat() != 100)
                {
                    pollution = ((100 - getEtat()) * pollutionSiteProd) + pollutionSiteProd;

                }
                else
                {
                    pollution = pollutionSiteProd;
                }
                break;
            case Type.Eolien:
                pollutionSiteProd = getEmissionCo2("Eoliéenne");
                if (getEtat() != 100)
                {
                    pollution = ((100 - getEtat()) * pollutionSiteProd) + pollutionSiteProd;

                }
                else
                {
                    pollution = pollutionSiteProd;
                }
                break;
            case Type.Charbon:
                pollutionSiteProd = getEmissionCo2("Usine à charbon");
                if (getEtat() != 100)
                {
                    Debug.Log(pollutionSiteProd);
                    pollution = ((100 - getEtat()) * pollutionSiteProd) + pollutionSiteProd;

                }
                else
                {
                    pollution = pollutionSiteProd;
                }
                break;
            case Type.Solaire:
                pollutionSiteProd = getEmissionCo2("Panneaux solaires");
                if (getEtat() != 100)
                {
                    pollution = ((100 - getEtat()) * pollutionSiteProd) + pollutionSiteProd;

                }
                else
                {
                    pollution = pollutionSiteProd;
                }
                break;
        }
    }

    public void setEmissionCO2()
    {
        emissionCO2 = (getProduction() / 100) * pollution;
    }

    public void updateProgessValues()
    {
        setEmissionCO2();
        if (infoProducteur != null
            && infoProducteur.getProgressEtatLoadingBar() != null
                && infoProducteur.getProgressProdLoadingBar() != null
                    && infoProducteur.getProgressEmissionLoadingBar() != null)
        {
            infoProducteur.updateLoadingBar(infoProducteur.getProgressProdLoadingBar(), production / 100.0);
            infoProducteur.updateLoadingBar(infoProducteur.getProgressEtatLoadingBar(), etat / 100.0);
            infoProducteur.updateLoadingBar(infoProducteur.getProgressEmissionLoadingBar(), emissionCO2 / 100.0);
        }
    }


    public void closeProgressBar()
    {
        if (infoProducteur != null
            && infoProducteur.getProgressEtatLoadingBar() != null
                && infoProducteur.getProgressProdLoadingBar() != null
                    && infoProducteur.getProgressProdLoadingBar() != null)
        {
            infoProducteur.closeProgressAsync(infoProducteur.getProgressProdLoadingBar());
            infoProducteur.closeProgressAsync(infoProducteur.getProgressEtatLoadingBar());
            infoProducteur.closeProgressAsync(infoProducteur.getProgressEmissionLoadingBar());
        }
    }

    public void showInfo()
    {
        GameObject.FindGameObjectWithTag("GraphManager").SendMessage("showProducer", this);
    }

    public void hideInfo()
    {
        closeProgressBar();
    }

    public void ApplyEvent(NoRotationDockable e)
    {
        
        TypeEvent te = e.GetComponent<EventDockable>().typeEvent;
        Debug.Log(te);

        if (type == Type.Charbon)
        {
            if (te == TypeEvent.PENURIECHARBON)
            {
                productionEvent -= 15;
                emissionCO2Event = 0;
            }
            if (te == TypeEvent.CENTRALEHS)
            {
                productionEvent -= 15;
                emissionCO2Event += 5;
            }
        }
        
        if (type == Type.Nucléaire)
        {
            if (te == TypeEvent.CENTRALEHS)
            {
                    productionEvent -= 15;
                    emissionCO2Event += 5;
            }
            if (te == TypeEvent.PENURIEURANIUM)
            {
                    productionEvent = 0;
                    etat -= 25;
                    emissionCO2Event = 0;
            }
            if (te == TypeEvent.CANICULE)
            {
                    productionEvent -= 45;
                    emissionCO2Event = 0;
                    etat -= 20;
            }
        }
        if (type == Type.Solaire && te == TypeEvent.PENURIESOLEIL)
        {
            productionEvent -= 50;
        }
        if (type == Type.Eolien && te == TypeEvent.PENURIEVENT)
        {
            productionEvent -= 50;
            emissionCO2Event = 0;
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
                productionEvent += 15;
                emissionCO2Event = 0;
            }
            if (te == TypeEvent.CENTRALEHS)
            {
                productionEvent += 15;
                emissionCO2Event -= 5;
            }
        }
        if(type == Type.Nucléaire)
        {
            if (te == TypeEvent.CENTRALEHS)
            {
                productionEvent += 15;
                emissionCO2Event -= 5;
            }
            if (te == TypeEvent.PENURIEURANIUM)
            {
                productionEvent = 0;
                etatEvent += 25;
                emissionCO2Event = 0;

            }
            if (te == TypeEvent.CANICULE)
            {
                productionEvent += 45;
                emissionCO2Event = 0;
                etatEvent += 20;
            }
        }
        if(type == Type.Solaire && te == TypeEvent.PENURIESOLEIL)
        {
            productionEvent += 50;
        }
        if(type == Type.Eolien && te == TypeEvent.PENURIEVENT)
        {
            productionEvent += 50;
            emissionCO2Event = 0;
            etatEvent += 10;
        }
    }

    public void Delete(){
        if(isConnected){
            this.electricalNetwork.GetComponent<ElectricalNetwork>().disconnect(this.gameObject);
        }
        Destroy(this.gameObject);
    }

    public int getEmissionCo2(string typeProd)
    {
        string json = File.ReadAllText(Application.dataPath + "/Resources/données.json");
        ensembleDonnees s = JsonUtility.FromJson<ensembleDonnees>(json);
        foreach (donneesEmissionCo2 donneeProd in s.emissionCO2)
        {
            if (typeProd == donneeProd.name)
            {
                return int.Parse(donneeProd.emission);
            }
        }
        return 0;
    }
}