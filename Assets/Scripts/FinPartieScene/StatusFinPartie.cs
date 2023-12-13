using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatusFinPartie : MonoBehaviour
{
    [SerializeField] GameObject victoireUI;
    [SerializeField] GameObject défaiteUI;
    [SerializeField] Image imagePanel;
    [SerializeField] TextMeshProUGUI titreDéfaiteVictoire;
    [SerializeField] TextMeshProUGUI description;
    
    string victoireDescription = "Votre adversaire s'est fait attraper avant vous! Vous avez gagné.";
    string défaiteDescription = "Vous vous êtes faites attraper avant votre adversaire! Vous avez perdu.";

    Color32 victoireCouleurPanel = new Color32(162, 255, 116, 150);
    Color32 défaiteCouleurPanel = new Color32(255, 120, 116, 150);

    bool victoire = false;

    private void Awake()
    {
        if (victoire)
        {
            défaiteUI.SetActive(false);
            victoireUI.SetActive(true);
            titreDéfaiteVictoire.text = "Victoire";
            description.text = victoireDescription;
            imagePanel.color = victoireCouleurPanel;
        }
        else
        {
            victoireUI.SetActive(false);
            défaiteUI.SetActive(true);
            titreDéfaiteVictoire.text = "Défaite";
            description.text = défaiteDescription;
            imagePanel.color = défaiteCouleurPanel;
        }
    }
}
