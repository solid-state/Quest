using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;



namespace Quest
{
    public partial class Form1 : Form
    {

        private Game game;
        private Random random = new Random();
        
        public int round = 0;
        //public int enemiesShown = 0;

        public Form1()
        {
            InitializeComponent(); 
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            game = new Game(new Rectangle(78, 57, 420, 155));
            game.NewLevel(random);
            picturePlayer.Visible = true;          
            UpdateCharacters();

        }

        /* UpdateCharacters Method */
        public void UpdateCharacters()
        {
            picturePlayer.Location = game.PlayerLocation;

            /* Hide Weapon icon PictureBoxes */
            pictureSword.Visible = false;
            pictureBow.Visible = false;
            pictureMace.Visible = false;
            pictureRed.Visible = false;
            pictureBlue.Visible = false;
            playerHitPoint.Text = game.PlayerHitPoints.ToString();
            label6.Text = game.Level.ToString();
            round++;
            label10.Text = round.ToString();
            int enemiesShown = 0;   // This is reset each time method is called

            foreach (Enemy enemy in game.Enemies)
            {
                if (enemy is Bat)
                {
                    pictureBat.Location = enemy.Location;
                    batHitPoint.Text = enemy.HitPoints.ToString();
                    pictureBat.Visible = true;
                    if (enemy.HitPoints > 0)
                    {
                        enemiesShown++;
                    }
                    else
                    {
                        pictureBat.Visible = false;
                    }
                }
                if (enemy is Ghost)
                {
                    pictureGhost.Location = enemy.Location;     // Placed within first if statment!
                    ghostHitPoint.Text = enemy.HitPoints.ToString();
                    pictureGhost.Visible = true;
                    if (enemy.HitPoints > 0)
                    {
                        enemiesShown++;
                    }
                    else
                    {
                        pictureGhost.Visible = false;
                    }
                }
                if (enemy is Ghoul)
                {
                    pictureGhoul.Location = enemy.Location;
                    ghoulHitPoint.Text = enemy.HitPoints.ToString();
                    pictureGhoul.Visible = true;
                    if (enemy.HitPoints > 0)
                    {
                        enemiesShown++;
                    }
                    else
                    {
                        pictureGhoul.Visible = false;
                    }
                }
            }   //End of Enemy foreach


            
            Control weaponControl = null;
            switch (game.WeaponInRoom.Name)
            {
                case "Sword":
                    weaponControl = pictureSword; break;
                case "Bow":
                    weaponControl = pictureBow; break;
                case "Mace":
                    weaponControl = pictureMace; break;
                case "RedPotion":
                    weaponControl = pictureRed; break;
                case "BluePotion":
                    weaponControl = pictureBlue; break;
                default: break;
            }
            weaponControl.Visible = true;

            /* Inventory weapon PictureBoxes */     // !!! Must be put out of method as something is slow
            if (game.CheckPlayerInventory("Sword"))
            { pictureEquipSword.Visible = true; }
            else
            { pictureEquipSword.Visible = false; }

            if (game.CheckPlayerInventory("Bow"))
            { pictureEquipBow.Visible = true;
            }
            else
            { pictureEquipBow.Visible = false; }

            if (game.CheckPlayerInventory("Mace"))
            { pictureEquipMace.Visible = true; }
            else
            { pictureEquipMace.Visible = false; }

            if (game.CheckPlayerInventory("RedPotion"))
            { pictureEquipRed.Visible = true; }
            else
            { pictureEquipRed.Visible = false; }

            if (game.CheckPlayerInventory("BluePotion"))
            { pictureEquipBlue.Visible = true; }
            else
            { pictureEquipBlue.Visible = false; }

            /* Update Player, weapons and level */
            weaponControl.Location = game.WeaponInRoom.Location;
            if (game.WeaponInRoom.PickedUp)
            {
                weaponControl.Visible = false;
                /* To automatically load weapon when no other */
                if (game.Level == 1)
                {
                    game.Equip("Sword");
                    pictureEquipSword.BorderStyle = BorderStyle.FixedSingle;
                }
            }

            if (game.PlayerHitPoints <= 0)
            {
                picturePlayer.Visible = false;
                game.sounds.URL = @"death.wav";
                game.sounds.controls.play();
                MessageBox.Show("You died");
                Application.Exit();
            }

            if (enemiesShown < 1)       // If made to this point and enemiesShown = 0 - all is dead
            {
                if (game.Level < 7)
                {
                    game.sounds.URL = @"win.wav";
                    game.sounds.controls.play();
                    MessageBox.Show("You have defeated the enemies on this level");
                }

                batHitPoint.Text = "-";
                ghostHitPoint.Text = "-";
                ghoulHitPoint.Text = "-";
                game.NewLevel(random);
                round = 0;
                UpdateCharacters();   
            }

        }

