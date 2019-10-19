using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour {

    public float rechargeTime;
    public bool active;
    public int cost;
    public string effect;
    public float effectTime;
    public float effectPower;
    public bool purchased;
    public string info;
    public bool ready = true;
    public Sprite icon;
    public float hp;
    public GameObject fire;
    public int id;
    public GameObject visualEffect;
    public AudioClip soundEffect;

	// Use this for initialization
	void Start () {
        hp = effectPower;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

}
