using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DemoSQL201005
{
    public partial class Form1 : Form
    {

        SqlConnection myDBConnection ;
        SqlCommand mySQLCommand;
        SqlDataAdapter mySQLDataAdapter;
        DataSet myData;

        public Form1()
        {
            InitializeComponent();

            button1.Text = "Add";
            button2.Text = "Save";

            myDBConnection = new SqlConnection();

            myDBConnection.ConnectionString = @"Data Source=(LocalDb)\MSSQLLocalDB;Initial Catalog=Halloween;Integrated Security=True";
            myDBConnection.Open();

            mySQLCommand = new SqlCommand();
            mySQLCommand.Connection = myDBConnection;
            mySQLCommand.CommandText = "SELECT * FROM Customer";
        
            mySQLDataAdapter = new SqlDataAdapter("SELECT * FROM Customer", myDBConnection);
            myData = new DataSet();
            mySQLDataAdapter.Fill(myData);

            Display();
        }

        private void Display()
        {
            listBox1.Items.Clear();
            foreach (DataRow item in myData.Tables[0].Rows)
            {
                listBox1.Items.Add(item[1]);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataRow myRow = myData.Tables[0].NewRow();
            myRow[1] = textBox1.Text;
            myRow[2] = textBox2.Text;
            myData.Tables[0].Rows.Add(myRow);
           
            Display();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlCommandBuilder myCommandBuilder = new SqlCommandBuilder(mySQLDataAdapter);
            mySQLDataAdapter.Update(myData);

            MessageBox.Show("DB Updated");
        }
    }
}
