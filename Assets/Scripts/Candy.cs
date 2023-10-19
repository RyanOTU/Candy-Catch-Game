using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candy : MonoBehaviour
{
    [SerializeField] private int pointAmount;
    public int GetPointValue()
    {
        return pointAmount;
    }
}
