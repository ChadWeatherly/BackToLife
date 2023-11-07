using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;

public class MemoryBar : MonoBehaviour
{
    public Slider orpheusMemorySlider;      // access the memory slider -deb
    public Gradient gradient;
    public Image fill;

    // Start is called before the first frame update
    void Start()
    {
        orpheusMemorySlider.value = Updater.orpheusMemory;  // accesses the current memory -deb
        fill.color = gradient.Evaluate(orpheusMemorySlider.value);

        UpdateMemory(Updater.orpheusMemory);                      // sends the current fill amount to update memory -deb
    }

    // Update is called once per frame
    void Update()
    {
        //float newMem = Updater.orpheusMemory;               // checks if memory amount has changed -deb
        //UpdateMemory(newMem);                               // sends changed memory amount to update memory -deb
    }

    public void UpdateMemory(float mem)
    {
        orpheusMemorySlider.value = mem;                    // updates the slider value with amount coming in -deb
        fill.color = gradient.Evaluate(orpheusMemorySlider.value);
    }
}
