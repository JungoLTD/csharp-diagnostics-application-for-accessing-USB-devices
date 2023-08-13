/* Jungo Connectivity Confidential. Copyright (c) 2023 Jungo Connectivity Ltd.  https://www.jungo.com */

// Note: This code sample is provided AS-IS and as a guiding sample only.

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using Jungo.wdapi_dotnet;
using Jungo.usb_lib;

using DWORD = System.UInt32;

namespace Jungo.csharp_usb_sample
{
    public class FormChangeSettings : System.Windows.Forms.Form
    {
        internal System.Windows.Forms.ComboBox cmboAltSettings;
        internal System.Windows.Forms.Button btCancel;
        internal System.Windows.Forms.Button btSubmit;

        private System.ComponentModel.Container components = null;
        private DWORD[,] settingsArr;
        private DWORD dwChosenInterface;
        private DWORD dwChosenSetting;

        public FormChangeSettings(ref UsbDevice usbDev)
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
            DWORD numOfAltSettings = usbDev.GetNumOfAlternateSettingsTotal();
            DWORD currInterfaceIndex = (DWORD)usbDev.GetCurrInterfaceIndex();
            DWORD currAltSetting = usbDev.GetCurrAlternateSettingNum();
            settingsArr = new DWORD[numOfAltSettings,2];
            DWORD currIndex = 0;
            int i = 0;

            for (DWORD interfac = 0; interfac < usbDev.GetNumOfInteraces();
                ++interfac)
            {
                DWORD dwInterfaceNumber =
                    usbDev.GetInterfaceNumberByIndex(interfac);
                for (DWORD altSetting = 0;
                    altSetting < usbDev.GetNumOfAlternateSettingsPerInterface(
                    dwInterfaceNumber); ++altSetting)
                {
                    cmboAltSettings.Items.Add("Interface " +
                        dwInterfaceNumber.ToString() + ",  Alternate Setting " +
                        altSetting.ToString());
                    settingsArr[i,0] = dwInterfaceNumber;
                    settingsArr[i,1] = altSetting;
                    ++i;

                    if (interfac <= currInterfaceIndex &&
                        altSetting<currAltSetting)
                    {
                        ++currIndex;
                    }
                }
            }

            cmboAltSettings.SelectedIndex = (int)currIndex;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            btCancel = new System.Windows.Forms.Button();
            btSubmit = new System.Windows.Forms.Button();
            cmboAltSettings = new System.Windows.Forms.ComboBox();
            SuspendLayout();

            btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            btCancel.Font = new System.Drawing.Font("Microsoft Sans Serif",
                8.25F, System.Drawing.FontStyle.Bold,
                System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            btCancel.Location = new System.Drawing.Point(120, 88);
            btCancel.Name = "btCancel";
            btCancel.Size = new System.Drawing.Size(64, 24);
            btCancel.TabIndex = 19;
            btCancel.Text = "Cancel";

            btSubmit.Font = new System.Drawing.Font("Microsoft Sans Serif",
                8.25F, System.Drawing.FontStyle.Bold,
                System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            btSubmit.Location = new System.Drawing.Point(32, 88);
            btSubmit.Name = "btSubmit";
            btSubmit.Size = new System.Drawing.Size(64, 24);
            btSubmit.TabIndex = 18;
            btSubmit.Text = "Submit";
            btSubmit.Click += new System.EventHandler(btSubmit_Click);

            cmboAltSettings.ItemHeight = 13;
            cmboAltSettings.Location = new System.Drawing.Point(32, 32);
            cmboAltSettings.Name = "cmboAltSettings";
            cmboAltSettings.Size = new System.Drawing.Size(152, 21);
            cmboAltSettings.TabIndex = 25;
            cmboAltSettings.Text = "Choose Setting";

            AcceptButton = btSubmit;
            AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            CancelButton = btCancel;
            ClientSize = new System.Drawing.Size(232, 149);
            Controls.Add(cmboAltSettings);
            Controls.Add(btCancel);
            Controls.Add(btSubmit);
            Name = "FormChangeSettings";
            Text = "Change device's Settings";
            ResumeLayout(false);
        }
        #endregion

        public DWORD GetChosenInterface()
        {
            return dwChosenInterface;
        }

        public DWORD GetChosenSetting()
        {
            return dwChosenSetting;
        }

        private void btSubmit_Click(object sender, System.EventArgs e)
        {
            int index = cmboAltSettings.SelectedIndex;

            dwChosenInterface = settingsArr[index, 0];
            dwChosenSetting = settingsArr[index, 1];
            DialogResult = DialogResult.OK;
        }
    }
}

