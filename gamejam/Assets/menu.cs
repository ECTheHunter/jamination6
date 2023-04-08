using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour
{
    public void Play_Button()
    {
      SceneManager.LoadScene(0);

    }



    public void Quit_Button()
    {
       Application.Quit();
    }





















}
