using System;
using UnityEngine;
using UnityEngine.Audio;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float speed  = 4f;

    private Animator anim;
    private Player player;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(UnityEngine.Random.Range(-11f, 11f), 8f, 0);
        player = GameObject.Find("Player").GetComponent<Player>();
        audioSource = GetComponent<AudioSource>();
        if (player == null)
        {

        }

        anim = GetComponent<Animator>();

        if(anim == null)
        {
            Debug.Log("Null Animator");
        }

    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
    }


    private void OnTriggerEnter2D(Collider2D other)
    { 
        if (other.tag == "Laser")
        {
            anim.SetTrigger("OnEnemyDeath");
            speed = 0;
            Destroy(other.gameObject);
            player.AddScore();
            audioSource.Play();
            Destroy(gameObject, 2.3f);
          
        }
        else if(other.tag == "Player")
        {
            player = other.transform.GetComponent<Player>();

            if (player != null)
            {
               player.Damage();
            }
            anim.SetTrigger("OnEnemyDeath");
            speed = 0;
            audioSource.Play();
            Destroy(gameObject, 2.3f);
            
        }
         
    }



    void CalculateMovement()
    {
        transform.Translate(new Vector3(0, -1, 0) * speed * Time.deltaTime);
        if (IsOffEdge())
        {
            transform.position = new Vector3(UnityEngine.Random.Range(-11f, 11f), 8f, 0);
        }
    }

    bool IsOffEdge()
    {
        float bottomEdge = -6f;
        if(transform.position.y <= bottomEdge)
        {
            return true;
        }
        return false;
    }


}
