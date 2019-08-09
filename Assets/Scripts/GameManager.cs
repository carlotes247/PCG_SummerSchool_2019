using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    List<GeneratorManager> generators;
    GameObject cursor;
    public ParticleSystem particles;

    
    // Start is called before the first frame update
    void Start()
    {
        generators = new List<GeneratorManager>();

        // Create one generator
        GameObject firstGeneratorObject = new GameObject("Generator0");
        GeneratorManager generator = firstGeneratorObject.AddComponent<GeneratorManager>();
        generator.effectBoundaries.x = -0.1f;
        generator.effectBoundaries.y = 0.1f;

        // Add to list
        generators.Add(generator);

        // Get mouse cursor
        cursor = GameObject.Find("Cursor");
    }

    // Update is called once per frame
    void Update()
    {
        // Update values of generator based on mouseMover
        generators[0].effectBoundaries.x = -0.1f * cursor.transform.position.x;
        generators[0].effectBoundaries.y = 0.1f * cursor.transform.position.y;

        generators[0].effectIntensityAudio = cursor.transform.position.x * 1000f;

        // Update values particle system
        ParticleSystem.MainModule particlesMain = particles.main;
        particlesMain.startSpeed = cursor.transform.position.x;
    }
}
