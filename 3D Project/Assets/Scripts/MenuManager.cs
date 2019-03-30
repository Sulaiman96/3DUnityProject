using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject menuPanel;
    public GameObject difficultyPanel;
    private PickUpManager manager;

    private void Start()
    {
        manager = GetComponent<PickUpManager>();
    }
    //int totalKills;
    public void Play()
    {
        SceneManager.LoadScene("Game");
    }

    public void Difficulty()
    {
        menuPanel.SetActive(false);
        difficultyPanel.SetActive(true);
    }

    public void Easy()
    {
         //manager.someDelay= 5f;
         difficultyPanel.SetActive(false);
         menuPanel.SetActive(true);
    }

    public void Medium()
    {
        //manager.someDelay = 15f;
        difficultyPanel.SetActive(false);
        menuPanel.SetActive(true);
    }

    public void Hard()
    {
        //manager.someDelay = 25f;
        difficultyPanel.SetActive(false);
        menuPanel.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
