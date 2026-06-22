using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Leaderboard
{
    public class LeaderboardView : MonoBehaviour
    {
        [Header("Items Setup")]
        [SerializeField] private RectTransform _contentContainer;
        [SerializeField] private int _itemsCount;
        [SerializeField] private LeaderboardItemView _itemPrefab;

        [Space(5), Header("Sticky Places Setup")]
        [SerializeField] private GameObject _topStickyRoot;
        [SerializeField] private GameObject _bottomStickyRoot;
        [SerializeField] private RectTransform _viewport;
        [SerializeField] private ScrollRect _scrollRect;

        private LeaderboardItemView _playerItem;

        private readonly List<LeaderboardItemView> _items = new();
        private readonly Vector3[] _playerCorners = new Vector3[4];
        private readonly Vector3[] _viewportCorners = new Vector3[4];

        private void Awake()
        {
            _scrollRect.onValueChanged.AddListener(HandleChangedScroll);
        }

        private void OnDestroy()
        {
            _scrollRect.onValueChanged.RemoveListener(HandleChangedScroll);
        }

        private void Start()
        {
            for(int i = 0; i < _itemsCount; i++)
            {
                var item = Instantiate(_itemPrefab, _contentContainer);
                LeaderboardEntry entry = new(i, $"Player_{i}", 10);
                item.SetPlayerData(entry);
                _items.Add(item);
            }

            int randomPlayerId = Random.Range(0, _items.Count);
            var playerItem = _items[randomPlayerId];

            playerItem.MarkAsIsPlayer();
            _playerItem = playerItem;

            Instantiate(playerItem, _topStickyRoot.transform);
            Instantiate(playerItem, _bottomStickyRoot.transform);

            Canvas.ForceUpdateCanvases();
            Refresh();
        }

        private void Refresh()
        {
            _playerItem.RectTransform.GetWorldCorners(_playerCorners);
            _viewport.GetWorldCorners(_viewportCorners);

            float playerTop = _playerCorners[1].y;
            float playerBottom = _playerCorners[0].y;

            float viewportTop = _viewportCorners[1].y;
            float viewportBottom = _viewportCorners[0].y;

            bool isAbove = playerBottom > viewportTop;

            bool isBelow = playerTop < viewportBottom;

            _topStickyRoot.SetActive(isAbove);
            _bottomStickyRoot.SetActive(isBelow);

            bool playerVisible = !isAbove && !isBelow;

            SetOriginalVisibility(playerVisible);
        }

        private void SetOriginalVisibility(bool visible)
        {
            if (!_playerItem.TryGetComponent<CanvasGroup>(out var canvasGroup))
                return;

            canvasGroup.alpha = visible ? 1f : 0f;
            canvasGroup.blocksRaycasts = visible;
            canvasGroup.interactable = visible;
        }

        private void HandleChangedScroll(Vector2 _) => Refresh();
    }
}