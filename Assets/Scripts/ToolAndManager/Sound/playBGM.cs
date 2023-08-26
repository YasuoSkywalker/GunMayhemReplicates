using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playBGM : MonoBehaviour
{
    void Start()
    {
        GetComponent<SoundManager>().playRandom();       
    }
}
