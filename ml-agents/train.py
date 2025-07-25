import os
import subprocess
from datetime import datetime

# Timestamped run ID for logging (include microseconds to avoid duplicates)
run_id = f"robotic_sorter_{datetime.now().strftime('%Y%m%d_%H%M%S_%f')}"
results_dir = os.path.join("results", run_id)

# Ensure results folder exists
os.makedirs(results_dir, exist_ok=True)

# Path to your config YAML
config_path = os.path.join("config", "robotic_sorter.yaml")

# Base command
command = [
    "mlagents-learn",
    config_path,
    "--run-id", run_id,
    "--time-scale", "1",
    "--no-graphics",
    #"--env", "NONE"  # replace with path to Unity executable when ready
]

print(f"Running training with run ID: {run_id}")
print("Launching ML-Agents...\n")

# Launch the training process
command.append("--force")
subprocess.run(command)