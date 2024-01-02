using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NNetwork : IComparable<NNetwork>
{
    private int[] layers;
    private float[][] neurons;
    private float[][][] weights;
    private float fitness;

    public NNetwork(int[] layers)
    {
        this.layers = new int[layers.Length];
        for (int i = 0; i < layers.Length; i++)
            this.layers[i] = layers[i];

        InitNeurons();
        InitWeights();
    }

    public NNetwork(NNetwork copyNetwork)
    {
        this.layers = new int[copyNetwork.layers.Length];
        for (int i = 0; i < copyNetwork.layers.Length; i++)
            this.layers[i] = copyNetwork.layers[i];

        InitNeurons();
        InitWeights();
        CopyWeights(copyNetwork.weights);
    }

    private void CopyWeights(float[][][] copyWeights)
    {
        for (int i = 0; i < weights.Length; i++)
        {
            for (int j = 0; j < weights[i].Length; j++)
            {
                for (int k = 0; k < weights[i][j].Length; k++)
                {
                    weights[i][j][k] = copyWeights[i][j][k];
                }

            }

        }
    }

    private void InitNeurons()
    {
        List<float[]> neuronsList = new List<float[]>();

        for (int i = 0; i < layers.Length; i++)
            neuronsList.Add(new float[layers[i]]);

        neurons = neuronsList.ToArray();
    }

    private void InitWeights()
    {

        List<float[][]> weightsList = new List<float[][]>();

        for (int i = 1; i < layers.Length; i++)
        {
            List<float[]> layerWeightsList = new List<float[]>();

            int neuronsInPreviousLayer = layers[i - 1];

            for (int j = 0; j < neurons[i].Length; j++)
            {
                float[] neuronWeights = new float[neuronsInPreviousLayer];

                for (int k = 0; k < neuronsInPreviousLayer; k++)
                    neuronWeights[k] = UnityEngine.Random.Range(-1f, 1f);

                layerWeightsList.Add(neuronWeights);
            }

            weightsList.Add(layerWeightsList.ToArray());
        }

        weights = weightsList.ToArray();
    }

    public float[] FeedForward(float[] inputs)
    {
        for (int i = 0; i < inputs.Length; i++)
            neurons[0][i] = inputs[i];

        for (int i = 1; i < layers.Length; i++)
        {
            for (int j = 0; j < neurons[i].Length; j++)
            {
                float value = 0f;

                for (int k = 0; k < neurons[i - 1].Length; k++)
                    value += weights[i - 1][j][k] * neurons[i - 1][k];

                neurons[i][j] = (float)Math.Tanh(value);
            }
        }

        return neurons[neurons.Length - 1];
    }

    public void Mutate(float percentage)
    {
        for (int i = 0; i < weights.Length; i++)
        {
            for (int j = 0; j < weights[i].Length; j++)
            {
                for (int k = 0; k < weights[i][j].Length; k++)
                {
                    float weight = weights[i][j][k];
                    float rdmNum = UnityEngine.Random.Range(0f, 100f);

                    if (rdmNum <= percentage)
                    {
                        float newWeight = UnityEngine.Random.Range(-1f, 1f);
                        weight = newWeight;
                    }

                    weights[i][j][k] = weight;
                }
            }
        }
    }

    public void AddFitness(float fit)
    {
        fitness += fit;
    }

    public void SetFitness(float fit)
    {
        fitness = fit;
    }

    public float GetFitness()
    {
        return fitness;
    }

    public string GetWeightsAsString()
    {
        List<string> weightsList = new List<string>();

        for (int i = 0; i < weights.Length; i++)
        {
            for (int j = 0; j < weights[i].Length; j++)
            {
                string weightRowString = string.Join(",", weights[i][j]);
                weightsList.Add(weightRowString);
            }
        }

        string weightsString = string.Join(";", weightsList);

        return weightsString;
    }

    public void SetWeightsFromString(string weightsString)
    {
        Debug.Log("Weights String: " + weightsString);

        string[] weightRows = weightsString.Split(';');

        for (int i = 0; i < weights.Length; i++)
        {
            string[] weightValues = weightRows[i].Split(',');

            for (int j = 0; j < weights[i].Length; j++)
            {
                for (int k = 0; k < weights[i][j].Length; k++)
                {
                    weights[i][j][k] = float.Parse(weightValues[k]);
                }
            }
        }
    }

    public int CompareTo(NNetwork other)
    {
        if (other == null) return 1;

        if (fitness > other.fitness)
            return 1;
        else if (fitness < other.fitness)
            return -1;
        else
            return 0;
    }
}
