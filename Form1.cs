using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml.Linq;

namespace LibrarySystem
{
    public partial class Form1 : Form
    {
        public static CurrencyManager currencyManagerBook;
        public static CurrencyManager currencyManagerUser;
        public Form1()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            InitializeComponent();
            currencyManagerBook = (CurrencyManager)this.BindingContext[DataManager.Books];
            currencyManagerUser = (CurrencyManager)this.BindingContext[DataManager.Users];
            Text = "도서관 관리다!!";
            //MessageBox.Show(DataManager.Books.Count.ToString());


            label2.Text = DataManager.Books.Count.ToString();
            label6.Text = DataManager.Users.Count.ToString();
            label7.Text = DataManager.Books.Where((x) => x.isBorrowed).Count().ToString();
            label8.Text = DataManager.Books.Where((x) =>
            {
                return x.isBorrowed && x.BorrowedAt.AddDays(7) < DateTime.Now;
            }).Count().ToString();

            dataGridView1.ReadOnly = true;
            dataGridView2.ReadOnly = true;
            dataGridView1.DataSource = DataManager.Books;
            dataGridView2.DataSource = DataManager.Users;

            dataGridView1.Columns[1].HeaderText = "책 이름";
            dataGridView1.Columns[2].HeaderText = "출판사";
            dataGridView1.Columns[3].HeaderText = "총 페이지";
            dataGridView1.Columns[4].HeaderText = "대여자 번호";
            dataGridView1.Columns[5].HeaderText = "대여자";
            dataGridView1.Columns[6].HeaderText = "대출 여부";
            
            dataGridView1.Columns[7].HeaderText = "대출 날짜";

            dataGridView2.Columns[0].HeaderText = "회원 Id";
            dataGridView2.Columns[1].HeaderText = "회원 이름";
            dataGridView2.Columns[2].HeaderText = "연락처";
            dataGridView2.Columns[3].HeaderText = "대출수";

            dataGridView1.CurrentCellChanged += dataGridView1_CurrentCellChanged;
            dataGridView2.CurrentCellChanged += dataGridView2_CurrentCellChanged;

            //button1.Click += button1_Click;
            //button2.Click += button2_Click;
            button3.Click += button3_click;

            textBox4.KeyDown += search_keydown;

            button4.Click += initialize_book_dataSource;


            comboBox1.Items.Add("책이름");
            comboBox1.Items.Add("출판사");

            comboBox1.SelectedIndex = 0;
        }

        private void initialize_book_dataSource(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = DataManager.Books;
        }

        private void search_keydown(object sender, KeyEventArgs e)
        {
            //List<Book> searchedBookList = new List<Book>();
            if (e.KeyCode == Keys.Enter)
            {
                button3_click(sender, e);

            }


        }

