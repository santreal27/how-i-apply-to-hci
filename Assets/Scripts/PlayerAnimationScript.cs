using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationScript : MonoBehaviour
{
    Animator playerAnim;

    Vector2 movement;
    // Start is called before the first frame update
    void Start()
    {
        playerAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        playerAnim.SetFloat("Horizontal", movement.x);
        playerAnim.SetFloat("Vertical", movement.y);
        playerAnim.SetFloat("Speed", movement.sqrMagnitude);
    }
}
