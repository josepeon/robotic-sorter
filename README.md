# Robotic Sorter

**Reinforcement Learning + Vision Simulation Project**  
A robotic agent is trained to visually identify and sort 3D objects using Unity ML-Agents and PPO.

---

## Project Structure

robotic_sorter/
├── ml-agents/         # Python training launcher and utilities
├── config/            # ML-Agents training config (robotic_sorter.yaml)
├── models/            # Saved model checkpoints (.onnx)
├── vision/            # OpenCV tools (optional)
├── videos/            # Training and result captures
├── docs/              # Architecture diagrams, notes
├── unity_env/         # Unity simulation project (built on ASUS)
├── results/           # Training logs and summaries
├── README.md
└── train.py

---

## Training Pipeline

- Uses `mlagents-learn` with PPO
- Controlled via `train.py` (auto-generates run IDs and logs)
- Training config stored in `config/robotic_sorter.yaml`
- Results saved under `results/`

---

## Python Environment

- Python 3.8 (via Conda)
- Torch 1.13.1 (Apple Silicon compatible)
- ML-Agents 0.28.0 (manual dependency control)
- NumPy 1.23.5 (for ML-Agent compatibility)
- Protobuf 3.20.3, TensorBoard, OpenCV, etc.

> See `requirements.txt` or `conda list` export for exact versions.

---

## Unity Setup

Unity is used only for simulation and runs on a separate machine (ASUS).  
Version: **Unity Editor 2022.3.x LTS** with ML-Agents Unity package.

---

## TODO

- [ ] Build sorting agent scene in Unity
- [ ] Define reward function logic
- [ ] Connect Unity ↔ Mac via ML-Agents
- [ ] Train and visualize policy
- [ ] Export trained model for inference