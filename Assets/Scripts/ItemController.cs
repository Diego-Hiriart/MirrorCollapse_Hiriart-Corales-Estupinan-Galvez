using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    private Item item;
    [SerializeField]
    private bool isPickable;
    [SerializeField]
    private bool isWeaponAmmo;
    [SerializeField]
    private string itemName;
    private LevelController level;
    [SerializeField]
    private string itemID;

    // Start is called before the first frame update
    void Start()
    {
        this.item = new Item(isWeaponAmmo, itemName, isPickable, this.itemID);
        this.level = GetComponentInParent<LevelController>();
        this.AddThisToLevel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void AddThisToLevel()
    {
        this.level.AddItem(this);
    }

    public void PickItemUp()
    {
        StartCoroutine(PlayAudio());       
        if (this.item.IsPickable())
        {                     
            level.AddToPlayerInventory(this.item);
        }
    }

    private IEnumerator PlayAudio()
    {
        this.GetComponent<AudioSource>().Play();
        foreach (Renderer renderer in this.GetComponentsInChildren<Renderer>())
        {
            renderer.enabled = false;
        }
        yield return new WaitForSeconds(1.5f);
        Destroy(this.gameObject);
    }

    public Item GetItem()
    {
        return this.item;
    }

    public string GetItemID()
    {
        return this.itemID;
    }
}
