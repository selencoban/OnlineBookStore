﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OnlineBookStore
{
    public partial class Payment : Form
    {
        public Payment()
        {
            InitializeComponent();
        }
        public  static bool payment = false;
        public static bool rtnpay()
        {
            return payment;
        }
        private void Payment_Load(object sender, EventArgs e)
        {
            label7.Text = " " + Home.shopCart.PaymentAmount;
            foreach (var item in Home.shopCart.ItemsToPurchase)
            {
                PaymentPanel panel = new PaymentPanel();
                panel.create(item);
                flowLayoutPanel1.Controls.Add(panel);
            }
            txtcrediname.Visible = false;
            txtccv.Visible = false;
            txtcardnumber.Visible = false;
            cmbmonth.Visible = false;
            cmbyear.Visible = false;
            namee.Visible = false;
            number.Visible = false;
            ccvv.Visible = false;
        }

        private void cmbPayment_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbPayment.Visible = true;
            switch (cmbPayment.SelectedIndex)
            {
                case 0:
                    Home.shopCart.PaymentType = "Cash";
                    namee.Visible = false;
                    number.Visible = false;
                    ccvv.Visible = false;
                    txtcrediname.Visible = false;
                    txtccv.Visible = false;
                    txtcardnumber.Visible = false;
                    cmbmonth.Visible = false;
                    cmbyear.Visible = false;
                    break;
                case 1:
                    Home.shopCart.PaymentType = "Credit Card";
                    txtcrediname.Visible = true;
                    txtccv.Visible = true;
                    txtcardnumber.Visible = true;
                    cmbmonth.Visible = true;
                    cmbyear.Visible = true;
                    namee.Visible = true;
                    number.Visible = true;
                    ccvv.Visible = true;
                    break;
            }
        }

        private void btnBuy_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtAdress.Text))
            {
                MessageBox.Show("You cannot complete your shopping without adress!");
            }
            else if (rdbBuKoli.Checked == false && rdbYurtici.Checked == false && rdbUps.Checked == false)
            {
                MessageBox.Show("You must select cargo company.");
            }
            else if (cmbPayment.SelectedIndex == -1)
            {
                MessageBox.Show("You should select payment type");
            }
            else if (cmbPayment.SelectedIndex == 1)
            {
                if (string.IsNullOrWhiteSpace(txtcrediname.Text) || string.IsNullOrWhiteSpace(txtcardnumber.Text) || string.IsNullOrWhiteSpace(txtccv.Text) || cmbyear.SelectedIndex == -1 || cmbmonth.SelectedIndex == -1)
                {
                    MessageBox.Show("Please, be sure credit card infos write correctly");
                }

                else
                {
                    payment = true;
                    MessageBox.Show("Payment is completed. Thank you for choosing us.");
                    Profile profile = new Profile();
                    Home.shopCart.ItemsToPurchase.Clear();
                    flowLayoutPanel1.Controls.Clear();
                    this.Hide();
                    profile.ShowDialog();
                    rtnpay();
                }
            }
            else
            {
                payment = true;
                MessageBox.Show("Payment is completed. Thank you for choosing us.");
                Profile profile = new Profile();
                Home.shopCart.ItemsToPurchase.Clear();
                flowLayoutPanel1.Controls.Clear();
                this.Hide();
                profile.ShowDialog();
                rtnpay();
            }
            string buttonname = "Buy";
            SaveLog.Savelog(buttonname);
        }

        private void txtccv_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtcardnumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtcrediname_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
                        && !char.IsSeparator(e.KeyChar);
        }
    }
}
