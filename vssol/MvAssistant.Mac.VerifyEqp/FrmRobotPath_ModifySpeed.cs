using MaskCleanerVerify;
using MvAssistant.Mac.v1_0.JSon;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MvAssistantMacVerifyEqp
{
    public partial class FrmRobotPath_ModifySpeed : Form
    {
        private PositionInfo Position = null;
        private Form Owner = null;
        public FrmRobotPath_ModifySpeed()
        {
            InitializeComponent();
        }
        public FrmRobotPath_ModifySpeed(Form owner, PositionInfo position) : this()
        {
            Position = position;
            Owner = owner;
            owner.Enabled = false;
        }
        private void FrmRobotPath_ModifySpeed_Load(object sender, EventArgs e)
        {
            numberSpeed.Value = Position.Position.Speed;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ExitThis();
        }

        private void ExitThis()
        {
            Owner.Enabled = true;
            this.Close();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            Position.Position.Speed = (int)numberSpeed.Value;
            ((FmRobotPath)Owner).ToModifySpeed(Position);
            ExitThis();
        }
    }
}
