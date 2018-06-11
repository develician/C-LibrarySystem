using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibrarySystem
{
    public partial class Form3 : Form
    {
        public static CurrencyManager currencyManagerUser;
        public Form3()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            InitializeComponent();
            currencyManagerUser = (CurrencyManager)this.BindingContext[DataManager.Users];
            dataGridView1.DataSource = DataManager.Users;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            

            if(textBox1.Text.Trim() == "")
            {
                MessageBox.Show("사용자 Id를 입력해주세요.");
                return;
            }

            if(textBox2.Text.Trim() == "")
            {
                MessageBox.Show("사용자 이름을 입력해주세요.");
                return;
            }

            if(textBox3.Text.Trim() == "")
            {
                MessageBox.Show("전화번호를 입력해주세요.");
                return;
            }



            int id;
            bool isIdParsed = int.TryParse(textBox1.Text, out id);

            string name = textBox2.Text;

            int phoneNumber;
            bool isPhoneParsed = int.TryParse(textBox3.Text, out phoneNumber);

            if(!isPhoneParsed)
            {
                MessageBox.Show("전화번호는 - 없이 숫자만 입력해주세요.");
                return;
            }

            try
            {
                if(DataManager.Users.Exists((x) => x.Id == id))
                {
                    MessageBox.Show("이미 존재하는 사용자 Id 입니다.");
                    return;
                }

                User user = new User()
                {
                    Id = id,
                    Name = name,
                    Phone = textBox3.Text
                };

                DataManager.Users.Add(user);

                DataManager.Save();

                dataGridView1.DataSource = null;
                dataGridView1.DataSource = DataManager.Users;

                Form1.dataGridView2.DataSource = null;
                Form1.dataGridView2.DataSource = DataManager.Users;

                currencyManagerUser.Refresh();
                Form1.currencyManagerUser.Refresh();


                textBox1.Text = "";
                textBox2.Text = "";

            } catch(Exception exception)
            {

            }

        }

        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                User user = dataGridView1.CurrentRow.DataBoundItem as User;
                textBox1.Text = user.Id.ToString();
                textBox2.Text = user.Name;
            } catch(Exception exception)
            {

            }

            
        }

        private void button2_Click(object sender, EventArgs e)
        {

            if(textBox1.Text.Trim() == "")
            {
                MessageBox.Show("Id를 입력해주세요.");
                return;
            }

            if (textBox2.Text.Trim() == "")
            {
                MessageBox.Show("사용자 이름을 입력해주세요.");
                return;
            }

            int id;
            bool isParsed = int.TryParse(textBox1.Text, out id);
            if(!isParsed)
            {
                MessageBox.Show("Id는 숫자만 가능합니다.");
                return;
            }

            try
            {
                User user = DataManager.Users.Single((x) => x.Id == id);
                user.Name = textBox2.Text;

                dataGridView1.DataSource = null;
                dataGridView1.DataSource = DataManager.Users;

                Form1.dataGridView2.DataSource = null;
                Form1.dataGridView2.DataSource = DataManager.Users;

                DataManager.Save();

            } catch(Exception exception)
            {
                MessageBox.Show("없는 사용자입니다.");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            if (textBox1.Text.Trim() == "")
            {
                MessageBox.Show("Id를 입력해주세요.");
                return;
            }


            int id;

            bool isParsed = int.TryParse(textBox1.Text, out id);
            if (!isParsed)
            {
                MessageBox.Show("Id는 숫자만 가능합니다.");
                return;
            }


            try
            {
                User user = DataManager.Users.Single((x) => x.Id == id);
                DataManager.Users.Remove(user);

                dataGridView1.DataSource = null;
                dataGridView1.DataSource = DataManager.Users;

                Form1.dataGridView2.DataSource = null;
                Form1.dataGridView2.DataSource = DataManager.Users;

                DataManager.Save();

            } catch(Exception exception)
            {
                MessageBox.Show("없는 사용자입니다.");
            }
        }
    }
}