        private void button3_click(object sender, EventArgs e)
        {
            string searchWord = textBox4.Text.ToString();
            List<Book> searchedBookList = new List<Book>();
            List<Book> originalBookList = DataManager.Books;
            try
            {

                if (comboBox1.SelectedIndex == 0)
                {
                    foreach (var item in originalBookList)
                    {
                        if (item.Name.ToString().EndsWith(searchWord) || item.Name.Contains(searchWord) || item.Name.ToString().StartsWith(searchWord))
                        {
                            searchedBookList.Add(item);
                        }
                    }
                }
                else if (comboBox1.SelectedIndex == 1)
                {
                    foreach (var item in originalBookList)
                    {
                        if (item.Publisher.ToString().EndsWith(searchWord) || item.Publisher.Contains(searchWord) || item.Publisher.ToString().StartsWith(searchWord))
                        {
                            searchedBookList.Add(item);
                        }
                    }
                }

                if(searchedBookList.Count == 0)
                {
                    MessageBox.Show("찾는 책이 없습니다.");
                    return;
                }
                
                dataGridView1.DataSource = null;
                dataGridView1.Rows.Clear();
                dataGridView1.Refresh();

                foreach (var item in searchedBookList)
                {
                    if(item.isBorrowed)
                    {
                        string[] row = { item.Isbn, item.Name, item.Publisher, item.Page.ToString(), item.UserId.ToString(), item.UserName.ToString(), item.isBorrowed.ToString(), item.BorrowedAt.ToString() };
                        dataGridView1.Rows.Add(row);
                    } else
                    {
                        string[] row = { item.Isbn, item.Name, item.Publisher, item.Page.ToString(), "", "", item.isBorrowed.ToString(), "" };
                        dataGridView1.Rows.Add(row);
                    }
                    
                }



                //DataGridViewRow dataGridViewRow = (DataGridViewRow)dataGridView1.Rows[0].Clone();
                //dataGridViewRow.Cells[0].Value = book.Isbn;
                //dataGridViewRow.Cells[1].Value = book.Name;
                //dataGridViewRow.Cells[2].Value = book.Publisher;
                //dataGridViewRow.Cells[3].Value = book.Page.ToString();

                //dataGridViewRow.Cells[6].Value = book.isBorrowed.ToString();
                //if(book.isBorrowed)
                //{
                //    dataGridViewRow.Cells[7].Value = book.BorrowedAt.ToString();
                //    dataGridViewRow.Cells[4].Value = book.UserId.ToString();
                //    dataGridViewRow.Cells[5].Value = book.UserName.ToString();
                //}

                //dataGridView1.Rows.Add(dataGridViewRow);

            } catch(Exception exception)
            {
                MessageBox.Show("찾는 책이 없습니다.");
            }

            
            //dataGridView1.Rows.Clear();
            //dataGridView1.Refresh();

            //try
            //{


            
            //    //dataGridView1.DataSource = book;
            //} catch(Exception exception)
            //{
            //    MessageBox.Show("찾는 책이 없습니다.");
            //}


            //dataGridView1.DataSource = null;
            //dataGridView1.DataSource = DataManager.Books;
            //DataManager.Save();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                Book book = dataGridView1.CurrentRow.DataBoundItem as Book;
                textBox1.Text = book.Isbn;
                textBox2.Text = book.Name;
            }
            catch (Exception exception)
            {

            }
        }

        private void dataGridView2_CurrentCellChanged(object sender, EventArgs e)
        {
           
            try
            {
                User user = dataGridView2.CurrentRow.DataBoundItem as User;
                textBox3.Text = user.Id.ToString();

            }
            catch (Exception exception)
            {

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() == "")
            {
                MessageBox.Show("Isbn을 입력해주세요");
                return;
            }

            if(textBox2.Text.Trim() == "")
            {
                MessageBox.Show("책 이름을 입력해주세요.");
                return;
            }

            if (textBox3.Text.Trim() == "")
            {
                MessageBox.Show("사용자 Id를 입력해주세요");
                return;
            }

            try
            {
                Book book = DataManager.Books.Single((x) => x.Isbn == textBox1.Text);
                if (book.isBorrowed)
                {
                    MessageBox.Show("이미 대여중인 도서입니다.");
                    return;
                }

                User user = DataManager.Users.Single((x) => x.Id.ToString() == textBox3.Text);

                if(user.borrowedNumber > 2)
                {
                    MessageBox.Show("최대 대여개수를 초과했습니다.");
                    return;
                }

                book.UserId = user.Id;
                book.UserName = user.Name;
                book.isBorrowed = true;
                book.BorrowedAt = DateTime.Now;
                user.borrowedNumber = user.borrowedNumber + 1;

                dataGridView1.DataSource = null;
                dataGridView1.DataSource = DataManager.Books;
                dataGridView2.DataSource = null;
                dataGridView2.DataSource = DataManager.Users;
                DataManager.Save();

                MessageBox.Show(book.Name + "도서가 " + user.Name + "님께 대여되었습니다.");
            }
            catch (Exception exception)
            {
                MessageBox.Show("존재하지 않는 도서 또는 사용자입니다.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(textBox2.Text.Trim() == "")
            {
                MessageBox.Show("Isbn을 입력해주세요.");
                return;
            }

            try
            {
                Book book = DataManager.Books.Single((x) => x.Isbn == textBox1.Text);
                if(book.isBorrowed)
                {
                    User user = DataManager.Users.Single((x) => x.Id.ToString() == book.UserId.ToString());
                    book.UserId = 0;
                    book.UserName = "";
                    book.isBorrowed = false;
                    book.BorrowedAt = new DateTime();
                    user.borrowedNumber = user.borrowedNumber - 1;

                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = DataManager.Books;
                    dataGridView2.DataSource = null;
                    dataGridView2.DataSource = DataManager.Users;
                    DataManager.Save();

                    if(book.BorrowedAt.AddDays(7) > DateTime.Now)
                    {
                        MessageBox.Show(book.Name + "도서가 연체상태로 반납되었습니다.");
                    } else
                    {
                        MessageBox.Show(book.Name + "도서가 반납되었습니다.");
                    }
                } else
                {
                    MessageBox.Show("대여 상태가 아닙니다.");
                    return;
                }
            } catch(Exception exception)
            {
                MessageBox.Show("존재하지 않는 도서 또는 사용자입니다.");
            }
        }

        private void 도서관리ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 form = new Form2();
            form.Show();

        }

        private void 사용자관리ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 form = new Form3();
            form.Show();
            
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }
    }
}
