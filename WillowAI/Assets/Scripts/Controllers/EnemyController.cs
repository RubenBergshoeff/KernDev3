﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : BaseController {

    [SerializeField] private List<Whisp> whisps = new List<Whisp>();

    protected override void OnInitialize(MainController mainController) {
        FragmentController fragmentController = mainController.GetControllerOfType(typeof(FragmentController)) as FragmentController;
        foreach (Whisp whisp in whisps) {
            whisp.Initialize(fragmentController);
        }
    }

    protected override void OnSetup() {
        foreach (Whisp whisp in whisps) {
            whisp.Setup();
        }
    }

    protected override void OnTick(float deltaTime) {
        foreach (Whisp whisp in whisps) {
            whisp.Tick(deltaTime);
        }
    }
}
