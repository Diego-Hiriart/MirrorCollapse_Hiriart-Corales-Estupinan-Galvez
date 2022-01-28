using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    [SerializeField] GameObject switcher;
    [SerializeField] List<GameObject> lamps = new List<GameObject>();

    public void TurnOnOffLights()
    {
        switcher.transform.Rotate(180, 0, 0);

        foreach (var lamp in lamps)
        {
            var light = lamp.GetComponentInChildren<Light>();
            light.enabled = !light.enabled;
        }
    }
}
