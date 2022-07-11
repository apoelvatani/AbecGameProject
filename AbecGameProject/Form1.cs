using AbecGameProject.Abstract;
using AbecGameProject.Adapters;
using AbecGameProject.Concrete;
using AbecGameProject.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AbecGameProject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //Polymorpishm
        BaseGamerManager gamerManager = new AGamerManager(new MernisServiceAdapter());

        private void Form1_Load(object sender, EventArgs e)
        {
            WriteTotalGamers();
        }

        // Oyuncuları kayıta ekler.
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (CheckRequiredFields()) return;

            if (CheckDuplicatedId()) return;

            if (gamerManager.Add(new Gamer { NatioanlityId = Convert.ToInt64(txtNatioanlityId.Text), FirstName = Convert.ToString(txtFirstName.Text), LastName = Convert.ToString(txtLastName.Text), BirthOfYear = Convert.ToInt32(txtBirthOfYear.Text) })) {
                AddListView();
                WriteTotalGamers();
                Clear();
                ListArray();
            }
        }

        // Kayıttaki oyuncuları günceller.
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (CheckRequiredFields()) return;

            gamerManager.Update(new Gamer { NatioanlityId = Convert.ToInt64(txtNatioanlityId.Text), FirstName = Convert.ToString(txtFirstName.Text), LastName = Convert.ToString(txtLastName.Text), BirthOfYear = Convert.ToInt32(txtBirthOfYear.Text) });

            UpdateListView();
            Clear();
            ListArray();
        }

        // Hem checkbox'ına tik atılan hem üzerine tıklanan hem de textte bilgileri verilen oyuncuların kayıtlarını siler. Remove butonuna tıklandıktan sonra  
        // kayıtlar eğer ViewList'ten seçilmişse textteki alanlar dolu olmak zorunda değildir, doluysa da o bilgilere göre de oyuncu silinir. Kayıtlar ViewList'ten
        // seçilmemişse textteki alanların dolu olma zorunluluğu vardır, program hata verir.
        private void btnRemove_Click(object sender, EventArgs e)
        {
            if(ListView.CheckedItems.Count == 0 && ListView.SelectedItems.Count == 0)
            {
                if (CheckRequiredFields(true)) return;
                else
                {
                    gamerManager.Remove(Convert.ToInt64(txtNatioanlityId.Text));
                    RemoveListView();
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(txtNatioanlityId.Text))
                {
                    gamerManager.Remove(Convert.ToInt64(txtNatioanlityId.Text));
                    RemoveListView();
                }
                RemoveItems(ListView.SelectedItems);
                RemoveItems(ListView.CheckedItems);
            }
            WriteTotalGamers();
            Clear();
            ListArray();
        }

        // Textleri temizler.
        private void Clear()
        {
            txtNatioanlityId.Clear();
            txtFirstName.Clear();
            txtLastName.Clear();
            txtBirthOfYear.Clear();
            richTextBox1.Clear();
        }

        // Verilen TcKimlikle kayıtta böyle bir oyuncunun olup olmadığını sorgular.
        private void btnFind_Click(object sender, EventArgs e)
        {
            bool IsRegisteredGamer = false;
            for (int i = 0; i < ListView.Items.Count; i++)
            {
                if (ListView.Items[i].SubItems[0].Text == txtNatioanlityId.Text)
                {
                    IsRegisteredGamer = true;
                    txtFirstName.Text = ListView.Items[i].SubItems[1].Text;
                    txtLastName.Text = ListView.Items[i].SubItems[2].Text;
                    txtBirthOfYear.Text = ListView.Items[i].SubItems[3].Text;
                    break;
                }
            }
            if (!IsRegisteredGamer)
            {
                if (string.IsNullOrEmpty(txtNatioanlityId.Text))
                    MessageBox.Show("Please fill in the required fields...!", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                    MessageBox.Show("Not a valid Gamer...!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Kayıttaki toplam oyuncu sayısını ekrana yazar.
        private void WriteTotalGamers()
        {
            lbltotalGamers.Text = Convert.ToString(ListView.Items.Count);
        }

        // Texteki alanların doluluğunu kontrol eder.
        private bool CheckRequiredFields(bool checkRequiredFieldId = false)
        {
            bool returnValue = false;
            if (!checkRequiredFieldId)
            {
                if (string.IsNullOrEmpty(txtNatioanlityId.Text) || string.IsNullOrEmpty(txtFirstName.Text) || string.IsNullOrEmpty(txtLastName.Text) || string.IsNullOrEmpty(txtBirthOfYear.Text))
                {
                    MessageBox.Show("Please fill in the required fields...!", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    returnValue = true;
                }
            }
            else
            {
                // Sadece Id'nin doluluğunu kontrol eder.
                if (string.IsNullOrEmpty(txtNatioanlityId.Text))
                {
                    MessageBox.Show("Please fill in the required Nationality id field...!", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    returnValue = true;
                }
            }
            return returnValue;
        }

        // Kayıtta aynı Id'li birisi olup olmadığını kontrol eder.
        private bool CheckDuplicatedId()
        {
            for (int i = 0; i < ListView.Items.Count; i++)
            {
                if (ListView.Items[i].SubItems[0].Text == txtNatioanlityId.Text)
                {
                    MessageBox.Show("There are already in the register this nationality ID...!", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return true;
                }
            }
            return false;
        }

        // Gamerleri ListView'e ekler.
        private void AddListView()
        {
            ListViewItem item = new ListViewItem(txtNatioanlityId.Text);
            item.SubItems.Add(txtFirstName.Text);
            item.SubItems.Add(txtLastName.Text);
            item.SubItems.Add(txtBirthOfYear.Text);
            ListView.Items.Add(item);
        }

        // ListView'deki gamerleri günceller.
        private void UpdateListView()
        {
            for (int i = 0; i < ListView.Items.Count; i++)
            {
                if (ListView.Items[i].SubItems[0].Text == txtNatioanlityId.Text)
                {
                    ListView.Items[i].SubItems[1].Text = txtFirstName.Text;
                    ListView.Items[i].SubItems[2].Text = txtLastName.Text;
                    ListView.Items[i].SubItems[3].Text = txtBirthOfYear.Text;
                    break;
                }
            }
        }

        // ListView'deki gamerleri siler.
        private void RemoveListView()
        {
            for (int i = 0; i < ListView.Items.Count; i++)
            {
                if (ListView.Items[i].SubItems[0].Text == txtNatioanlityId.Text)
                {
                    ListView.Items[i].Remove();
                    break;
                }
            }
        }

        // Selected ya da checked edilen kayıtları siler.
        private void RemoveItems(IList listView)
        {
            foreach (ListViewItem item in listView)
            {
                if (txtNatioanlityId.Text != item.Text.Trim())
                {
                    gamerManager.Remove(Convert.ToInt64(item.Text.Trim()));
                    item.Remove();
                }
            }
        }

        // Listview'da yapılan işlemlerin arkaplanda çalışan dizide de yapılıp yapılmadığını kontrol etmek için yazılmıştır. gamers dizisini texte yazar.
        private void ListArray()
        {
            Gamer[] gamers = (Gamer[])gamerManager.GetGamers();
            foreach (var gamer in gamers)
            {
                richTextBox1.Text += gamer.NatioanlityId + "\t" + gamer.FirstName + "\t" + gamer.LastName + "\t" + gamer.BirthOfYear + "\n";
            }
        }
    }
}
