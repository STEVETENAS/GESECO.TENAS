﻿using GESECO.Winforms.GESECO.FORMS;
using System;
using System.Windows.Forms;

namespace GESECO.Winforms
{
    public partial class FrmMenu : Form
    {
        public FrmMenu()
        {
            InitializeComponent();
        }

        private void btnInscription_Click(object sender, EventArgs e)
        {
            FrmInscription frm = new FrmInscription();
            OpenChildForm(frm);
        }
        private void OpenChildForm(Form childForm)
        {
            //pnlMenuBackground.Controls.Clear();
            childForm.TopLevel = false;
            childForm.AutoScroll = true;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            pnlMenuBackground.Controls.Add(childForm);
            pnlMenuBackground.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void btnAdminstration_Click(object sender, EventArgs e)
        {
        }

        private void btnCaisse_Click(object sender, EventArgs e)
        {
        }

        private void btnPayement_Click(object sender, EventArgs e)
        {
            FrmPaiement frm = new FrmPaiement();
            OpenChildForm(frm);
        }

        private void btnAuthentification_Click(object sender, EventArgs e)
        {
        }
    }
}
