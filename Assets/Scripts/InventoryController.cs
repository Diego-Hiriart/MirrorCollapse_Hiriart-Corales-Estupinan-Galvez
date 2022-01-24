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
    [SerializeField] InventoryObject inventory;

    [SerializeField] GameController gameController;

    private void Awake()
    {
        // this.useEquipButton.onClick.AddListener(delegate { UseEquipItem(); } );
        this.exitButton.onClick.AddListener(delegate { ExitInventory(); } );
        this.itemName = this.descriptionPanel.transform.GetChild(0).GetComponentInChildren<TextMeshProUGUI>();
        this.itemDescription = this.descriptionPanel.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>();
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

        foreach (var item in inventory.Container)
        {
            if(item.item.itemName == selectedItem.name)
            {
                itemName.text = item.item.itemName;
                itemDescription.text = item.item.description;
                break;
            }
        }

        
    }

    public void UseEquipItem()
    {
        if(selectedItem)
        {
            var inv = inventory.Container;

            foreach (var item in inv)
            {
                if(item.item.itemName == selectedItem.name)
                {
                    if(item.item.type == ItemType.Health)
                    {
                        if(item.amount > 1)
                        {
                            item.amount--;
                            var text = selectedItem.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
                            text.text = item.amount.ToString("n0");
                        }
                        else if(item.amount == 1)
                        {
                            inv.Remove(item);
                            Destroy(selectedItem);

                            itemName.text = "";
                            itemDescription.text = "";
                        }

                        break;
                    }
                    else if(item.item.type == ItemType.Weapon)
                    {
                        //equip weapon
                    }
                    else
                    {
                        // do nothing if its ammo or collectibles
                    }
                }
            }
        }
    }

    public void ExitInventory()
    {
        gameController.OpenCloseInventory();
    }
}
