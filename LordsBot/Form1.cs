using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;

namespace LordsBot
{
    public partial class Form1 : Form
    {


        List<BackgroundWorker> workers = new List<BackgroundWorker>();
        List<BotNox> nox = new List<BotNox>();


        StringBuilder SystemMessage = new StringBuilder();


        const string windowClass = "Qt5QWindowIcon";


        private List<string> getAccountList(string path) {
            string[] fileArray;
            List<string> tempList = new List<string>();
            try
            {
               
                fileArray = Directory.GetFiles(@"" + path, "*.bmp");
                for (int i = 0; i < fileArray.Length; i++)
                {
                    tempList.Add(fileArray[i]);
                }
               
            }
            catch
            {
                tempList.Clear();
            }

            return tempList;
        }


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            //this.BackgroundImage = LordsBot.Properties.Resources.picture; //Sets Background Image
            textBox1.Text = "10";
            textBox2.Text = "5";
            textBox3.Text = "1";
            textBox4.Text = "1";
            textBox9.Text = "1";
            textBox10.Text = "1";
            textBox11.Text = "1";
            textBox12.Text = "1";
            textBox5.Text = "10";
            textBox7.Text = "1";
            textBox8.Text = "15";
        }

        private void button1_Click(object sender, EventArgs e)
        {





             

            nox[listBox1.SelectedIndex].run = true;

            if (!workers[listBox1.SelectedIndex].IsBusy) {
                workers[listBox1.SelectedIndex].RunWorkerAsync();
            }
            

            









        }

