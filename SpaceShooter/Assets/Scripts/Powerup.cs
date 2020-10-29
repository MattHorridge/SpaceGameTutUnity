using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{

    [SerializeField]
    private float speed = 3f;

    [SerializeField]
    private int powerupID;


    private AudioSource audioSource;

    [SerializeField]
    private AudioClip clip;
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {

            AudioSource.PlayClipAtPoint(clip, transform.position);

            Player player = other.transform.GetComponent<Player>(); 
            if(player != null)
            {
                switch (powerupID)
                {

                    case 0:
                        player.TripleShotActivate();
                        break;
                    case 1:
                        player.SpeedBoostActivate();
                        break;
                    case 2:
                        player.ShieldActivate();
                        break;
                }


                Destroy(gameObject);

            }

        }
    }

    private void CalculateMovement()
    {
        transform.Translate(new Vector3(0, -1, 0) * speed * Time.deltaTime);

        if (IsOffEdge())
        {
            Destroy(gameObject);
        }

    }

    private bool IsOffEdge()
    {
        float bottomEdge = -6f;
        if (transform.position.y <= bottomEdge)
        {
            return true;
        }
        return false;
    }
}
