﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Lobe {

    public readonly BrainLobeID LobeID;
    public readonly Type NeuronEnumType;
    public readonly Vector2Int Location;
    public readonly Vector2Int Dimension;
    public int NumNeurons
    {
        get { return Neurons.Count; }
    }
    private List<Neuron> Neurons;

    public Lobe(BrainLobeID lobeID, Type neuronEnumType, Vector2Int location, Vector2Int dimension, List<Neuron> neurons)
    {
        LobeID = lobeID;
        NeuronEnumType = neuronEnumType;
        Location = location;
        Dimension = dimension;
        Neurons = neurons;
    }

    public void Process()
    {
        foreach(var Neuron in Neurons)
        {
            Neuron.Process();
        }
    }

    public void FireNeuron(int neuronIndex, int amount = 255)
    {
        Neurons[neuronIndex].SetStrength(amount);
    }

    public Neuron GetFiringNeuron()
    {
        var N = Neurons.OrderByDescending(n => n.Value).First();
        if (N.Value > 0)
        {
            return N;
        }
        else
        {
            return null;
        }
    }

    public float GetValueOfNeuron(int index)
    {
        return Neurons[index].Value;
    }
    
    public void SetUpDendrites(Brain brain)
    {
        if (Neurons.Any(n => n.Dendrites0 == null && n.Dendrites1 == null))
        {
            return;
        }
        foreach(var Neuron in Neurons)
        {
            if (Neuron.Dendrites0 != null)
            {
                foreach (var Dendrite0 in Neuron.Dendrites0)
                {
                    Dendrite0.SetSourceLobe(brain.LobeFromIndex(Dendrite0.SourceLobeIndex));
                }
            }
            if (Neuron.Dendrites1 != null)
            {
                foreach (var Dendrite1 in Neuron.Dendrites1)
                {
                    Dendrite1.SetSourceLobe(brain.LobeFromIndex(Dendrite1.SourceLobeIndex));
                }
            }
            
        }
        
    }

    public void Render()
    {
        var Neuron = GetFiringNeuron();
        var NeuronIndex = (Neuron == null) ? -1 : Neuron.Index;
        LobeRenderer.Render(Location, Dimension, NeuronEnumType, NeuronIndex);
    }

    public void RenderDendrites()
    {
        foreach (var Neuron in Neurons)
        {
            if (Neuron.Dendrites0 == null)
                continue;
            foreach(var Dendrite0 in Neuron.Dendrites0)
            {
                DendriteRenderer.Render(Dendrite0.SourceLobe.Location, Dendrite0.SourceLobe.Dimension, Dendrite0.SourceNeuronIndex, Location, Dimension, Neuron.Index); 
            }
        }
    }
}