        private void button2_Click(object sender, EventArgs e)
        {
            //stop button
            if (listBox1.SelectedIndex >= 0) {
                nox[listBox1.SelectedIndex].run = false;
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            //create button

            int handle = Win32.FindWindow(windowClass, textBox6.Text.ToString());
            int listSize = nox.Count;
            bool handleExist = false;



            if (handle != 0)
            {
                
                foreach (var noxItem in nox)
                {
                    if (noxItem.handler == handle)
                    {
                        handleExist = true;
                    }
                }

                if (!handleExist)
                {
                    workers.Add(new BackgroundWorker());
                    nox.Add(new BotNox(handle));
                    workers[workers.Count - 1].WorkerReportsProgress = true;
                    workers[workers.Count - 1].DoWork += (obj, ea) => nox[nox.Count - 1].Start(obj as BackgroundWorker);
                    workers[workers.Count - 1].ProgressChanged += new ProgressChangedEventHandler(progressChanged);
                    workers[workers.Count - 1].RunWorkerCompleted += new RunWorkerCompletedEventHandler(workerCompleted);

                    nox[nox.Count - 1].name = textBox6.Text.ToString();
                    nox[nox.Count - 1].ID = (nox.Count - 1);
                    nox[nox.Count - 1].sleepTime = Int32.Parse(textBox5.Text);

                    nox[nox.Count - 1].alwaysShield = checkBox3.Checked;

                    nox[nox.Count - 1].shelterAll = checkBox4.Checked;
                    nox[nox.Count - 1].shelterOne = checkBox5.Checked;

                    nox[nox.Count - 1].collectChests = checkBox6.Checked;
                    nox[nox.Count - 1].collectGuildGift = checkBox7.Checked;
                    nox[nox.Count - 1].healTroops = checkBox8.Checked;
                    nox[nox.Count - 1].helpOthers = checkBox9.Checked;
                    nox[nox.Count - 1].Quest = checkBox11.Checked;
                    nox[nox.Count - 1].turfQuest = checkBox32.Checked;

                    nox[nox.Count - 1].trainTroops = checkBox13.Checked;
                    nox[nox.Count - 1].trainTroops_T1_Infantry = checkBox34.Checked;
                    nox[nox.Count - 1].trainTroops_T1_Ranged = checkBox35.Checked;
                    nox[nox.Count - 1].trainTroops_T1_Cavalry = checkBox36.Checked;
                    nox[nox.Count - 1].trainTroops_T1_Siege = checkBox37.Checked;

                    nox[nox.Count - 1].upgradeBuildings = checkBox14.Checked;
                    nox[nox.Count - 1].research = checkBox10.Checked;

                    nox[nox.Count - 1].gather = checkBox33.Checked;
                    nox[nox.Count - 1].foodGather = checkBox15.Checked;
                    nox[nox.Count - 1].woodGather = checkBox17.Checked;
                    nox[nox.Count - 1].oreGather = checkBox16.Checked;
                    nox[nox.Count - 1].stoneGather = checkBox18.Checked;
                    nox[nox.Count - 1].goldGather = checkBox19.Checked;
                    nox[nox.Count - 1].searchDistance = Int32.Parse(textBox1.Text);
                    nox[nox.Count - 1].marchesGather = Int32.Parse(textBox2.Text);
                    nox[nox.Count - 1].randomGather = checkBox24.Checked;
                    nox[nox.Count - 1].moveUpGather = checkBox20.Checked;
                    nox[nox.Count - 1].moveDownGather = checkBox21.Checked;
                    nox[nox.Count - 1].moveLeftGather = checkBox23.Checked;
                    nox[nox.Count - 1].moveRightGather = checkBox22.Checked;

                    nox[nox.Count - 1].transferResource = checkBox25.Checked;
                    nox[nox.Count - 1].useGuildBookmarks = checkBox2.Checked;
                    nox[nox.Count - 1].foodTransfer = checkBox26.Checked;
                    nox[nox.Count - 1].stoneTransfer = checkBox27.Checked;
                    nox[nox.Count - 1].woodTransfer = checkBox28.Checked;
                    nox[nox.Count - 1].oreTransfer = checkBox29.Checked;
                    nox[nox.Count - 1].goldTransfer = checkBox30.Checked;

                    nox[nox.Count - 1].bookmarkNumberTransfer = Int32.Parse(textBox3.Text);
                    nox[nox.Count - 1].marchesTransferFood = Int32.Parse(textBox4.Text);
                    nox[nox.Count - 1].marchesTransferStone = Int32.Parse(textBox9.Text);
                    nox[nox.Count - 1].marchesTransferWood = Int32.Parse(textBox10.Text);
                    nox[nox.Count - 1].marchesTransferOre = Int32.Parse(textBox11.Text);
                    nox[nox.Count - 1].marchesTransferGold = Int32.Parse(textBox12.Text);
                    nox[nox.Count - 1].transferFrequency = Int32.Parse(textBox7.Text);

                    nox[nox.Count - 1].correctWindowSizeEnable = checkBox1.Checked;

                    nox[nox.Count - 1].AccountSwitch = checkBox12.Checked;
                    if (checkBox12.Checked) {
                        nox[nox.Count - 1].AccountSwitch_Count = Int32.Parse(textBox8.Text);
                    }


                    listBox1.Items.Add(nox[nox.Count - 1].name);
                    listBox1.SetSelected(listBox1.Items.Count-1,true);
                }
                else {
                    MessageBox.Show("The name you entered already exist.");
                }
            }
            else {
                MessageBox.Show("The name you entered does not exist.");

            }

    }

        private void progressChanged(object sender, ProgressChangedEventArgs e) {
            //do something e.Progresspercentage
            int i = e.ProgressPercentage;


            if (listBox1.SelectedIndex >= 0)
            {

                if (i == listBox1.SelectedIndex)
                {


                    if (richTextBox1.TextLength > 800)
                    {
                        SystemMessage.Clear();
                    }

                    SystemMessage.AppendLine((string)e.UserState);
                    richTextBox1.Text = SystemMessage.ToString(); ;

                }
            }

             


        }
        private void workerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            //do something

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

            int aNumber;
            if (int.TryParse(textBox5.Text, out aNumber) == false)
            {
                textBox5.Text = "60";

            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            int aNumber;
            if (int.TryParse(textBox1.Text, out aNumber) == false)
            {
                textBox1.Text = "10";

            }
            if (textBox1.Text == "0") { textBox1.Text = "1"; }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            int aNumber;
            if (int.TryParse(textBox2.Text, out aNumber) == false)
            {
                textBox2.Text = "5";

            }
            if (textBox2.Text == "0") { textBox2.Text = "1"; }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            int aNumber;
            if (int.TryParse(textBox3.Text, out aNumber) == false)
            {
                textBox3.Text = "1";

            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            int aNumber;
            if (int.TryParse(textBox4.Text, out aNumber) == false)
            {
                textBox4.Text = "1";

            }
            if (textBox4.Text == "0") { textBox4.Text = "1"; }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {

            if (checkBox4.Checked == true) {
                checkBox5.Checked = false;
            }

        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox5.Checked == true)
            {
                checkBox4.Checked = false;
            }
        }

        private void checkBox33_CheckedChanged(object sender, EventArgs e)
        {

            if (checkBox33.Checked == false) {

                checkBox15.Enabled = false;
                checkBox17.Enabled = false;
                checkBox16.Enabled = false;
                checkBox19.Enabled = false;
                checkBox18.Enabled = false;
                textBox1.Enabled = false;
                textBox2.Enabled = false;
                checkBox24.Enabled = false;
                checkBox20.Enabled = false;
                checkBox21.Enabled = false;
                checkBox23.Enabled = false;
                checkBox22.Enabled = false;

            }
            else if (checkBox33.Checked == true){

                checkBox15.Enabled = true;
                checkBox17.Enabled = true;
                checkBox16.Enabled = true;
                checkBox19.Enabled = true;
                checkBox18.Enabled = true;
                textBox1.Enabled = true;
                textBox2.Enabled = true;
                checkBox24.Enabled = true;
                checkBox20.Enabled = true;
                checkBox21.Enabled = true;
                checkBox23.Enabled = true;
                checkBox22.Enabled = true;

            }




        }

        private void checkBox24_CheckedChanged(object sender, EventArgs e)
        {

            if (checkBox24.Checked) {
                checkBox20.Checked = false;
                checkBox21.Checked = false;
                checkBox22.Checked = false;
                checkBox23.Checked = false;

            }
            if (checkBox24.Checked == false)
            {

                if (!(checkBox20.Checked || checkBox21.Checked || checkBox22.Checked || checkBox23.Checked) ) {
                    checkBox24.Checked = true;
                }


            }


        }

        private void checkBox20_CheckedChanged(object sender, EventArgs e)
        {

            if (checkBox20.Checked) {
                checkBox24.Checked = false;
            }

        }

        private void checkBox21_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox21.Checked)
            {
                checkBox24.Checked = false;
            }
        }

