using System;
using DG.Tweening;
using Managers;
using UnityEngine.SceneManagement;

namespace Behaviours
{
    public class PlayerDeathFeedback : CharacterBehaviour
    {
        private PlayerStat _playerStat;
        protected override void Awake()
        {
            base.Awake();
            _playerStat = GetComponent<PlayerStat>();
            _playerStat.OnDie += FeedBack;
        }

        private void FeedBack()
        {
            FadeManager.Instance.FadeIn(3f, ReInitialize);
        }
        
        private void ReInitialize()
        {
            DOTween.KillAll();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}