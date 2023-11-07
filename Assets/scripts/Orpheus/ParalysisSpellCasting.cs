using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParalysisSpellCasting : MonoBehaviour
{
    public ParalysisSpellCounter spellCount;
    public Animator spellAnima;
    public GameObject spellCasting;
    public float freezeTime = 16;                    // the time enemy is frozen; can be adjusted in Paralysis_Spell game object

    public GameObject enemyFreeze01;
    public GameObject enemyFreeze02;
    public GameObject enemyFreeze03;
    public GameObject enemyFreeze04;
    public GameObject enemyFreeze05;
    public GameObject enemyFreeze06;
    public GameObject enemyFreeze07;
    public GameObject enemyFreeze08;
    public GameObject enemyFreeze09;
    public GameObject enemyFreeze10;
    public GameObject enemyFreeze11;
    public GameObject enemyFreeze12;

    public void SetSpellAnimator()
    {
        spellCount.SpellCountUpdater();                                   // Goes to update the spell count -deb
        spellAnima = GetComponent<Animator>();                           // Accesses the spell animator -deb
        spellCasting.SetActive(true);                                    // Activates the spell animation -deb
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemies"))                 // If the spell collides with enemies -deb
        {                                                                   // freeze them -deb
            enemyFreeze01.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            enemyFreeze02.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            enemyFreeze03.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            enemyFreeze04.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            enemyFreeze05.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            enemyFreeze06.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            enemyFreeze07.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            enemyFreeze08.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            enemyFreeze09.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            enemyFreeze10.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            enemyFreeze11.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            enemyFreeze12.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            Invoke("Unfreeze", freezeTime);                             // wait for specified freeze time then go to unfreeze -deb
        }                                                   
    }

    private void Unfreeze()                                             // unfreeze enemies
    {
        enemyFreeze01.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        enemyFreeze02.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        enemyFreeze03.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        enemyFreeze04.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        enemyFreeze05.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        enemyFreeze06.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        enemyFreeze07.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        enemyFreeze08.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        enemyFreeze09.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        enemyFreeze10.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        enemyFreeze11.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        enemyFreeze12.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
    }
}
