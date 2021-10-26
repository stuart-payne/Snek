using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Snek.Core
{
    public class DifficultyParser
    {
        private const string m_FileName = "Difficulty.json";
        
        public DifficultyArray GetDifficultiesFromFile()
        {
            var path = Path.Combine(Application.streamingAssetsPath, m_FileName);
            string json;
            using (StreamReader r = new StreamReader(path))
            {
                json = r.ReadToEnd();
            }
            return JsonUtility.FromJson<DifficultyArray>(json);
        }
    }
}
