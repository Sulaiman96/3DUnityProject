using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class GameOver : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public GameObject title;
    public GameObject restart;
    private bool gameIsOver = false;
   //public GameObject stat1;
    //public GameObject stat2;
    //public GameObject stat3;

   //public TMP_Text stat1Text;
   //public TMP_Text stat2Text;
    //public TMP_Text stat3Text;

    Animator anim;
    //private EnemyHealth enemyHealth;
    //private HealthPickUp pickup;

    // Start is called before the first frame update
    void Start()
    {
        //pickup = GetComponent<HealthPickUp>();
        //enemyHealth = GetComponent<EnemyHealth>();
        anim = GetComponent<Animator>();
        //stat1Text = GetComponent<TextMeshProUGUI>();
        //stat2Text = GetComponent<TextMeshProUGUI>();
        //stat3Text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        /*stat1Text.text = "Total Kills: " + enemyHealth.totalKills;
        stat2Text.text = "Total Money Earned: " + enemyHealth.totalMoneyEarned;
        stat1Text.text = "Number of Health PickUps Acquired:  " + pickup.totalHealthPickupsAcquired;
        */
        if (playerHealth.currentHealth <= 0)
        {
            gameIsOver = true;
            anim.SetTrigger("GameOver");
            StartCoroutine(WaitALittle());
            //stat1.SetActive(true);
            //stat2.SetActive(true);
            //stat3.SetActive(true);
        }

        if (gameIsOver && Input.GetKeyDown(KeyCode.R))
        {
            Restart();
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene("Main Menu");
    }

    IEnumerator WaitALittle()
    {
        yield return new WaitForSeconds(2f);
        title.SetActive(true);
        restart.SetActive(true);
    }
}
