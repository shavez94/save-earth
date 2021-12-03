using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gamemanager : MonoBehaviour
{
    public GameObject gamepause;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void pause()
    {
        Time.timeScale = 0;
        gamepause.SetActive(true);
    }
    public void resume()
    {
        Time.timeScale = 1;
        gamepause.SetActive(false);
    }
    public void retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void changetoscene(int n)
    {
        SceneManager.LoadScene(n);
    }
    public void startlevel()
    {
        var n = PlayerPrefs.GetInt("currentlevel", 1);
        SceneManager.LoadScene(n);
    }
}
