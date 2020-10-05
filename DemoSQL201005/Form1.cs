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
        public Form1()
        {
            InitializeComponent();

            //
            //Hämta data från DB och visa upp i listboxen
            //Hmm....
            //Skapa en anslutning/connection till DB
            //Ställa en SQL fråga till DB och tabellen Customer
            //Hmm, hur tar vi emot Data från DB??
            //Mata in svaret till ListBoxen 

            //Anslut till DB
            SqlConnection myDBConnection = new SqlConnection();
            myDBConnection.ConnectionString = @"Data Source=(LocalDb)\MSSQLLocalDB;Initial Catalog=Halloween;Integrated Security=True";
            myDBConnection.Open();

            MessageBox.Show("Connection OK");

            SqlCommand mySQLCommand = new SqlCommand();
            mySQLCommand.Connection = myDBConnection;
            mySQLCommand.CommandText = "SELECT * FROM Customer";
            //int i=mySQLCommand.ExecuteNonQuery();
            //MessageBox.Show("Rows: " + i);

            //Hmm, vi behöver DataAdapter och DataSet...tydligen
            SqlDataAdapter mySQLDataAdapter = new SqlDataAdapter("SELECT * FROM Customer", myDBConnection);
            DataSet myData = new DataSet();
            mySQLDataAdapter.Fill(myData);

            MessageBox.Show("Test OK " + myData.Tables[0].Rows.Count);

            //Yay, nu blev det ju enkelt igen, eller hur! Array och foreach!
            foreach (DataRow item in myData.Tables[0].Rows)
            {
                listBox1.Items.Add(item[1]);
            }

            //DEMO DEMO, obs tänk på att variablerna ska vara Form scope
            //därför kan jag nu inte använda button1 nedan
            DataRow myRow = myData.Tables[0].NewRow();
            myRow[1] = "Kajsa";
            myRow[2] = "Anka";
            myData.Tables[0].Rows.Add(myRow);

            MessageBox.Show("Antal rader; " + myData.Tables[0].Rows.Count);

            //Hur kan vi uppdatera DB?
            SqlCommandBuilder myCommandBuilder = new SqlCommandBuilder(mySQLDataAdapter);
            mySQLDataAdapter.Update(myData);

            MessageBox.Show("DB Updated");

            //CRUD
            //CREATE - INSERT INTO / http post
            //READ - SELECT / http get
            //UPDATE - UPDATE / http put
            //DELETE - DELETE / http delete

            //CommandBuilder skapar automatiskt de fyra SQL kommandona som behövs för CRUD
            //DataSet kan ju innehålla nya rader, uppdaterade och borttagna, det är nu automatiskt

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Demo lägga till en ny Customer
            //Prata med den lokala DB, dvs DataSet
            //Skapa en ny rad och lägg till värden för fälten
            //Obs, första fältet, ID är fortfarande automatiskt
            //Detta är endast lokalt, hur ska vi uppdatera vår DB?

        }
    }
}
