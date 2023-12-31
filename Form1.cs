//import StackExchange.Redis;
using System;
using System.Windows.Forms;
using StackExchange.Redis;

namespace RedisAdmin
{
    public partial class Form1 : Form
    {
        private ConnectionMultiplexer? redis;
        private IDatabase? db;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (redis != null)
            {
                redis.Close();
                redis.Dispose();
                redis = null;
                db = null;

                // clear other control values
                lstEndpoints.Items.Clear();
                lstKeys.Items.Clear();
                txtValue.Text = "";
                lblType.Text = "";


                btnConnect.Text = "Connect";

                return;
            }

            redis = ConnectionMultiplexer.Connect(txtHostPort.Text);

            // get database
            db = redis.GetDatabase();
            if (db != null)
            {
                btnConnect.Text = "Disconnect";
            }

            lstEndpoints.Items.Clear();
            for (int i = 0; i < redis.GetEndPoints().Length; i++)
            {
                lstEndpoints.Items.Add(i + ": " + redis.GetEndPoints()[i]);
            }
            //select the first endpoint
            lstEndpoints.SelectedIndex = 0;

            // get keys
            lstKeys.Items.Clear();
            foreach (var key in redis.GetServer(txtHostPort.Text).Keys())
            {
                lstKeys.Items.Add(key);
            }

        }

        private void lstKeys_SelectedIndexChanged(object sender, EventArgs e)
        {
            // get value
            if (lstKeys.SelectedItem != null)
            {
                // check key type
                var type = db?.KeyType(lstKeys.SelectedItem.ToString());

                lblType.Text = type.ToString();

                // if string
                if (type == RedisType.String)
                {
                    var value = db?.StringGet(lstKeys.SelectedItem.ToString());
                    txtValue.Text = value;
                }

                // if list
                if (type == RedisType.List)
                {
                    var value = db?.ListRange(lstKeys.SelectedItem.ToString());
                    txtValue.Text = "";
                    foreach (var item in value)
                    {
                        txtValue.Text += item + "\r\n";
                    }
                }

                // if set
                if (type == RedisType.Set)
                {
                    var value = db?.SetMembers(lstKeys.SelectedItem.ToString());
                    txtValue.Text = "";
                    foreach (var item in value)
                    {
                        txtValue.Text += item + "\r\n";
                    }
                }

                // if hash
                if (type == RedisType.Hash)
                {
                    var value = db?.HashGetAll(lstKeys.SelectedItem.ToString());
                    txtValue.Text = "";
                    foreach (var item in value)
                    {
                        txtValue.Text += item.Name + ": " + item.Value + "\r\n";
                    }
                }


            }

        }

        private void txtKeySearch_TextChanged(object sender, EventArgs e)
        {
            // get keys
            lstKeys.Items.Clear();
            foreach (var key in redis.GetServer(txtHostPort.Text).Keys(pattern: "*" + txtKeySearch.Text + "*"))
            {
                lstKeys.Items.Add(key);
            }


        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtKeySearch.PlaceholderText = "Search keys";
            txtHostPort.PlaceholderText = "127.0.0.1:6379,127.0.0.1:6378";
            lblType.Text = "";
        }

        private void tvDatabases_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void lstEndpoints_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
    }
}
