using System;
using Characters;
using UnityEngine;

namespace Behaviours
{
    public class PlayerAnimation : CharacterAnimation
    {
        public Transform hitscanTransform;

        protected override void Face(Vector2 dir)
        {
            base.Face(dir);
            hitscanTransform.localScale = new Vector3(_lastDir, 1, 1);
        }
    }
}