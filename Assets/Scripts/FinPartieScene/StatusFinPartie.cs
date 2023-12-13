using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatusFinPartie : MonoBehaviour
{
    [SerializeField] GameObject victoireUI;
    [SerializeField] GameObject d�faiteUI;
    [SerializeField] Image imagePanel;
    [SerializeField] TextMeshProUGUI titreD�faiteVictoire;
    [SerializeField] TextMeshProUGUI description;
    
    string victoireDescription = "Votre adversaire s'est fait attraper avant vous! Vous avez gagn�.";
    string d�faiteDescription = "Vous vous �tes faites attraper avant votre adversaire! Vous avez perdu.";

    Color32 victoireCouleurPanel = new Color32(162, 255, 116, 150);
    Color32 d�faiteCouleurPanel = new Color32(255, 120, 116, 150);

    bool victoire = false;

    private void Awake()
    {
        if (victoire)
        {
            d�faiteUI.SetActive(false);
            victoireUI.SetActive(true);
            titreD�faiteVictoire.text = "Victoire";
            description.text = victoireDescription;
            imagePanel.color = victoireCouleurPanel;
        }
        else
        {
            victoireUI.SetActive(false);
            d�faiteUI.SetActive(true);
            titreD�faiteVictoire.text = "D�faite";
            description.text = d�faiteDescription;
            imagePanel.color = d�faiteCouleurPanel;
        }
    }
}
