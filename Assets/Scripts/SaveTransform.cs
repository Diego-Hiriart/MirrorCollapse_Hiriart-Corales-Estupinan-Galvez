using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[Serializable]
public class SaveTransform
{
    public float xPos { set; get; }
    public float yPos { set; get; }
    public float zPos { set; get; }
    public float xRot { set; get; }
    public float yRot { set; get; }
    public float zRot { set; get; }
    public float w { set; get; }

    public SaveTransform() { }

    public SaveTransform(float xP, float yP, float zP, float xR, float yR, float zR, float w)
    {
        this.xPos = xP;
        this.yPos = yP;
        this.zPos = zP;
        this.xRot = xR;
        this.yRot = yR;
        this.zRot = zR;
        this.w = w;
    }

    public List<float> GetPosition()
    {      
        return new List<float> { this.xPos, this.yPos, this.zPos };
    }

    public List<float> GetRotation()
    {
        return new List<float> { this.xRot, this.yRot, this.zRot, this.w };
    }
}

