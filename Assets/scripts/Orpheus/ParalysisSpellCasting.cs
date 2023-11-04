using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParalysisSpellCasting : MonoBehaviour
{
    public float timer;
    public Transform enemies;
    private Rigidbody rb;

    //public CharacterController enemyController;
    

    // Start is called before the first frame update
    void Start()
    {
        enemies = GameObject.FindGameObjectWithTag("Enemies").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator CastSpell()
    {
        if (gameObject.CompareTag("Enemies"))
        {
            enemies.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll; 
            enemies.GetComponent<CharacterController>().enabled = false;
        }

        yield return new WaitForSeconds(8);

        if (gameObject.CompareTag("Enemies"))
        {
            enemies.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            enemies.GetComponent<CharacterController>().enabled = true;
        }
    }
}
