using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace De
{
    public partial class Form1 : Form
    {
        private int current = 0;
        private string sortingOption = "";

        public Form1()
        {
            InitializeComponent();
            LoadData();
        }
        public void LoadData()
        {
            
            flowLayoutPanel1.Controls.Clear();
            using (ModelDB db = new ModelDB())
            {
                var users = from agent in db.Agent
                            join sale in db.ProductSale on agent.Title equals sale.AgentID
                            join product in db.Product on sale.Title equals product.Title
                            select new
                            {
                                Name = sale.Title,
                                AgentEmail = agent.Email,
                                Count = sale.ProductCount,
                                Number = agent.Phone,
                                TypeAgent = agent.AgentTypeID,
                                Agent = agent.Title,
                                AgentPriority = agent.Priority,
                                Picture = agent.Logo
                            };


                var list = users.ToList();
                for (int i = current; i < Math.Min(current + 10, list.Count); i++)
                {
                    int disc = 0;
                    if (list[i].Count < 10000) disc = 0;
                    else if (list[i].Count > 10000 && list[i].Count < 50000) disc = 5;
                    else if (list[i].Count > 50000 && list[i].Count < 150000) disc = 10;
                    else if (list[i].Count > 150000 && list[i].Count < 500000) disc = 20;
                    else disc = 25;

                    UserAgent userAgent = new UserAgent()
                    {
                        Label1 = list[i].TypeAgent + "|" + list[i].Agent,
                        Label2 = list[i].Count + " продаж за год",
                        Label3 = list[i].Number,
                        Label4 = list[i].AgentPriority.ToString(),
                        Label5 = disc.ToString() + "%"

                    };
                    userAgent.AddPicture(list[i].Picture);
                    flowLayoutPanel1.Controls.Add(userAgent);
                }
            }
        }
       

        private void button2_Click(object sender, EventArgs e)
        {
            current += 10;
            LoadData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            current = Math.Max(0, current - 10);
            LoadData();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Clear();
            if (comboBox1.Text.ToString() == "наименование")
            {
               
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                // Сортировка по возрастанию
               
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                // Сортировка по убыванию
                sortingOption += " (убывание)";
                LoadData();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string searchText = textBox1.Text.ToLower(); // Приводим текст поиска к нижнему регистру для регистронезависимого поиска

            //foreach (Control control in flowLayoutPanel1.Controls)
            //{
            //    if (control is UserAgent userAgent)
            //    {
            //        if (userAgent.Label3.ToLower().Contains(searchText))//Label3 содержит номер
            //        {
            //            userAgent.Visible = true;
            //        }
            //        else
            //        {
            //            userAgent.Visible = false;
            //        }
            //    }
            //}
            flowLayoutPanel1.Controls.Clear();
            using (ModelDB db = new ModelDB()) 
            {
                var users = from agent in db.Agent
                            join sale in db.ProductSale on agent.Title equals sale.AgentID
                            join product in db.Product on sale.Title equals product.Title
                            select new
                            {
                                Name = sale.Title,
                                AgentEmail = agent.Email,
                                Count = sale.ProductCount,
                                Number = agent.Phone,
                                TypeAgent = agent.AgentTypeID,
                                Agent = agent.Title,
                                AgentPriority = agent.Priority,
                                Picture = agent.Logo
                            };


                var list = users.Where(p=>
                        p.AgentEmail.StartsWith(searchText)|| 
                        p.TypeAgent.StartsWith(searchText)||
                        p.Agent.StartsWith(searchText) ||
                        p.Number.StartsWith(searchText)).ToList();
                for (int i = current; i < Math.Min(current + 10, list.Count); i++)
                {
                    int disc = 0;
                    if (list[i].Count < 10000) disc = 0;
                    else if (list[i].Count > 10000 && list[i].Count < 50000) disc = 5;
                    else if (list[i].Count > 50000 && list[i].Count < 150000) disc = 10;
                    else if (list[i].Count > 150000 && list[i].Count < 500000) disc = 20;
                    else disc = 25;

                    UserAgent userAgent = new UserAgent()
                    {
                        Label1 = list[i].TypeAgent + "|" + list[i].Agent,
                        Label2 = list[i].Count + " продаж за год",
                        Label3 = list[i].Number,
                        Label4 = list[i].AgentPriority.ToString(),
                        Label5 = disc.ToString() + "%"

                    };
                    userAgent.AddPicture(list[i].Picture);
                    flowLayoutPanel1.Controls.Add(userAgent);
                }
            }

        }



        private void button3_Click ( object sender, EventArgs e )
        {
            Form2 addForm = new Form2();
            if (addForm.ShowDialog() == DialogResult.OK)
            {
                using (ModelDB db = new ModelDB())
                {
                    Agent agent = new Agent();
                    agent.Title = addForm.textBox1.Text;
                    agent.Logo = addForm.Foto;
                    agent.Address = addForm.textBox2.Text;
                    agent.Email = addForm.textBox3.Text;
                    agent.Phone = addForm.textBox4.Text;
                    agent.DirectorName = addForm.textBox5.Text;
                    agent.INN = addForm.textBox6.Text;
                    agent.KPP = addForm.textBox7.Text;
                    agent.Priority = int.Parse(addForm.textBox8.Text);
                    agent.AgentTypeID = addForm.comboBox1.SelectedItem.ToString();
                    agent.AgentType = db.AgentType.Where(p => p.Title == addForm.comboBox1.SelectedItem.ToString()).FirstOrDefault();
                    db.Agent.Add(agent);
                    db.SaveChanges();
                }
                LoadData();
            }
        }
    }
}
