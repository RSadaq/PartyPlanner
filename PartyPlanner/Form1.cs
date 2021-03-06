﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PartyPlanner
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            p = new Party();

            PopulateListBoxes(p.entertainmentPrices, EntertainmentListBox);
            PopulateListBoxes(p.drinkPrices, DrinksListBox);
            PopulateListBoxes(p.foodPrices, MenuListBox);
        }

        Party p;
        public void PopulateListBoxes(Dictionary<string, decimal> items, ListBox x)//method to populate ListBoxes on UI. Values from db.
        {
            foreach (KeyValuePair<string, decimal> item in items)
            {
                x.Items.Add(item.Key);
            }
        }

        private void MenuListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            p.ItemSelected("food", p.foodPrices[MenuListBox.SelectedItem.ToString()]);
        }

        private void EntertainmentListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            p.ItemSelected("entertainment", p.entertainmentPrices[EntertainmentListBox.SelectedItem.ToString()]);          
        }

        private void DrinksListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            p.ItemSelected("drink", p.drinkPrices[DrinksListBox.SelectedItem.ToString()]);         
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            p.DecorationsRequired(checkBox1.Checked ? true : false);
        }

        private void numericUpDownGuestNo_ValueChanged(object sender, EventArgs e)
        {
            p.GetNoOfGuests((int)numericUpDownGuestNo.Value);
        }

        private void CalculateCostButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (DrinksListBox.SelectedItem == null || numericUpDownGuestNo.Value == 0 || MenuListBox.SelectedItem == null || EntertainmentListBox.SelectedItem == null)
                {
                    throw new ArgumentNullException();
                }
                TotalCostLabel.Text = "£ " + p.CostOfParty().ToString("#.##");
            }
            catch (ArgumentNullException ex)
            {
                TotalCostLabel.Text = ex.Message + "You must complete all fields!";
            }
        }
    }
}
