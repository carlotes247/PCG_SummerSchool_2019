using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorManager : MonoBehaviour
{
    // The geometry generator
    IcoSphereGenerator geometryGenerator;
    // Sound Generator
    SineWaveExample soundGenerator;

    public Vector2 effectBoundaries;
    public float effectIntensityAudio;

    private float lastNoiseValue;

    IEnumerator sineWaveCoroutine;
    
    // Start is called before the first frame update
    void Start()
    {
        // Add icosphere 
        geometryGenerator = this.gameObject.AddComponent<IcoSphereGenerator>();
        // Add sinewave
        soundGenerator = this.gameObject.AddComponent<SineWaveExample>();

        // Start sinewave coroutine
        sineWaveCoroutine = coroutineSineWave();
        StartCoroutine(sineWaveCoroutine );
    }

    // Update is called once per frame
    void Update()
    {

        // Configure values icosphere
        geometryGenerator.noiseBoundaries.x = effectBoundaries.x;
        geometryGenerator.noiseBoundaries.y = effectBoundaries.y;
        geometryGenerator.noiseY = true;
        geometryGenerator.noiseX = true;

    }

    IEnumerator coroutineSineWave()
    {
        float secondsWaited = 0;
        bool canLoop = true;
        soundGenerator.canPlay = true;
        while (true)
        {
            // Configure values sinewave
            secondsWaited = 0;
            canLoop = true;
            soundGenerator.canPlay = true;

            while (canLoop)
            {
                // If this is the last time we can loop
                if (secondsWaited > 0.9f)
                {
                    canLoop = false;
                }
                // Lerp audio
                float lastValue = Mathf.Abs( lastNoiseValue * effectIntensityAudio);
                float currentValue = Mathf.Abs( geometryGenerator.noiseToVerts * effectIntensityAudio);

                soundGenerator.frequency1 = Mathf.Lerp(
                    lastValue,
                    currentValue,
                    secondsWaited);

                soundGenerator.frequency2 = Mathf.Lerp(
                    lastValue,
                    currentValue,
                    secondsWaited);

                // Add amount to secondsWaited
                secondsWaited += 0.1f;

                yield return new WaitForSeconds(0.1f);
            }

            lastNoiseValue = geometryGenerator.noiseToVerts;

        }


        yield break;
    }

    private void LoopSineWave(bool canLoop, float secondsWaited)
    {
    }
}
