using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Perdant
{
    public static string LePerdant { get; set; }
    public static void AnnoncerPerdant(string p)
    {
        LePerdant = p;
        SceneManager.LoadScene(2);
    }
}
