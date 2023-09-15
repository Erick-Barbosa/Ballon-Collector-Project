using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private float gravity;
    [SerializeField] private float boostPower;
    [SerializeField] private float speed;
    [SerializeField] private float xLimit;
    [SerializeField] private float yLimit;

    private bool isBoosting;

    private Rigidbody2D rigidBody;
    private SpriteRenderer spriteRenderer;
    private AudioSource jumpSound;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        jumpSound = GetComponent<AudioSource>();

        rigidBody.gravityScale = gravity;
    }

    // Update is called once per frame
    void Update()
    {
        float xPosition = Input.GetAxis("Horizontal") * speed * Time.deltaTime;

        transform.Translate(xPosition, 0f, 0f);

        if (Input.GetAxis("Vertical") > 0) {
            jumpSound.Play();
            isBoosting = true;
        }

        if (Input.GetAxis("Horizontal") < 0) {
            spriteRenderer.flipX = false;
        } else if (Input.GetAxis("Horizontal") > 0) {
            spriteRenderer.flipX = true;
        }        
    }

    private void FixedUpdate() {
        if (isBoosting) {
            rigidBody.velocity = new Vector2(0f, boostPower);
            isBoosting = false;
        }

        if (transform.position.x <= -xLimit || transform.position.x >= xLimit) {
            transform.position = new Vector2(transform.position.x * -1, transform.position.y);
        } 
        if (transform.position.y <= -yLimit || transform.position.y >= yLimit) {
            transform.position = new Vector2(transform.position.x, transform.position.y * -1);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Coin") {
            collision.GetComponent<AudioSource>().Play();
            collision.gameObject.GetComponent<Renderer>().enabled = false;
            Destroy(collision.gameObject, 0.5f);
        }
    }
}
