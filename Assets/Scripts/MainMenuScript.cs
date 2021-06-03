using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenuScript : MonoBehaviour
{
   
   public GameObject menuPlayButton, menuCommandButton, menuExitButton, menuReturnButton;
   public void PlayTheGame()
   {
      SceneManager.LoadScene("Race");
      
      EventSystem.current.SetSelectedGameObject(null);
        
      EventSystem.current.SetSelectedGameObject(menuPlayButton);
   }

   public void CommandMenu()
   {
      SceneManager.LoadScene("Command_Menu");
      
      EventSystem.current.SetSelectedGameObject(null);
        
      EventSystem.current.SetSelectedGameObject(menuCommandButton);
   }
   
   public void ReturnMenu()
   {
      SceneManager.LoadScene("Main_Menu");

      EventSystem.current.SetSelectedGameObject(null);
        
      EventSystem.current.SetSelectedGameObject(menuReturnButton);
   }

   public void QuitTheGame()
   {
      Debug.Log("QUIT THE GAME");
      Application.Quit();
      
      EventSystem.current.SetSelectedGameObject(null);
        
      EventSystem.current.SetSelectedGameObject(menuExitButton);
   }
}
