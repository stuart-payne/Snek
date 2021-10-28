using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Snek.Core
{
    public class GameoverUIManager : MonoBehaviour
    {
        [SerializeField] private GameManager m_GameManager;
        [SerializeField] private Button m_GameoverButton;

        private void Awake()
        {
            m_GameoverButton.onClick.AddListener(m_GameManager.RestartGame);
        }
    }
}
