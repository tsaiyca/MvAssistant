using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MvAssistant.WCFService.Host
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            this.Initial();
            MvAssistantWCFServiceHostInstance.GetInstance();
        }

        private void Initial()
        {
            this.SetFormTitle();
        }
        private void SetFormTitle()
        {
            this.Text = MvAssistantWCFServiceHostInstance.ServiceUrl;
        }

        private void BtnStartService_Click(object sender, EventArgs e)
        {
           
        }

        private void FrmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            MvAssistantWCFServiceHostInstance.StopService();
        }
    }
}
