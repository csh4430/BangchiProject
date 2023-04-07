using System;
using Tools;

namespace Characters.Enemies
{
    public class Enemy : Character
    {
        private void Update()
        {
            Chase();
            Attack();
        }
    }
}