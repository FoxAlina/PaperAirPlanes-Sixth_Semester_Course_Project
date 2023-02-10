using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cleanPlayerPrefs : MonoBehaviour
{
    [SerializeField]
    string key;

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.DeleteKey(key);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
