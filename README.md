# Robotic Sorter – Unity ML-Agents Project

This project trains a simple robotic agent to move toward a target using Unity's ML-Agents toolkit.

## Overview

- **Environment**: Unity 3D simulation with Rigidbody-based agent physics
- **Agent**: `SorterAgent.cs` — collects relative position + velocity, outputs continuous movement
- **Training Framework**: ML-Agents (v0.30.0), PyTorch backend
- **Training Output**: Trained `.onnx` model for inference inside Unity
- **Behavior**: Agent learns to reach a target while avoiding falling or drifting inefficiently

## Files

- `config/robotic_sorter.yaml`: Training config
- `Assets/Scripts/SorterAgent.cs`: Core agent logic
- `unity_env/robotic_sorter_sim/`: Complete Unity environment with scene, materials, prefabs
- `models/Sorter_run_03/`: Output folder with trained ONNX model

## Usage

1. Open Unity project from `unity_env/robotic_sorter_sim`
2. Load the `RoboticSorter` scene
3. Drag trained `.onnx` model into the `Behavior Parameters > Model` field
4. Hit **Play** to observe agent inference

To retrain:
```bash
mlagents-learn config/robotic_sorter.yaml --run-id=Sorter_run_04 --force

Author
Developed by Jose Peon — josepeon.com