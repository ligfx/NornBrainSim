﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Brain : MonoBehaviour {
    private List<Lobe> Lobes = new List<Lobe>();
    public int NumLobes
    {
        get
        {
            return Lobes.Count;
        }
    }

    public void FixedUpdate()
    {
        Lobes.ForEach(l => l.Process());
    }

    public virtual Lobe LobeFromIndex(BrainLobeType index)
    {
        return Lobes[(int)index];
    }

    public void SetUpLobes()
    {
        foreach (var Lobe in Lobes)
        {
            Lobe.SetUpDendrites(this);
        }
    }

    public void AddLobe(Lobe lobe)
    {
        Lobes.Add(lobe);
    }

    public void AddStimulus(StimulusGenus stimulus)
    {
        Lobes[(int)BrainLobeType.StimulusSource].FireNeuron((int)stimulus);
    }

    public void AddNoun(StimulusGenus noun)
    {
        Lobes[(int)BrainLobeType.Noun].FireNeuron((int)noun);
    }

    public void AddVerb(VerbGenus verb)
    {
        Lobes[(int)BrainLobeType.Verb].FireNeuron((int)verb);
    }

    public void AddDrive(DriveGenus drive, int amount)
    {
        Lobes[(int)BrainLobeType.Drive].FireNeuron((int)drive, amount);
    }

    public StimulusGenus GetHighestStimulus()
    {
        return (StimulusGenus)Lobes[(int)BrainLobeType.StimulusSource].GetFiringNeuron().Index;
    }

    public void OnGUI()
    {
        foreach (var Record in Lobes)
        {
            Record.Render();
        }
    }

    public void OnRenderObject()
    {
        foreach(var Record in Lobes)
        {
            Record.RenderDendrites();
        }
    }
}
