using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class SkinMenu : MonoBehaviour
{
    //variable gérant l'UI du menu
    public bool isActive = false;
    public GameObject setting;
    public Image skin;
    public GameObject player;

    //pour le drop down de skin
    public TMP_Dropdown skinDropdown;

    private void Start()
    {
        //on créer une liste des différentes options de résolution
        List<string> options = new List<string>();

        int curentResolutionIndex = 60;
        for (int i = 1; i <= 65; i++)
        {
            string option = "skin "+i;
            options.Add(option);
        }

        skinDropdown.AddOptions(options);
        skinDropdown.value = curentResolutionIndex;
        skinDropdown.RefreshShownValue();
    }

    //fonction permettant la gestion de l'affichage de l'UI
    public void ActiveSetting()
    {
        isActive = !isActive;
        setting.SetActive(isActive);
    }

    //Fonction gérant le changement de skin
    public void SetSkin(int indexSkin)
    {
        player.GetComponent<MultipleLayerSprite>().index = indexSkin;
        skin.sprite = player.GetComponent<MultipleLayerSprite>().getFirstLayer(indexSkin);
    }
}
