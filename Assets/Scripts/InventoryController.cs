using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryController : MonoBehaviour
{
    [SerializeField]
    private Button itemsButton;
    [SerializeField]
    private Button weaponsButton;
    [SerializeField]
    private Button recoveryButton;
    [SerializeField]
    private GameObject titlePanel;
    private TextMeshProUGUI titleText;
    [SerializeField]
    private GameObject itemsPanel;   
    [SerializeField]
    private GameObject descriptionPanel;
    private TextMeshProUGUI itemName;
    private TextMeshProUGUI itemDescription;
    [SerializeField]
    private GameObject weaponPanel;
    private Image weaponIcon;
    private TextMeshProUGUI ammoText;
    [SerializeField]
    private GameObject healthPanel;
    private Image healthBar;

    private void Awake()
    {
        this.itemsButton.onClick.AddListener(delegate { OpenItems(); } );
        this.weaponsButton.onClick.AddListener(delegate { OpenWeapons(); } );
        this.recoveryButton.onClick.AddListener(delegate { OpenRecovery(); } );
        this.titleText = this.titlePanel.GetComponentInChildren<TextMeshProUGUI>();
        this.itemName = this.descriptionPanel.GetComponentInChildren<TextMeshProUGUI>();
        this.itemDescription = this.descriptionPanel.GetComponentInChildren<TextMeshProUGUI>();
        this.weaponIcon = this.weaponPanel.GetComponentInChildren<Image>();
        this.ammoText = this.weaponPanel.GetComponentInChildren<TextMeshProUGUI>();
        this.healthBar = this.healthPanel.GetComponentInChildren<Image>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OpenItems()
    {

    }

    private void OpenWeapons()
    {

    }

    private void OpenRecovery()
    {

    }
}
