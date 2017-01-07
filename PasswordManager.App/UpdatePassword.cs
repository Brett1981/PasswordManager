﻿using PasswordManager.Entities;
using PasswordManager.Globals;
using PasswordManager.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PasswordManager.App
{
    public partial class UpdatePassword : Form
    {
        User user;
        Password password;

        public UpdatePassword(User user, Password password)
        {
            InitializeComponent();

            this.user = user;
            this.password = password;
        }

        private void UpdatePassword_Load(object sender, EventArgs e)
        {
            txtName.Text = password.Name;
            txtEmail.Text = password.Email;
            txtUsername.Text = password.Username;
            txtWebsite.Text = password.Website;
            txtPassword.Text = password.Text;
            rtxtNotes.Text = password.Notes;
            
            btnSave.Enabled = IsEnable();
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            password.Name = txtName.Text;
            password.Email = txtEmail.Text;
            password.Username = txtUsername.Text;
            password.Website = txtWebsite.Text;
            password.Text =  txtPassword.Text;
            password.Notes = rtxtNotes.Text;
            password.DateModified = DateTime.Now;

            await PasswordsService.Instance().UpdateUserPasswordAsync(user, password);
        }

        private void ForSaveBtnEnable(object sender, EventArgs e)
        {
            btnSave.Enabled = IsEnable();
        }

        private bool IsEnable()
        {
            if (Verifier.Text(txtName.Text) && Verifier.Text(txtPassword.Text))
                return true;
            return false;
        }

        private async void btnGenerate_Click(object sender, EventArgs e)
        {
            txtPassword.Text = await PasswordsService.Instance().GeneratePasswordAsync(user);
        }

        private async void btnOptions_Click(object sender, EventArgs e)
        {
            PasswordGenerateOptions passwordGenerateOptionsForm = new PasswordGenerateOptions(user);

            if (passwordGenerateOptionsForm.ShowDialog() == DialogResult.OK)
            {
                user.Settings.PasswordOptions = passwordGenerateOptionsForm.passwordOptions;
                txtPassword.Text = await PasswordsService.Instance().GeneratePasswordAsync(user);
            }
        }
    }
}
