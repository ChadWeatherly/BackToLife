using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CrystalText : MonoBehaviour
{
    private TextMeshProUGUI crystalTxt;

    // Start is called before the first frame update
    void Start()
    {
        crystalTxt = GetComponent<TextMeshProUGUI>();
        crystalTxt.text = "Crystals: ";
    }

    // Update is called once per frame
    void Update()
    {
        crystalTxt.text = "Crystals: ";
    }
}
