using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 6f;
    private float _speedMultiplier = 2.5f;
    [SerializeField]
    private GameObject _laserPrefab = null;

    [SerializeField]
    private GameObject _tripleShotPrefab = null;

    [SerializeField]
    private float _fireRate = 0.15f;
    private float _nextFire = 0.0f;
    [SerializeField]
    private int _lives = 3;
    [SerializeField]
    private int _score = 0;

    private SpawnManager _spawnManager;

    private UIManager _uiManager;

    private bool _tripleShotActive = false;
    private bool _speedBoostActive = false;
    private bool _shieldActive = false;

    [SerializeField]
    private GameObject shieldVisualizer;
    [SerializeField]
    private GameObject rightEngine;
    [SerializeField]
    private GameObject leftEngine;

    [SerializeField]
    private AudioClip laserClip;
    [SerializeField]
    private AudioSource audioSource;

    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("spawn_Manager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        audioSource = GetComponent<AudioSource>();

        if (_spawnManager == null) 
        {
            Debug.LogError("SpawnManager is NULL");    
        }

        audioSource.clip = laserClip;
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _nextFire)
        {
            FireLaser();
        }
    }

    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

       
        
        transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * _speed * Time.deltaTime);

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, (float)-5.5, (float)5.5), 0);

        if (transform.position.x > 13.5)
        {
            transform.position = new Vector3((float)-13.5, transform.position.y, 0);
        }
        else if (transform.position.x < (float)-13.5)
        {
            transform.position = new Vector3((float)13.5, transform.position.y, 0);
        }
    }


    void FireLaser()
    {
        audioSource.Play();
        _nextFire = Time.time + _fireRate;
        float offset = 0.8f;
        Vector3 offsetPosition;

        if (_tripleShotActive)
        {
            
            Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            offsetPosition =  new Vector3(transform.position.x, transform.position.y + offset, transform.position.z);
            Instantiate(_laserPrefab, offsetPosition, Quaternion.identity);
        }

        
    }

    public void Damage()
    {

        if (_shieldActive)
        {
            _shieldActive = false;
            shieldVisualizer.SetActive(false);
            return;
        }

        _lives -= 1;

        if(_lives == 2)
        {
            leftEngine.SetActive(true);
        }
        else if(_lives == 1)
        {
            rightEngine.SetActive(true);
        }

        
        _uiManager.UpdateLives(_lives);

        if (_lives <= 0)
        {
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }

    public void AddScore()
    {
        _score += 10;
       
        _uiManager.UpdateScore(_score);
    }


    public void TripleShotActivate()
    {
        _tripleShotActive = true;
        StartCoroutine(TripleRoutine());
    }

    public void SpeedBoostActivate()
    {
        _speedBoostActive = true;
        _speed = _speed * _speedMultiplier;
        StartCoroutine(SpeedRoutine());
  
    }

    public void ShieldActivate()
    {
        _shieldActive = true;
        shieldVisualizer.gameObject.SetActive(true);
      
    }


    IEnumerator ShieldRoutine()
    {
        while (_shieldActive)
        {
            yield return new WaitForSeconds(5f);
            _shieldActive = false;
        }
    }

    IEnumerator SpeedRoutine()
    {
        while (_speedBoostActive)
        {
            yield return new WaitForSeconds(5f);
            _speedBoostActive = false;
        }
        _speed = _speed / _speedMultiplier;


    }

    IEnumerator TripleRoutine()
    {
        while (_tripleShotActive)
        {
            yield return new WaitForSeconds(5.0f);
            _tripleShotActive = false;
        }
        
    }
}
