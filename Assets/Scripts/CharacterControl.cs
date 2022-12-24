using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    public Sprite[] IdleAnim;
    public Sprite[] JumpAnim;
    public Sprite[] WalkAnim;

    SpriteRenderer SpriteRendere;

    Rigidbody2D physics;

    
    float horizontal = 0;
    void Start()
    {
        SpriteRendere= GetComponent<SpriteRenderer>();
        physics= GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CharacterMovement();
        Animation();
    }

    void CharacterMovement()
    {
        horizontal=Input.GetAxisRaw("Horizontal");
        vec = new Vector3(horizontal*10,physics.velocity.y,0);
        physics.velocity=vec;
    }

    void Animation()
    {

    }
}
