using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject AIPrefab;

    public int populationSize   = 100;
    public int generationNumber = 0;
    public float maxFitness = 0f;
    public string filename;

    public bool save = false;
    public bool load = false;

    public List<GameObject> AIList = null;
    public float timer = 15f;

    private List<NNetwork>   networks;
    private List<InputEntry> entries = new List<InputEntry>();
    private int[] layers       = new int[] { 6, 5, 4, 2 };
    private bool isTraning     = false;

    void Timer()
    {
        isTraning = false;
    }

    void Update()
    {
        if (isTraning == false)
        {
            if (generationNumber == 0)
            {
                InitAINNetworks();
                GenerateAIPrefab();
            }
            else
            {

                for (int i = 0; i < populationSize; i++)
                {
                    AIController script = AIList[i].GetComponent<AIController>();
                    float fitness = script.fitness;
                    networks[i].SetFitness(fitness);
                }

                networks.Sort();
                networks.Reverse();

                maxFitness = 0;
                for (int i = 0; i < populationSize; i++)
                    maxFitness += networks[i].GetFitness();
                maxFitness /= populationSize;
                Debug.Log(maxFitness);

                List<NNetwork> newNetworks = new List<NNetwork>();

                for (int i = 0; i < populationSize; i++)
                {
                    NNetwork net = new NNetwork(networks[i % (populationSize / 4)]);

                    if (i < populationSize / 4)
                        net.Mutate(0.2f);
                    else if (i < populationSize / 2)
                        net.Mutate(5f);
                    else if (i < 3 * populationSize / 4)
                        net.Mutate(9f);

                    newNetworks.Add(net);
                }

                networks = newNetworks;
            }

            generationNumber++;
            isTraning = true;
            Invoke("Timer", timer);
            GenerateAIPrefab();
        }

        for (int i = 0; i < populationSize; i++)
        {
            AIController aiController = AIList[i].GetComponent<AIController>();

            float vel = aiController.currentVelocity / aiController.maxDistance;
            float distForward = aiController.distForward / aiController.maxDistance;
            float distLeft = aiController.distLeft / aiController.maxDistance;
            float distRight = aiController.distRight / aiController.maxDistance;
            float distDiagLeft = aiController.distDiagLeft / aiController.maxDistance;
            float distDiagRight = aiController.distDiagRight / aiController.maxDistance;

            float[] tInputs = new float[] { vel, distForward, distLeft, distRight, distDiagLeft, distDiagRight };
            float[] result  = networks[i].FeedForward(tInputs);

            aiController.results = result;
        }

        if (save)
        {
            for (int i = 0; i < populationSize; i++)
            {
                entries.Add(new InputEntry(networks[i].GetWeightsAsString()));
                FileHandler.SaveToJSON<InputEntry>(entries, filename);
            }
            save = false;
        }

        if (load)
        {
            for (int i = 0; i < populationSize; i++)
            {
                entries = FileHandler.ReadListFromJSON<InputEntry>(filename);
                networks[i].SetWeightsFromString(entries[i].weights);
            }
            load = false;

        }
    }

    private void GenerateAIPrefab()
    {
        for (int i = 0; i < AIList.Count; i++)
            Destroy(AIList[i]);

        AIList = new List<GameObject>();
        for (int i = 0; i < populationSize; i++)
        {
            Quaternion carRotation = Quaternion.Euler(AIPrefab.transform.rotation.x, 90, AIPrefab.transform.rotation.z);
            GameObject car = Instantiate(AIPrefab, new Vector3(-55f, 1, -5), carRotation);
            AIList.Add(car);
            AIList[i] = car;
        }

    }

    void InitAINNetworks()
    {
        networks = new List<NNetwork>();

        for (int i = 0; i < populationSize; i++)
        {
            NNetwork net = new NNetwork(layers);
            net.Mutate(0.5f);
            networks.Add(net);
        }
    }
}
