using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerTriggers : MonoBehaviour
{
    [SerializeField] private RichnessBar richnessBar;
    [SerializeField] private TMP_Text billsText;
    [SerializeField] private TMP_Text pointsText;

    [SerializeField] private Animator playerAnimator;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Canvas victoryCanvas;

    private void Start()
    {
        victoryCanvas.enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bills")
        {
            richnessBar.AddBills();
            other.enabled = false;
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "Alcohol")
        {
            richnessBar.RemoveBills();
            other.enabled = false;
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "Dollar")
        {
            Destroy(other.gameObject);
            victoryCanvas.enabled = true;
            playerController.enabled = false;
            richnessBar.enabled = false;

            playerAnimator.SetTrigger("Victory");
            playerAnimator.applyRootMotion = true;

            billsText.text = richnessBar.GetCurBills().ToString("0");
            pointsText.text = "0";

        }
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
