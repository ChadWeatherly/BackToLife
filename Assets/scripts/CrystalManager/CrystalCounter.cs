using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class CrystalCounter : MonoBehaviour
{
    public int crystalNum = 0;        // adds to the crystalTxt count - deb
    
    public void CrystalCollection()
    {
        crystalNum = Updater.crystalCount += 1;     // updates the crystal counter -deb
    }
}
