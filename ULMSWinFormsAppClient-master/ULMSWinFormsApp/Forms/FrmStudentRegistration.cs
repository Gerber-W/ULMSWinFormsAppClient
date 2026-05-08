using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ULMSWinFormsApp.Models;

namespace ULMSWinFormsApp.Forms
{
    public partial class FrmStudentRegistration : Form
    {
        public FrmStudentRegistration()
        {
            InitializeComponent();
        }


        private void btnSaveStudent_Click(object sender, EventArgs e)
        {
            // BUG-003 FIX: int.Parse() crashes the app if Age is empty or non-numeric
            // OLD (faulty): Age = int.Parse(txtAge.Text), — no safety check was done
            if (!int.TryParse(txtAge.Text, out int age) || age <= 0)
            {
                MessageBox.Show("Please enter a valid numeric age.");
                return;
            }

            // BUG-007 FIX: No email format validation allowed corrupt data to be saved
            // OLD (faulty): Email = txtEmail.Text — any string was accepted with no check
            try
            {
                var emailCheck = new System.Net.Mail.MailAddress(txtEmail.Text);
            }
            catch
            {
                MessageBox.Show("Please enter a valid email address (e.g. user@domain.com).");
                return;
            }

            Student student = new Student
            {
                StudentId = txtStudentId.Text,
                FullName = txtFullName.Text,
                Email = txtEmail.Text,
                Age = age, // BUG-003 FIX: using safely parsed value from TryParse above
                Programme = cmbProgramme.Text
            };

            txtStudentOutput.Text =
                "Student saved successfully!" + Environment.NewLine +
                "Student ID: " + student.StudentId + Environment.NewLine +
                "Full Name: " + student.FullName + Environment.NewLine +
                "Email: " + student.Email + Environment.NewLine +
                "Age: " + student.Age + Environment.NewLine +
                "Programme: " + student.Programme;
        }

        private void btnClearStudent_Click(object sender, EventArgs e)
        {
            txtStudentId.Clear();
            txtFullName.Clear();
            txtEmail.Clear();
            txtAge.Clear();
            cmbProgramme.SelectedIndex = -1;
            txtStudentOutput.Clear();
            txtStudentId.Focus();
        }

        //Add Back button to return to dashboard
        private void btnBackToDashboard_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
