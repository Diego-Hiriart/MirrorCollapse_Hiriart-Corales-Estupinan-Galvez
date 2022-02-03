using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public InventoryObject inventory;
    public InventoryObject inventory2;

    [SerializeField] private GameObject playerPrefab;
    private GameObject player;
    private PlayerController playerControl;
    private List<ItemController> levelItems = new List<ItemController>();
    private List<EnemyController> levelEnemies = new List<EnemyController>();
    private EnemyList deadEnemies = new EnemyList();
    [SerializeField] private string level;
    [SerializeField] Vector3 levelStartPosition;
    [SerializeField] private GameController gameControl;


    // Start is called before the first frame update
    void Start()
    {
        if (!PrefsKeys.newGame && !PrefsKeys.sceneChanged)
        {
            LoadGame();//Try to load the game, since this scene might have been loaded by the main menu
        }
        else if(PrefsKeys.sceneChanged)
        {
            // spawn player
            this.player = Instantiate(playerPrefab, levelStartPosition, new Quaternion(0, 0, 0, 0));
            this.playerControl = this.player.GetComponent<PlayerController>();
            this.playerControl.GetPlayerInfo().SetInventory(PrefsKeys.inventory);
            this.playerControl.GetPlayerInfo().SetHealth(PrefsKeys.interLevelHealth);
            PrefsKeys.sceneChanged = false;
        }
        else if(PrefsKeys.newGame)
        {
            NewGame();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddItem(ItemController item)
    {
        this.levelItems.Add(item);
    }

    public void AddEnemy(EnemyController enemy)
    {
        this.levelEnemies.Add(enemy);
    }

    public void AddDefeatedEnemy(Enemy enemy)
    {
        this.deadEnemies.AddEnemy(enemy);
    }

    public void SaveGame()
    {
        SaveData save = this.CreateSaveData();
        //Save as binary to avoid cheating
        var bf = new BinaryFormatter();
        var filePath = Application.persistentDataPath + PrefsKeys.saveFileFormat + ".data";
        //Debug.Log(filePath);
        //Debug.Log("Saved ammo: "+save.GetPlayer().GetAmmoItem().GetAmmoAmount());       
        try
        {
            var fs = File.Create(filePath);
            bf.Serialize(fs, save);
            StartCoroutine(this.gameControl.ActivateSavedGameNotification());//Show saved game notification
            fs.Close();//Close file
        }
        catch (IOException fileException)
        {
            //This is here in case the player presses the save button too many times, a file error ocurrs because it cant be accessed
        }
        
    }

    private SaveData CreateSaveData()
    {
        return new SaveData(this.level, this.playerControl.GetPlayerInfo(), this.playerControl.GetPlayerInfo().GetInventory(), this.deadEnemies);
    }

    public void LoadGame()
    {
        string filePath = Application.persistentDataPath + PrefsKeys.saveFileFormat + ".data";
        if (File.Exists(filePath))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = File.Open(filePath, FileMode.Open);
            SaveData save = (SaveData)bf.Deserialize(fs);
            //Delete picked up items
            List<Object> toBeDeleted = new List<Object>();//Object list to use with items and enemies
            foreach (ItemController levelItem in this.levelItems)
            {
                foreach (Item playerItem in save.GetPlayerInventory())
                {                 
                    foreach (string id in playerItem.GetIds())
                    {
                        if (levelItem.GetItemID().Equals(id))
                        {
                            toBeDeleted.Add(levelItem);
                        }
                    }
                }
            }
            foreach (ItemController item in toBeDeleted)
            {                
                Destroy(item.gameObject);
            }
            toBeDeleted.Clear();//Reset list to use with enemies
            //Delete defeated enemies
            foreach (EnemyController levelEnemy in this.levelEnemies)
            {
                foreach (Enemy playerEnemy in save.GetPlayerDefeatedEnemies())
                {                    
                    foreach (string id in playerEnemy.GetIds())
                    {
                        if (levelEnemy.GetEnemyID().Equals(id))
                        {
                            toBeDeleted.Add(levelEnemy);
                        }
                    }
                }
            }
            foreach (EnemyController item in toBeDeleted)
            {
                Destroy(item.gameObject);
            }           

            List<float> pos = save.GetPlayer().GetTransform().GetPosition();
            List<float> rot = save.GetPlayer().GetTransform().GetRotation();
            this.player = Instantiate(playerPrefab, new Vector3(pos[0], pos[1], pos[2]), new Quaternion(rot[0], rot[1], rot[2], rot[3]));
            this.playerControl = this.player.GetComponent<PlayerController>();
            this.playerControl.GetPlayerInfo().SetInventory(save.GetPlayer().GetInventory());

            ClearInventory();

            foreach (var item in playerControl.GetPlayerInfo().GetInventory().GetItems())
            {
                string itemName = item.GetName();
                int amount = item.GetIds().Count;

                foreach (var item2 in inventory2.Container)
                {
                    if(item2.item.itemName == itemName)
                    {
                        if (!item.IsWeaponAmmo())//Normally add to inventory if it is not ammo
                        {
                            var newAmount = item.amountUsed;
                            amount -= newAmount;

                            if (amount > 0)
                            {
                                inventory.AddItem(item2.item, amount);
                                break;
                            }
                        }
                        else
                        {
                            (item2.item as AmmoObject).quantity = item.GetAmmoAmount();
                            inventory.AddItem(item2.item, 1);
                            break;
                        }
                        
                    }
                }
            }

            this.playerControl.GetPlayerInfo().SetHealth(save.GetPlayer().GetHealth());
            fs.Close();//Close file
            PrefsKeys.newGame = false;
        }
        else
        {
            this.player = Instantiate(playerPrefab, levelStartPosition, new Quaternion(0,0,0,0));
        }
        this.AddAmmoIfNeeded();
    }

    private void NewGame()
    {
        ClearInventory();
        this.player = Instantiate(playerPrefab, levelStartPosition, new Quaternion(0, 0, 0, 0));
        this.playerControl = this.player.GetComponent<PlayerController>();
        this.AddAmmoIfNeeded();
    }

    public void AddToPlayerInventory(Item item)
    {
        this.playerControl.GetPlayerInfo().AddToInventory(item);
    }

    private void OnApplicationQuit()
    {
        ClearInventory();
    }

    private void ClearInventory()
    {
        inventory.Container.Clear();
    }

    private void AddAmmoIfNeeded(){
        if(this.playerControl.GetPlayerInfo().GetAmmoItem() is null){
            Item emptyAmmo = new Item(true, "GunAmmo", true);
            emptyAmmo.SetAmmoAmount(0);
            this.playerControl.GetPlayerInfo().AddToInventory(emptyAmmo);
        }
    }
}
