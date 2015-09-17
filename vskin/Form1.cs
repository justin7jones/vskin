using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.DirectoryServices;
using System.DirectoryServices.Protocols;
using System.Security.Permissions;
using Renci.SshNet;
using System.Security.Principal;
using System.IO;


namespace vskin
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Global variable declarations

        // stores the vCenter type, either "win" or "vcsa"

        public static string vcenter_type = "not set";
        private void Form1_Load(object sender, EventArgs e)
        {
            // Error message for vCenter Connection is hidden
            label4.Visible = false;

            // Customize tab is hidden
            tabControl1.TabPages.Remove(tabPage2);
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            vcenter_type_chosen();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            vcenter_type_chosen();
        }

        private void vcenter_type_chosen()
        {
            if (radioButton1.Checked)   { 
            
                // In this case, they have indicated a Windows Based Appliance
                vcenter_type = "win";
                textBox2.Enabled = true;
                textBox2.Text = "administrator@corp.local";

                                        }
            else            { 
                // In this case, they have indicated a vCSA (Linux Based vCenter Appliance)
                vcenter_type = "vcsa";
                textBox2.Enabled = false;
                textBox2.Text = "root";
                            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // customize button clicked.
            validate_vcenter_connection();
        }

        private void validate_vcenter_connection()
        {
            switch (vcenter_type)
            {

                case "vcsa":


                    break;


                case "win":

                    try { 
                AppDomain.CurrentDomain.SetPrincipalPolicy(PrincipalPolicy.WindowsPrincipal);
                var unc_path = @"\\" + textBox1.Text + @"\c$\ProgramData\VMware\vCenterServer\runtime\VMwareSTSService\webapps\websso\WEB-INF\views\";


                NetworkCredential nc = new NetworkCredential(textBox2.Text, textBox3.Text);
                CredentialCache cc = new CredentialCache();
                cc.Add(new Uri(@"\\" + textBox1.Text), "Basic", nc);

                
                string[] file_list = Directory.GetFiles(unc_path);

                // File.Copy(@"test_file.txt", unc_path + "test_file.txt");

                // After this point, we have connected successfully
                // Set message
                label4.ForeColor = Color.Green;
                label4.Text = "Test Connection Succeeded. Proceed to customizing.";
                label4.Visible = true;

                // Enable Customize Button
                button2.Enabled = true;

                        }
                    catch (Exception e)
                    {
                        // Error on attempting to open windows connection

                        // Make sure button is disabled
                        button2.Enabled = false;

                        // Set message
                        label4.ForeColor = Color.Red;
                        label4.Text = "Test Connection Failed.  Please check the following:";
                        label4.Visible = true;
                    }

                    break;
            }
        }

    }
}
