using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatScrolling : MonoBehaviour
{
    [SerializeField]private float beatTempo;

    [SerializeField] private bool hasStarted;

    [SerializeField] GameEvent onStartEvent;

    [SerializeField] GameEvent onKeysCleared;
    // Start is called before the first frame update
    void Start()
    {
        beatTempo = beatTempo / 60f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasStarted)
        {
            if (Input.anyKeyDown)
            {
                hasStarted = true;
                onStartEvent.Raise(this, hasStarted);
            }
        }
        else
        {
            transform.Translate(Vector2.down * beatTempo * Time.deltaTime);
        }
       
    }

    public void CheckIfCleared()
    {
        Debug.Log("Child Count" +transform.childCount);
        if (transform.childCount <= 1)
        {
            onKeysCleared.Raise(this, 0);
            Debug.Log("Pattern Cleared");
        }
    }
}
