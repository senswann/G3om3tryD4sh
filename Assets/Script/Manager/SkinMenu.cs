using UnityEngine;
using UnityEngine.UI;

public class SkinMenu : MonoBehaviour
{
    bool isActive = false;
    public GameObject setting;
    public Image skin;
    public GameObject player;
    
    public void ActiveSetting()
    {
        isActive = !isActive;
        setting.SetActive(isActive);
    }

    public void SetSkin(int indexSkin)
    {
        player.GetComponent<MultipleLayerSprite>().index = indexSkin;
        skin.sprite = player.GetComponent<MultipleLayerSprite>().getFirstLayer(indexSkin);
    }
}
