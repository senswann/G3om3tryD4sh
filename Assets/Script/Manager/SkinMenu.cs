using UnityEngine;
using UnityEngine.UI;

public class SkinMenu : MonoBehaviour
{
    //variable g�rant l'UI du menu
    bool isActive = false;
    public GameObject setting;
    public Image skin;
    public GameObject player;
    
    //fonction permettant la gestion de l'affichage de l'UI
    public void ActiveSetting()
    {
        isActive = !isActive;
        setting.SetActive(isActive);
    }

    //Fonction g�rant le changement de skin
    public void SetSkin(int indexSkin)
    {
        player.GetComponent<MultipleLayerSprite>().index = indexSkin;
        skin.sprite = player.GetComponent<MultipleLayerSprite>().getFirstLayer(indexSkin);
    }
}
