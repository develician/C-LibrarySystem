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
    public partial class Form2 : Form
    {
        public static CurrencyManager currencyManagerBook;
        public Form2()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            InitializeComponent();
            currencyManagerBook = (CurrencyManager)this.BindingContext[DataManager.Books];

            dataGridView1.DataSource = DataManager.Books;
            

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if(DataManager.Books.Exists((x) => x.Isbn == textBox1.Text))
                {
                    MessageBox.Show("이미 존재하는 도서입니다.");
                    return;
                }

                Book book = new Book()
                {
                    Isbn = textBox1.Text,
                    Name = textBox2.Text,
                    Publisher = textBox3.Text,
                    Page = int.Parse(textBox4.Text)
                };
                DataManager.Books.Add(book);

                DataManager.Save();


                dataGridView1.DataSource = null;
                dataGridView1.DataSource = DataManager.Books;
                

                Form1.dataGridView1.DataSource = null;
                Form1.dataGridView1.DataSource = DataManager.Books;

                
                currencyManagerBook.Refresh();

                Form1.currencyManagerBook.Refresh();
                
               







            } catch(Exception exception)
            {

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Book book = DataManager.Books.Single((x) => x.Isbn == textBox1.Text);
                book.Name = textBox2.Text;
                book.Publisher = textBox3.Text;
                book.Page = int.Parse(textBox4.Text);

                dataGridView1.DataSource = null;
                dataGridView1.DataSource = DataManager.Books;

                Form1.dataGridView1.DataSource = null;
                Form1.dataGridView1.DataSource = DataManager.Books;

                DataManager.Save();

            } catch(Exception exception)
            {
                MessageBox.Show("존재하지 않는 도서입니다.");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                Book book = DataManager.Books.Single((x) => x.Isbn == textBox1.Text);
                DataManager.Books.Remove(book);

                dataGridView1.DataSource = null;
                dataGridView1.DataSource = DataManager.Books;

                Form1.dataGridView1.DataSource = null;
                Form1.dataGridView1.DataSource = DataManager.Books;


                DataManager.Save();


            } catch(Exception exception)
            {
                MessageBox.Show("존재하지 않는 도서입니다.");
            }
        }

        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                Book book = dataGridView1.CurrentRow.DataBoundItem as Book;
                textBox1.Text = book.Isbn;
                textBox2.Text = book.Name;
                textBox3.Text = book.Publisher;
                textBox4.Text = book.Page.ToString();
            }
            catch (Exception exception)
            {

            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }
    }
}
