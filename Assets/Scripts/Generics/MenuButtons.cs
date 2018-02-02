using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuButtons : MonoBehaviour
{
    [SerializeField] private GameObject menuGameObject;
    [SerializeField] private GameObject[] disableAfterStart;
    [SerializeField] private GameObject loadScreen;
    
    public static bool CrazyMode { get; set; }
    
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
        CrazyMode = false;
        StartCoroutine(LoadNormal());
    }

    void DisableFirstButtons()
    {
        disableAfterStart.ToList().ForEach(x => x.SetActive(false));
    }

    public void StartCrazyMode()
    {
        var randomiser = Resources.Load<GameObject>("randomiser");
        Instantiate(randomiser);
        CrazyMode = true;
        StartCoroutine(LoadCrazyMode());
    }

    public IEnumerator LoadNormal()
    {
        menuGameObject.SetActive(false);
        loadScreen.SetActive(true);
        
        var operation = SceneManager.LoadSceneAsync(2, LoadSceneMode.Single);
        operation.allowSceneActivation = false;

        while (operation.progress < 0.89f)
        {
            print("Loading: " + (operation.progress*100));
            yield return null;
        }

        operation.allowSceneActivation = true;
    }

    public IEnumerator LoadCrazyMode()
    {
        menuGameObject.SetActive(false);
        loadScreen.SetActive(true);
        
        var operation = SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
        operation.allowSceneActivation = false;

        while (operation.progress < 0.89f)
        {
            print("Loading: " + (operation.progress*100));
            yield return null;
        }

        operation.allowSceneActivation = true;

    }
}
