using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MenuScript : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject helpPanel;
    [SerializeField] private GameObject background;

    [HideInInspector] public bool isOpen = false;
    //private AnimatorStateInfo openStateInfo;
    //private AnimatorStateInfo closeStateInfo;

    void Start()
    {
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isOpen = !isOpen;
            animator.SetBool("isMenuOpen", isOpen);
            background.SetActive(isOpen);
            ExitHelpButton();
        }
    }

    public void ContinueButton()
    {
        isOpen = !isOpen;
        animator.SetBool("isMenuOpen", isOpen);
        background.SetActive(isOpen);
        ExitHelpButton();
    }

    public void HelpButton()
    {
        menuPanel.SetActive(false);
        helpPanel.SetActive(true);
    }

    public void ExitHelpButton()
    {
        menuPanel.SetActive(true);
        helpPanel.SetActive(false);
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public void NewButton()
    {
        StartCoroutine(ReloadSceneAfterDelay(2f));
    }

    public void PauseContinue()
    {
        if (isOpen == false)
        {
            Time.timeScale = 1;
        }
        else
        {
            Time.timeScale = 0;
        }
    }

    IEnumerator ReloadSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
