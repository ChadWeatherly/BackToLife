using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrpheusStrength : MonoBehaviour
{
    public Slider orpheusStrengthSlider;                                // accesses the strength slider -deb
    public Gradient gradient;                                           // assigns the color the strenth slider should be -deb
    public Image fill;                                                  // assigns the amount of fill the slider should be -deb

    // Start is called before the first frame update
    void Start()
    {
        orpheusStrengthSlider.value = Updater.orpheusStrength;          // accesses the current memory -deb
        fill.color = gradient.Evaluate(orpheusStrengthSlider.value);    // sets the slider fill with current memory  -deb
        UpdateStrength(Updater.orpheusStrength);                        // sends the current memory to update strength  -deb
    }

    // Update is called once per frame
    void Update()
    {
        float strong = Updater.orpheusStrength;                         // checks if strength amount has changed -deb
        UpdateStrength(strong);                                         // sends changed strength amount to update memory -deb
    }

    public void UpdateStrength(float strength)
    {
        orpheusStrengthSlider.value = strength;                         // updates the slider value with amount coming in -deb
        fill.color = gradient.Evaluate(orpheusStrengthSlider.value);    // fills the slider with the new strength amount -deb
    }
}
