using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        int state;

        Int32 K1, K2, R, W;

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
        char[] GetChars()

        {

            return textBox1.Text.ToCharArray();

        }

        void SetChars(char[] c)

        {

            textBox1.Clear();

            textBox1.Text = new String(c);

        }

        public Form1()
        {
            InitializeComponent();
            state = 1;

            K1 = 245689;

            K2 = 985214;

            R = 128;

            W = 4;
        }

        Int32 h(int r)

        {

            return ((K1 << r) ^ (K2 >> r));

        }

        Int32 Vi(int X1, int r)

        {

            return X1 ^ h(r);

        }

        Int32 F(int x1, Int32 Vir)//F(Vi)

        {

            return x1 + Vir;

        }

        

        int[] raunder(int[] xn)

        {

            int[] old = new int[xn.Length];



            for (int i = 0; i < R; i++)

            {

                xn.CopyTo(old, 0);

                xn[0] = old[1] ^ F(old[0], Vi(old[0], i));

                for (int n = 1; n < xn.Length; n++)

                    xn[n] = old[(n + 1) % (xn.Length)];

            }

            return xn;

        }

        int[] deraunder(int[] xn)

        {

            int[] old = new int[xn.Length];

            for (int i = (R - 1); i > (-1); i--)

            {

                xn.CopyTo(old, 0);

                xn[1] = old[0] ^ F(old[xn.Length - 1], Vi(old[xn.Length - 1], i));

                for (int n = 1; n < xn.Length; n++)

                    xn[(n + 1) % (xn.Length)] = old[n];

            }

            return xn;

        }

        char[] crypt(char[] msg, bool f)

        {

            int i = 0;



            char[] res = new char[msg.Length];

            if (msg.Length >= W)

                for (i = 0; i < (msg.Length - W + 1); i += W)

                {

                    int[] tmp = new int[W];

                    for (int n = 0; n < W; n++) tmp[n] = msg[i + n];

                    if (f) tmp = raunder(tmp);

                    else tmp = deraunder(tmp);

                    for (int n = 0; n < W; n++) res[i + n] = (char)tmp[n];

                }

            for (int n = 0; n < (msg.Length % W); n++) res[i + n] = (char)msg[i + n];

            return res;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            switch (state)

            {

                case 1://Зашифровать

                    {

                        label1.Text = "Криптограмма";

                        button1.Text = "Расшифровать";

                        SetChars(crypt(GetChars(), true));

                        state = 2;

                        break;

                    }

                case 2://Расшифровать

                    {

                        label1.Text = "Исходный текст";

                        button1.Text = "Зашифровать";

                        SetChars(crypt(GetChars(), false));

                        state = 1;

                        break;

                    }

            }

        }
    }
    
}
