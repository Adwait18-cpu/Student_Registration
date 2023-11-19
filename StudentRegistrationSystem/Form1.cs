using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace StudentRegistrationSystem
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Load();
        }

        SqlConnection con = new SqlConnection("Data Source=Ryzen; Initial Catalog=gcbd; User Id=sa; Password=123456789");
        SqlCommand cmd;
        SqlDataReader read;
        SqlDataAdapter drr;
        string id;
        bool Mode = true;
        string sql;


        public void Load()
        {
            try
            {
                sql = "Select * from student";
                cmd = new SqlCommand(sql, con);
                con.Open();

                read = cmd.ExecuteReader();
                drr = new SqlDataAdapter(sql , con);

                dataGridView1.Rows.Clear();

                while(read.Read())
                {
                    dataGridView1.Rows.Add(read[0], read[1], read[2], read[3]);
                }
                con.Close();

            }
            catch(Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        public void getID(string id)
        {
            sql = "Select * from student where id = '" + id + "'";

            cmd = new SqlCommand(sql, con);
            con.Open();
            read = cmd.ExecuteReader();

            while(read.Read())
            {
                txtName.Text = read[1].ToString();
                txtCourse.Text = read[2].ToString();
                txtFee.Text = read[3].ToString();
            }
            con.Close();
        }


        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string name = txtName.Text;
            string course = txtCourse.Text;
            string fee = txtFee.Text;

            if(Mode == true)
            {
                sql = "insert into student(stname, course, fee) values(@stname, @course, @fee)";
                con.Open();
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@stname", name);
                cmd.Parameters.AddWithValue("@course", course);
                cmd.Parameters.AddWithValue("@fee", fee);
                MessageBox.Show("Record Addedd Successfully");
                cmd.ExecuteNonQuery();

                txtName.Clear();
                txtCourse.Clear(); 
                txtFee.Clear(); 
                txtName.Focus();
            }
            else
            {
                id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                sql = "update student set stname = @stname, course = @course, fee = @fee where id = @id";
                con.Open();
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@stname", name);
                cmd.Parameters.AddWithValue("@course", course);
                cmd.Parameters.AddWithValue("@fee", fee);
                cmd.Parameters.AddWithValue("@id", id);
                MessageBox.Show("Record Updated Successfully");
                cmd.ExecuteNonQuery();

                txtName.Clear();
                txtCourse.Clear();
                txtFee.Clear();
                txtName.Focus();
                button1.Text = "Save";
                Mode = true;
            }
            con.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex == dataGridView1.Columns["Edit"].Index && e.RowIndex >= 0)
            {
                Mode = false;
                id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                getID(id);
                button1.Text = "Edit";
            }
            else if (e.ColumnIndex == dataGridView1.Columns["Edit"].Index && e.RowIndex >= 0)
            {
                Mode = false;
                id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                sql = "Delete from student where id =  @id";
                con.Open();
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record Deleted");
                con.Close();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Load();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtName.Clear();
            txtCourse.Clear();
            txtFee.Clear();
            txtName.Focus();
            button1.Text = "Save";
            Mode = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string id = txtID.Text;
            Mode = false;

            sql = "Delete from student where id =  @id";
            con.Open();
            cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Record Deleted");
            con.Close();

        }
    }
}
