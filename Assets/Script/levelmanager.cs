using UnityEngine;
using UnityEngine.SceneManagement;

public class levelmanager : MonoBehaviour
{
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
