using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ScoreViewController : MonoBehaviour
{
    [SerializeField] private Text scoreText;

    [Inject] Player player;

    void Start()
    {
        player.Data.OnScoreChanged += OnPlayerScoreChanged;
        UpdateView();
    }

    private void OnPlayerScoreChanged()
    {
        UpdateView();
    }

    void UpdateView()
    {
        scoreText.text = player.Data.score.ToString();
    }

    void OnDestroy()
    {
        player.Data.OnScoreChanged -= OnPlayerScoreChanged;
    }
}
