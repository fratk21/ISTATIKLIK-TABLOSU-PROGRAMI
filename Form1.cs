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

namespace firat
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void dosyaSeçToolStripMenuItem_Click(object sender, EventArgs e)
        {

            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "Txt dosyası |*.txt";
            file.FilterIndex = 1;
            file.RestoreDirectory = true;
            file.CheckFileExists = false;
            file.Title = "TXT Dosyası Seçiniz..";

            if (file.ShowDialog() == DialogResult.OK)
            {
                // dosya seçildi ise
                string DosyaYolu = file.FileName;
                string DosyaAdi = file.SafeFileName;
                MessageBox.Show("Dosya başarıyla eklendi", "Bilgilendirme Penceresi");
                string dosyakonum;
                /* InitializeComponent();*/
                dosyakonum = DosyaYolu;
                FileStream fileStream = new FileStream(dosyakonum, FileMode.OpenOrCreate, FileAccess.Read);


                using (StreamReader reader = new StreamReader(fileStream))
                {

                    while (true)
                    {
                        string satir = reader.ReadLine();
                        string sayi = satir;
                        listBox1.Items.Add(sayi + "\n");
                        if (satir == null) break;
                    }

                    reader.Close();
                }
                fileStream.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (listBox1.Items.Count < 0)
            {
                MessageBox.Show("lütfen ilk verileri yükleyiniz");
            }
            else if (listBox1.Items.Count > 0)
            {
                string[] veriler1 = new string[listBox1.Items.Count];
                for (int i = 0; i < listBox1.Items.Count; i++)
                {
                    veriler1[i] = listBox1.Items[i].ToString();
                }
                listBox1.Items.Clear();
                Array.Sort(veriler1);

                listBox1.Items.AddRange(veriler1);
                listBox1.SelectedIndex = 0;

                listBox1.Items.Remove(listBox1.SelectedItem);

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (listBox1.Items.Count < 0)
            {
                MessageBox.Show("lütfen ilk verileri yükleyiniz");
            }
            else if (listBox1.Items.Count > 0)
            {
                /* TEKRAR EDEN SAYIYI BULMA */
                string sayi = "";
                int tekrarSayisi = 0;
                int geciciTekrar = 1;

                string[] veriler1 = new string[listBox1.Items.Count];
                for (int i = 0; i < listBox1.Items.Count; i++)
                {
                    veriler1[i] = listBox1.Items[i].ToString();
                }

                for (int i = 0; i < veriler1.Length - 1; i++)
                {
                    if (veriler1[i] == veriler1[i + 1])
                        geciciTekrar++;

                    if (i == (veriler1.Length - 2) || veriler1[i] != veriler1[i + 1])
                    {
                        if (geciciTekrar >= tekrarSayisi)
                        {
                            tekrarSayisi = geciciTekrar;

                            sayi = veriler1[i];



                        }
                        textBox9.Text = sayi;
                        geciciTekrar = 1;
                    }
                }



                int elemansay = listBox1.Items.Count;
                elemansay = elemansay - 1;
                string[] aradakisay = new string[listBox1.Items.Count];
                string enbüyüksayi = (string)listBox1.Items[elemansay];
                string enküçüksayi = (string)listBox1.Items[0];
                int sinif = int.Parse(textBox6.Text);
                double aralik = (Convert.ToDouble(enbüyüksayi) - Convert.ToDouble(enküçüksayi)) / sinif;
                textBox7.Text = Convert.ToString(Math.Round(Convert.ToDecimal(aralik), 2));

                /******************medyan************************/
                int medsay = listBox1.Items.Count;

                string[] msayı = new string[listBox1.Items.Count];
                for (int m = 0; m <= elemansay; m++)
                {
                    msayı[m] = (string)listBox1.Items[m];
                }
                if (medsay % 2 == 1)
                {
                    int tekliSira = (medsay + 1) / 2;
                    textBox10.Text = msayı[tekliSira - 1];
                    /*double buzak = (Convert.ToDouble(msayı[elemansay]) - Convert.ToDouble(textBox10.Text)) * (Convert.ToDouble(msayı[elemansay]) - Convert.ToDouble(textBox10.Text));
                    double kuzak = (Convert.ToDouble(msayı[0]) - Convert.ToDouble(textBox10.Text)) * (Convert.ToDouble(msayı[0]) - Convert.ToDouble(textBox10.Text));
                    double varyans = (buzak + kuzak) / elemansay;
                    if (varyans < 0)
                    {
                        varyans = varyans * -1;
                    }
                    textBox11.Text = varyans.ToString();*/
                }
                else
                {
                    double ikiliSira = medsay / 2.0;
                    double ortaSayi = (Convert.ToDouble(msayı[(int)ikiliSira - 1]) + Convert.ToDouble(msayı[(int)ikiliSira])) / 2.0;
                    textBox10.Text = Convert.ToString(ortaSayi);
                   /* double buzak = (Convert.ToDouble(msayı[elemansay]) - Convert.ToDouble(textBox10.Text)) * (Convert.ToDouble(msayı[elemansay]) - Convert.ToDouble(textBox10.Text));
                    double kuzak = (Convert.ToDouble(msayı[0]) - Convert.ToDouble(textBox10.Text)) * (Convert.ToDouble(msayı[0]) - Convert.ToDouble(textBox10.Text));
                    double varyans = (buzak + kuzak) / elemansay;
                    if (varyans < 0)
                    {
                        varyans = varyans * -1;
                    }
                    textBox11.Text = varyans.ToString();*/
                }




                /*******************ortalama*********************/
                double toplam = 0;
                for (int i = 0; i < listBox1.Items.Count; i++)
                {
                    toplam += Convert.ToDouble(listBox1.Items[i]);
                }
                textBox8.Text = Convert.ToString(Math.Round(Convert.ToDecimal(toplam / (elemansay+1)), 6));
               /* Convert.ToString(toplam / elemansay);*/

                /*******************s.sapma*********************/
                double[] sapma = new double[listBox1.Items.Count];
                double cıkarma = Convert.ToDouble(textBox8.Text);
                double cık = 0;
                for (int l = 0; l < listBox1.Items.Count; l++)
                {
                    cık = Convert.ToDouble(msayı[l]) - cıkarma;
                    sapma[l] = cık * cık;


                }

                double tsapma = 0;
                for (int o = 0; o < sapma.Length; o++)
                {
                    tsapma += sapma[o];
                }
                int uzunluk = 0;
                uzunluk = sapma.Length;
                double vsapma = 0;

                vsapma = tsapma / (uzunluk - 1);
                textBox12.Text = Convert.ToString(Math.Sqrt(vsapma));
                textBox11.Text = Convert.ToString(Convert.ToDouble(textBox12.Text) * Convert.ToDouble(textBox12.Text));
                /********************************************/
                int elemansayısı = listBox1.Items.Count;

                for (int z = 0; z <= elemansay; z++)
                {
                    aradakisay[z] = (string)listBox1.Items[z];

                }

                /********************************************/
                /*sınıf kadar text box oluşturmak için */
                int sayı = Convert.ToInt32(sinif);
                double artıs = Convert.ToDouble(enküçüksayi);
                double[] asveri = new double[sinif];
                double[] altveri = new double[sinif];

                double artıs1 = Convert.ToDouble(enküçüksayi);
                for (int i = 0; i < sayı; i++)
                {   /*üst*/
                    TextBox t = new TextBox();
                    flowLayoutPanel3.Controls.Add(t);
                    double aralikdeğer = Convert.ToDouble(Math.Round(Convert.ToDecimal(aralik), 2));
                    artıs = artıs + aralikdeğer;
                    t.Text = Convert.ToString(Math.Round(Convert.ToDecimal(artıs - 0.01), 2));
                    asveri[i] = Convert.ToDouble(t.Text);

                }
                for (int i = 0; i < sayı; i++)
                {
                    /*alt*/
                    TextBox t = new TextBox();
                    flowLayoutPanel2.Controls.Add(t);
                    t.Text = Convert.ToString(Math.Round(Convert.ToDecimal(artıs1), 3));
                    artıs1 = artıs1 + aralik;
                    string yazı = t.Text;
                    int j = i + 1;
                    if (j < sayı)
                    {
                        if (asveri[j] > artıs1)
                        {
                            artıs1 += 0.002;
                        }
                    }
                    altveri[i] = Convert.ToDouble(t.Text);

                }
                double[] ortada = new double[sinif];
                for (int i = 0; i < sayı; i++)
                {   /*orta*/
                    TextBox t = new TextBox();

                    flowLayoutPanel4.Controls.Add(t);
                    double orta = (altveri[i] + asveri[i]) / 2;
                    t.Text = Convert.ToString(Math.Round(Convert.ToDecimal(orta), 2));
                    ortada[i] = orta;
                }
                int[] sınıfsay = new int[sinif];
                int topla = 0;

                for (int i = 0; i < sayı; i++)
                {   /*Frekans*/
                    TextBox t = new TextBox();

                    flowLayoutPanel5.Controls.Add(t);
                    double alt = altveri[i];
                    double ust = asveri[i];
                    int sayac = 0;

                    for (int j = 0; j < elemansay; j++)
                    {
                        if ((alt <= Convert.ToDouble(aradakisay[j])) && (ust >= Convert.ToDouble(aradakisay[j])))
                        {
                            sayac++;
                            t.Text = Convert.ToString(sayac);
                            sınıfsay[i] = sayac;

                        }

                    }
                    topla = topla + Convert.ToInt32(t.Text);


                }
                double[] gfr = new double[sinif];
                for (int i = 0; i < sayı; i++)
                {   /*g.frekans*/
                    TextBox t = new TextBox();

                    flowLayoutPanel6.Controls.Add(t);
                    double gf = Convert.ToDouble(sınıfsay[i]) / topla;
                    t.Text = Convert.ToString(Math.Round(Convert.ToDecimal(gf), 2));
                    gfr[i] = gf;
                }

                string zaman = DateTime.Now.ToString("yyyy'.'MM'.'dd'Time'HH'.'mm'.'ss");
                StreamWriter SW = new StreamWriter(".\\out" + zaman + ".txt", true);
                SW.WriteLine("ALT" + "     " + "ÜST" + "     " + "ORTA" + "     " + "FREKANS" + "     " + "GORELİ FREKANS");
                for (int i = 0; i < sinif; i++)
                {

                    SW.WriteLine(altveri[i] + "     " + asveri[i] + "     " + ortada[i] + "     " + sınıfsay[i] + "     " + gfr[i]);
                }
                SW.WriteLine("                                ");
                SW.WriteLine("ORTLAMA :" + " " + textBox8.Text);
                SW.WriteLine("MOD :" + " " + textBox9.Text);
                SW.WriteLine("MEDYAN :" + " " + textBox10.Text);
                SW.WriteLine("VARYANS :" + " " + textBox11.Text);
                SW.WriteLine("STANDART SAPMA :" + " " + textBox12.Text);


                SW.Close();

                MessageBox.Show("çıktı dosyanız oluşturuldu. çıktı program dizinindedir.");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox6.Text = 10.ToString();
            timer1.Enabled = true;
            label8.Text = " NECMETTİN ERBAKAN ÜNİVERSİTESİ  ";
        }

        private void çıkışToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label8.Text = label8.Text.Substring(1) + label8.Text.Substring(0, 1);
        }
    }/*FIRAT KAYA */
}
