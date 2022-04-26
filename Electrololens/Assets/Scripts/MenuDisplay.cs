using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuDisplay : MonoBehaviour
{
    [SerializeField]
    private GameObject menu;

    public void afficheMenu()
    {
        menu.SetActive(!menu.active);
    }
}
