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
            this.Resize += Form1_Resize;
        }

        public void Form1_Resize(object sender, EventArgs e)
        {
            lstKeys.Height = this.Height - 120;
            lstEndpoints.Height = this.Height - 90;
            txtValue.Height = this.Height - 100;
            txtValue.Width = this.Width - 320;

            btnRefresh.Location = new System.Drawing.Point(this.Width - 220, 12);
            btnConnect.Location = new System.Drawing.Point(this.Width - 120, 12);
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
            if (db == null)
            {
                return;
            }
            // get value
            if (lstKeys.SelectedItem != null)
            {
                // check key type
                var type = db?.KeyType(lstKeys.SelectedItem.ToString());

                lblType.Text = type.ToString();

                // if string
                if (type == RedisType.String)
                {
                    var value = db!.StringGet(lstKeys.SelectedItem.ToString());
                    txtValue.Text = value;
                }

                // if list
                if (type == RedisType.List)
                {
                    var value = db!.ListRange(lstKeys.SelectedItem.ToString());
                    txtValue.Text = "";
                    foreach (RedisValue item in value)
                    {
                        txtValue.Text += item + "\r\n";
                    }
                }

                // if set
                if (type == RedisType.Set)
                {
                    var value = db!.SetMembers(lstKeys.SelectedItem.ToString());
                    txtValue.Text = "";
                    foreach (var item in value)
                    {
                        txtValue.Text += item + "\r\n";
                    }
                }

                // if hash
                if (type == RedisType.Hash)
                {
                    var value = db!.HashGetAll(lstKeys.SelectedItem.ToString());
                    txtValue.Text = "";
                    foreach (var item in value)
                    {
                        txtValue.Text += item.Name + ": " + item.Value + "\r\n";
                    }
                }

                // if sorted set
                if (type == RedisType.SortedSet)
                {
                    var value = db!.SortedSetRangeByRankWithScores(lstKeys.SelectedItem.ToString());
                    txtValue.Text = "";
                    foreach (var item in value)
                    {
                        txtValue.Text += item.Element + ": " + item.Score + "\r\n";
                    }
                }

                if (type == RedisType.Set)
                {
                    var value = db!.SetMembers(lstKeys.SelectedItem.ToString());
                    txtValue.Text = "";
                    foreach (var item in value)
                    {
                        txtValue.Text += item + "\r\n";
                    }
                }
            }
        }

        private void txtKeySearch_TextChanged(object sender, EventArgs e)
        {
            if (redis == null)
            {
                return;
            }
            var server = redis.GetServer(txtHostPort.Text);
            // get keys
            lstKeys.Items.Clear();
            foreach (var key in server.Keys(pattern: "*" + txtKeySearch.Text + "*"))
            {
                lstKeys.Items.Add(key);
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtKeySearch.PlaceholderText = "Search keys";
            txtHostPort.PlaceholderText = "127.0.0.1:6379,127.0.0.1:6378";
            lblType.Text = "";

            cboKeyType.Items.Add(RedisType.String);
            cboKeyType.Items.Add(RedisType.List);
            cboKeyType.Items.Add(RedisType.Set);
            cboKeyType.Items.Add(RedisType.SortedSet);
            cboKeyType.Items.Add(RedisType.Hash);

            cboKeyType.SelectedIndex = 0;
            //make it not editable
            cboKeyType.DropDownStyle = ComboBoxStyle.DropDownList;

            Form1_Resize(sender, e);
        }


        private void lstEndpoints_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // check key type
            if (cboKeyType.SelectedItem == null)
            {
                return;
            }

            if (db == null)
            {
                return;
            }

            var defaultValue = "";
            var defaultKey = "key";

            switch (cboKeyType.SelectedItem)
            {
                case RedisType.String:
                    db.StringSet(txtKeySearch.Text, defaultValue);
                    break;
                case RedisType.List:
                    db.ListRightPush(txtKeySearch.Text, defaultValue);
                    break;
                case RedisType.Set:
                    db.SetAdd(txtKeySearch.Text, defaultValue);
                    break;
                case RedisType.SortedSet:
                    db.SortedSetAdd(txtKeySearch.Text, defaultValue, 0);
                    break;
                case RedisType.Hash:
                    db.HashSet(txtKeySearch.Text, defaultKey, defaultValue);
                    break;
            }

            refreshKeys();

        }

        private void refreshKeys()
        {
            if (db == null || redis == null)
            {
                return;
            }
            // get keys
            lstKeys.Items.Clear();
            foreach (var key in redis.GetServer(txtHostPort.Text).Keys())
            {
                lstKeys.Items.Add(key);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            refreshKeys();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // check key type
            if (cboKeyType.SelectedItem == null)
            {
                return;
            }

            if (db == null)
            {
                return;
            }

            if (lstKeys.SelectedItem == null)
            {
                return;
            }

            switch(cboKeyType.SelectedItem)
            {
                case RedisType.String:
                    db.StringSet(lstKeys.SelectedItem.ToString(), txtValue.Text);
                    break;
                case RedisType.List:
                    db.ListRightPush(lstKeys.SelectedItem.ToString(), txtValue.Text);
                    break;
                case RedisType.Set:
                    db.SetAdd(lstKeys.SelectedItem.ToString(), txtValue.Text);
                    break;
                case RedisType.SortedSet:
                    db.SortedSetAdd(lstKeys.SelectedItem.ToString(), txtValue.Text, 0);
                    break;
                case RedisType.Hash:
                    db.HashSet(lstKeys.SelectedItem.ToString(), "key", txtValue.Text);
                    break;
            }
        }
    }
}
