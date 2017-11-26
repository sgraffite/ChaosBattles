using UnityEngine;

public class EnemySpawner : Entity {

    public GameObject enemyPrefab;
    public float width = 10f;
    public float height = 5f;
    //public float speed = 5f;
    public float spawnDelay = 0.5f;

    private float xMax;
    private float xMin;
    private int moveDirection = 1;

    void Start () {
        var distanceToCamera = transform.position.z - Camera.main.transform.position.z;
        var leftEdge = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distanceToCamera));
        var rightEdge = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distanceToCamera));
        xMin = leftEdge.x;
        xMax = rightEdge.x;

        //InvokeRepeating("SpawnEnemyAtEnemyPositions", 1.0f, 0.2f);
    }

    private void Update()
    {
        transform.position += new Vector3(moveDirection * speed * Time.deltaTime, 0);
        this.Move(moveDirection);


        // Check boundaries of the formation
        float rightEdgeOfFormation = transform.position.x + (0.5f * width);
        float leftEdgeOfFormation = transform.position.x - (0.5f * width);
        
        if(rightEdgeOfFormation >= xMax)
        {
            moveDirection = -1;
        }else if(leftEdgeOfFormation <= xMin)
        {
            moveDirection = 1;
        }

        if (AllMembersDead())
        {
            SpawnEnemyAtEnemyPositions();
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height));
    }

    private bool AllMembersDead()
    {
        foreach (Transform enemyPosition in transform)
        {
            if (enemyPosition.childCount > 0)
            {
                return false;
            }
        }

        return true;
    }

    Transform NextFreePosition()
    {
        foreach (Transform enemyPosition in transform)
        {
            if (enemyPosition.childCount <= 0)
            {
                return enemyPosition;
            }
        }

        return null;
    }

    public void SpawnEnemyAtEnemyPositions()
    {
        var enemyPosition = NextFreePosition();
        if(enemyPosition == null)
        {
            return;
        }

        var enemy = Instantiate(enemyPrefab, enemyPosition.position, Quaternion.identity) as GameObject;
        enemy.transform.parent = enemyPosition;

        Invoke("SpawnEnemyAtEnemyPositions", spawnDelay);
    }
}
