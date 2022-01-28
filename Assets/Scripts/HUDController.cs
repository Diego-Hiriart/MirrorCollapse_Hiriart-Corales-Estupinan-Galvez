using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDController : MonoBehaviour
{
    [SerializeField]
    private GameObject saveText;

    private void Start()
    {
        this.saveText.SetActive(false);
    }

    public void ActivateDeactivateSaveNotificaction(bool status)
    {
        this.saveText.SetActive(status);
    }
}
