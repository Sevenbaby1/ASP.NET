﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StuManage
{
    public partial class ClassModify : Form
    {
        private UpData upData;
        string flag;
        StuClass clas = new StuClass();
        EFDemo ef = new EFDemo();

        public ClassModify(StuClass s, UpData upData, string flag)
        {
            clas = s;
            this.flag = flag;
            this.upData = upData;
            InitializeComponent();
        }

        private void ClassModify_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(flag))//不为空则执行修改
            {
                textBox2.Focus();
                textBox1.ReadOnly = true;
                textBox1.Enabled = false;

                textBox1.Text = clas.ClassId.ToString();
                textBox2.Text = clas.ClassName;
                textBox3.Text = clas.Remark;
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(flag))
                {
                    #region 修改班级记录
                    if (!CheckData())
                    {
                        return;
                    }
                    clas.ClassName = textBox2.Text.Trim();
                    clas.Remark = textBox3.Text.Trim();


                    if (!ef.UpData(clas))
                    {
                        MessageBox.Show("修改失败");
                        return;
                    }
                    textBox1.ReadOnly = false;
                    upData();
                    MessageBox.Show("修改成功");
                    this.Close(); 
                    #endregion
                }

                else
                {
                    #region 添加班级记录
                    if (!CheckData())
                    {
                        return;
                    }
                    clas.ClassName = textBox2.Text.Trim();
                    clas.Remark = textBox3.Text.Trim();

                    if (!ef.Insert(clas))
                    {
                        MessageBox.Show("添加失败");
                        return;
                    }
                    upData();
                    MessageBox.Show("添加成功");
                    this.Close(); 
                    #endregion
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 检查文本框数据
        /// </summary>
        /// <returns></returns>
        private bool CheckData()
        {
            bool flag = true;

            if (textBox1.Text != "")
            {
                int n;
                if (!int.TryParse(textBox1.Text.Trim(), out n))
                {
                    MessageBox.Show("ClassId请输入整形数据", "提示：", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    flag = false;
                } 
            }

            if (textBox2.Text == "")
            {
                MessageBox.Show("ClassName不能为空", "提示：", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                flag = false;
            }
            return flag;
        }
    }
}
