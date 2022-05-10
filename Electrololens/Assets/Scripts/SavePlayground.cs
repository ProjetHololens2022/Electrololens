using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;

[System.Serializable]
public struct ObjetScene{
    public int instanceID;
    public string path;
    public string name;
    public Vector3 position;
    public Vector3 rotation;
    public string type;
    public Vector3 scale;
    public int electricalNetwork;
    
    public double production;
    public double etat;

    public double emissionCO2Ville;
    public double consommation; //Besoin
    public double apportElectricite; //Reçu
    public double tauxDeSatisfaction;
    public int nbHabitants;
    // public 
}

[System.Serializable]
public struct Save
{
    public List<ObjetScene> objects;
}

public class SavePlayground : MonoBehaviour
{
    [SerializeField]
    private GameObject platform;
    [SerializeField]
    private GameObject dock;
    [SerializeField]
    private GameObject dockGameZone;
    [SerializeField]
    private GameObject gameZone;
    [SerializeField]
    private GameObject Nucléaire;
    [SerializeField]
    private GameObject Charbon;
    [SerializeField]
    private GameObject Eolien;
    [SerializeField]
    private GameObject Solaire;
    [SerializeField]
    private GameObject city;
    [SerializeField]
    private GameObject electricPole;



    public void Save()
    {
        print("recup");
        Save s = new Save();
        List<ObjetScene> listeObjet = new List<ObjetScene>();
        // Object[] GameobjectList = Resources.FindObjectsOfTypeAll(typeof(GameObject));
        Transform transform = platform.transform;
        foreach (Transform t in transform)
        {
            GameObject go = t.gameObject;
            if(go.GetComponent<Saveable>() && go.GetComponent<Saveable>().isSaveable){
                print(go.name);
                string path = go.name.Replace("(Clone)", "");
                print(path);
                print(go);
                if(path!=""){

                    ObjetScene os = new ObjetScene();
                    os.instanceID = go.GetInstanceID();
                    os.path = path;
                    os.name = go.name.Replace("(Clone)", "");
                    os.position = go.transform.position;
                    os.rotation = go.transform.rotation.eulerAngles;
                    os.scale = go.transform.localScale;
                    if (go.GetComponent<ProducteurClass>()){
                        os.type = go.GetComponent<ProducteurClass>().getType().ToString();
                        os.electricalNetwork = go.GetComponent<ProducteurClass>().electricalNetwork.GetInstanceID();
                        os.production = go.GetComponent<ProducteurClass>().getProduction();
                        os.etat = go.GetComponent<ProducteurClass>().getEtat();
                    }else if (go.GetComponent<ConsommateurClass>()){
                        os.type = "ConsommateurClass";
                        os.consommation = go.GetComponent<ConsommateurClass>().getConsommation();
                        os.apportElectricite = go.GetComponent<ConsommateurClass>().GetApportElectricite();
                        os.emissionCO2Ville = go.GetComponent<ConsommateurClass>().getEmissionCO2();
                        os.tauxDeSatisfaction = go.GetComponent<ConsommateurClass>().getTauxDeSatisfaction();
                        os.nbHabitants = go.GetComponent<ConsommateurClass>().getNbHabitants();
                        os.electricalNetwork = go.GetComponent<ConsommateurClass>().electricalNetwork.GetInstanceID();
                    }else if (go.GetComponent<ElectricalNetwork>()){
                        os.type = "ElectricalNetwork";
                    }
                    listeObjet.Add(os);                  
                }
            }

        }
        s.objects = listeObjet;
        string jsonSave = JsonUtility.ToJson(s,true);
        File.WriteAllText(Application.dataPath + "/Resources/save.json", jsonSave);
        print(jsonSave);
    }

    public void LoadSave()
    {
        foreach(Transform child in platform.transform){
            Destroy(child.gameObject);
        }

        Destroy(dockGameZone);
        GameObject resetDock = Instantiate(dock, dockGameZone.transform.position, dockGameZone.transform.rotation);
        resetDock.transform.parent = gameZone.transform;
        this.dockGameZone = resetDock;

        print("LoadSave");
        string json = File.ReadAllText(Application.dataPath + "/Resources/save.json");
        Save s = JsonUtility.FromJson<Save>(json);

        
        Dictionary<int, List<GameObject>> connectedDict = new Dictionary<int, List<GameObject>>();
        Dictionary<int, GameObject> poleDict = new Dictionary<int, GameObject>();
        

        foreach (ObjetScene os in s.objects)
        {
            GameObject go = null;
            switch (os.type)
            {
                case "Nucléaire":
                go = Nucléaire;

                break;

                case "Charbon":
                go = Charbon;
                break;

                case "Eolien":
                go = Eolien;
                break;

                case "Solaire":
                go = Solaire;
                break;

                case "ConsommateurClass":
                go = city;
                break;

                case "ElectricalNetwork":
                go = electricPole;
                //poleDict.Add(os.instanceID, go);
                break;
            }


            GameObject go2 = Instantiate(go, os.position, Quaternion.Euler(os.rotation));
            go2.transform.parent = platform.transform;
            go2.transform.localScale = os.scale;
            go2.name = os.name;
            if (go2.GetComponent<ProducteurClass>())
            {
                go2.GetComponent<ProducteurClass>().setEtat(os.etat);
                go2.GetComponent<ProducteurClass>().setVraiProduction(os.production);
                go2.GetComponent<ProducteurClass>().startDegradation();
            }
            else if (go2.GetComponent<ConsommateurClass>())
            {
                go2.GetComponent<ConsommateurClass>().setConsommation(os.consommation);
                go2.GetComponent<ConsommateurClass>().SetApportElectricite(os.apportElectricite);
                go2.GetComponent<ConsommateurClass>().setTauxDeSatisfaction(os.tauxDeSatisfaction);
                go2.GetComponent<ConsommateurClass>().setNbHabitants(os.nbHabitants);
                go2.GetComponent<ConsommateurClass>().setEmissionCO2(os.emissionCO2Ville);
            }
            if (os.electricalNetwork != 0)
            {
                if (!connectedDict.ContainsKey(os.electricalNetwork))
                {
                    connectedDict.Add(os.electricalNetwork, new List<GameObject>());
                }
                connectedDict[os.electricalNetwork].Add(go2);

            }
            if (os.type == "ElectricalNetwork")
            {
                poleDict.Add(os.instanceID, go2);
            }
        }

        foreach (var item in connectedDict)
        {
            print(item);
            foreach (GameObject go3 in item.Value)
            {
                print(go3);
                if (go3.GetComponent<ProducteurClass>())
                {
                    go3.GetComponent<ProducteurClass>().electricalNetwork = poleDict[item.Key];
                    print("kkk");
                }
                else if (go3.GetComponent<ConsommateurClass>())
                {
                    go3.GetComponent<ConsommateurClass>().electricalNetwork = poleDict[item.Key];
                    print("kkk1");
                }
                poleDict[item.Key].GetComponent<ElectricalNetwork>().addBuilding(go3);
                print("kkk2");
            }
        }
    }
}
