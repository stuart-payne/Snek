using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Snek.Core
{
    public class DifficultyUIManager : MonoBehaviour
    {
        [SerializeField] private GameManager m_GameManager;
        [SerializeField] private GameObject m_ButtonPrefab;
        [SerializeField] private GameObject m_Panel;

        private void Start()
        {
            CreateDifficultyButtons();
        }

        private void CreateDifficultyButtons()
        {
            var difficulties = m_GameManager.Difficulties;
            foreach (var difficulty in difficulties)
            {
                var button = Instantiate(m_ButtonPrefab, m_Panel.transform).GetComponent<Button>();
                button.onClick.AddListener(() => m_GameManager.SetupGame(difficulty));
                button.onClick.AddListener(() => m_Panel.SetActive(false));
                button.GetComponentInChildren<Text>().text = difficulty.Name;
            }
        }
    }
}
