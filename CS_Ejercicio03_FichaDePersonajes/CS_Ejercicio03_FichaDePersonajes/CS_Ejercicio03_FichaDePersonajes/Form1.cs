﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CS_Ejercicio03_FichaDePersonajes
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void clicCerrar(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboboxCambiado(object sender, EventArgs e)
        {
            if (combRaza.SelectedIndex == 0)
            {
                String[] personajes = { "Mago", "Nigromante" };
                combClase.Items.AddRange(personajes);
            }
            else
            {

            }
        }
    }
}
