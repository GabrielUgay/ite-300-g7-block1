using UnityEngine;

public class AlmanacController : MonoBehaviour
{
    // References to the Main Menu, Almanac, and each Engkanto's information panels
    public GameObject mainMenuPanel;
    public GameObject almanacPanel;
    public GameObject sirenaPanel;
    public GameObject nunoPanel;
    public GameObject aswangPanel;
    public GameObject kaprePanel;
    public GameObject tiyanakPanel;

    // Function to show Sirena's information when the button is clicked
    public void OnSirenaButtonClick()
    {
        almanacPanel.SetActive(false); // hides the almanac panel
        sirenaPanel.SetActive(true);  // shows the sirena panel
    }

    // Function to show Nuno sa Punso's information
    public void OnNunoButtonClick()
    {
        almanacPanel.SetActive(false);
        nunoPanel.SetActive(true);
    }

    // Function to show Aswang's information
    public void OnAswangButtonClick()
    {
        almanacPanel.SetActive(false);
        aswangPanel.SetActive(true);
    }

    // Function to show Kapre's information
    public void OnKapreButtonClick()
    {
        almanacPanel.SetActive(false);
        kaprePanel.SetActive(true);
    }

    // Function to show Tiyanak's information
    public void OnTiyanakButtonClick()
    {
        almanacPanel.SetActive(false);
        tiyanakPanel.SetActive(true);
    }

    // Function for the Back button to return to the Almanac panel
    public void OnBackButtonClick()
    {
        almanacPanel.SetActive(true);
        sirenaPanel.SetActive(false);
        nunoPanel.SetActive(false);
        aswangPanel.SetActive(false);
        kaprePanel.SetActive(false);
        tiyanakPanel.SetActive(false);
    }

    // Function to go back to the Main Menu from the Almanac panel
    public void OnBackToMainMenuClick()
    {
        almanacPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    public void OnAlmanacPanelClick()
    {
        almanacPanel.SetActive(true);
        mainMenuPanel.SetActive(false);
    }
}
