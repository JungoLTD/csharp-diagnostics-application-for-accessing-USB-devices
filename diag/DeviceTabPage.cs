/* Jungo Connectivity Confidential. Copyright (c) 2023 Jungo Connectivity Ltd.  https://www.jungo.com */

// Note: This code sample is provided AS-IS and as a guiding sample only.

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using Jungo.wdapi_dotnet;
using Jungo.usb_lib;

namespace Jungo.csharp_usb_sample
{
    public class DeviceTabPage : System.Windows.Forms.TabPage
    {
        private UsbDevice usbDev;
        private System.Windows.Forms.ListView pipesListView;
        private int iSelectedPipeIndex = -1;
        private csharp_usb_sample.UsbSample ParentForm;

        public DeviceTabPage(csharp_usb_sample.UsbSample Parent,
            ref UsbDevice pUsbDev)
        {
            ParentForm = Parent;
            pipesListView = new System.Windows.Forms.ListView();
            usbDev = pUsbDev;

            this.RightToLeft = RightToLeft.No;
            this.Text = usbDev.DeviceDescription();
            CreateListViewSettings();
            UpdatePipesListView();
            this.Controls.Add(pipesListView);
            this.pipesListView.SelectedIndexChanged += new
                System.EventHandler(this.PipeListSelectedIndexChanged);
        }

        private void CreateListViewSettings()
        {
            pipesListView.Location = new Point(8, 8);
            pipesListView.Name = "pipesListView";
            pipesListView.HideSelection = false;
            pipesListView.Size = new System.Drawing.Size(
                    ParentForm.tabDevices.Width - 16,
                    ParentForm.tabDevices.Height - 16);
            pipesListView.View = View.Details;
            pipesListView.MultiSelect = false;
            pipesListView.GridLines = false;
            pipesListView.FullRowSelect = true;
            pipesListView.LabelEdit = false;
            pipesListView.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            pipesListView.Columns.Add("Pipe", pipesListView.Width / 4 - 1,
                HorizontalAlignment.Left);
            pipesListView.Columns.Add("Type", pipesListView.Width / 4 - 1,
                HorizontalAlignment.Left);
            pipesListView.Columns.Add("Direction", pipesListView.Width / 4 - 1,
                HorizontalAlignment.Left);
            pipesListView.Columns.Add("Max Packet Size",
                pipesListView.Width / 4 - 1, HorizontalAlignment.Left);
        }

        public void UpdatePipesListView()
        {
            int index = 0;

            pipesListView.BeginUpdate();
            pipesListView.Items.Clear();

            for (index = 0; index < usbDev.GetpPipesList().Count; ++index)
            {
                UsbPipe currUsbPipe = (UsbPipe)(usbDev.GetpPipesList())[index];
                ListViewItem pipeListItem = new
                    ListViewItem(string.Format("0x{0}",
                    currUsbPipe.GetPipeNum().ToString("X")));

                pipeListItem.SubItems.Add(PipeTypeToString(currUsbPipe));
                pipeListItem.SubItems.Add(PipeDirectionToString(currUsbPipe));
                pipeListItem.SubItems.Add(string.Concat("0x",
                    (currUsbPipe.GetPipeMaxPacketSz()).ToString("X")));
                pipesListView.Items.Add(pipeListItem);
            }

            pipesListView.Items[0].Selected = true;
            pipesListView.EndUpdate();
        }

        void PipeListSelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (pipesListView.SelectedItems.Count <= 0)
                return;
            int Index = pipesListView.SelectedItems[0].Index;
            if (iSelectedPipeIndex == -1 || iSelectedPipeIndex != Index)
            {
                iSelectedPipeIndex = Index;
                if (ParentForm.GetActiveTab() == this)
                    ParentForm.UpdateButtons();
            }
        }

        public void UpdateAlternateSettings()
        {
            UpdatePipesListView();
            this.Text = usbDev.DeviceDescription();
        }

        public UsbPipe GetActivePipe()
        {
            return (UsbPipe)usbDev.GetpPipesList()[iSelectedPipeIndex];

        }

        public uint GetActivePipeNum()
        {
            return ((UsbPipe)usbDev.GetpPipesList()[iSelectedPipeIndex]).
                GetPipeNum();
        }

        public UsbDevice GetUsbDev()
        {
            return usbDev;
        }

        public void SetUsbDev(ref UsbDevice newUsbDev)
        {
            usbDev = newUsbDev;
        }

        private string PipeTypeToString(UsbPipe pipe)
        {
            string strPipeType;

            if (pipe.IsControlPipe())
                strPipeType = "Control";
            else if (pipe.IsBulkPipe())
                strPipeType = "Bulk";
            else if (pipe.IsInterruptPipe())
                strPipeType = "Interrupt";
            else if (pipe.IsIsochronousPipe())
                strPipeType = "Isochronous";
            else
                strPipeType = "N/A";

            return strPipeType;
        }

        private string PipeDirectionToString(UsbPipe pipe)
        {
            string strPipeDirection;

            if (pipe.IsPipeDirectionIn())
                strPipeDirection = "In";
            else if (pipe.IsPipeDirectionOut())
                strPipeDirection = "Out";
            else if (pipe.IsPipeDirectionInOut())
                strPipeDirection = "In/Out";
            else
                strPipeDirection = "N/A";

            return strPipeDirection;
        }
    };
}

