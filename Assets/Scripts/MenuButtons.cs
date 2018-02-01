using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuButtons : MonoBehaviour
{
    [SerializeField] private GameObject menuGameObject;
    [SerializeField] private GameObject[] disableAfterStart;
    
    public void ShowStartMenu()
    {
        menuGameObject.SetActive(true);
        DisableFirstButtons();
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void StartNormalMode()
    {
        SceneManager.LoadScene(1);
    }

    void DisableFirstButtons()
    {
        disableAfterStart.ToList().ForEach(x => x.SetActive(false));
    }

    public void StartCrazyMode()
    {
        var randomiser = Resources.Load<GameObject>("randomiser");
        Instantiate(randomiser);
        SceneManager.LoadScene(1);
    }
}
