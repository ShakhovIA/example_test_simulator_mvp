using UnityEngine;
using System.IO;

namespace Game.Scripts
{
    public static class SaveLoadManager {
        private static string savePath = Application.persistentDataPath + "/simulation.json";

        public static void SaveSimulation(SimulationState state) {
            string json = JsonUtility.ToJson(state);
            File.WriteAllText(savePath, json);
        }

        public static SimulationState LoadSimulation() {
            if (File.Exists(savePath)) {
                string json = File.ReadAllText(savePath);
                return JsonUtility.FromJson<SimulationState>(json);
            }
            return null;
        }
    }
}