using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeKey : MonoBehaviour
{
    [SerializeField] GameEvent onDeleteEvent;
    public bool isTappable = false;
    public int shapeID;
    public float delayTime;
    Animator animator;

    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
       
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Key"))
        {
            Debug.Log("Key tappable");
            isTappable = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Key"))
        {
            isTappable = false;
            Debug.Log("KeyMissed");
            StartCoroutine(DelayDestroy(delayTime));
            

        }
    }

    IEnumerator DelayDestroy(float delay)
    {
      
        yield return new WaitForSeconds(delay);
        onDeleteEvent.Raise(this, false);
        animator.Play("DeleteAnim");
    }
    public void OnTappedButton(Component sender, object data)
    {
        if((int)data == shapeID && isTappable)
        {
            animator.Play("DeleteAnim");
            this.GetComponent<Collider2D>().enabled = false;
            onDeleteEvent.Raise(this, true);
            
        }
    }

    public void DeleteObject()
    {
        Destroy(this.gameObject);   
    }


}
