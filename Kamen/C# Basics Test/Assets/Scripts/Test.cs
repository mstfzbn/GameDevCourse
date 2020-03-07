﻿using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using System;
using static UnityEngine.Mathf;

[RequireComponent(typeof(Camera))]
public class Test : MonoBehaviour {

    [SerializeField]
    private float speed = 5;

    [SerializeField]
    private TestSerializable testSerializable;

    [Serializable]
    public class PlayerStats {
        public int movementSpeed;
        public int hitPoints;
        public bool hasHealthPotion;
    }

    [SerializeField]
    private PlayerStats playerStats;

    private PlayerStats stats = new PlayerStats {
        movementSpeed = 5,
        hitPoints = 100,
        hasHealthPotion = true
    };

    public PlayerStats Stats {
        get {
            stats.hasHealthPotion = false;
            return stats;
        }
        set {
            stats = value;
        }
    }

    void Start() {
        var updateMethod = typeof(Test).GetMethod("Update", 
                                                  BindingFlags.NonPublic | BindingFlags.Instance);
  
        if (updateMethod != null) {
            updateMethod.Invoke(this, null);
        }

        PlayerStats temp = Stats;
        Stats.hasHealthPotion = true;

        stats = new PlayerStats { };
        stats.hasHealthPotion = true;

        print(typeof(Test).GetMethod("Start", BindingFlags.NonPublic | BindingFlags.Instance));
        print(typeof(Test).GetCustomAttribute<RequireComponent>());
        foreach (var method in typeof(Test).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)) {
            if (method.DeclaringType == typeof(Test)) {
                print(method.Name);
            }
        }

        var speedField = typeof(Test)
            .GetField("speed", 
             BindingFlags.NonPublic | BindingFlags.Instance);
        if (speedField.GetCustomAttribute<SerializeField>() != null) {
            speedField.SetValue(this, 6);
            print($"The new values for speed is {speed}");
        }
        
        int a = 1, b = 2;
        print("a = " + a + "; b = " + b);
        print($"a = {a}; b = {b}");

    }

    public void TestMethod() {

    }

    void Update() {
        //print("In Update");
    }
}