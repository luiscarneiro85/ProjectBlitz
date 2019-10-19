using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System;
using System.Linq;
using System.Text;




public class knnNew : MonoBehaviour {

    // Use this for initialization
    void Start () {
        double[] unknown = new double[] { 2.75, 2.1 };
        KNN(unknown, 4);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void KNN (double[] unknown, int k)
    {
        //carrega a matriz de treinamento
        double[][] trainData = LoadData();
        int numClasses = 4;
        int predicted = Classify(unknown, trainData, numClasses, k);
        print("Predicted class = " + predicted);
    }
    static int Classify(double[] unknown, double[][] trainData, int numClasses, int k)
    {
        int n = trainData.Length;
        IndexAndDistance[] info = new IndexAndDistance[n];
        //coloca o index e a distancia do vetor desconhecido em relaçãoa todos os pontos de treinamento em um vetor auxiliar
        for (int i = 0; i < n; ++i)
        {
            IndexAndDistance curr = new IndexAndDistance();
            double dist = Distance(unknown, trainData[i]);
            curr.idx = i;
            curr.dist = dist;
            info[i] = curr;
        }
        Array.Sort(info);  // sort by distance crescent order
        print("Nearest / Distance / Class");
        print("==========================");
        for (int i = 0; i < k; ++i)
        {
            int c = (int)trainData[info[i].idx][2];
            string dist = info[i].dist.ToString("F3");
            print("( " + trainData[info[i].idx][0] +
              "," + trainData[info[i].idx][1] + " )  :  " +
              dist + "        " + c);
        }

        int result = Vote(info, trainData, numClasses, k);
        return result;
    } // Classify

    static int Vote(IndexAndDistance[] info, double[][] trainData, int numClasses, int k)
    {
        int[] votes = new int[numClasses];  // One cell per class
        for (int i = 0; i < k; ++i)
        {       // Just first k
            int idx = info[i].idx;            // Which train item
            int c = (int)trainData[idx][2];   // Class in last cell
                                              //c = 11;
            ++votes[c];
        }
        int mostVotes = 0;
        int classWithMostVotes = 0;
        for (int j = 0; j < numClasses; ++j)
        {
            if (votes[j] > mostVotes)
            {
                mostVotes = votes[j];
                classWithMostVotes = j;
            }
        }
        return classWithMostVotes;
    }

    static double Distance(double[] unknown, double[] data)
    {
        double sum = 0.0;
        for (int i = 0; i < unknown.Length; ++i)
            sum += (unknown[i] - data[i]) * (unknown[i] - data[i]);
        return Math.Sqrt(sum);
    }

    static double[][] LoadData()
    {
        double[][] data = new double[14][];
        data[0] = new double[] { 2.0, 4.0, 0 };
        data[1] = new double[] { 3.0, 4.0, 1 };
        data[2] = new double[] { 4.6, 2.5, 2 };
        data[3] = new double[] { 4.3, 1.5, 3 };
        data[4] = new double[] { 4.7, 2.0, 3 };
        data[5] = new double[] { 4.8, 2.5, 2 };
        data[6] = new double[] { 4.5, 2.3, 2 };
        data[7] = new double[] { 2.3, 3.8, 1 };
        data[8] = new double[] { 1.8, 3.7, 1 };
        data[9] = new double[] { 1.5, 4.6, 0 };
        data[10] = new double[] { 4.5, 2.3, 2 };
        data[11] = new double[] { 2.3, 3.8, 1 };
        data[12] = new double[] { 1.8, 3.7, 1 };
        data[13] = new double[] { 1.5, 4.6, 0 };
        return data;
    }
    // Program class
    public class IndexAndDistance : IComparable<IndexAndDistance>
    {
        public int idx;  // Index of a training item
        public double dist;  // To unknown
                             // Need to sort these to find k closest
        public int CompareTo(IndexAndDistance other)
        {
            if (this.dist < other.dist) return -1;
            else if (this.dist > other.dist) return +1;
            else return 0;
        }
    }
}
