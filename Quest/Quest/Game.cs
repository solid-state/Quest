using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using System.Media; // Declaring here and not in code speeds operation
using WMPLib; // Used as an extra sound player


namespace Quest
{
    class Game
    {
        /* FIELD OBJECTS*/
        public List<Enemy> Enemies;
        public Weapon WeaponInRoom;
        private Player player;
        public WindowsMediaPlayer backSnd;
        public WindowsMediaPlayer stepSnd;  // Used in Move() method
        public WindowsMediaPlayer attackSnd;
        public WindowsMediaPlayer sounds;
                
        /* PROPERTIES */
        public Point PlayerLocation { get { return player.Location; } }
        public int PlayerHitPoints { get { return player.HitPoints; } }
        public List<string> PlayerWeapons { get { return player.Weapons; } }

        private int level = 0;
        public int Level { get { return level; } }

        private Rectangle boundaries;   // Area of the limits of playable game
        public Rectangle Boundaries { get { return boundaries; } }

        /* CONSTRUCTOR */
        public Game(Rectangle boundaries)
        {
            this.boundaries = boundaries;
            player = new Player(this, new Point(boundaries.Left + 10, boundaries.Top + 70),
                                boundaries);
            backSnd = new WindowsMediaPlayer();
            stepSnd = new WindowsMediaPlayer();
            attackSnd = new WindowsMediaPlayer();
            sounds = new WindowsMediaPlayer();
            
        }

        /* METHODS */
        public void Move(Direction direction, Random random)
        {
            player.Move(direction);
            stepSnd.URL = @"move.wav";
            stepSnd.controls.play();
            foreach (Enemy enemy in Enemies)
            {
                if (enemy.HitPoints > 0)
                {
                    enemy.Move(random);
                }
            }
        }

        public void Attack(Direction direction, Random random)
        {
            player.Attack(direction, random);
            foreach (Enemy enemy in Enemies)
            {
                if (enemy.HitPoints > 0)
                {
                    enemy.Move(random);
                }
            }
        }

        private Point GetRandomLocation(Random random)
        {
            return new Point(boundaries.Left + random.Next(
                            boundaries.Right / 10 - boundaries.Left / 10) * 10,
                            boundaries.Top + random.Next(
                            boundaries.Bottom / 10 - boundaries.Top / 10) * 10);
        }

        public void Equip(string weaponName)        // Single line method, seems wasteful
        { player.Equip(weaponName); }

        public bool CheckPlayerInventory(string weaponName)
        {
             return player.Weapons.Contains(weaponName); 
        }

        public void HitPlayer(int maxDamage, Random random)
        {
            attackSnd.URL = @"attack.wav";
            attackSnd.controls.play();
            player.Hit(maxDamage, random); 
        }

        public bool PotionUsed(string potionName)
        { return player.PotonUsed(potionName); }

        public void IncreasePlayerHealth(int health, Random random)
        { player.IncreaseHealth(health, random); }

        public void NewLevel(Random random)
        {
            level++;
            if (!(level == 8))
            {
                backSnd.URL = @"start.wav";
                backSnd.controls.play();
            }

            switch (level)
            {
                case 1:
                    
                    Enemies = new List<Enemy>();
                    Enemies.Add(new Bat(this, GetRandomLocation(random), boundaries));
                    WeaponInRoom = new Sword(this, GetRandomLocation(random));
                    break;
                case 2:
                    
                    Enemies.Clear();
                    Enemies.Add(new Ghost(this, GetRandomLocation(random), boundaries));
                    WeaponInRoom = new BluePotion(this, GetRandomLocation(random));
                    break;
                case 3:
                    Enemies.Clear();
                    Enemies.Add(new Ghoul(this, GetRandomLocation(random), boundaries));
                    WeaponInRoom = new Bow(this, GetRandomLocation(random));
                    break;
                case 4:
                    Enemies.Clear();
                    Enemies.Add(new Bat(this, GetRandomLocation(random), boundaries));
                    Enemies.Add(new Ghost(this, GetRandomLocation(random), boundaries));
                    if (player.Weapons.Contains("Bow"))
                        WeaponInRoom = new BluePotion(this, GetRandomLocation(random));
                    else
                        WeaponInRoom = new Bow(this, GetRandomLocation(random));
                    break;
                case 5:
                    Enemies.Clear();
                    Enemies.Add(new Bat(this, GetRandomLocation(random), boundaries));
                    Enemies.Add(new Ghoul(this, GetRandomLocation(random), boundaries));
                    WeaponInRoom = new RedPotion(this, GetRandomLocation(random));
                    break;
                case 6:
                    Enemies.Clear();
                    Enemies.Add(new Ghost(this, GetRandomLocation(random), boundaries));
                    Enemies.Add(new Ghoul(this, GetRandomLocation(random), boundaries));
                    WeaponInRoom = new Mace(this, GetRandomLocation(random));
                    break;
                case 7:
                    Enemies.Clear();
                    Enemies.Add(new Bat(this, GetRandomLocation(random), boundaries));
                    Enemies.Add(new Ghost(this, GetRandomLocation(random), boundaries));
                    Enemies.Add(new Ghoul(this, GetRandomLocation(random), boundaries));
                    if (player.Weapons.Contains("Mace"))
                        WeaponInRoom = new RedPotion(this, GetRandomLocation(random));
                    else
                        WeaponInRoom = new Mace(this, GetRandomLocation(random));
                    break;
                case 8:
                    Enemies.Clear();
                    stepSnd.URL = @"win.wav";
                    stepSnd.controls.play();
                    MessageBox.Show("You have slain all known beasty, \nyou art hero of this land.");
                    Environment.Exit(0);
                    break;
                default: break;
            }
        }
    }
}
