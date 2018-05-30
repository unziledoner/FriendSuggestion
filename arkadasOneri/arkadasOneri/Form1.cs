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
using System.Collections;


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
            string[] arkadasOlanlar = new string[10];//girilen numaraya göre olusturulan arkadasların listesinin tutulacağı list
            string[] arkadaslar1 = new string[18];
            string[] arkadasOlmayanlarinBilgisi = new string[95];
            for (int i = 0; i < 9; i++)
            {
                arkadasOlanlar[i] = "-";
            }



            string arkadaslar = "Select * From arkadaslar Where  (OgrNo=" + textBox1.Text + ")"; //girilen numaranın arkadaslarını cekecek sql sorgusu

            SqlCommand komut = new SqlCommand(arkadaslar, baglanti); //sql sorgusunun veritabanı ile bağlantısını sağladm
            SqlDataReader dr = komut.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    //DataReader'daki verileri arkadasolanlar adındaki listte attım

                    arkadasOlanlar[0] = dr["ark1"].ToString();
                    arkadasOlanlar[1] = dr["ark2"].ToString();
                    arkadasOlanlar[2] = dr["ark3"].ToString();
                    arkadasOlanlar[3] = dr["ark4"].ToString();
                    arkadasOlanlar[4] = dr["ark5"].ToString();
                    arkadasOlanlar[5] = dr["ark6"].ToString();
                    arkadasOlanlar[6] = dr["ark7"].ToString();
                    arkadasOlanlar[7] = dr["ark8"].ToString();
                    arkadasOlanlar[8] = dr["ark9"].ToString();
                    arkadasOlanlar[9] = dr["ark10"].ToString();

                }

                
            }
        dr.Close();

            //arkadasların aktivitelerin bulundugu kısım
            DataTable dttt = new DataTable(); //verilerimi atacagım bir dataTable olusturdum
            List<string> ogrenci = new List<string>();

            foreach (string parca in arkadasOlanlar) // arkadaslarOlanlar listini gezinerek arkadaşların aktivitelerini getirilmesi
            {

                string ara = "Select * From aktivite Where (OgrNo=" + parca + ")"; //arkadasların öğrenci numaralarına göre aktivite bilgilerini cektim
                SqlCommand komut22 = new SqlCommand(ara, baglanti); //veritabanı ile sql sorgusu arasında baglantıyı kurdum
                SqlDataAdapter da1 = new SqlDataAdapter(komut22);
                da1.Fill(dttt); //dataTable içerisini sql sorgusu sonucu gelen verilerle doldurdum
                SqlDataReader dre1 = komut22.ExecuteReader();
                dre1.Close(); 

            }
           
            foreach(DataRow dre1 in dttt.Rows)
            {
                for(int i = 0; i < 17; i++)
                {
                    
                    if (i != 16)
                    {
                        ogrenci.Add(dre1[i].ToString());
                    }
                    else
                    {
                        ogrenci.Add("1");
                    }
                   
                }

            }  
   
            //arkadas olmayanların aktivite bulma kısmı
            string arkadasOlmayan = "SELECT * FROM liste WHERE not OgrNo=" + textBox1.Text + " AND " + " not OgrNo=" + arkadasOlanlar[0].ToString() + " AND " + "not OgrNo=" + arkadasOlanlar[1] + " AND " + "not OgrNo=" + arkadasOlanlar[2] + " AND " + "not OgrNo=" + arkadasOlanlar[3] + " AND " + "not OgrNo=" + arkadasOlanlar[4] + " AND " + "not OgrNo=" + arkadasOlanlar[5] + " AND " + "not OgrNo=" + arkadasOlanlar[6] + " AND " + "not OgrNo=" + arkadasOlanlar[7] + " AND " + "not OgrNo=" + arkadasOlanlar[8] + " AND " + "not OgrNo=" + arkadasOlanlar[9] + "";


            SqlCommand dene = new SqlCommand(arkadasOlmayan, baglanti);
            SqlDataReader drr = dene.ExecuteReader();
            
            int a = 0;
            if (drr.HasRows)
            {
                while (drr.Read())
                {

                    arkadasOlmayanlarinBilgisi[a] = drr["OgrNo"].ToString();
                    a++;
                }
            }
            drr.Close();

           

            List<string> ogrenci2 = new List<string>();
            DataTable data1 = new DataTable();

            for (int c = 0; c < 40; c++)
            // arkadaslarOlanlar listini gezinerek arkadaşların aktivitelerini getirilmesi
            {
               
                string arama1 = "Select * From aktivite Where (OgrNo=" + arkadasOlmayanlarinBilgisi[c] + ")"; //arkadasların öğrenci numaralarına göre aktivite bilgilerini cektim
                SqlCommand kmt2 = new SqlCommand(arama1, baglanti); //veritabanı ile sql sorgusu arasında baglantıyı kurdum
                SqlDataAdapter daa2 = new SqlDataAdapter(kmt2);
                daa2.Fill(data1); //dataTable içerisini sql sorgusu sonucu gelen verilerle doldurdum
                SqlDataReader dre2 = kmt2.ExecuteReader();
                dre2.Close();
            }
           
            foreach (DataRow dre2 in data1.Rows)
            {
                for (int i = 0; i < 17; i++)
                {

                    if (i != 16)
                    {
                        ogrenci2.Add(dre2[i].ToString());
                    }
                    else
                    {
                        ogrenci2.Add("0");
                    }
                   // MessageBox.Show(ogrenci2[i].ToString());
                }
                
            }
            
            


            ogrenci.AddRange(ogrenci2); //arkadas olanların devamına arkadas olmayanları ekledim
            
            dataGridView1.ColumnCount = 17;

            dataGridView1.Columns[0].Name = "id";
            dataGridView1.Columns[1].Name = "OgrNo";
            dataGridView1.Columns[2].Name = "aktv1";
            dataGridView1.Columns[3].Name = "aktv2";
            dataGridView1.Columns[4].Name = "aktv3";
            dataGridView1.Columns[5].Name = "aktv4";
            dataGridView1.Columns[6].Name = "aktv5";
            dataGridView1.Columns[7].Name = "aktv6";
            dataGridView1.Columns[8].Name = "aktv7";
            dataGridView1.Columns[9].Name = "aktv8";
            dataGridView1.Columns[10].Name = "aktv9";
            dataGridView1.Columns[11].Name = "aktv10";
            dataGridView1.Columns[12].Name = "aktv11";
            dataGridView1.Columns[13].Name = "aktv12";
            dataGridView1.Columns[14].Name = "aktv13";
            dataGridView1.Columns[15].Name = "aktv15";
            dataGridView1.Columns[16].Name = "etiket";

            for (int i = 0; i < ogrenci.Count; i+=17)
            {
                
                this.dataGridView1.Rows.Add(ogrenci[i], ogrenci[i+1], ogrenci[i+2], ogrenci[i+3], ogrenci[i+4], ogrenci[i+5], ogrenci[i+6], ogrenci[i+7], ogrenci[i+8], ogrenci[i+9], ogrenci[i+10], ogrenci[i+11], ogrenci[i+12], ogrenci[i+13], ogrenci[i+14], ogrenci[i+15], ogrenci[i+16]);
            }






            dataGridView2.ColumnCount = 17;

            dataGridView2.Columns[0].Name = "id";
            dataGridView2.Columns[1].Name = "OgrNo";
            dataGridView2.Columns[2].Name = "aktv1";
            dataGridView2.Columns[3].Name = "aktv2";
            dataGridView2.Columns[4].Name = "aktv3";
            dataGridView2.Columns[5].Name = "aktv4";
            dataGridView2.Columns[6].Name = "aktv5";
            dataGridView2.Columns[7].Name = "aktv6";
            dataGridView2.Columns[8].Name = "aktv7";
            dataGridView2.Columns[9].Name = "aktv8";
            dataGridView2.Columns[10].Name = "aktv9";
            dataGridView2.Columns[11].Name = "aktv10";
            dataGridView2.Columns[12].Name = "aktv11";
            dataGridView2.Columns[13].Name = "aktv12";
            dataGridView2.Columns[14].Name = "aktv13";
            dataGridView2.Columns[15].Name = "aktv15";
            dataGridView2.Columns[16].Name = "etiket";


            List<string> ogrenci3 = new List<string>();
            DataTable sondata = new DataTable();
            for (int k = 40; k < 80; k++)
            // arkadasOlmayanlar listini gezinerek arkadaşların aktivitelerini getirilmesi
            {

                string son= "Select * From aktivite Where (OgrNo=" + arkadasOlmayanlarinBilgisi[k-1] + ")"; //arkadasların öğrenci numaralarına göre aktivite bilgilerini cektim
                SqlCommand komutson = new SqlCommand(son, baglanti); //veritabanı ile sql sorgusu arasında baglantıyı kurdum
                SqlDataAdapter sa = new SqlDataAdapter(komutson);
                sa.Fill(sondata); //dataTable içerisini sql sorgusu sonucu gelen verilerle doldurdum
                SqlDataReader sd = komutson.ExecuteReader();
                sd.Close();
            }
            foreach (DataRow sd in sondata.Rows)
            {
                for (int i = 0; i < 17; i++)
                {

                    if (i != 16)
                    {
                        ogrenci3.Add(sd[i].ToString());
                    }
                    else
                    {
                        ogrenci3.Add("0");
                    }
                    
                }

            }
            for (int i = 0; i < ogrenci3.Count; i += 17)
            {

                this.dataGridView2.Rows.Add(ogrenci3[i], ogrenci3[i+1], ogrenci3[i+2], ogrenci3[i+3], ogrenci3[i+4], ogrenci3[i+5], ogrenci3[i+6], ogrenci3[i+7], ogrenci3[i+8], ogrenci3[i+9], ogrenci3[i+10], ogrenci3[i+11], ogrenci3[i+12], ogrenci3[i+13], ogrenci3[i+14], ogrenci3[i+15], ogrenci3[i+16]);
            }


            baglanti.Close();

        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e) //enter tusu ile arama yapabilmek icin
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1_Click(sender, e);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
    



