using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoVille : MonoBehaviour
{
    [SerializeField]
    private GameObject ProgressConso = null;
    [SerializeField]
    private GameObject ProgressApport = null;
    [SerializeField]
    private GameObject ProgressEmission = null;

    private IProgressIndicator ProgressConsoLoadingBar;
    private IProgressIndicator ProgressApportLoadingBar;
    private IProgressIndicator ProgressEmissionLoadingBar;
    // private int 


    void updateConso(float value)
    {
        ProgressConsoLoadingBar.Progress = value;
    }

    void updateApport(float value)
    {
        ProgressApportLoadingBar.Progress = value;
    }

    void updateEmission(float value)
    {
        ProgressEmissionLoadingBar.Progress = value;
    }

    void Start()
    {
        IProgressIndicator ProgressConsoLoadingBar = ProgressConso.GetComponent<IProgressIndicator>();
        IProgressIndicator ProgressApportLoadingBar = ProgressApport.GetComponent<IProgressIndicator>();
        IProgressIndicator ProgressEmissionLoadingBar = ProgressEmission.GetComponent<IProgressIndicator>();

        this.updateConso(0.75f);
        this.updateApport(0.35f);
        this.updateEmission(0.15f);

    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
