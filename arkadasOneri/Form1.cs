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


namespace arkadasOneri
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //veritabanı baglantı cumlecigi veritabanı adı=muhendislik 
        SqlConnection baglanti = new SqlConnection(@"server=UNZILE\SQLEXPRESS; Initial Catalog=muhendislik; Integrated Security=SSPI");
       
        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'muhendislikDataSet.aktivite' table. You can move, or remove it, as needed.
            this.aktiviteTableAdapter.Fill(this.muhendislikDataSet.aktivite);



        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open(); //veritabanı baglantısını actıkm
            List<string> arkadasOlanlar = new List<string>(); //girilen numaraya göre olusturulan arkadasların listesinin tutulacağı list
            string arkadaslar = "Select * From arkadas Where  (OgrNo=" + textBox1.Text + ")"; //girilen numaranın arkadaslarını cekecek sql sorgusu
            SqlCommand komut = new SqlCommand(arkadaslar, baglanti); //sql sorgusunun veritabanı ile bağlantısını sağladm
            SqlDataReader dr = komut.ExecuteReader();

            if (!dr.HasRows)
            {
                MessageBox.Show("Bu numara sistemde bulunmamaktadır");
            }
          
            while (dr.Read())
                {
                    //DataReader'daki verileri arkadasolanlar adındaki listte attım
                    arkadasOlanlar.Add(dr["ark1"].ToString());
                    arkadasOlanlar.Add(dr["ark2"].ToString());
                    arkadasOlanlar.Add(dr["ark3"].ToString());
                    arkadasOlanlar.Add(dr["ark4"].ToString());
                    arkadasOlanlar.Add(dr["ark5"].ToString());
                    arkadasOlanlar.Add(dr["ark6"].ToString());
                    arkadasOlanlar.Add(dr["ark7"].ToString());
                    arkadasOlanlar.Add(dr["ark8"].ToString());
                    arkadasOlanlar.Add(dr["ark9"].ToString());
                    arkadasOlanlar.Add(dr["ark10"].ToString());


                }


            dr.Close();
                DataTable dt = new DataTable(); //verilerimi atacagım bir dataTable olusturdum

                foreach (string parca in arkadasOlanlar) // arkadaslarOlanlar listini gezinerek arkadaşların aktivitelerini getirilmesi
                {

                    string ara = "Select * From aktivite Where (OgrNo=" + parca + ")"; //arkadasların öğrenci numaralarına göre aktivite bilgilerini cektim
                    SqlCommand komut2 = new SqlCommand(ara, baglanti); //veritabanı ile sql sorgusu arasında baglantıyı kurdum
                    SqlDataAdapter da = new SqlDataAdapter(komut2);
                    da.Fill(dt); //dataTable içerisini sql sorgusu sonucu gelen verilerle doldurdum

                }

           

            dataGridView1.DataSource = dt; //datatable icerigin gride atarak ekranda görülmesini sağladım.
            
            //string arkadasOlmayan = "SELECT * FROM arkadas WHERE OgrNo not in (" + textBox1.Text + arkadasOlanlar["ark1"]+")";
            //SqlCommand kmt = new SqlCommand(arkadasOlmayan, baglanti);
            //SqlDataAdapter daa = new SqlDataAdapter(kmt);
            //MessageBox.Show(arkadasOlmayan);
            //daa.Fill(dt);
            //dataGridView2.DataSource = dt;

            //List<string> arkadasOlmayanlarınIlkkismi = new List<string>();
            //int i = 0;
            //while(i != 40){
            //    foreach (string deneme in arkadasOlmayanlarınIlkkismi)
            //    {
            //        //DataReader'daki verileri parcalama a dındaki listte attım
            //        arkadasOlanlar.Add(dr["ark"].ToString());
            //        i++;
            //    }
            //}
            ////veri oldugu sürece arkadasların aktivitesini arasın
            //dr.Close();
            //DataTable dtt = new DataTable();

            //foreach (string parca in arkadasOlmayanlarınIlkkismi)
            //{

            //    string ara = "Select * From aktivite Where (OgrNo!=" + parca + ")";
            //    SqlCommand komut2 = new SqlCommand(ara, baglanti);
            //    SqlDataAdapter da = new SqlDataAdapter(komut2);
            //    da.Fill(dt);

            //}


            //dataGridView2.DataSource = dt;
























            //SqlDataAdapter da = new SqlDataAdapter(komut);
            //DataTable dt = new DataTable();

            //da.Fill(dt);
            //dataGridView1.DataSource = dt;
            baglanti.Close();
        }

       
    }
}
