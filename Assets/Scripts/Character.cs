using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
public class Character
{
    protected SaveTransform characterTransform;
    protected float characterHealth;
    protected float minHealth = 0;
    protected float maxHeatth = 100;

    public Character() { }

    public Character(SaveTransform transf, float health)
    {
        this.characterTransform = transf;
        this.characterHealth = health;
    }

    public void SetTransform(SaveTransform transf)
    {
        this.characterTransform = transf;
    }

    public SaveTransform GetTransform()
    {
        return this.characterTransform;
    }

    public void SetHealth(float value)
    {
        this.characterHealth = value;
    }

    public float GetHealth()
    {
        return this.characterHealth;
    }
}

