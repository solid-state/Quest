using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Quest
{
    class Player : Mover
    {
        /*  FIELD VARIABLES */
        private Weapon equippedWeapon;
        
        /* PROPERTIES */
        private int hitPoints;
        public int HitPoints { get { return hitPoints; } }
        private Size dimension = new Size(30, 30);
        public Size Dimension { get { return dimension; } }
        private List<Weapon> inventory = new List<Weapon>();
        public List<string> Weapons
        { get {
            List<string> names = new List<string>();
            foreach (Weapon weapon in inventory)
                names.Add(weapon.Name);
            return names; }
        }

        /* CONSTRUCTOR */
        public Player(Game game, Point location, Rectangle boundaries)
            : base(game, location)
        {
            hitPoints = 10;
        }

        /* METHODS */
        public void Heal(int hp)
        {
            hitPoints += hp;
        }
        public void Hit(int maxDamage, Random random)
        {
            hitPoints -= random.Next(1, maxDamage);
        }

        public void IncreaseHealth(int health, Random random)
        {
            hitPoints += random.Next(1, health);
        }

        public void Equip(string weaponName)
        {
            foreach (Weapon weapon in inventory)
            {
                if (weapon.Name == weaponName)
                    equippedWeapon = weapon;
            }
        }

        public void Move(Direction direction)
        {
            base.location = Move(direction, game.Boundaries);
            if (!game.WeaponInRoom.PickedUp)
            {
                if (Nearby(game.WeaponInRoom.Location, 25))   // had parrameter distance 10
                {
                    game.sounds.URL = @"pickup.wav";
                    game.sounds.controls.play();
                    game.WeaponInRoom.PickUpWeapon();
                    inventory.Add(game.WeaponInRoom);
                }
            }
        }

        public void Attack(Direction direction, Random random)
        {
            if (equippedWeapon != null)     // If weapon is equiped - must make automatic
            {
                equippedWeapon.Sound();
                if (!(equippedWeapon is IPotion))     // If its a weapon - Attack, attack
                {
                    equippedWeapon.Attack(direction, random);

                }
                if (equippedWeapon is IPotion)
                {
                    if (equippedWeapon.Name == "BluePotion")
                        IncreaseHealth(10, random);
                    if (equippedWeapon.Name == "RedPotion")
                        hitPoints = 10;        // Full health
                    inventory.Remove(equippedWeapon);       // Take away used potion
                }
            }
            else
                MessageBox.Show("You have no weapon equipped!");
        }   // End of Attack method

        public bool PotonUsed(string potionName)
        {
            IPotion potion;
            bool potionUsed = true;
            foreach (Weapon weapon in inventory)
            {
                if (weapon.Name == potionName)
                {
                    potion = weapon as IPotion;
                    potionUsed = potion.Used;
                }
            }
            return potionUsed;
        }


    }
}
