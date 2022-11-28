using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    //variable permettant le replacement du monde
    public GameObject platform;
    public GameObject player;
    private Vector3 startPosPlat;
    private Vector3 startPosPlay;
    public int coins = 0;
    public int deathCount = 0;

    //variable pour l'ui
    public GameObject coinsCount;
    public TextMeshProUGUI coinsText;
    //variable stockant l'UI
    GameObject ui;
    bool isPause = false;

    //variable d'instance du GameManager
    public static GameManager instance;

    //permet de créer une instance unique de GameManager appelable avec GameManager.instance
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
        ui = GameObject.FindGameObjectWithTag("UI");
        ui.SetActive(false);
        coinsCount.SetActive(false);
        //récupération des coordonnées de début de la platforme
        startPosPlat = platform.transform.position;
        //récupération des coordonnées de début du player
        startPosPlay = player.transform.position;
    }

    //replacement du monde comme il était au début
    public void ReplaceWord()
    {
        coins = 0;
        deathCount++;
        AudioManager.instance.Reset();
        platform.transform.position = startPosPlat;
        player.transform.position = startPosPlay;
        player.transform.rotation = Quaternion.identity;
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Pause();
        }
    }
    public void Pause()
    {
        isPause = !isPause;
        ui.SetActive(isPause);
        AudioManager.instance.PauseMusic();
        if (isPause)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void AddCoins()
    {
        StartCoroutine(addCoins());
    }

    private IEnumerator addCoins()
    {
        coinsCount.SetActive(true);
        coins++;
        coinsText.text = "0" + coins + " x ";
        yield return new WaitForSeconds(2f);
        coinsCount.SetActive(false);
    }
}
