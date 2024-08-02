using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private List<GameObject> _tutorialPages;
    [SerializeField] private GameObject _continueButton;

    private int _pageIndex;

    private void Start()
    {
        _pageIndex = 0;

        for(int i = 0; i < _tutorialPages.Count; i++)
        {
            _tutorialPages[i].SetActive(i == 0);
        }
    }

    public void NextPage()
    {
        _pageIndex++;

        foreach (GameObject pages in _tutorialPages)
        {
            pages.SetActive(false);
        }

        _tutorialPages[_pageIndex].SetActive(true);

        if (_pageIndex == _tutorialPages.Count - 1)
        {
            _continueButton.SetActive(false);
        }
    }

    public void ResetTutorialPages()
    {
        _pageIndex = 0;

        foreach (GameObject pages in _tutorialPages)
        {
            pages.SetActive(false);
        }

        _continueButton.SetActive(true);
        _tutorialPages[_pageIndex].SetActive(true);
    }
}
