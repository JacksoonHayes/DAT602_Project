﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DAT602_Project
{
    public partial class adminForm : Form
    {
        public adminForm()
        {
            InitializeComponent();
        }

        private void AdminForm_Load(object sender, EventArgs e)
        {
            try
            {
                var dbAccess = new AdminDAO();
                List<Player> players = dbAccess.getAllPlayers();
                List<Game> games = dbAccess.getAllGames();

                playersListBox.Items.Clear();
                gamesListBox.Items.Clear();

                foreach (var player in players)
                {
                    playersListBox.Items.Add(player); // Add Player objects into the listbox
                }
                playersListBox.DisplayMember = "username"; // Display only the username in the listbox

                foreach (var game in games)
                {
                    // Add each game's details to the list
                    gamesListBox.Items.Add(game);
                }
                gamesListBox.DisplayMember = "game_id"; // Display only the game name in the listbox
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while retrieving data: " + ex.Message, "Error");

            }
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            new AddUserForm().Show();
            AdminForm_Load(sender, e);
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            if (playersListBox.SelectedItem != null)
            {
                Player selectedPlayer = (Player)playersListBox.SelectedItem;

                // Pass the selected player's data to the ProfileForm
                ProfileForm profileForm = new ProfileForm(
                    selectedPlayer.player_id,
                    selectedPlayer.username,
                    selectedPlayer.password,
                    selectedPlayer.email,
                    selectedPlayer.locked_out,
                    selectedPlayer.is_banned
                );

                profileForm.ShowDialog(); // Open the ProfileForm as a modal dialog
                AdminForm_Load(sender, e); // Refresh the listbox after the ProfileForm is closed
            }
            else
            {
                MessageBox.Show("Please select a player to edit.");
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (playersListBox.SelectedItem != null)
            {
                Player selectedPlayer = (Player)playersListBox.SelectedItem;
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete this player account?", "Delete Player", MessageBoxButtons.YesNo);

                if (dialogResult == DialogResult.Yes)
                {
                    try
                    {
                        var dbAccess = new AdminDAO();
                        string result = dbAccess.deletePlayer(selectedPlayer.player_id);
                        MessageBox.Show(result, "Player deleted successfully");
                        AdminForm_Load(sender, e);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("An error occurred while trying to delete the player: " + ex.Message, "Error");
                    }
                }
            }
            else if (gamesListBox.SelectedItem != null)
            {
                Game selectedGame = (Game)gamesListBox.SelectedItem;
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete this game?", "Delete Game", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    try
                    {
                        var dbAccess = new AdminDAO();
                        string result = dbAccess.deleteGame(selectedGame.game_id);
                        MessageBox.Show(result, "Game deleted successfully");
                        AdminForm_Load(sender, e);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("An error occurred while trying to delete the game: " + ex.Message, "Error");
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select either a player or game to delete.");
            }
        }

        private void gamesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (gamesListBox.SelectedIndex != -1)
            {
                playersListBox.ClearSelected();
            }
        }

        private void playersListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (playersListBox.SelectedIndex != -1)
            {
                gamesListBox.ClearSelected();
            }
        }
    }
}
