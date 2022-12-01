using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //variable permettant le replacement du monde
    public GameObject platform;
    public GameObject player;
    private Vector3 startPosPlat;
    private Vector3 startPosPlay;
    public GameObject[] coinsObj;

    //variable servant aux compteurs
    public int coins = 0;
    static int deathCount = 0;

    //varaible pour la progression bar
    public Slider progressionSlider;
    public GameObject wayponWorld;
    private int worlMaxCoord;


    //variable pour l'ui
    public GameObject coinsCount;
    public TextMeshProUGUI coinsText;
    public TextMeshProUGUI deathCountText;
    public Animator deathAnim;

    //variable stockant l'UI
    public GameObject ui;
    bool isPause = false;
    public GameObject uiEnd;
    public TextMeshProUGUI coinsCountEnd;
    public TextMeshProUGUI Attempt;

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
        if (ui != null)
        {
            //on désactive l'UI inutile
            ui.SetActive(false);
            uiEnd.SetActive(false);
            deathAnim.SetTrigger("draw");
            coinsCount.SetActive(false);
            //récupération des coordonnées de début de la platforme
            startPosPlat = platform.transform.position;
            //récupération des coordonnées de début du player
            startPosPlay = player.transform.position;
            //récupération de la coordonnées de fin de niveau
            worlMaxCoord = (int)wayponWorld.transform.position.x;
        }
    }

    private void FixedUpdate()
    {
        //si la progress bar est bien assigné on commence le calcul de progression
        if (progressionSlider != null)
        {
            progressionSlider.value = (wayponWorld.transform.position.x + 4 - player.transform.position.x) * 100 / worlMaxCoord;
            //si le player dépasse le waypon de fin on appelle la fonction de fin de partie
            if(wayponWorld.transform.position.x <= player.transform.position.x)
                EndGame();
        }
    }

    //replacement du monde comme il était au début
    public void ReplaceWord()
    {
        foreach (GameObject coin in coinsObj)
            coin.SetActive(true);
        coins = 0;
        deathCount++;
        AudioManager.instance.Reset();
        platform.transform.position = startPosPlat;
        player.transform.position = startPosPlay;
        player.transform.rotation = Quaternion.identity;
        CameraS.instance.ResetCam();
        deathCountText.text = "Attempt " + deathCount;
        deathAnim.SetTrigger("draw");
    }

    //si on appuie sur la touche Tab on met en pause le jeu
    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.started)
            Pause();
    }

    //foncton activant l'ui de pause et mettant en pause le jeu
    public void Pause()
    {
        isPause = !isPause;
        ui.SetActive(isPause);
        AudioManager.instance.PauseMusic();
        Time.timeScale = (isPause ? 0 : 1);
    }

    //fonction permettant l'ajout de piece
    public void AddCoins()
    {
        //on lance la coroutine d'ajout de piece.
        StartCoroutine(addCoins());
    }

    //fonction permettant de faire la transition entre les scenes.
    public void LoadTheGame(int index)
    {
        SceneManager.LoadScene(index);
        Time.timeScale = 1;
    }

    //fonction permettant de quitter le jeu
    public void StopGame()
    {
        Application.Quit();
    }

    //fonction permettant de lancer la fin du niveau
    public void EndGame()
    {
        Time.timeScale = 0;
        coinsCountEnd.text = "0" + coins + " x ";
        Attempt.text = "Attempt " + deathCount;
        uiEnd.SetActive(true);
    }

    //coroutine qui ajoute une piece ainsi que fait apparaitre temporairement le compteur de piece
    private IEnumerator addCoins()
    {
        coinsCount.SetActive(true);
        coins++;
        coinsText.text = "0" + coins + " x ";
        yield return new WaitForSeconds(2f);
        coinsCount.SetActive(false);
    }
}
