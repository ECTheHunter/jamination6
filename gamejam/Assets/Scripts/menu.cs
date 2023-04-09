using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour
{
    public void Play_Button()
    {
      SceneManager.LoadScene(2);

    }
    public void Controls_Button()
    {
        SceneManager.LoadScene(1);
    }


    public void Quit_Button()
    {
       Application.Quit();
    }





















}
