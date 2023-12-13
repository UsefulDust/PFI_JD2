using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuBouton : MonoBehaviour
{
    public void JouerJeu()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitterApplication()
    {
        Application.Quit();
    }
}
