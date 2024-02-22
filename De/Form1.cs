using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

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

        private void LoadData()
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

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                // Сортировка по возрастанию
                sortingOption += " (возрастание)";
                LoadData();
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
    }
}
