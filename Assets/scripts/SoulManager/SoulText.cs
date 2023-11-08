using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SoulText : MonoBehaviour
{
    private TextMeshProUGUI soulTxt;

    // Start is called before the first frame update
    void Start()
    {
        soulTxt = GetComponent<TextMeshProUGUI>();
        soulTxt.text = "Soul Fragments: ";
    }

    // Update is called once per frame
    void Update()
    {
        soulTxt.text = "Soul Fragments: ";
    }
}
