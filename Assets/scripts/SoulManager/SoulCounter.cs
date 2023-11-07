using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulCounter : MonoBehaviour
{
    public int soulNum = 0;        // adds to the crystalTxt count - deb

    public void SoulCollection()
    {
        soulNum = Updater.soulFragments += 1;     // updates the crystal counter -deb
    }
}
