using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;

namespace CSE3902.Shared
{
    public class SoundFactory
    {
        public static SoundFactory Instance { get; private set; } = new();
        private Dictionary<string, SoundEffect> _soundEffects = new();
        private Dictionary<string, SoundEffectInstance> _soundEffectInstances = new();
        private bool _muted = false;
        private SoundFactory() { }

        public void LoadAllSounds(ContentManager content)
        {
            _soundEffects = new Dictionary<string, SoundEffect>
            {
                { "Bomb Drop", content.Load<SoundEffect>("LOZ_Bomb_Drop") },
                { "Bomb Blow", content.Load<SoundEffect>("LOZ_Bomb_Blow") },
                { "Fire Arrow", content.Load<SoundEffect>("LOZ_Arrow_Boomerang") },
                { "Fire Hadoken", content.Load<SoundEffect>("Hadoken") },
                { "Boss Scream", content.Load<SoundEffect>("LOZ_Boss_Scream1") },
                { "Door Unlock", content.Load<SoundEffect>("LOZ_Door_Unlock") },
                { "Enemy Die", content.Load<SoundEffect>("LOZ_Enemy_Die") },
                { "Enemy Hit", content.Load<SoundEffect>("LOZ_Enemy_Hit") },
                { "Get Key Item", content.Load<SoundEffect>("LOZ_Fanfare") },
                { "Get Heart", content.Load<SoundEffect>("LOZ_Get_Heart") },
                { "Get Minor Item", content.Load<SoundEffect>("LOZ_Get_Item") },
                { "Get Rupee", content.Load<SoundEffect>("LOZ_Get_Rupee") },
                { "Key Appear", content.Load<SoundEffect>("LOZ_Key_Appear") },
                { "Link Die", content.Load<SoundEffect>("LOZ_Link_Die") },
                { "Link Hurt", content.Load<SoundEffect>("LOZ_Link_Hurt") },
                { "Secret Found", content.Load<SoundEffect>("LOZ_Secret") },
                { "Shield Block", content.Load<SoundEffect>("LOZ_Shield") },
                { "Walk Stairs", content.Load<SoundEffect>("LOZ_Stairs") },
                { "Sword Projectile Fire", content.Load<SoundEffect>("LOZ_Sword_Shoot") },
                { "Sword Slash", content.Load<SoundEffect>("LOZ_Sword_Slash") },
                { "Collect Triforce", content.Load<SoundEffect>("LOZ_Pickup_Triforce") }
            };
            _soundEffectInstances = new Dictionary<string, SoundEffectInstance>
            {
                { "Boomerang Fly", content.Load<SoundEffect>("LOZ_Arrow_Boomerang").CreateInstance() },
                { "Low Health", content.Load<SoundEffect>("LOZ_LowHealth").CreateInstance() },
                { "Text Writing", content.Load<SoundEffect>("LOZ_Text").CreateInstance() },
                { "Background Music", content.Load<SoundEffect>("Legend of Zelda - NES - Dungeon Theme").CreateInstance() }
            };
        }

        public void ChangeMuted()
        {
            _muted = !_muted;
            if (_muted)
            {
                StopBackgroundMusic();
                StopLowHealth();
                StopBoomerangFly();
                StopTextWriting();
            }
            else
            {
                PlayBackgroundMusic();
            }
        }
        public void PlayTriforcePickup()
        {
            StopBackgroundMusic();
            if (!_muted) _soundEffects["Collect Triforce"].Play();
        }
        public void PlayBombDrop()
        {
            if (!_muted) _soundEffects["Bomb Drop"].Play();
        }
        public void PlayBombExplode()
        {
            if (!_muted) _soundEffects["Bomb Blow"].Play();
        }
        public void PlayArrowFire()
        {
            if (!_muted) _soundEffects["Fire Arrow"].Play();
        }
        public void PlayHadokenFire()
        {
            if (!_muted) _soundEffects["Fire Hadoken"].Play();
        }
        public void PlayBossScream()
        {
            if (!_muted) _soundEffects["Boss Scream"].Play();
        }
        public void PlayUnlockDoor()
        {
            if (!_muted) _soundEffects["Door Unlock"].Play();
        }
        public void PlayEnemyDie()
        {
            if (!_muted) _soundEffects["Enemy Die"].Play();
        }
        public void PlayEnemyHit()
        {
            if (!_muted) _soundEffects["Enemy Hit"].Play();
        }
        public void PlayGetKeyItem()
        {
            if (!_muted) _soundEffects["Get Key Item"].Play();
        }
        public void PlayGetHeart()
        {
            if (!_muted) _soundEffects["Get Heart"].Play();
        }
        public void PlayGetMinorItem()
        {
            if (!_muted) _soundEffects["Get Minor Item"].Play();
        }
        public void PlayGetRupee()
        {
            if (!_muted) _soundEffects["Get Rupee"].Play();
        }
        public void PlayKeyAppear()
        {
            if (!_muted) _soundEffects["Key Appear"].Play();
        }
        public void PlayLinkDie()
        {
            if (!_muted) _soundEffects["Link Die"].Play();
        }
        public void PlayLinkHurt()
        {
            if (!_muted) _soundEffects["Link Hurt"].Play();
        }
        public void PlaySecretFound()
        {
            if (!_muted) _soundEffects["Secret Found"].Play();
        }
        public void PlayShieldBlock()
        {
            if (!_muted) _soundEffects["Shield Block"].Play();
        }
        public void PlayWalkStairs()
        {
            if (!_muted) _soundEffects["Walk Stairs"].Play();
        }
        public void PlaySwordSlash()
        {
            if (!_muted) _soundEffects["Sword Slash"].Play();
        }
        public void PlaySwordProjectileFire()
        {
            if (!_muted) _soundEffects["Sword Projectile Fire"].Play();
        }
        public void PlayBoomerangFly()
        {
            if (!_muted) _soundEffectInstances["Boomerang Fly"].IsLooped = true;
            if (!_muted) _soundEffectInstances["Boomerang Fly"].Play();
        }
        public void StopBoomerangFly()
        {
            if (!_muted) _soundEffectInstances["Boomerang Fly"].IsLooped = false;
            if (!_muted) _soundEffectInstances["Boomerang Fly"].Play();
        }
        public void PlayLowHealth()
        {
            if (!_muted) _soundEffectInstances["Low Health"].IsLooped = true;
            if (!_muted) _soundEffectInstances["Low Health"].Play();
        }
        public void StopLowHealth()
        {
            if (!_muted && _soundEffectInstances["Low Health"].IsLooped == true) {
                _soundEffectInstances["Low Health"].IsLooped = false;
                _soundEffectInstances["Low Health"].Play();
                _soundEffectInstances["Low Health"].Stop();
            }
        }
        public void PlayTextWriting()
        {
            if (!_muted) _soundEffectInstances["Text Writing"].IsLooped = true;
            if (!_muted) _soundEffectInstances["Text Writing"].Play();
        }
        public void StopTextWriting()
        {
            if (!_muted) _soundEffectInstances["Text Writing"].IsLooped = false;
            if (!_muted) _soundEffectInstances["Text Writing"].Play();
        }
        public void PlayBackgroundMusic()
        {
            if (!_muted)
            {
                _soundEffectInstances["Background Music"].IsLooped = true;
                _soundEffectInstances["Background Music"].Volume = 0.2f;
                _soundEffectInstances["Background Music"].Play();
            }
        }
        public void StopBackgroundMusic()
        {
            _soundEffectInstances["Background Music"].IsLooped = false;
            _soundEffectInstances["Background Music"].Stop();
        }
    }
}