        private void checkBox23_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox23.Checked)
            {
                checkBox24.Checked = false;
            }
        }

        private void checkBox22_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox22.Checked)
            {
                checkBox24.Checked = false;
            }
        }

        private void checkBox25_CheckedChanged(object sender, EventArgs e)
        {

            if (checkBox25.Checked == false) {
                textBox4.Enabled = false;
                textBox9.Enabled = false;
                textBox10.Enabled = false;
                textBox11.Enabled = false;
                textBox12.Enabled = false;
                textBox7.Enabled = false;
                checkBox26.Enabled = false;
                checkBox27.Enabled = false;
                checkBox28.Enabled = false;
                checkBox29.Enabled = false;
                checkBox30.Enabled = false;
            }
            if (checkBox25.Checked == true)
            {
                if (checkBox26.Checked == true) { textBox4.Enabled = true; }
                if (checkBox27.Checked == true) { textBox9.Enabled = true; }
                if (checkBox28.Checked == true) { textBox10.Enabled = true; }
                if (checkBox29.Checked == true) { textBox11.Enabled = true; }
                if (checkBox30.Checked == true) { textBox12.Enabled = true; }
                textBox7.Enabled = true;
                checkBox26.Enabled = true;
                checkBox27.Enabled = true;
                checkBox28.Enabled = true;
                checkBox29.Enabled = true;
                checkBox30.Enabled = true;
            }

        }

        private void checkBox26_CheckedChanged(object sender, EventArgs e)
        {
            if (!(checkBox26.Checked || checkBox27.Checked || checkBox28.Checked || checkBox29.Checked || checkBox30.Checked))
            {
                checkBox26.Checked = true;
                checkBox25.Checked = false;
            }

            if (checkBox26.Checked == true) {
                textBox4.Enabled = true;
            }
            else if (checkBox26.Checked == false)
            {
                textBox4.Enabled = false;
            }

        }

        private void checkBox27_CheckedChanged(object sender, EventArgs e)
        {
            if (!(checkBox26.Checked || checkBox27.Checked || checkBox28.Checked || checkBox29.Checked || checkBox30.Checked))
            {
                checkBox27.Checked = true;
                checkBox25.Checked = false;
            }

            if (checkBox27.Checked == true)
            {
                textBox9.Enabled = true;
            }
            else if (checkBox27.Checked == false)
            {
                textBox9.Enabled = false;
            }
        }

        private void checkBox28_CheckedChanged(object sender, EventArgs e)
        {
            if (!(checkBox26.Checked || checkBox27.Checked || checkBox28.Checked || checkBox29.Checked || checkBox30.Checked))
            {
                checkBox28.Checked = true;
                checkBox25.Checked = false;
            }

            if (checkBox28.Checked == true)
            {
                textBox10.Enabled = true;
            }
            else if (checkBox28.Checked == false)
            {
                textBox10.Enabled = false;
            }
        }

        private void checkBox29_CheckedChanged(object sender, EventArgs e)
        {
            if (!(checkBox26.Checked || checkBox27.Checked || checkBox28.Checked || checkBox29.Checked || checkBox30.Checked))
            {
                checkBox29.Checked = true;
                checkBox25.Checked = false;
            }

            if (checkBox29.Checked == true)
            {
                textBox11.Enabled = true;
            }
            else if (checkBox29.Checked == false)
            {
                textBox11.Enabled = false;
            }
        }

        private void checkBox30_CheckedChanged(object sender, EventArgs e)
        {
            if (!(checkBox26.Checked || checkBox27.Checked || checkBox28.Checked || checkBox29.Checked || checkBox30.Checked))
            {
                checkBox30.Checked = true;
                checkBox25.Checked = false;
            }

            if (checkBox30.Checked == true)
            {
                textBox12.Enabled = true;
            }
            else if (checkBox30.Checked == false)
            {
                textBox12.Enabled = false;
            }
        }

        private void checkBox13_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox13.Checked == false)
            {               
                checkBox34.Enabled = false;
                checkBox35.Enabled = false;
                checkBox36.Enabled = false;
                checkBox37.Enabled = false;
            }
            if (checkBox13.Checked == true)
            {
                checkBox34.Enabled = true;
                checkBox35.Enabled = true;
                checkBox36.Enabled = true;
                checkBox37.Enabled = true;
            }
        }

        private void checkBox34_CheckedChanged(object sender, EventArgs e)
        {

            if (checkBox34.Checked == false) {
                if (!(checkBox35.Checked || checkBox36.Checked || checkBox37.Checked)) {
                    checkBox34.Checked = true;
                }
            }
            if (checkBox34.Checked == true)
            {
                checkBox35.Checked = false;
                checkBox36.Checked = false;
                checkBox37.Checked = false;

            }

        }

        private void checkBox35_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox35.Checked == false)
            {
                if (!(checkBox34.Checked || checkBox36.Checked || checkBox37.Checked))
                {
                    checkBox35.Checked = true;
                }
            }
            if (checkBox35.Checked == true)
            {
                checkBox34.Checked = false;
                checkBox36.Checked = false;
                checkBox37.Checked = false;

            }
        }

        private void checkBox36_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox36.Checked == false)
            {
                if (!(checkBox35.Checked || checkBox34.Checked || checkBox37.Checked))
                {
                    checkBox36.Checked = true;
                }
            }
            if (checkBox36.Checked == true)
            {
                checkBox35.Checked = false;
                checkBox34.Checked = false;
                checkBox37.Checked = false;

            }
        }

        private void checkBox37_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox37.Checked == false)
            {
                if (!(checkBox35.Checked || checkBox36.Checked || checkBox34.Checked))
                {
                    checkBox37.Checked = true;
                }
            }
            if (checkBox37.Checked == true)
            {
                checkBox35.Checked = false;
                checkBox36.Checked = false;
                checkBox34.Checked = false;

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {


        }

        private void button4_Click(object sender, EventArgs e)
        {
            //delete button
            if (listBox1.SelectedIndex >= 0)
            {
                nox[listBox1.SelectedIndex].True_Run = false;
                nox[listBox1.SelectedIndex].run = false;
           
                nox.RemoveAt(listBox1.SelectedIndex);
                workers.RemoveAt(listBox1.SelectedIndex);
                listBox1.Items.RemoveAt(listBox1.SelectedIndex);

                for (int i = 0; i < nox.Count;i++) {
                    nox[i].ID = i;
                }


            }


        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
            richTextBox1.Clear();
            SystemMessage.Clear();

            if (listBox1.SelectedIndex >= 0) {

                if (nox[listBox1.SelectedIndex].shelterAll) {
                    listBox2.Items.Add("-Shelter All");
                }
                if (nox[listBox1.SelectedIndex].shelterOne)
                {
                    listBox2.Items.Add("-Shelter One Troop");
                }
                if (nox[listBox1.SelectedIndex].alwaysShield)
                {
                    listBox2.Items.Add("-Always Shield");
                }
                if (nox[listBox1.SelectedIndex].collectChests)
                { 
                    listBox2.Items.Add("-Collect Mystery Box");
                }
                if (nox[listBox1.SelectedIndex].collectGuildGift)
                {
                    listBox2.Items.Add("-Collect Guild Gifts");
                }
                if (nox[listBox1.SelectedIndex].turfQuest)
                {
                    listBox2.Items.Add("-Collect Turf Quests");
                }
                if (nox[listBox1.SelectedIndex].Quest)
                {
                    listBox2.Items.Add("-Collect Admin/Guild Quest");
                }
                if (nox[listBox1.SelectedIndex].helpOthers)
                {
                    listBox2.Items.Add("-Press Helps");
                }
                if (nox[listBox1.SelectedIndex].healTroops)
                {
                    listBox2.Items.Add("-Heal Troops");
                }
                if (nox[listBox1.SelectedIndex].upgradeBuildings)
                {
                    listBox2.Items.Add("-Upgrade Buildings");

                }
                if (nox[listBox1.SelectedIndex].research)
                {
                    listBox2.Items.Add("-Auto Research");

                }

                if (nox[listBox1.SelectedIndex].trainTroops)
                {
                    listBox2.Items.Add("");
                    listBox2.Items.Add("-Train Troops");

                    if (nox[listBox1.SelectedIndex].trainTroops_T1_Infantry)
                    {
                        listBox2.Items.Add("-->T1 Infrantry");

                    }
                    if (nox[listBox1.SelectedIndex].trainTroops_T1_Ranged)
                    {
                        listBox2.Items.Add("-->T1 Ranged");

                    }
                    if (nox[listBox1.SelectedIndex].trainTroops_T1_Cavalry)
                    {
                        listBox2.Items.Add("-->T1 Cavalry");

                    }
                    if (nox[listBox1.SelectedIndex].trainTroops_T1_Siege)
                    {
                        listBox2.Items.Add("-->T1 Siege");

                    }
                    listBox2.Items.Add("");
                }
              
                if (nox[listBox1.SelectedIndex].transferResource)
                {
                    listBox2.Items.Add("");
                    listBox2.Items.Add("-Transfer Resource");
                    listBox2.Items.Add("-Bookmark Number: 1");
                    if (nox[listBox1.SelectedIndex].useGuildBookmarks) {
                        listBox2.Items.Add("-Using Guild Bookmarks");
                    }

                    if (nox[listBox1.SelectedIndex].foodTransfer)
                    {
                        listBox2.Items.Add("-->Supply Food " + "-Marches: " + nox[listBox1.SelectedIndex].marchesTransferFood.ToString());

                    }
                    if (nox[listBox1.SelectedIndex].stoneTransfer)
                    {
                        listBox2.Items.Add("-->Supply Stone " + "-Marches: " + nox[listBox1.SelectedIndex].marchesTransferStone.ToString());

                    }
                    if (nox[listBox1.SelectedIndex].woodTransfer)
                    {
                        listBox2.Items.Add("-->Supply Wood " + "-Marches: " + nox[listBox1.SelectedIndex].marchesTransferWood.ToString());

                    }
                    if (nox[listBox1.SelectedIndex].oreTransfer)
                    {
                        listBox2.Items.Add("-->Supply Ore " + "-Marches: " + nox[listBox1.SelectedIndex].marchesTransferOre.ToString());

                    }
                    if (nox[listBox1.SelectedIndex].goldTransfer)
                    {
                        listBox2.Items.Add("-->Supply Gold " + "-Marches: " + nox[listBox1.SelectedIndex].marchesTransferGold.ToString());

                    }

                    listBox2.Items.Add("");
                }

                if (nox[listBox1.SelectedIndex].gather)
                {
                    listBox2.Items.Add("");
                    listBox2.Items.Add("-Gather");
                    listBox2.Items.Add("-Search Distance: " + nox[listBox1.SelectedIndex].searchDistance.ToString());
                    listBox2.Items.Add("-Marches: " + nox[listBox1.SelectedIndex].marchesGather.ToString());
                    if (nox[listBox1.SelectedIndex].foodGather)
                    {
                        listBox2.Items.Add("-->Food");

                    }
                    if (nox[listBox1.SelectedIndex].stoneGather)
                    {
                        listBox2.Items.Add("-->Stone");

                    }
                    if (nox[listBox1.SelectedIndex].woodGather)
                    {
                        listBox2.Items.Add("-->Wood");

                    }
                    if (nox[listBox1.SelectedIndex].oreGather)
                    {
                        listBox2.Items.Add("-->Ore");

                    }
                    if (nox[listBox1.SelectedIndex].goldGather)
                    {
                        listBox2.Items.Add("-->Gold");

                    }

                    listBox2.Items.Add("-Gather Direction");

                    if (nox[listBox1.SelectedIndex].randomGather)
                    {
                        listBox2.Items.Add("-->Random");

                    }
                    if (nox[listBox1.SelectedIndex].moveUpGather)
                    {
                        listBox2.Items.Add("-->Up");

                    }
                    if (nox[listBox1.SelectedIndex].moveRightGather)
                    {
                        listBox2.Items.Add("-->Right");

                    }
                    if (nox[listBox1.SelectedIndex].moveDownGather)
                    {
                        listBox2.Items.Add("-->Down");

                    }
                    if (nox[listBox1.SelectedIndex].moveLeftGather)
                    {
                        listBox2.Items.Add("-->Left");

                    }
                    listBox2.Items.Add("");

                }
                if (nox[listBox1.SelectedIndex].AccountSwitch)
                {
                    listBox2.Items.Add("");
                    listBox2.Items.Add("Account Switch #:" + nox[listBox1.SelectedIndex].AccountSwitch_Count);
                    

                }

            }


        }

        private void checkBox12_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox12.Checked == false)
            {
                textBox8.Text = "1";
                textBox8.Enabled = false;
            }
            else {
                textBox8.Enabled = true;
            }
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            int aNumber;
            if (int.TryParse(textBox7.Text, out aNumber) == false)
            {
                textBox7.Text = "1";

            }
            if (textBox7.Text == "0") { textBox7.Text = "1"; }
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            int aNumber;
            if (int.TryParse(textBox8.Text, out aNumber) == false)
            {
                textBox8.Text = "1";

            }
            if (textBox8.Text == "0") { textBox8.Text = "1"; }
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            int aNumber;
            if (int.TryParse(textBox9.Text, out aNumber) == false)
            {
                textBox9.Text = "1";

            }
            if (textBox9.Text == "0") { textBox9.Text = "1"; }
        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            int aNumber;
            if (int.TryParse(textBox10.Text, out aNumber) == false)
            {
                textBox10.Text = "1";

            }
            if (textBox10.Text == "0") { textBox10.Text = "1"; }
        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {
            int aNumber;
            if (int.TryParse(textBox11.Text, out aNumber) == false)
            {
                textBox11.Text = "1";

            }
            if (textBox11.Text == "0") { textBox11.Text = "1"; }
        }

        private void textBox12_TextChanged(object sender, EventArgs e)
        {
            int aNumber;
            if (int.TryParse(textBox12.Text, out aNumber) == false)
            {
                textBox12.Text = "1";

            }
            if (textBox12.Text == "0") { textBox12.Text = "1"; }
        }
    }
}
