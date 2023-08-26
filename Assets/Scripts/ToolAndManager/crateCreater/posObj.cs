using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class posObj
{
    public Transform leftPoint;
    public Transform rightPoint;

    public float getLeftX()
    {
        return leftPoint.position.x;
    }

    public float getRightX()
    {
        return rightPoint.position.x;
    }

    public float getY()
    {
        return Mathf.Min(leftPoint.position.y, rightPoint.position.y);
    }
}
