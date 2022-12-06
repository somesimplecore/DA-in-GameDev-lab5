using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    private bool isPaused = false;
    public GameObject panel;

    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(!isPaused)
            {
                Time.timeScale = 0;
                isPaused = true;
                panel.SetActive(true);
            }
            else
            {
                Time.timeScale = 1;
                isPaused = false;
                panel.SetActive(false);
            }
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
    }
}
