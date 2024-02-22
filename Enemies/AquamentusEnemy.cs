using CSE3902.AbstractClasses;
using CSE3902.Projectiles;
using CSE3902.Shared;
using CSE3902.Shared.Managers;
using Microsoft.Xna.Framework;

namespace CSE3902.Enemies;

public class AquamentusEnemy : AbstractEnemy
{
    private int _currentChargeTime;
    private readonly int _totalChargeTime;
    private bool _isCharging;
    private bool _fireballActive;
    private AquamentusFireball _fireballUpper;
    private AquamentusFireball _fireballMiddle;
    private AquamentusFireball _fireballLower;

    public AquamentusEnemy(Vector2 position)
    {
        Height = 32 * Globals.GlobalSizeMult;
        Width = 24 * Globals.GlobalSizeMult;
        Sprite = SpritesheetFactory.Instance.CreateAquamentusSprite();
        Position = position;
        TimeCounter = 0;
        //_rand = new System.Random();
        CurrentDirection = Rand.Next(2);
        _isCharging = true;
        _currentChargeTime = 0;
        _totalChargeTime = 32;
        _fireballActive = false;
        DamageAmount = 2;
        MovingDirection = Direction.Left;
        UpdateHitbox();
        Active = true;
        Health = 6;
    }

    public override void Update()
    {
        if (InvulnerableTime > 0)
        {
            InvulnerableTime--;
            TakeKnockback();
        }
        else
        {
            Move();
            UpdateSprite();
        }

        int randomInt = Rand.Next(200);
        if (randomInt <= 1)
        {
            CurrentDirection = randomInt;
        }

        if (_isCharging)
        {
            if (_currentChargeTime >= _totalChargeTime)
            {
                //fire
                _fireballActive = true;
                _currentChargeTime = 0;
                _fireballUpper = new AquamentusFireball(Position, new Vector2(-4, -1));
                _fireballMiddle = new AquamentusFireball(Position, new Vector2(-4, 0));
                _fireballLower = new AquamentusFireball(Position, new Vector2(-4, 1));
                GameObjectManagers.EnemyProjectileManager.Spawn(_fireballLower);
                GameObjectManagers.EnemyProjectileManager.Spawn(_fireballUpper);
                GameObjectManagers.EnemyProjectileManager.Spawn(_fireballMiddle);
                _isCharging = false;
                SoundFactory.Instance.PlayBossScream();
            }
            else
            {
                _currentChargeTime++;
            }
        }
        else if (_fireballActive)
        {
            if (!(_fireballUpper.Active || _fireballMiddle.Active || _fireballLower.Active))
            {
                _fireballActive = false;
                _isCharging = true;
            }
        }
    }

    public override void Move()
    {
        switch (CurrentDirection)
        {
            case 0:
                Position = new Vector2(Position.X - 1, Position.Y);
                MovingDirection = Direction.Left;
                break;
            case 1:
                Position = new Vector2(Position.X + 1, Position.Y);
                MovingDirection = Direction.Right;
                break;
        }

        UpdateHitbox();
    }

    public override void UpdateSprite()
    {
        if (TimeCounter >= 16)
        {
            if (_isCharging)
            {
                if (Sprite.Column == 1)
                {
                    Sprite.SetFrame(2, 2);
                    TimeCounter = 0;
                }
                else
                {
                    Sprite.SetFrame(2, 1);
                    TimeCounter = 0;
                }
            }
            else
            {
                if (Sprite.Column == 1)
                {
                    Sprite.SetFrame(1, 2);
                    TimeCounter = 0;
                }
                else
                {
                    Sprite.SetFrame(1, 1);
                    TimeCounter = 0;
                }
            }
        }

        TimeCounter++;
    }

    public override void TakeKnockback()
    {
    }

    public override void Draw()
    {
        //base.Draw();
        if (InvulnerableTime % 5 == 0) Sprite.DrawFromCenter(Position); //Has to be drawn from center..?
        if (_fireballActive)
        {
            _fireballUpper.Draw();
            _fireballMiddle.Draw();
            _fireballLower.Draw();
        }
    }
}