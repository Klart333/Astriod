using System;
using System.Collections.Generic;
using UnityEngine;

public class AudioSyncer : MonoBehaviour
{
    [Header("Stats")]
    public float Bias = 15;
    public float TimeStep = 0.25f;
    public float TimeToBeat = 0.05f;
    public float RestSmoothTime = 4f;

    private float previousAudioValue;
    private float audioValue;
    private float timer;

    protected bool isBeat;

    private void Update()
    {
        OnUpdate();
    }

    protected virtual void OnUpdate()
    {
        previousAudioValue = audioValue;
        audioValue = AudioSpectrum.SpectrumValue;

        if (previousAudioValue > Bias && audioValue <= Bias)
        {
            if (timer > TimeStep)
            {
                OnBeat();
            }
        }

        if (previousAudioValue <= Bias && audioValue > Bias)
        {
            if (timer > TimeStep)
            {
                OnBeat();
            }
        }

        timer += Time.deltaTime;
    }

    protected virtual void OnBeat()
    {
        timer = 0;
        isBeat = true;
    }
}
