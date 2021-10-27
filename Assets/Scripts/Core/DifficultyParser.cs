using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Snek.Core
{
    public class DifficultyParser : IDifficultyProvider
    {
        private const string m_FileName = "Difficulty.json";
        
        public Difficulty[] GetDifficulties()
        {
            var path = Path.Combine(Application.streamingAssetsPath, m_FileName);
            string json;
            using (StreamReader r = new StreamReader(path))
            {
                json = r.ReadToEnd();
            }
            var difficultyArray = JsonUtility.FromJson<DifficultyArray>(json);
            return difficultyArray.Difficulties;
        }
    }
}
