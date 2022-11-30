using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGame : MonoBehaviour
{
    public void LoadTheGame(int index)
    {
        SceneManager.LoadScene(index);
    }
}
