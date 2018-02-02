using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FlipbookManager : MonoBehaviour {
    private int _currentPage = 1;

    [SerializeField] private Animator _pageOne;
    [SerializeField] private Animator _pageTwo;
    [SerializeField] private Animator _pageThree;
    [SerializeField] private Animator _pageFour;
    [SerializeField] private Animator _pageFive;
    [SerializeField] private Animator _pageSix;

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
                    StartCoroutine(GoToGame());
                    break;
            }

            _currentPage++;
        }
    }

    IEnumerator GoToGame() {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Timo's Work Scene");
    }
}