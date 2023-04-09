using System;
using System.Collections.Generic;
using System.Linq;

namespace Resources
{
    [Serializable]
    public class SoundList
    {
        public List<Sound> sounds = new List<Sound>();
        
        public Sound GetSound(string type)
        {
            return sounds.FirstOrDefault(sound => sound.type == type);
        }
    }
}