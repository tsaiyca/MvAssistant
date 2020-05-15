using MvAssistant.Mac.v1_0.Hal;
using MvAssistant.Mac.v1_0.Hal.Assembly;
using MvAssistant.Mac.v1_0.Manifest;
using MvAssistant.OperateModel.TransferModel.ResponseModel.MaskRobot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MvAssistant.WCFService.TestFrm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            WCFService.MvAssistantWCFServiceClient service = new WCFService.MvAssistantWCFServiceClient();
            var feedBack= service.UtHalMaskTransfer_TestPathMove();
            var response = Newtonsoft.Json.JsonConvert.DeserializeObject<MaskRobot_Connect_ResponseModel>(feedBack); 
           
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
