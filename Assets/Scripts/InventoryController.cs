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
    PlayerController playerController;

    [SerializeField] GameObject endMenu;

    float prevHealth;
    float newHealth;
    
    int count = 0;

    private void Awake()
    {
        // this.useEquipButton.onClick.AddListener(delegate { UseEquipItem(); } );
        this.exitButton.onClick.AddListener(delegate { ExitInventory(); } );
        this.itemName = this.descriptionPanel.transform.GetChild(0).GetComponentInChildren<TextMeshProUGUI>();
        this.itemDescription = this.descriptionPanel.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>();
        this.weaponIcon = this.weaponPanel.GetComponentInChildren<Image>();
        this.ammoText = this.weaponPanel.GetComponentInChildren<TextMeshProUGUI>();
        this.healthBar = this.healthPanel.transform.GetChild(1).GetChild(1).GetComponentInChildren<Image>();
        weaponPanel.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "-";
    }

    // Update is called once per frame
    void Update()
    {
        if(count == 0)
        {
            playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
            prevHealth = playerController.GetPlayerInfo().GetHealth();
            SetHealthUI(playerController.GetPlayerInfo().GetHealth());

            count++;
        }

        newHealth = playerController.GetPlayerInfo().GetHealth();
        if(newHealth != prevHealth)
        {
            prevHealth = newHealth;
            SetHealthUI(newHealth);
        }    

        HandleBullets();
    }

    private void OpenItems()
    {
        foreach (var item in inventory.Container)
        {
            if(item.item is AmmoObject)
            {
                
            }
            else
            {
                
            }
        }
    }

    public void SetHealthUI(float newHp)
    {
        healthBar.fillAmount = newHp / playerController.GetPlayerInfo().GetMaxHealth();
    }

    public void SelectItem(GameObject itemImage)
    {
        selectedItem = itemImage;

        foreach (var item in inventory.Container)
        {
            if(item.item.itemName == selectedItem.name)
            {
                itemName.text = item.item.name;
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
            var invItems = playerController.GetPlayerInfo().GetInventory();

            foreach (var item in inv)
            {
                if(item.item.itemName == selectedItem.name)
                {
                    if(item.item is HealthObject)
                    {
                        var healthItem = item.item as HealthObject;

                        if(item.amount > 1)
                        {
                            item.amount--;
                            var text = selectedItem.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
                            text.text = item.amount.ToString("n0");
                            
                            foreach (var item2 in invItems.GetItems())
                            {
                                if(item2.GetName() == item.item.itemName)
                                    item2.amountUsed++;
                                //{
                                //    invItems.RemoveItem(item2);
                                //    break;
                                //}
                            }
                        }
                        else if(item.amount == 1)
                        {
                            foreach (var item2 in invItems.GetItems())
                            {
                                if(item2.GetName() == item.item.itemName)
                                    item2.amountUsed++;
                                //{
                                //    invItems.RemoveItem(item2);
                                //}
                            }

                            inv.Remove(item);                            
                            Destroy(selectedItem);

                            itemName.text = "";
                            itemDescription.text = "";
                        }

                        

                        playerController.ChangeHealth(healthItem.restoreHealthValue, true);

                        break;
                    }
                    else if(item.item is WeaponObject)
                    {
                        var weapon = item.item as WeaponObject;
                        if(weapon.isGun == true)
                        {
                            playerController.EquipWeapon(true);
                            weaponPanel.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "-";
                        }
                        else
                        {
                            playerController.EquipWeapon(false);
                            weaponPanel.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "∞";
                        }

                        weaponPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = weapon.itemName;
                    }
                    else if(item.item is AmmoObject)
                    {
                        if(playerController.pistol.activeSelf)
                        {
                            float ammoQuantity = 0;
                            var ammo = item.item as AmmoObject;
                            ammoQuantity = item.amount * ammo.quantity;
                            
                            weaponPanel.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = ammoQuantity.ToString("n0");
                        }
                    }
                    else if(item.item.type == ItemType.Collectible)
                    {
                        // do nothing
                    }
                }
            }
        }
    }

    public void HandleBullets()
    {
        if(playerController.pistol.activeSelf)
        {
            foreach (var item in inventory.Container)
            {
                if(item.item is AmmoObject)
                {
                    var ammo = item.item as AmmoObject;

                    weaponPanel.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = (item.amount * ammo.quantity).ToString("n0");
                }
            }
        }
    }

    public void ExitInventory()
    {
        gameController.OpenCloseInventory();
    }
}
