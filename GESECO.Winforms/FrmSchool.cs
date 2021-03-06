﻿using _GESECO.BLL;
using _GESECO.BO;
using System;
using System.Configuration;
using System.IO;
using System.Windows.Forms;

namespace GESECO.Winforms
{
    public partial class FrmSchool : Form
    {
        private EcoleBLO ecoleBLO;
        private string picture; 
        public FrmSchool()
        {
            InitializeComponent();
            ecoleBLO = new EcoleBLO(ConfigurationManager.AppSettings["DbFolder"]);
            picture = pbLogo.ImageLocation;

            CreateFiliers();
            CreateSpecialite();
        }

        private void CreateSpecialite()
        {
            FiliereBLO filiereBLO = new FiliereBLO(ConfigurationManager.AppSettings["DbFolder"]);
            SpecialiteBLO specialiteBLO = new SpecialiteBLO(ConfigurationManager.AppSettings["DbFolder"]);

            Filiere filiere = new Filiere("TI01", "Tenchnologie de l'Information et de la Communication", "TIC", 900000, "Diplome : DEC Durrer 2Ans");
            Filiere filiere1 = new Filiere("BTS", "Bachelor", "BTS", 450000, "Diplome : BTS Durrer 2Ans");
            Filiere filiere2 = new Filiere("HND", "Higher National Degree ", "HND", 900000, "Diplome : HND Durrer 2Ans");

            filiereBLO.AddFiliere(filiere);
            filiereBLO.AddFiliere(filiere1);
            filiereBLO.AddFiliere(filiere2);

            Specialite specialite = new Specialite("S001","Programmation et Application Mobile","PAM",filiere);
            Specialite specialite1 = new Specialite("S002", "Reseau et Securite Informatique","RSI",filiere);
            Specialite specialite2 = new Specialite("S003", "Genie Logiciel","GL",filiere1);
            Specialite specialite3 = new Specialite("S004", "Comptabilite","COMP",filiere1);
            Specialite specialite4 = new Specialite("S005", "Software Engeneering","SE",filiere2);
            Specialite specialite5 = new Specialite("S006", "Accounting","ACC",filiere2);
            Specialite specialite6 = new Specialite("S007", "Marketing","MK", filiere1);
            Specialite specialite7 = new Specialite("S008", "Logistique","LG", filiere1);
            Specialite specialite8 = new Specialite("S009", "Computer Engeneering","CE", filiere2);
            Specialite specialite9 = new Specialite("S010", "Mecanic Engeneering","ME", filiere2);

            specialiteBLO.AddSpecialite(specialite);
            specialiteBLO.AddSpecialite(specialite1);
            specialiteBLO.AddSpecialite(specialite2);
            specialiteBLO.AddSpecialite(specialite3);
            specialiteBLO.AddSpecialite(specialite4);
            specialiteBLO.AddSpecialite(specialite5);
            specialiteBLO.AddSpecialite(specialite6);
            specialiteBLO.AddSpecialite(specialite7);
            specialiteBLO.AddSpecialite(specialite8);
            specialiteBLO.AddSpecialite(specialite9);

        }

        private void CreateFiliers()
        {
        }

        private void FrmSchool_Load(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Ecole ecole = new Ecole(txtID.Text,txtNom.Text,txtAbreger.Text,txtEmail.Text,txtAdresse.Text,long.Parse(txtContact.Text),File.ReadAllBytes(pbLogo.ImageLocation));
                ecoleBLO.AddEcole(ecole);

                MessageBox.Show("Save Done!", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtNom.Text = string.Empty;
                pbLogo.ImageLocation = picture;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void pbClose_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pbLogo.ImageLocation = picture;
        }

        private void pbLogo_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Choose a Picture";
            ofd.Filter = "Image files|*.jpg; *.jpeg; *.png; *.gif";

            if (ofd.ShowDialog() == DialogResult.OK)
                pbLogo.ImageLocation = ofd.FileName;
        }

        private void txtContact_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (!char.IsDigit(ch) && ch != 8)
                e.Handled = true;
        }
    }
}
