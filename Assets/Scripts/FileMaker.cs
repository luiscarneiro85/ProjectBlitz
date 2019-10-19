using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class FileMaker : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void WriteFile(float[] knn)
    {
        string path = Application.dataPath;
        using (StreamWriter sw = new StreamWriter(path + @"\knn.txt",true))
        {
            foreach (float atrb in knn)
            {
                sw.Write(atrb + ",");
                print(atrb.ToString());
            }
            sw.WriteLine("");
        }
    }
}