        // End of UpdateCharacter Method */


        /* Weapon Inventary */
        private void pictureEquipBow_Click(object sender, EventArgs e)
        {
            game.Equip("Bow");
            RemoveInventory();           
            pictureEquipBow.BorderStyle = BorderStyle.FixedSingle;
            SetButtons(true);
        }

        private void pictureEquipSword_Click(object sender, EventArgs e)
        {
            game.Equip("Sword");
            RemoveInventory();
            pictureEquipSword.BorderStyle = BorderStyle.FixedSingle;
            SetButtons(true);
        }

        private void pictureEquipMace_Click(object sender, EventArgs e)
        {
            game.Equip("Mace");
            RemoveInventory();
            pictureEquipMace.BorderStyle = BorderStyle.FixedSingle;
            SetButtons(true);
        }

        private void pictureEquipRed_Click(object sender, EventArgs e)
        {
            game.Equip("RedPotion");
            RemoveInventory();
            pictureEquipRed.BorderStyle = BorderStyle.FixedSingle;
            SetButtons(false);
        }

        private void pictureEquipBlue_Click(object sender, EventArgs e)
        {
            game.Equip("BluePotion");
            RemoveInventory();
            pictureEquipBlue.BorderStyle = BorderStyle.FixedSingle;
            SetButtons(false);

        }

        private void SetButtons(bool action)
        {
            if (action)
            {
                buttonDrink.Visible = false;
                attackUp.Visible = true;
                attackRight.Visible = true;
                attackDown.Visible = true;
                attackLeft.Visible = true;
            }
            else
            {
                attackUp.Visible = false;
                attackRight.Visible = false;
                attackDown.Visible = false;
                attackLeft.Visible = false;
                buttonDrink.Visible = true;
            }
        }

        private void RemoveInventory()
        {
            pictureEquipBow.BorderStyle = BorderStyle.None;
            pictureEquipSword.BorderStyle = BorderStyle.None;
            pictureEquipMace.BorderStyle = BorderStyle.None;
            pictureEquipRed.BorderStyle = BorderStyle.None;
            pictureEquipBlue.BorderStyle = BorderStyle.None;
        }

        /* Move Buttons */
        private void moveUp_Click(object sender, EventArgs e)
        {
            game.Move(Direction.Up, random);
            UpdateCharacters();
        }

        private void moveLeft_Click(object sender, EventArgs e)
        {
            game.Move(Direction.Left, random);
            UpdateCharacters();
        }

        private void moveRight_Click(object sender, EventArgs e)
        {
            game.Move(Direction.Right, random);
            UpdateCharacters();
        }

        private void moveDown_Click(object sender, EventArgs e)
        {
            game.Move(Direction.Down, random);
            UpdateCharacters();
        }

        /* Attack Buttons */
        private void attackUp_Click(object sender, EventArgs e)
        {
            game.Attack(Direction.Up, random);
            UpdateCharacters();
        }

        private void attackLeft_Click(object sender, EventArgs e)
        {
            game.Attack(Direction.Left, random);
            UpdateCharacters();
        }

        private void attackRight_Click(object sender, EventArgs e)
        {
            game.Attack(Direction.Right, random);
            UpdateCharacters();
        }

        private void attackDown_Click(object sender, EventArgs e)
        {
            game.Attack(Direction.Down, random);
            UpdateCharacters();
        }

        private void buttonDrink_Click(object sender, EventArgs e)
        {
            game.Attack(Direction.Down, random);

            if (!game.PotionUsed("BluePotion"))
                pictureEquipBlue.Visible = false;
            if (!game.PotionUsed("RedPotion"))
                pictureEquipRed.Visible = false;
            UpdateCharacters();
        }

        /* Allows the User to move using Arrow keys */
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Up)
            {
                game.Move(Direction.Up, random);
                UpdateCharacters();
                return true;
            }
            if (keyData == Keys.Left)
            {
                game.Move(Direction.Left, random);
                UpdateCharacters();
                return true;
            }
            if (keyData == Keys.Right)
            {
                game.Move(Direction.Right, random);
                UpdateCharacters();
                return true;
            }
            if (keyData == Keys.Down)
            {
                game.Move(Direction.Down, random);
                UpdateCharacters();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }




    }
}
