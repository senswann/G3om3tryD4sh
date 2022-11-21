using UnityEngine;

public class GameManager : MonoBehaviour
{
    //variable permettant le replacement du monde
    public GameObject platform;
    public GameObject player;
    private Vector3 startPosPlat;
    private Vector3 startPosPlay;
    public int coins = 0;
    public int deathCount = 0;

    //variable d'instance du GameManager
    public static GameManager instance;

    //permet de cr�er une instance unique de GameManager appelable avec GameManager.instance
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("il y a plus d'une instance de GameManager");
            return;
        }
        instance = this;
    }

    private void Start()
    {
        //r�cup�ration des coordonn�es de d�but de la platforme
        startPosPlat = platform.transform.position;
        //r�cup�ration des coordonn�es de d�but du player
        startPosPlay = player.transform.position;
    }

    //replacement du monde comme il �tait au d�but
    public void ReplaceWord()
    {
        coins = 0;
        deathCount++;
        AudioManager.instance.Reset();
        platform.transform.position = startPosPlat;
        player.transform.position = startPosPlay;
        player.transform.rotation = Quaternion.identity;
    }

    public void PauseGame(bool isPause)
    {
        if (isPause)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
}
