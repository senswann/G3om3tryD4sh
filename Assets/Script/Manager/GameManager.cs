using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //variable permettant le replacement du monde
    public GameObject player;
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
    public GameObject fpsCount;

    //variable stockant l'UI
    public GameObject ui;
    public SkinMenu uiSkin;
    bool isPause = false;
    public GameObject uiEnd;
    public TextMeshProUGUI coinsCountEnd;
    public TextMeshProUGUI Attempt;

    //variable sus
    private bool isAlternative;

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
        if (ui != null)
        {
            //on d�sactive l'UI inutile
            ui.SetActive(false);
            uiEnd.SetActive(false);
            deathAnim.SetTrigger("draw");
            coinsCount.SetActive(false);
            //r�cup�ration des coordonn�es de d�but du player
            startPosPlay = player.transform.position;
            //r�cup�ration de la coordonn�es de fin de niveau
            worlMaxCoord = (int)wayponWorld.transform.position.x;
        }
    }

    private void FixedUpdate()
    {
        //si la progress bar est bien assign� on commence le calcul de progression
        if (progressionSlider != null)
        {
            progressionSlider.value = (wayponWorld.transform.position.x + 4 - player.transform.position.x) * 100 / worlMaxCoord;
            //si le player d�passe le waypon de fin on appelle la fonction de fin de partie
            if(wayponWorld.transform.position.x <= player.transform.position.x)
                EndGame();
        }
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
        if (uiSkin.isActive)
            uiSkin.ActiveSetting();
        isPause = !isPause;
        fpsCount.SetActive(!isPause);
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

    //avec cette coroutine on arrete de bouger le Monde, on lance le SFX ainsi que l'animation de mort, puis on replace tout comme au d�but et on recommence
    public IEnumerator ReplaceWorld(bool _isAlternative)
    {
        //varaiable temporaire pour cette coroutine
        Rigidbody2D rbPlayer = player.GetComponent<Rigidbody2D>();
        MultipleLayerSprite multiLayerPlayer = player.GetComponent<MultipleLayerSprite>();
        MovePlayer movePlayer = player.GetComponent<MovePlayer>();
        BoxCollider2D boxColliderPlayer = player.GetComponent<BoxCollider2D>();

        //on commence par enlever la possibilit� de bouger
        MovePlayer.runSpeed = 0.0f;
        Scroller.speed = 0.0f;
        boxColliderPlayer.enabled = false;
        rbPlayer.gravityScale = 0.0f;
        multiLayerPlayer.Death();

        //on attend 1 seconde
        yield return new WaitForSeconds(1f);

        if (_isAlternative)
        {
            deathCount = -1;
            isAlternative = !isAlternative;
            AudioManager.instance.playNextSong();
        }
        //puis on remet le monde dans son �tat initial
        if (movePlayer.isSheep)
            movePlayer.OnSheep();
        boxColliderPlayer.enabled = true;
        multiLayerPlayer.Restart();
        foreach (GameObject coin in coinsObj)
            coin.SetActive(true);
        coins = 0;
        deathCount++;
        AudioManager.instance.Reset();
        player.transform.position = startPosPlay;
        player.transform.rotation = Quaternion.identity;
        CameraS.instance.ResetCam();
        deathCountText.text = "Attempt " + deathCount;
        deathAnim.SetTrigger("draw");
        rbPlayer.gravityScale = 65;
        MovePlayer.runSpeed = 11.0f;
        Scroller.speed = 0.15f;
        if (isAlternative)
            Scroller.speed = 0.45f;
    }
}
