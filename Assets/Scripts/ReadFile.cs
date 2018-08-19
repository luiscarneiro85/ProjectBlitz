using Assets.Scripts;
using System.Collections.Generic;
using UnityEngine;

public class ReadFile : MonoBehaviour
{

    public void Start()
    {
        new ReadFile();
    }
    public ReadFile()
    {
        kNN examplekNN = new kNN().initialiseKNN(1, "teste.txt", true);

        List<double> instance2Classify = new List<double> { 54, 16, 36, 31, 63, 51, 89 };

        string result = examplekNN.Classify(instance2Classify);
        //Console.WriteLine("This instance is classified as: {0}", result);
        //Console.ReadLine();S
    }

}
