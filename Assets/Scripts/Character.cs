using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
public class Character
{
    protected Transform characterTransform;
    protected float characterHealth;

    public Character() { }

    public Character(Transform transf, float health)
    {
        this.characterTransform = transf;
        this.characterHealth = health;
    }

    protected void SetTransform(Transform transf)
    {
        this.characterTransform = transf;
    }

    protected Transform GetTransform()
    {
        return this.characterTransform;
    }

    protected void SetHealth(float value)
    {
        this.characterHealth = value;
    }

    protected float GetHealth()
    {
        return this.characterHealth;
    }
}

