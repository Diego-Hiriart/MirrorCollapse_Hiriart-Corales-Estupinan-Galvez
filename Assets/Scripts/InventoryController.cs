using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryController : MonoBehaviour
{
    [SerializeField] private Button useEquipButton;
    [SerializeField] private Button exitButton;
    private TextMeshProUGUI titleText;
    [SerializeField] private GameObject itemsPanel;
    [SerializeField] private GameObject descriptionPanel;
    private TextMeshProUGUI itemName;
    private TextMeshProUGUI itemDescription;
    [SerializeField] private GameObject weaponPanel;
    private Image weaponIcon;
    private TextMeshProUGUI ammoText;
    [SerializeField] private GameObject healthPanel;
    private Image healthBar;

    GameObject selectedItem;

    [SerializeField] GameController gameController;

    private void Awake()
    {
        this.useEquipButton.onClick.AddListener(delegate { UseEquipItem(); } );
        this.exitButton.onClick.AddListener(delegate { ExitInventory(); } );
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

    public void SelectItem(GameObject itemImage)
    {
        selectedItem = itemImage;
    }

    public void UseEquipItem()
    {
        
    }

    public void ExitInventory()
    {
        gameController.OpenCloseInventory();
    }
}
