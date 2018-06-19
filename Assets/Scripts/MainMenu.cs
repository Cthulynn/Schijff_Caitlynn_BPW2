using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	//klik en het spel begint
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }
    //klik en het spel sluit af
    public void QuitGame()
    {
        //Debug.Log("Quit!");
        Application.Quit();
    }
}
