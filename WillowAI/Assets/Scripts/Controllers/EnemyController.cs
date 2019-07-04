﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : BaseController {

    [SerializeField] private List<Whisp> whisps = new List<Whisp>();

    protected override void OnInitialize(MainController mainController) {
        foreach (Whisp whisp in whisps) {
            whisp.Initialize();
        }
    }

    protected override void OnSetup() {
        foreach (Whisp whisp in whisps) {
            whisp.OnSetup();
        }
    }

    protected override void OnTick(float deltaTime) {
        foreach (Whisp whisp in whisps) {
            whisp.Tick(deltaTime);
        }
    }
}
