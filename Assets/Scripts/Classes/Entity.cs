using UnityEngine;

public class Entity : MonoBehaviour {
    protected int HpMax;
    protected float Hp;
    protected int Str;
    protected int Dex;
    protected int Int;
    protected int Wis;
    protected int Luc;

    protected float speed = 5f;
    protected float maxspeed = 5f;
    protected float xOffset = 0f;
    protected float yOffset = 0f;

    // Use this for initialization
    void Start()
    {
        //this.maxspeed = p.map(this.dna.genes["Speed-Size"], 0, 1, 15, 0);
        //this.r = p.map(this.dna.genes["Speed-Size"], 0, 1, 0, 50);
    }

    // Update is called once per frame
    void Update()
    {

    }

    protected void Move(float speedDirection)
    {
        var xNoise = Mathf.PerlinNoise(xOffset, 0f);
        var yNoise = Mathf.PerlinNoise(yOffset, 0f);
        var vx = Mathf.Lerp(speed, -speed, xNoise) * Time.deltaTime * speedDirection;
        var vy = Mathf.Lerp(speed, -speed, yNoise) * Time.deltaTime * speedDirection;
        var newVelocity = new Vector3(vx, vy, 0f);
        transform.position += newVelocity;

        xOffset += 0.01f;
        yOffset += 0.01f;

        Debug.Log("xNoise " + xNoise);
        Debug.Log("yNoise " + yNoise);
    }
}
