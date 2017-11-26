using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed = 10.0f;
    public float padding = 1f;
    public float projectileSpeed;
    public float firingRate = 0.2f;
    public GameObject projectilePrefab;

    public AudioClip laserSfx;
    public AudioClip deathSfx;

    private float xMin;
    private float xMax;
    private float Hp = 25f; 

    private LevelManager levelManagerInstance;

    void Start () {
        levelManagerInstance = FindObjectOfType<LevelManager>();
        if (levelManagerInstance == null)
        {
            throw new System.Exception("Level manager not found!!");
        }

        var distance = transform.position.z - Camera.main.transform.position.z;
        var leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        var rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));

        xMin = leftmost.x + padding;
        xMax = rightmost.x - padding;
    }
	
	// Update is called once per frame
	void Update () {
        HandleInput();

    }

    private void Fire()
    {
        var projectile = Instantiate(this.projectilePrefab, transform.position, Quaternion.identity) as GameObject;
        projectile.GetComponent<Rigidbody2D>().velocity = new Vector3(0, projectileSpeed, 0);
        AudioSource.PlayClipAtPoint(laserSfx, projectile.transform.position);
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            InvokeRepeating("Fire", 0.0000001f, firingRate);
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            CancelInvoke("Fire");
        }

        if (Input.GetKey(KeyCode.A))
        {
            //gameObject.transform.position += new Vector3(-speed * Time.deltaTime, 0f, 0f);

            gameObject.transform.position += Vector3.left * speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            //gameObject.transform.position += new Vector3(speed * Time.deltaTime, 0f, 0f);

            gameObject.transform.position += Vector3.right * speed * Time.deltaTime;
        }

        var newX = Mathf.Clamp(transform.position.x, xMin, xMax);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        var projectile = collider.gameObject.GetComponent<ProjectileScript>();
        if (projectile)
        {
            this.HitByProjectile(projectile);
        }
    }

    private void HitByProjectile(ProjectileScript projectile)
    {
        var velocity = projectile.GetComponent<Rigidbody2D>().velocity;
        if (velocity.y > 0)
        {
            return;
        }

        var damage = projectile.GetDamage();
        Hp = Hp - damage;
        Debug.Log(Hp);
        if (Hp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        AudioSource.PlayClipAtPoint(deathSfx, this.transform.position);
        Destroy(this.gameObject);
        levelManagerInstance.LoadLevel("Lose Screen");

    }

}
