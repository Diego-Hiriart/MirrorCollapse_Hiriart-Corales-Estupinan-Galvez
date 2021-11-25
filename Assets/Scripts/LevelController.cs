using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    [SerializeField]
    private GameObject playerPrefab;
    private GameObject player;
    private PlayerController playerControl;
    private ItemList levelItems = new ItemList();
    private EnemyList levelEnemies = new EnemyList();
    [SerializeField]
    private string level;

    // Start is called before the first frame update
    void Start()
    {
        LoadGame();//Try to load the game, since this scene might have been loaded by the main menu
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddItem(Item item, string newID)
    {
        this.levelItems.AddItem(item, newID);
    }

    public void SaveGame()
    {
        SaveData save = this.CreateSaveData();
        //Save as binary to avoid cheating
        var bf = new BinaryFormatter();
        var filePath = Application.persistentDataPath + PrefsKeys.saveFileFormat + ".data";
        Debug.Log(filePath);

        var fs = File.Create(filePath);
        bf.Serialize(fs, save);
    }

    private SaveData CreateSaveData()
    {
        return new SaveData(this.level, this.playerControl.GetPlayerInfo(), this.playerControl.GetPlayerInfo().GetInventory(), this.levelEnemies);
    }

    public void LoadGame()
    {
        string filePath = Application.persistentDataPath + PrefsKeys.saveFileFormat + ".data";
        if (File.Exists(filePath))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = File.Open(filePath, FileMode.Open);
            SaveData save = (SaveData)bf.Deserialize(fs);

            foreach (Item levelItem in this.levelItems.GetItems())
            {
                foreach (Item playerItem in save.GetPlayerInventory())
                {
                    foreach (string id in playerItem.GetIds())
                    {

                    }

                }                
            }

            
            List<float> pos = save.GetPlayer().GetTransform().GetPosition();
            List<float> rot = save.GetPlayer().GetTransform().GetRotation();
            this.player = Instantiate(player, new Vector3(pos[0], pos[1], pos[2]), new Quaternion(rot[0], rot[1], rot[2], rot[3]));
            this.playerControl = this.player.GetComponent<PlayerController>();
            this.playerControl.GetPlayerInfo().SetHealth(save.GetPlayer().GetHealth());
        }
        else
        {
            Instantiate(player, new Vector3(-30, 3.25f, -45), new Quaternion(0,0,0,0));
        }
    }

    public void AddToPlayerInventory(Item item, string newID)
    {
        this.playerControl.GetPlayerInfo().AddToInventory(item, newID);
    }
}
