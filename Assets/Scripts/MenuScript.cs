using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    [SerializeField] GameObject Intro;
    [SerializeField] GameObject Intro1;
    [SerializeField] GameObject Intro2;
    [SerializeField] GameObject Intro3;

    [SerializeField] GameObject Tuto;
    [SerializeField] GameObject Tuto1;

    [SerializeField] GameObject Credits;
    private enum MenuState { noIntro, intro, intro2, intro3 }
    MenuState state;
    // Start is called before the first frame update
    void Start()
    {
        state = MenuState.noIntro;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Continue()
    {
        switch (state)
        {
            case MenuState.noIntro:
                Intro.SetActive(true);
                Intro1.SetActive(true);
                Intro1.GetComponent<TextScript>().Write();
                state = MenuState.intro;
                break;
            case MenuState.intro:
                Intro1.SetActive(false);
                Intro2.SetActive(true);
                Intro2.GetComponent<TextScript>().Write();
                state = MenuState.intro2;
                break;
            case MenuState.intro2:
                Intro2.SetActive(false);
                Intro3.SetActive(true);
                Intro3.GetComponent<TextScript>().Write();
                state = MenuState.intro3;
                break;
            case MenuState.intro3   :
                OnRun();
                break;
        }
    }

    public void OnRun()
    {

        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    public void OnBR()
    {
        SceneManager.LoadScene(3, LoadSceneMode.Single);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void OnTutorial()
    {
        Tuto.gameObject.SetActive(true);
        Tuto1.GetComponent<TextScript>().Write();
    }
    public void OnCredits()
    {
        SceneManager.LoadScene(2, LoadSceneMode.Single);
    }

    public void OnBack()
    {
        Tuto.gameObject.SetActive(false); 
    }
}
