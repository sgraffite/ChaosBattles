using System.Collections.Generic;
using UnityEngine;

public class Dna : MonoBehaviour {
    private List<double> genes = new List<double>();
    //Guid.NewGuid().GetHashCode()

    private void Generate(int seed)
    {
        var rnd = new System.Random(seed);
        genes = new List<double>();
        genes.Add(0);
        for (int i = 0; i < genes.Count; i++)
        {
            genes[i] = rnd.NextDouble();
        }
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
