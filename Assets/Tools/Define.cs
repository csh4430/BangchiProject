using Characters;
using Characters.Players;
using UnityEngine;

namespace Tools
{
    public static class Define
    {
        private static Player _player;
        public static Player Player => _player ?? (_player = GameObject.FindObjectOfType<Player>());
    }
}