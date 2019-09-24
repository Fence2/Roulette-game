using System;
using System.Drawing;
using System.Windows.Forms;

namespace roulette_helper
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        double bet, balance, koef;
        int black, red, green, roulette_num, lose_count, save_count;

        private void Form1_Load(object sender, EventArgs e)
        {
            black = green = red = 0;
            save_count = Convert.ToInt32(textBox3.Text);
            textBox5_TextChanged(sender, e);

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled)
            {
                timer1.Enabled = false;
                button3.Text = "Старт";
                button2.Enabled = true;
            }
            else
            {
                black = green = red = 0;
                textBox2.Text = "";
                timer1.Enabled = true;
                button3.Text = "Стоп";
                button2.Enabled = false;
            }
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            textBox1_TextChanged(sender, e);
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            if (double.TryParse(textBox5.Text, out koef))
            {
                try
                {
                    koef = Convert.ToDouble(textBox5.Text);
                }
                catch (Exception)
                {
                    label8.ForeColor = Color.Red;
                }
            }
        }

        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {
            textBox1_TextChanged(sender, e);
        }

        private void bets8()
        {
            bet = Math.Round((balance * 0.00098368), 0);
            textBox4.Text = bet.ToString();
        }

        private void bets10()
        {
            bet = Math.Round((balance * 0.00015729), 0);
            textBox4.Text = bet.ToString();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (radioButton5.Checked)
                button2_Click(sender, e);
            else
            {
                button2_Click(sender, e);

                //Изменение ставки
                if (label9.BackColor == Color.Red)
                {
                    if (radioButton1.Checked)
                    {
                        textBox2.Text = "";
                        radioButton2.Checked = true;
                        if (radioButton7.Checked)
                            bets8();
                        else
                            bets10();
                        lose_count = 0;
                    }
                    else
                    {
                        lose_count++;
                        if (lose_count <= save_count)
                        {
                            bet = Math.Round((Convert.ToDouble(textBox4.Text) * koef), 0);
                            if (balance < bet)
                            {
                                if (radioButton7.Checked)
                                    bets8();
                                else
                                    bets10();
                            }
                            else
                                textBox4.Text = bet.ToString();
                        }
                        else
                            radioButton2.Checked = true;
                    }
                }
                else if (label9.BackColor == Color.Black)
                {
                    if (radioButton2.Checked)
                    {
                        textBox2.Text = "";
                        radioButton1.Checked = true;
                        if (radioButton7.Checked)
                            bets8();
                        else
                            bets10();
                        lose_count = 0;
                    }
                    else
                    {
                        lose_count++;
                        if (lose_count <= save_count)
                        {
                            bet = Math.Round((Convert.ToDouble(textBox4.Text) * koef), 0);
                            if (balance < bet)
                            {
                                if (radioButton7.Checked)
                                    bets8();
                                else
                                    bets10();
                            }
                            else
                                textBox4.Text = bet.ToString();
                        }
                        else
                            radioButton1.Checked = true;
                    }
                }
            }
            if (bet == 0)
                button3_Click(sender, e);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (double.TryParse(textBox1.Text, out balance))
            {
                balance = Convert.ToInt32(textBox1.Text);
                if (radioButton4.Checked && timer1.Enabled == false)
                {
                    if (radioButton7.Checked)
                        bets8();
                    else
                        bets10();
                }
            }
        }

        Random rnd = new Random();
        private void button2_Click(object sender, EventArgs e)
        {
            if (double.TryParse(textBox4.Text, out bet))
                bet = Convert.ToInt32(textBox4.Text);
            else
                bet = 0;

            if (balance < bet)
            {
                button2.Text = "ERROR";
                button2.BackColor = Color.Red;
                button3_Click(sender, e);
            }
            else
            {
                button2.Text = "крутить";
                button2.BackColor = Color.DodgerBlue;

                roulette_num = rnd.Next(0, 37);
                if (roulette_num == 0)
                    label9.BackColor = Color.Green;
                else if (roulette_num % 2 == 0)
                    label9.BackColor = Color.Black;
                else
                    label9.BackColor = Color.Red;

                if (roulette_num == 0)
                {
                    green++;
                    label15.Text = green.ToString();
                }
                else if (roulette_num % 2 == 0)
                {
                    black++;
                    label14.Text = black.ToString();
                }
                else
                {
                    red++;
                    label13.Text = red.ToString();
                }

                label9.Text = roulette_num.ToString();

                if (radioButton1.Checked)
                {
                    if (label9.BackColor == Color.Red)
                    {
                        balance += bet;
                        textBox2.Text += "ПОБЕДА КРАСНОЕ = +" + bet + Environment.NewLine;
                    }
                    else
                    {
                        balance -= bet;
                        textBox2.Text += "ПОРАЖЕНИЕ КРАСНОЕ = -" + bet + Environment.NewLine;
                    }
                }
                if (radioButton2.Checked)
                {
                    if (label9.BackColor == Color.Black)
                    {
                        balance += bet;
                        textBox2.Text += "ПОБЕДА ЧЁРНОЕ = +" + bet + Environment.NewLine;
                    }
                    else
                    {
                        balance -= bet;
                        textBox2.Text += "ПОРАЖЕНИЕ ЧЁРНОЕ = -" + bet + Environment.NewLine;
                    }
                }
                if (radioButton3.Checked)
                {
                    if (label9.BackColor == Color.Green)
                        balance += bet * 35;
                    else
                        balance -= bet;
                }
                textBox1.Text = balance.ToString();
            }
        }
    }
}
