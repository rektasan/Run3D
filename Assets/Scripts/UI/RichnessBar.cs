using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RichnessBar : MonoBehaviour
{
    [SerializeField] private Image richBar;
    [SerializeField] private TMP_Text richText;
    [SerializeField] private TMP_Text pointsText;
    [Space]
    [SerializeField] private Transform player;
    [SerializeField] private PlayerController playerController;
    [Space]
    [SerializeField] private GameObject[] playerPrefabs;
    [SerializeField] private Animator playerAnimator;

    private float maxBills = 100;
    private float curBills;

    
    void Start()
    {
        SetPoor();
        
        curBills = 20;
    }
    void Update()
    {
        DetermineRichess();

        richBar.fillAmount = curBills / maxBills;

        if (curBills <= 0f)
        {
            playerAnimator.SetBool("isDefeated", true);
            StartCoroutine(LoseCoroutine());
        }

        pointsText.text = curBills.ToString("0");
    }

    private void DetermineRichess()
    {
        if ( curBills/maxBills >= 0.5f && curBills/maxBills <= 0.8f)
        {
            SetAverage();
        }
        else if (curBills/maxBills >= 0.8f)
        {
            SetRich();
        }
        else
        {
            SetPoor();
        }
    }

    private void SetPoor()
    {
        playerPrefabs[0].SetActive(true);
        playerPrefabs[1].SetActive(false);
        playerPrefabs[2].SetActive(false);

        playerAnimator.SetTrigger("Poor");

        richText.color = Color.red;
        richBar.color = Color.red;
        richText.text = "Poor";
    }
    private void SetAverage()
    {
        playerPrefabs[0].SetActive(false);
        playerPrefabs[1].SetActive(true);
        playerPrefabs[2].SetActive(false);

        playerAnimator.SetTrigger("Average");

        richText.color = Color.yellow;
        richBar.color = Color.yellow;
        richText.text = "Average";
    }
    private void SetRich()
    {
        playerPrefabs[0].SetActive(false);
        playerPrefabs[1].SetActive(false);
        playerPrefabs[2].SetActive(true);

        playerAnimator.SetTrigger("Rich");

        richText.color = Color.green;
        richBar.color = Color.green;
        richText.text = "Rich";
    }
    public void AddBills()
    {
        curBills += 2f;
    }
    public void RemoveBills()
    {
        curBills -= 10f;
    }
    public float GetCurBills()
    {
        return curBills;
    }
    IEnumerator LoseCoroutine()
    {
        curBills = 0f;
        playerController.enabled = false;
        
        yield return new WaitForSeconds(1.5f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
