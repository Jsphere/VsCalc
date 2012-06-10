using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
namespace CalcExample
{
    public partial class Form1 : Form
    {
        Button[] btnNumbers = new Button[11];
        Button[] btnOperators = new Button[5];
        Button btnClear = new Button();
        Button btnExit = new Button();
        Label lblStatus = new Label();
        StringBuilder[] strNumbers = new StringBuilder[2];
        bool onFirstNumber = false, signDone = false, secondDone = false, canOperate = false;
        string[] signs = { "+", "-", "x", "/", "=" };
        long num1, num2, result = 0;
        string sign;
        public Form1()
        {
            InitializeComponent();
            InitializeControls();
            strNumbers[0] = new StringBuilder();
            strNumbers[1] = new StringBuilder();
        }
        private void _Click(object sender, EventArgs e)
        {
            string buttonText = ((Button)sender).Text;
            try
            {
                DoWork(buttonText);
            }
            catch (OverflowException)
            {
                MessageBox.Show("Number is to large.");
                lblStatus.Text = "";
                Reset();
            }
            catch (DivideByZeroException)
            {
                MessageBox.Show("Cannot divide by 0.");
                lblStatus.Text = "";
                Reset();
            }
        }
        private void DoWork(string buttonText)
        {
            if (buttonText.ToLower() == "exit")
            {
                DialogResult result = MessageBox.Show("Are you sure you want to exit?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    Application.Exit();
                }
            }
            else if (buttonText.ToLower() == "clear")
            {
                lblStatus.Text = "";
                Reset();
                onFirstNumber = true;
            }
            else
            {
                if (Char.IsNumber(Convert.ToChar(buttonText)))
                {
                    if (onFirstNumber && !canOperate) { lblStatus.Text = ""; }
                    for (long i = 0; i < btnNumbers.Length; i++)
                    {
                        if (i.ToString() == buttonText)
                        {
                            if (onFirstNumber == true)
                            {
                                lblStatus.Text += buttonText;
                                canOperate = true;
                                strNumbers[0].Append(buttonText);
                                //MessageBox.Show(strNumbers[0].ToString());
                            }
                            else
                            {
                                if (signDone)
                                {
                                    lblStatus.Text += buttonText;
                                    canOperate = true;
                                    secondDone = true;
                                    strNumbers[1].Append(buttonText);
                                }
                            }
                        }
                    }
                }
                else
                {

                    for (long i = 0; i < btnOperators.Length; i++)
                    {
                        if (buttonText == signs[i] && !signDone && canOperate)
                        {
                            num1 = Convert.ToInt64(strNumbers[0].ToString());
                            sign = buttonText;
                            lblStatus.Text += " " + buttonText + " ";
                            signDone = true;
                            onFirstNumber = false;
                            canOperate = false;
                            break;
                        }
                        if (buttonText == signs[4])
                        {
                            if (secondDone && canOperate)
                            {

                                num2 = Convert.ToInt64(strNumbers[1].ToString());
                                PerformOperation(num1, num2, sign);
                                Reset();
                                break;
                            }
                        }
                    }
                }
            }
        }
        private void Reset()
        {
            strNumbers[0].Clear();
            strNumbers[1].Clear();
            signDone = false;
            onFirstNumber = true;
            secondDone = false;
            canOperate = false;
        }
        private void PerformOperation(long _num1, long num2, string _operator)
        {
            if (_operator == "+") { result = num1 + num2; }
            else if (_operator == "-") { result = num1 - num2; }
            else if (_operator == "x") { result = num1 * num2; }
            else if (_operator == "/") { result = num1 / num2; }
            lblStatus.Text += " = " + result.ToString();
        }
        private void InitializeControls()
        {
            // add number buttons
            Point[] points = new Point[11];
            long x = 0, y = 1;
            int[,] locations = { { 10, 10 }, { 30, 10 }, { 50, 10 }, { 70, 10 }, { 10, 30 }, { 30, 30 }, { 50, 30 }, { 70, 30 }, { 10, 50 }, { 30, 50 } };
            for (long i = 0; i < btnNumbers.Length - 1; i++)
            {
                btnNumbers[i] = new Button();
                btnNumbers[i].Text = i.ToString();
                points[i] = new Point(locations[i, x], locations[i, y]);
                btnNumbers[i].Location = points[i];
                btnNumbers[i].Size = new Size(20, 20);
                btnNumbers[i].Click += new EventHandler(_Click);
            }
            this.Controls.AddRange(btnNumbers);
            // add sign buttons
            Point[] points2 = new Point[5];
            int[,] locations2 = { { 50, 50 }, { 70, 50 }, { 50, 70 }, { 70, 70 }, {50, 90 } };
            
            for (long i = 0; i < btnOperators.Length; i++)
            {
                btnOperators[i] = new Button();
                points2[i] = new Point(locations2[i, x], locations2[i, y]);
                btnOperators[i].Location = points2[i];
                btnOperators[i].Size = new Size(20, 20);
                btnOperators[i].Text = signs[i];
                btnOperators[i].Click += new EventHandler(_Click);
            }
            // add additional buttons
            this.Controls.AddRange(btnOperators);
            btnClear.Location = new Point(10, 70);
            btnClear.Size = new Size(40, 20);
            btnClear.Text = "Clear";
            btnClear.Click += new EventHandler(_Click);
            this.Controls.Add(btnClear);
            btnExit.Location = new Point(10, 90);
            btnExit.Size = new Size(40, 20);
            onFirstNumber = true;
            btnExit.Text = "Exit";
            btnExit.Click += new EventHandler(_Click);
            this.Controls.Add(btnExit);

            //initialize label
            lblStatus.Location = new Point(2, 10);
            lblStatus.Size = new Size(80, 60);
            lblStatus.MaximumSize = new System.Drawing.Size(80, 60);
            lblStatus.AutoEllipsis = true;
            lblStatus.ForeColor = Color.DarkBlue;
            lblStatus.Text = "";
            this.grpBoxStatus.Controls.Add(lblStatus);
        }
    }
}
