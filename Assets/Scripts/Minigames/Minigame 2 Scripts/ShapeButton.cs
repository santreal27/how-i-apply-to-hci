using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeButton : MonoBehaviour
{
    public Animator animator;
    public KeyCode assignKey;
    public int shapeID;

    public GameEvent onTapped;

    GameObject collidingObject = null;
    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(assignKey))
        {
            animator.SetTrigger("Tap");
            onTapped.Raise(this, shapeID);
            
        }
    }
    
    
}
