using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;


public class InfoVille : MonoBehaviour
{
    [SerializeField]
    private GameObject progressConso = null;
    [SerializeField]
    private GameObject progressApport = null;
    [SerializeField]
    private GameObject progressEmission = null;

    private IProgressIndicator progressConsoLoadingBar;
    private IProgressIndicator progressApportLoadingBar;
    private IProgressIndicator progressEmissionLoadingBar;
    // private int 


    void Start()
    {
        IProgressIndicator progressConsoLoadingBar = progressConso.GetComponent<IProgressIndicator>();
        IProgressIndicator progressApportLoadingBar = progressApport.GetComponent<IProgressIndicator>();
        IProgressIndicator progressEmissionLoadingBar = progressEmission.GetComponent<IProgressIndicator>();
    }
    
    // Update is called once per frame
    public IProgressIndicator getProgressConsoLoadingBar()
    {
        return progressConsoLoadingBar;
    }

    public IProgressIndicator getProgressApportLoadingBar()
    {
        return progressApportLoadingBar;
    }

    public IProgressIndicator getProgressEmissionLoadingBar()
    {
        return progressEmissionLoadingBar;
    }



    public async void updateLoadingBar(IProgressIndicator progressIndicator, float progress)
    {
        print("on est là");
        if (progressIndicator != null)
        {
            progressIndicator.OpenAsync();
            progressIndicator.Progress = progress;
            progressIndicator.CloseAsync();

        }
    }

}
