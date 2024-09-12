using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace ön_muhasebe
{
    public partial class Form1 : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader rdr;
        SqlDataAdapter da;
        public Form1()
        {
            InitializeComponent();
        }
        string constring = "data source=DESKTOP-NN5BI6E\\SQLEXPRESS; initial catalog= OnMuhasebeD2;Integrated Security=True ";

        void incomeRecord_call()
        {
            con = new SqlConnection(constring);
            con.Open();
            da = new SqlDataAdapter("SELECT * FROM gelirkayitD2", con);
            DataTable table = new DataTable();
            da.Fill(table);
            dg_gelir.DataSource = table;
            con.Close();
        }
        void expenseRecord_call()
        {
            con = new SqlConnection(constring);
            con.Open();
            da = new SqlDataAdapter("SELECT * FROM giderkayitD2", con);
            DataTable table = new DataTable();
            da.Fill(table);
            dg_gider.DataSource = table;
            con.Close();
        }
        void totalRecord_call()
        {
            con = new SqlConnection(constring);
            con.Open();
            da = new SqlDataAdapter("SELECT * FROM netD2", con);
            DataTable table = new DataTable();
            da.Fill(table);
            dg_net.DataSource = table;
            con.Close();
        }
        private void btn_glr_ek_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(constring);
            con.Open();

            DateTime selectedtime = dateTimePicker1.Value;

            string Query = "INSERT INTO gelirkayitD2(ad, miktar, deger , tarih)VALUES (@name,@quantity,@price,@date)";
            cmd = new SqlCommand(Query, con);
            int price = Convert.ToInt32(txt_price.Text);
            cmd.Parameters.AddWithValue("@name", txt_name.Text);
            cmd.Parameters.AddWithValue("@quantity", txt_quantity.Text);
            cmd.Parameters.AddWithValue("@price", price);
            cmd.Parameters.AddWithValue("@date", selectedtime);
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("başarıyla kaydedildi");
            incomeRecord_call();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void btn_gdr_ek_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(constring);
            con.Open();
            DateTime selectedtime = dateTimePicker1.Value;

            string Query = "INSERT INTO giderkayitD2(ad, miktar, deger , tarih)VALUES (@name,@quantity,@price,@date)";
            cmd = new SqlCommand(Query, con);
            int price = Convert.ToInt32(txt_price.Text);
            cmd.Parameters.AddWithValue("@name", txt_name.Text);
            cmd.Parameters.AddWithValue("@quantity", txt_quantity.Text);
            cmd.Parameters.AddWithValue("@price", price);
            cmd.Parameters.AddWithValue("@date", selectedtime);
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("başarıyla kaydedildi");
            expenseRecord_call();


        }

        private void Form1_Load(object sender, EventArgs e)
        {
            totalRecord_call();
            expenseRecord_call();
            Sum();
            Timer timer = new Timer();
            timer.Interval = 2000;
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void dg_gelir_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            txt_db_glr_name.Text = dg_gelir.CurrentRow.Cells[1].Value.ToString();
            txt_db_glr_quantity.Text = dg_gelir.CurrentRow.Cells[2].Value.ToString();
            txt_db_glr_price.Text = dg_gelir.CurrentRow.Cells[3].Value.ToString();
            dt_db_glr_date.Text = dg_gelir.CurrentRow.Cells[4].Value.ToString();
        }

        private void dg_gider_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            txt_db_gdr_name.Text = dg_gider.CurrentRow.Cells[1].Value.ToString();
            txt_db_gdr_quantity.Text = dg_gider.CurrentRow.Cells[2].Value.ToString();
            txt_db_gdr_price.Text = dg_gider.CurrentRow.Cells[3].Value.ToString();
            dt_db_gdr_date.Text = dg_gider.CurrentRow.Cells[4].Value.ToString();

        }

        private void button1_Click(object sender, EventArgs e)
        {

            con = new SqlConnection(constring);
            string Query = "DELETE FROM gelirkayitD2 WHERE id=@ID";
            cmd = new SqlCommand(Query, con);
            int id = Convert.ToInt32(dg_gelir.SelectedRows[0].Cells["id"].Value);
            cmd.Parameters.AddWithValue("@ID", id);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            incomeRecord_call();
        }

        private void btn_db_glr_gnc_Click(object sender, EventArgs e)
        {
            con = new SqlConnection(constring);
            string Query = "UPDATE gelirkayitD2 SET ad=@name , miktar=@quantity , deger=@price , tarih=@date WHERE id=@id ";
            cmd = new SqlCommand(Query, con);
            DateTime selectedtime = dt_db_glr_date.Value;
            int id = Convert.ToInt32(dg_gelir.SelectedRows[0].Cells["id"].Value);
            int price = Convert.ToInt32(txt_db_glr_price.Text);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@name", txt_db_glr_name.Text);
            cmd.Parameters.AddWithValue("@quantity", txt_db_glr_quantity.Text);
            cmd.Parameters.AddWithValue("@price", price);
            cmd.Parameters.AddWithValue("@date", selectedtime);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            incomeRecord_call();
        }

        private void btn_db_gdr_sil_Click(object sender, EventArgs e)
        {
            con = new SqlConnection(constring);
            string Query = "DELETE FROM giderkayitD2 WHERE id=@ID";
            cmd = new SqlCommand(Query, con);
            int id = Convert.ToInt32(dg_gider.SelectedRows[0].Cells["id"].Value);
            cmd.Parameters.AddWithValue("@ID", id);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            expenseRecord_call();
        }

        private void btn_db_gdr_gnc_Click(object sender, EventArgs e)
        {
            con = new SqlConnection(constring);
            string Query = "UPDATE giderkayitD2 SET ad=@name , miktar=@quantity , deger=@price , tarih=@date WHERE id=@id ";
            cmd = new SqlCommand(Query, con);
            DateTime selectedtime = dt_db_gdr_date.Value;
            int id = Convert.ToInt32(dg_gider.SelectedRows[0].Cells["id"].Value);
            int price = Convert.ToInt32(txt_db_glr_price.Text);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@name", txt_db_gdr_name.Text);
            cmd.Parameters.AddWithValue("@quantity", txt_db_gdr_quantity.Text);
            cmd.Parameters.AddWithValue("@price", price);
            cmd.Parameters.AddWithValue("@date", selectedtime);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            expenseRecord_call();
        }

        private void label16_Click(object sender, EventArgs e)
        {

        }
        private void Sum()
        {
            con = new SqlConnection(constring);
            int totalincome = 0;
            int totalexpense = 0;
            con.Open();
            cmd = new SqlCommand("SELECT SUM(deger) toplamGelir from gelirkayitD2", con);

            totalincome = (int)cmd.ExecuteScalar();

            SqlCommand cmd2 = new SqlCommand("SELECT SUM(deger) toplamGider from giderkayitD2", con);

            totalexpense = (int)cmd2.ExecuteScalar();
            int total = totalincome - totalexpense;
            lb_toplam_gelir.Text = Convert.ToString(totalincome);
            lb_toplam_gider.Text = Convert.ToString(totalexpense);
            lb_net.Text = Convert.ToString(total);
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            Sum();
        }
        private void datesum()
        {
            con = new SqlConnection(constring);
            DateTime startdate = dt_net1.Value;
            DateTime enddate = dt_net2.Value;
            int totalincome = 0;
            int totalexpense = 0;
            con.Open();
            cmd = new SqlCommand("SELECT SUM(deger) toplamGelir from gelirkayitD2 WHERE tarih BETWEEN @startdate AND @enddate", con);
            cmd.Parameters.AddWithValue("@startdate", startdate);
            cmd.Parameters.AddWithValue("@enddate", enddate);
            totalincome = (int)cmd.ExecuteScalar();

            SqlCommand cmd2 = new SqlCommand("SELECT SUM(deger) toplamGider from giderkayitD2 WHERE tarih BETWEEN @startdate AND @enddate", con);
            cmd2.Parameters.AddWithValue("@startdate", startdate);
            cmd2.Parameters.AddWithValue("@enddate", enddate);
            totalexpense = (int)cmd2.ExecuteScalar();

            int total = totalincome - totalexpense;
            SqlCommand cmd3 = new SqlCommand("INSERT INTO netD2 (SorguTarihi, ToplamGelir, ToplamGider, Net) VALUES (@Date, @Totalincome, @Totalexpense, @Total)", con);
            cmd3.Parameters.AddWithValue("@Date", DateTime.Now);
            cmd3.Parameters.AddWithValue("@Totalincome", totalincome);
            cmd3.Parameters.AddWithValue("@Totalexpense", totalexpense);
            cmd3.Parameters.AddWithValue("@Total", total);
            cmd3.ExecuteNonQuery();
            con.Close();
        }
        private void btn_gor_Click(object sender, EventArgs e)
        {
            datesum();
            totalRecord_call();
        }

        private void dg_gelir_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
