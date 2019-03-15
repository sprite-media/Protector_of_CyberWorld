using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Button : MonoBehaviour
{
    [Header("Indicator")]
    [SerializeField] AudioSource audio;
    [SerializeField] GameObject visual_indicator;

    public enum Button
    {
        MainMenu,
        Resume,
        Exit,
        Start,
        Hovering
    }

    public Button ButtonType;

    public void HoverOnButton()
    {
        //Visual
        if (visual_indicator)
            visual_indicator.SetActive(true);

        //Sound
        if (audio)
            audio.Play();
    }
    public void ExitHover()
    {
        //Visual
        if (visual_indicator)
            visual_indicator.SetActive(false);
    }
    public void GoTomainMenu()
    {
        Debug.Log("MainMenu");
        if (audio)
            audio.Play();
        SceneManager.LoadScene("MainMenu_woPlayer");
        GameObject.Find("Player").transform.position = new Vector3(0.28f, 0.157f, -0.81f);
    }
    public void Exit()
    {
        Debug.Log("Exit");
        if (audio)
            audio.Play();
        Application.Quit();
    }
    public void Resume()
    {
        Debug.Log("Resume");
        if (audio)
            audio.Play();
        //Time.timeScale = 1.0f;
    }
    public void StartGame()
    {
        if (audio)
            audio.Play();
        StartCoroutine("WaitForStart");

    }

    IEnumerator WaitForStart()
    {
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene("tempMap");
        GameObject.Find("Player").transform.position = new Vector3(10.0f, 0.01f, 1.0f);
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Hand")
        {
            switch ((int)ButtonType)
            {
                case (int)Button.MainMenu:
                    GoTomainMenu();
                    break;
                case (int)Button.Resume:
                    Exit();
                    break;
                case (int)Button.Exit:
                    Resume();
                    break;
                case (int)Button.Start:
                    StartGame();
                    break;
                case (int)Button.Hovering:
                    HoverOnButton();
                    break;
            }
        }
    }
    private void OnTriggerExit(Collider col)
    {
        if (col.tag == "Hand")
        {
            switch ((int)ButtonType)
            {
                case (int)Button.Hovering :
                        ExitHover();
                    break;
            }
        }
    }
}
