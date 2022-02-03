using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    [SerializeField] GameObject creditsUI;
    [SerializeField] LevelController levelController;
    bool rollCredits;
    float opacity = 0;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SaveGame());
        StartCoroutine(StartCredits());
    }

    IEnumerator SaveGame()
    {
        yield return new WaitForSeconds(0.1f);
        levelController.SaveGame();
    }

    // Update is called once per frame
    void Update()
    {
        if(rollCredits)
        {
            opacity += 0.01f;
            var bg = creditsUI.transform.GetChild(0).GetComponent<Image>();
            bg.color = new Color(0, 0, 0, opacity);

            var text = creditsUI.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            text.color = new Color(255, 255, 255, opacity);

            text = creditsUI.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
            text.color = new Color(255, 255, 255, opacity);

            text = creditsUI.transform.GetChild(3).GetComponent<TextMeshProUGUI>();
            text.color = new Color(255, 255, 255, opacity);

            text = creditsUI.transform.GetChild(4).GetComponent<TextMeshProUGUI>();
            text.color = new Color(255, 255, 255, opacity);
        }
    }

    public IEnumerator ShowCredits()
    {
        yield return new WaitForSeconds(15f);

        Cursor.lockState = CursorLockMode.None;

        SceneManager.LoadScene(0);
    }

    public IEnumerator StartCredits()
    {
        yield return new WaitForSeconds(15f);
        rollCredits = true;

        yield return ShowCredits();
    }
}
