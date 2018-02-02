using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Created By Timo Heijne
/// <summary>
/// Here handle the animation state of the "story" flipbook
/// </summary>
public class FlipbookManager : MonoBehaviour {
    private int _currentPage = 1;

    [SerializeField] private Animator _pageOne;
    [SerializeField] private Animator _pageTwo;
    [SerializeField] private Animator _pageThree;
    [SerializeField] private Animator _pageFour;
    [SerializeField] private Animator _pageFive;
    [SerializeField] private Animator _pageSix;

    private bool allowSwitching = false;

    void Start()
    {
        StartCoroutine(LoadLevel());
    }
    
    // Update is called once per frame
    void Update() {
        if (Input.anyKeyDown) {
            switch (_currentPage) {
                case 1:
                    _pageOne.SetTrigger("trigger");
                    break;

                case 2:
                    _pageTwo.SetTrigger("trigger");
                    break;

                case 3:
                    _pageThree.SetTrigger("trigger");
                    break;

                case 4:
                    _pageFour.SetTrigger("trigger");
                    break;

                case 5:
                    _pageFive.SetTrigger("trigger");
                    break;

                case 6:
                    _pageSix.SetTrigger("trigger");
                    allowSwitching = true;
                    break;
            }

            _currentPage++;
        }
    }

    IEnumerator LoadLevel() {
        var AO = SceneManager.LoadSceneAsync("Timo's Work Scene");
        AO.allowSceneActivation = false;
        

        while (AO.progress < 0.9f || !allowSwitching)
        {
            print("Loading: " + (AO.progress*100) + ". Can switch: " + allowSwitching);
            yield return null;
        }

        AO.allowSceneActivation = true;

    }
}