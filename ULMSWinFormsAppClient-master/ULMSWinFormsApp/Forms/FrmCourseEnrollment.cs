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
    public partial class FrmCourseEnrollment : Form
    {
        public FrmCourseEnrollment()
        {
            InitializeComponent();
        }

        private void btnEnroll_Click(object sender, EventArgs e)
        {
            // BUG-006 FIX: No validation meant blank enrollments were saved and shown as successful
            // OLD (faulty): enrollment was created and saved immediately with no field checks
            if (string.IsNullOrWhiteSpace(txtEnrollStudentId.Text) ||
                string.IsNullOrWhiteSpace(txtEnrollStudentName.Text) ||
                string.IsNullOrWhiteSpace(cmbCourse.Text) ||
                string.IsNullOrWhiteSpace(cmbSemester.Text))
            {
                MessageBox.Show("Please fill in all fields before enrolling.");
                return;
            }

            Enrollment enrollment = new Enrollment
            {
                StudentId = txtEnrollStudentId.Text,
                StudentName = txtEnrollStudentName.Text,
                CourseName = cmbCourse.Text,
                Semester = cmbSemester.Text
            };

            txtEnrollmentOutput.Text =
                "Enrollment completed successfully!" + Environment.NewLine +
                "Student ID: " + enrollment.StudentId + Environment.NewLine +
                "Student Name: " + enrollment.StudentName + Environment.NewLine +
                "Course: " + enrollment.CourseName + Environment.NewLine +
                "Semester: " + enrollment.Semester;
        }

        private void btnClearEnrollment_Click(object sender, EventArgs e)
        {
            txtEnrollStudentId.Clear();
            txtEnrollStudentName.Clear();
            cmbCourse.SelectedIndex = -1;
            cmbSemester.SelectedIndex = -1;
            txtEnrollmentOutput.Clear();
            txtEnrollStudentId.Focus();
        }

        private void btnBackEnrollment_Click(object sender, EventArgs e)
        {
            this.Close();
        }



    }
}
