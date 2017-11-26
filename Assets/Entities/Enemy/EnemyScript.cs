using UnityEngine;

public class EnemyScript : Entity {
    //public float Hp = 15;
    public GameObject projectilePrefab;
    public float projectileSpeed;
    public float shotsPerSecond = 0.9f;

    public AudioClip laserSfx;
    public AudioClip deathSfx;

    private ScoreScript scoreInstance;

    void Start () {
        scoreInstance = FindObjectOfType<ScoreScript>();
    }

    // Update is called once per frame
    void Update() {
        var probabilityOfFire = shotsPerSecond * Time.deltaTime;
        if (probabilityOfFire > Random.value)
        {
            Fire();
        }

        Move(-1);
    }

    protected new void Move(float speedDirection)
    {
        var enemyPosition = this.transform.parent;

        var xNoise = Mathf.PerlinNoise(xOffset, 0f);
        var yNoise = Mathf.PerlinNoise(yOffset, 0f);
        var vx = Mathf.Lerp(speed, -speed, xNoise) * Time.deltaTime * speedDirection;
        var vy = Mathf.Lerp(speed, -speed, yNoise) * Time.deltaTime * speedDirection;
        var newVelocity = new Vector3(vx, vy, 0f);
        var body = this.GetComponent<GameObject>();

        enemyPosition.transform.position += newVelocity;

        xOffset += 0.01f;
        yOffset += 0.01f;

        Debug.Log("xNoise " + xNoise);
        Debug.Log("yNoise " + yNoise);
    }

    private void HitByProjectile(ProjectileScript projectile)
    {
        var velocity = projectile.GetComponent<Rigidbody2D>().velocity;
        if(velocity.y < 0)
        {
            return;
        }

        Debug.Log("trigger, enemy hit!");
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
        scoreInstance.Add(1);
        Destroy(gameObject);
    }

    private void Fire()
    {
        var projectile = Instantiate(this.projectilePrefab, transform.position, Quaternion.identity) as GameObject;
        projectile.GetComponent<Rigidbody2D>().velocity = new Vector3(0, -1*projectileSpeed, 0);
        AudioSource.PlayClipAtPoint(laserSfx, projectile.transform.position);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("not trigger!");
    }

    void OnTriggerEnter2D(Collider2D collider) {
        var projectile = collider.gameObject.GetComponent<ProjectileScript>();
        if (projectile)
        {
            this.HitByProjectile(projectile);
        }
    }
}
