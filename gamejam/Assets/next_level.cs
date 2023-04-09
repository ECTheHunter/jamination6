using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class next_level : MonoBehaviour
{
   public int sceneBuildintex;


   private void OnTriggerEnter2D(Collider2D other) 
   {
        print("Trigger Enterad");
   
   
        if(other.tag == "Player")
        {
            print("Switching Scene to " + sceneBuildintex);
            SceneManager.LoadScene(sceneBuildintex, LoadSceneMode.Single);


        }
   
   
   
   }














}
