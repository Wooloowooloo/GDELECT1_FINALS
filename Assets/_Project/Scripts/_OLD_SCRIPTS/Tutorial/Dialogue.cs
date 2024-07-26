using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public string nameChar;

    [TextArea(10, 10)]
    public string[] sentences; 
}