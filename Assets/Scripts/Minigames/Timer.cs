using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimerScript : MonoBehaviour
{
    // Start is called before the first frame update
    float elapsedTime;

    //UnityEvent
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
    }

  

}
