using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace De
{
    public partial class UserAgent : UserControl
    {
        public UserAgent()
        {
            InitializeComponent();
        }
        public string Label1 { 
            get { return label1.Text; }
            set { label1.Text = value; }
        }
        public string Label2
        {
            get { return label2.Text; }
            set { label2.Text = value; }
        }
        public string Label3
        {
            get { return label3.Text; }
            set { label3.Text = value; }
        }
        public string Label4
        {
            get { return label4.Text; }
            set { label4.Text = value; }
        }
        public string Label5
        {
            get { return label5.Text; }
            set { label5.Text = value; }
        }
        public void AddPicture(string path)
        {
            if(path!="")
                pictureBox1.Image=Image.FromFile(Environment.CurrentDirectory + path);
        }

        private void UserAgent_DoubleClick ( object sender, EventArgs e )
        {
            
            Form2 edit = new Form2();
            edit.textBox1.Text = Label1.Split('|')[1];
            using (ModelDB db=new ModelDB())
            {
                string title = Label1.Split('|')[1].Trim();
                Agent agent= db.Agent.Where(p => p.Title == title).FirstOrDefault();
                edit.comboBox1.Text = agent.AgentTypeID;
                edit.textBox1.Text = agent.Title;
                edit.textBox2.Text = agent.Address;
                edit.textBox3.Text = agent.Email;
                edit.textBox4.Text = agent.Phone;
                edit.textBox5.Text = agent.DirectorName;
                edit.textBox6.Text = agent.INN;
                edit.textBox7.Text = agent.KPP;
                edit.textBox8.Text = agent.Priority.ToString();
                edit.LoadFoto(agent.Logo);
                edit.ForEdit = true;
                if (edit.ShowDialog() == DialogResult.OK)
                {
                    agent.Title = edit.textBox1.Text;
                    agent.AgentTypeID = edit.comboBox1.Text;
                    agent.Address = edit.textBox2.Text;
                    agent.Email = edit.textBox3.Text;
                    agent.Phone = edit.textBox4.Text;
                    agent.DirectorName = edit.textBox5.Text;
                    agent.INN = edit.textBox6.Text;
                    agent.KPP = edit.textBox7.Text;
                    agent.Priority = int.Parse(edit.textBox8.Text);
                    db.SaveChanges();
                    ( ParentForm as Form1 ).LoadData();
                }
            }
        }
    }
}
