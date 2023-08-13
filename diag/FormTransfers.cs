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
    public class FormTransfers : System.Windows.Forms.Form
    {
        internal System.Windows.Forms.Panel pnlData;
        internal System.Windows.Forms.TextBox txtData;
        internal System.Windows.Forms.Label lblData;
        internal System.Windows.Forms.Panel pnlBufSize;
        internal System.Windows.Forms.TextBox txtBufSize;
        internal System.Windows.Forms.Label lblBufSize;
        internal System.Windows.Forms.Button btCancel;
        internal System.Windows.Forms.Panel pnlSetupPacket;
        internal System.Windows.Forms.TextBox txtwLength;
        internal System.Windows.Forms.Label lblwLength;
        internal System.Windows.Forms.TextBox txtwIndex;
        internal System.Windows.Forms.Label lblwIndex;
        internal System.Windows.Forms.TextBox txtwValue;
        internal System.Windows.Forms.Label lblwValue;
        internal System.Windows.Forms.TextBox txtRequest;
        internal System.Windows.Forms.Label lblRequest;
        internal System.Windows.Forms.TextBox txtType;
        internal System.Windows.Forms.Label lblType;
        internal System.Windows.Forms.Label lblSetupPacket;
        internal System.Windows.Forms.Button btSubmit;

        private System.ComponentModel.Container components = null;

        private bool m_bIsControl;
        private bool m_bIsRead;
        private DWORD m_dwBuffSize;
        private byte[] m_buffer = null;
        private byte[] m_pSetupPacket = new byte[8];

        public FormTransfers(bool bIsRead, bool bIsControl)
        {
            // Required for Windows Form Designer support
            InitializeComponent();

            pnlBufSize.Enabled = !bIsControl;
            txtBufSize.Enabled = !bIsControl;

            pnlData.Enabled = !bIsRead;
            txtData.Enabled = !bIsRead;

            pnlSetupPacket.Enabled = bIsControl;
            txtRequest.Enabled = bIsControl;
            txtType.Enabled = bIsControl;
            txtwIndex.Enabled = bIsControl;
            txtwLength.Enabled = bIsControl;
            txtwValue.Enabled = bIsControl;
            m_bIsControl = bIsControl;
            m_bIsRead = bIsRead;
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
            pnlData = new System.Windows.Forms.Panel();
            txtData = new System.Windows.Forms.TextBox();
            lblData = new System.Windows.Forms.Label();
            pnlBufSize = new System.Windows.Forms.Panel();
            txtBufSize = new System.Windows.Forms.TextBox();
            lblBufSize = new System.Windows.Forms.Label();
            btCancel = new System.Windows.Forms.Button();
            pnlSetupPacket = new System.Windows.Forms.Panel();
            txtwLength = new System.Windows.Forms.TextBox();
            lblwLength = new System.Windows.Forms.Label();
            txtwIndex = new System.Windows.Forms.TextBox();
            lblwIndex = new System.Windows.Forms.Label();
            txtwValue = new System.Windows.Forms.TextBox();
            lblwValue = new System.Windows.Forms.Label();
            txtRequest = new System.Windows.Forms.TextBox();
            lblRequest = new System.Windows.Forms.Label();
            txtType = new System.Windows.Forms.TextBox();
            lblType = new System.Windows.Forms.Label();
            lblSetupPacket = new System.Windows.Forms.Label();
            btSubmit = new System.Windows.Forms.Button();
            pnlData.SuspendLayout();
            pnlBufSize.SuspendLayout();
            pnlSetupPacket.SuspendLayout();
            SuspendLayout();

            pnlData.Controls.Add(txtData);
            pnlData.Controls.Add(lblData);
            pnlData.Location = new System.Drawing.Point(16, 46);
            pnlData.Name = "pnlData";
            pnlData.Size = new System.Drawing.Size(360, 32);
            pnlData.TabIndex = 21;

            txtData.Enabled = false;
            txtData.Location = new System.Drawing.Point(112, 8);
            txtData.Name = "txtData";
            txtData.Size = new System.Drawing.Size(240, 20);
            txtData.TabIndex = 3;
            txtData.Text = "";

            lblData.Font = new System.Drawing.Font("Microsoft Sans Serif",
                8.25F,
                ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold |
                System.Drawing.FontStyle.Underline))),
                System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            lblData.Location = new System.Drawing.Point(8, 8);
            lblData.Name = "lblData";
            lblData.Size = new System.Drawing.Size(64, 16);
            lblData.TabIndex = 2;
            lblData.Text = "Data (hex):";

            pnlBufSize.Controls.Add(txtBufSize);
            pnlBufSize.Controls.Add(lblBufSize);
            pnlBufSize.Location = new System.Drawing.Point(16, 14);
            pnlBufSize.Name = "pnlBufSize";
            pnlBufSize.Size = new System.Drawing.Size(168, 32);
            pnlBufSize.TabIndex = 20;

            txtBufSize.Location = new System.Drawing.Point(112, 8);
            txtBufSize.Name = "txtBufSize";
            txtBufSize.Size = new System.Drawing.Size(40, 20);
            txtBufSize.TabIndex = 1;
            txtBufSize.Text = "";
            txtBufSize.TextAlign =
                System.Windows.Forms.HorizontalAlignment.Center;

            lblBufSize.Font = new System.Drawing.Font("Microsoft Sans Serif",
                    8.25F, ((System.Drawing.FontStyle)((
                    System.Drawing.FontStyle.Bold |
                    System.Drawing.FontStyle.Underline))),
                System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            lblBufSize.Location = new System.Drawing.Point(8, 8);
            lblBufSize.Name = "lblBufSize";
            lblBufSize.Size = new System.Drawing.Size(96, 16);
            lblBufSize.TabIndex = 0;
            lblBufSize.Text = "Buffer size (hex):";

            btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            btCancel.Font = new System.Drawing.Font("Microsoft Sans Serif",
                    8.25F, System.Drawing.FontStyle.Bold,
                System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            btCancel.Location = new System.Drawing.Point(112, 166);
            btCancel.Name = "btCancel";
            btCancel.Size = new System.Drawing.Size(75, 32);
            btCancel.TabIndex = 19;
            btCancel.Text = "Cancel";

            pnlSetupPacket.Controls.Add(txtwLength);
            pnlSetupPacket.Controls.Add(lblwLength);
            pnlSetupPacket.Controls.Add(txtwIndex);
            pnlSetupPacket.Controls.Add(lblwIndex);
            pnlSetupPacket.Controls.Add(txtwValue);
            pnlSetupPacket.Controls.Add(lblwValue);
            pnlSetupPacket.Controls.Add(txtRequest);
            pnlSetupPacket.Controls.Add(lblRequest);
            pnlSetupPacket.Controls.Add(txtType);
            pnlSetupPacket.Controls.Add(lblType);
            pnlSetupPacket.Controls.Add(lblSetupPacket);
            pnlSetupPacket.Location = new System.Drawing.Point(16, 78);
            pnlSetupPacket.Name = "pnlSetupPacket";
            pnlSetupPacket.Size = new System.Drawing.Size(312, 72);
            pnlSetupPacket.TabIndex = 17;

            txtwLength.Enabled = false;
            txtwLength.Location = new System.Drawing.Point(248, 48);
            txtwLength.MaxLength = 4;
            txtwLength.Name = "txtwLength";
            txtwLength.Size = new System.Drawing.Size(48, 20);
            txtwLength.TabIndex = 8;
            txtwLength.Text = "";
            txtwLength.TextAlign =
                    System.Windows.Forms.HorizontalAlignment.Center;
            txtwLength.TextChanged += new
                    System.EventHandler(txtwLength_TextChanged);

            lblwLength.Font = new System.Drawing.Font("Microsoft Sans Serif",
                    8.25F, System.Drawing.FontStyle.Bold,
                System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            lblwLength.Location = new System.Drawing.Point(248, 32);
            lblwLength.Name = "lblwLength";
            lblwLength.Size = new System.Drawing.Size(48, 16);
            lblwLength.TabIndex = 12;
            lblwLength.Text = "wLength";

            txtwIndex.Enabled = false;
            txtwIndex.Location = new System.Drawing.Point(184, 48);
            txtwIndex.MaxLength = 4;
            txtwIndex.Name = "txtwIndex";
            txtwIndex.Size = new System.Drawing.Size(48, 20);
            txtwIndex.TabIndex = 7;
            txtwIndex.Text = "";
            txtwIndex.TextAlign =
                    System.Windows.Forms.HorizontalAlignment.Center;
            txtwIndex.TextChanged += new
                    System.EventHandler(txtwIndex_TextChanged);

            lblwIndex.Font = new System.Drawing.Font("Microsoft Sans Serif",
                    8.25F, System.Drawing.FontStyle.Bold,
                System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            lblwIndex.Location = new System.Drawing.Point(184, 32);
            lblwIndex.Name = "lblwIndex";
            lblwIndex.Size = new System.Drawing.Size(48, 16);
            lblwIndex.TabIndex = 11;
            lblwIndex.Text = "wIndex";

            txtwValue.Enabled = false;
            txtwValue.Location = new System.Drawing.Point(120, 48);
            txtwValue.MaxLength = 4;
            txtwValue.Name = "txtwValue";
            txtwValue.Size = new System.Drawing.Size(48, 20);
            txtwValue.TabIndex = 6;
            txtwValue.Text = "";
            txtwValue.TextAlign =
                    System.Windows.Forms.HorizontalAlignment.Center;
            txtwValue.TextChanged += new
                    System.EventHandler(txtwValue_TextChanged);

            lblwValue.Font = new System.Drawing.Font("Microsoft Sans Serif",
                    8.25F, System.Drawing.FontStyle.Bold,
                System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            lblwValue.Location = new System.Drawing.Point(120, 32);
            lblwValue.Name = "lblwValue";
            lblwValue.Size = new System.Drawing.Size(48, 16);
            lblwValue.TabIndex = 10;
            lblwValue.Text = "wValue";

            txtRequest.Enabled = false;
            txtRequest.Location = new System.Drawing.Point(64, 48);
            txtRequest.MaxLength = 2;
            txtRequest.Name = "txtRequest";
            txtRequest.Size = new System.Drawing.Size(32, 20);
            txtRequest.TabIndex = 5;
            txtRequest.Text = "";
            txtRequest.TextAlign =
                    System.Windows.Forms.HorizontalAlignment.Center;
            txtRequest.TextChanged += new
                    System.EventHandler(txtRequest_TextChanged);

            lblRequest.Font = new System.Drawing.Font("Microsoft Sans Serif",
                    8.25F, System.Drawing.FontStyle.Bold,
                System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            lblRequest.Location = new System.Drawing.Point(56, 32);
            lblRequest.Name = "lblRequest";
            lblRequest.Size = new System.Drawing.Size(48, 16);
            lblRequest.TabIndex = 9;
            lblRequest.Text = "Request";

            txtType.Enabled = false;
            txtType.Location = new System.Drawing.Point(8, 48);
            txtType.MaxLength = 2;
            txtType.Name = "txtType";
            txtType.Size = new System.Drawing.Size(32, 20);
            txtType.TabIndex = 4;
            txtType.Text = "";
            txtType.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            txtType.TextChanged += new System.EventHandler(txtType_TextChanged);

            lblType.Font = new System.Drawing.Font("Microsoft Sans Serif",
                    8.25F, System.Drawing.FontStyle.Bold,
                System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            lblType.Location = new System.Drawing.Point(8, 32);
            lblType.Name = "lblType";
            lblType.Size = new System.Drawing.Size(32, 16);
            lblType.TabIndex = 3;
            lblType.Text = "Type";

            lblSetupPacket.Font = new
                    System.Drawing.Font("Microsoft Sans Serif", 8.25F,
                ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold |
                System.Drawing.FontStyle.Underline))),
                System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            lblSetupPacket.Location = new System.Drawing.Point(8, 8);
            lblSetupPacket.Name = "lblSetupPacket";
            lblSetupPacket.Size = new System.Drawing.Size(296, 16);
            lblSetupPacket.TabIndex = 17;
            lblSetupPacket.Text = "Setup Packet:";

            btSubmit.Font = new System.Drawing.Font("Microsoft Sans Serif",
                    8.25F, System.Drawing.FontStyle.Bold,
                System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            btSubmit.Location = new System.Drawing.Point(24, 166);
            btSubmit.Name = "btSubmit";
            btSubmit.Size = new System.Drawing.Size(75, 32);
            btSubmit.TabIndex = 18;
            btSubmit.Text = "Submit";
            btSubmit.Click += new System.EventHandler(btSubmit_Click);

            AcceptButton = btSubmit;
            AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            CancelButton = btCancel;
            ClientSize = new System.Drawing.Size(384, 213);
            Controls.Add(pnlData);
            Controls.Add(pnlBufSize);
            Controls.Add(btCancel);
            Controls.Add(pnlSetupPacket);
            Controls.Add(btSubmit);
            Name = "FormTransfers";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Read/Write device's Pipes";
            pnlData.ResumeLayout(false);
            pnlBufSize.ResumeLayout(false);
            pnlSetupPacket.ResumeLayout(false);
            ResumeLayout(false);

        }
        #endregion

        public bool GetInput(ref DWORD dwBuffSize, ref byte[] buffer,
            ref byte[] pSetupPacket)
        {
            DialogResult result = DialogResult.Retry;

            while ((result = ShowDialog()) == DialogResult.Retry);

            if (result != DialogResult.OK)
                return false;

            dwBuffSize = m_dwBuffSize;
            buffer = m_buffer;
            pSetupPacket = m_pSetupPacket;

            return true;
        }

        private void TranslateInput()
        {
            if (m_bIsControl)
            {
                GetSetupPacketData();
                m_dwBuffSize = Convert.ToUInt32(txtwLength.Text, 16);
            }
            else
            {
                m_dwBuffSize = Convert.ToUInt32(txtBufSize.Text,16);
            }

            if (m_dwBuffSize > 0)
                m_buffer = new byte[m_dwBuffSize];

            if (!m_bIsRead)
            {
                //padding the first bytes if necessary
                string str = PadBuffer(txtData.Text, txtData.Text.Length,
                    2 * (int)m_dwBuffSize);

                for (int i = 0; i < m_dwBuffSize; ++i)
                  m_buffer[i] = Convert.ToByte(str.Substring(2 * i, 2), 16);
            }
        }

        private string PadBuffer(string str, int fromIndex, int toIndex)
        {
            for (int i = fromIndex; i < toIndex; ++i)
                str = string.Concat("0", str);

            return str;
        }

        private void GetSetupPacketData()
        {
            string type = PadBuffer(txtType.Text, txtType.Text.Length, 2);
            string request =
                    PadBuffer(txtRequest.Text, txtRequest.Text.Length, 2);
            string wValue = PadBuffer(txtwValue.Text, txtwValue.Text.Length, 4);
            string wIndex = PadBuffer(txtwIndex.Text, txtwIndex.Text.Length, 4);
            string wLength =
                    PadBuffer(txtwLength.Text, txtwLength.Text.Length, 4);

            m_pSetupPacket[0] = Convert.ToByte(type, 16);
            m_pSetupPacket[1] = Convert.ToByte(request, 16);
            m_pSetupPacket[2] = Convert.ToByte(wValue.Substring(2,2), 16);
            m_pSetupPacket[3] = Convert.ToByte(wValue.Substring(0,2), 16);
            m_pSetupPacket[4] = Convert.ToByte(wIndex.Substring(2,2), 16);
            m_pSetupPacket[5] = Convert.ToByte(wIndex.Substring(0,2), 16);
            m_pSetupPacket[6] = Convert.ToByte(wLength.Substring(2,2), 16);
            m_pSetupPacket[7] = Convert.ToByte(wLength.Substring(0,2), 16);
        }

        private void btSubmit_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.OK;
            try
            {
                TranslateInput();
            }
            catch
            {
                MessageBox.Show(string.Concat(
                    "The text is not a valid hex number.",
                    "Please re-enter, or press Cancel to exit"),
                    "Input Entry Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                DialogResult = DialogResult.Retry;
            }
        }

        private void SetupPacketInputChanged(TextBox txtInput)
        {
            if (txtInput.Text.Length == txtInput.MaxLength)
                SelectNextControl(txtInput, true, true, true, false);
        }

        private void txtType_TextChanged(object sender, System.EventArgs e)
        {
            SetupPacketInputChanged(txtType);
        }

        private void txtRequest_TextChanged(object sender, System.EventArgs e)
        {
            SetupPacketInputChanged(txtRequest);
        }

        private void txtwValue_TextChanged(object sender, System.EventArgs e)
        {
            SetupPacketInputChanged(txtwValue);
        }

        private void txtwIndex_TextChanged(object sender, System.EventArgs e)
        {
            SetupPacketInputChanged(txtwIndex);
        }

        private void txtwLength_TextChanged(object sender, System.EventArgs e)
        {
            SetupPacketInputChanged(txtwLength);
        }
    }
}

