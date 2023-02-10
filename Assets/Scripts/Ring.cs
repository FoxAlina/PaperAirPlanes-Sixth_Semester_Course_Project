using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring : MonoBehaviour
{
    public int bulletIncrease = 20;
    public int healthIncrease = 4;

    public GameObject particleSystem;

    public AudioSource sound;

    void Start()
    {
        particleSystem.SetActive(false);
    }

    void Update()
    {
        
    }

    public void success()
    {
        particleSystem.SetActive(true);
        particleSystem.GetComponent<ParticleSystem>().Play();
        sound.Play();
        StartCoroutine(waitAndDie());
    }

    IEnumerator waitAndDie()
    {
        yield return new WaitForSeconds(0.6f);
        this.gameObject.SetActive(false);
    }
}
