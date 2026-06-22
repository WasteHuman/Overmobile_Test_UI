using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Leaderboard
{
    [RequireComponent(typeof(RectTransform))]
    public class LeaderboardItemView : MonoBehaviour
    {
        [SerializeField] private Color _isPlayerColor;
        [SerializeField] private TextMeshProUGUI _playerNameText;
        [SerializeField] private TextMeshProUGUI _rankText;
        [SerializeField] private TextMeshProUGUI _scoreText;

        private bool _isPlayer = false;
        private Image _itemBackground;
        private RectTransform _rectTransform;

        public RectTransform RectTransform => _rectTransform;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _itemBackground = GetComponent<Image>();
        }

        public void MarkAsIsPlayer()
        {
            _playerNameText.text = "You";
            _isPlayer = true;
            _itemBackground.color = _isPlayerColor;
        }

        public void SetPlayerData(LeaderboardEntry data)
        {
            _playerNameText.text = $"{data.Name}";
            _rankText.text = $"{data.Rank}";
            _scoreText.text = $"{data.Score}";
        }
    }
}