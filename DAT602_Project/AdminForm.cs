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
                var dbAccess = new DataAccess();
                List<Player> players = dbAccess.GetAllPlayers();
                List<Game> games = dbAccess.GetAllGames();

                playersListBox.Items.Clear();
                gamesListBox.Items.Clear();

                foreach (var player in players)
                {
                    // Add each player's details to the list
                    playersListBox.Items.Add($"{player.username} - Score: {player.score}");
                }
                foreach (var game in games)
                {
                    // Add each game's details to the list
                    gamesListBox.Items.Add($"Game ID:{game.game_id}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while retrieving data: " + ex.Message, "Error");

            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {

        }
    }
}
