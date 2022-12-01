using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Deepak_DotNetTask2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=db_Employees;Integrated Security=True");
        public int EmployeeId;
        private void Form1_Load(object sender, EventArgs e)
        {
            GetEmployeeRecords();
        }

        private void GetEmployeeRecords()
        {
            
            SqlCommand cmd = new SqlCommand("select * from Employees", con);
            DataTable dt = new DataTable();

            con.Open();

            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();

            EmployeeRecordDataGridView.DataSource = dt;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(IsValid())
            {
                SqlCommand cmd = new SqlCommand("Insert into Employees (EmpName, Dob, Doj, Department, ReportTo, ContactNo, ResignedDate) VALUES (@EmpName,@Dob,@Doj,@Department,@ReportTo,@ContactNo,@ResignedDate)",con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@EmpName", textEmployeeName.Text);
                cmd.Parameters.AddWithValue("@Dob", Convert.ToDateTime(txtDOB.Text));
                cmd.Parameters.AddWithValue("@Doj", Convert.ToDateTime(txtDOJ.Text));
                cmd.Parameters.AddWithValue("@Department", txtDepartment.Text);
                cmd.Parameters.AddWithValue("@ReportTo", txtReportTo.Text);
                cmd.Parameters.AddWithValue("@ContactNo", txtContact.Text);
                cmd.Parameters.AddWithValue("@ResignedDate", Convert.ToDateTime(txtResignedDate.Text));

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("New Employee is successfuly saved","Saved",MessageBoxButtons.OK,MessageBoxIcon.Information);

                GetEmployeeRecords();
            }
        }

        private bool IsValid()
        {
            if(textEmployeeName.Text == string.Empty)
            {
                MessageBox.Show("Employee name is required","Failed",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void EmployeeRecordDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            EmployeeId = Convert.ToInt32(EmployeeRecordDataGridView.SelectedRows[0].Cells[0].Value);
            textEmployeeName.Text = EmployeeRecordDataGridView.SelectedRows[0].Cells[1].Value.ToString();
            txtDOB.Text = EmployeeRecordDataGridView.SelectedRows[0].Cells[2].Value.ToString();
            txtDOJ.Text = EmployeeRecordDataGridView.SelectedRows[0].Cells[3].Value.ToString();
            txtDepartment.Text = EmployeeRecordDataGridView.SelectedRows[0].Cells[4].Value.ToString();
            txtReportTo.Text = EmployeeRecordDataGridView.SelectedRows[0].Cells[5].Value.ToString();
            txtContact.Text = EmployeeRecordDataGridView.SelectedRows[0].Cells[6].Value.ToString();
            txtResignedDate.Text = EmployeeRecordDataGridView.SelectedRows[0].Cells[7].Value.ToString();
            //textEmployeeName.Text = EmployeeRecordDataGridView.SelectedRows[0].Cells[1].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (EmployeeId>0)
            {
                SqlCommand cmd = new SqlCommand("Update Employees set EmpName=@EmpName,Dob=@Dob,Doj=@Doj,Department=@Department,ReportTo=@ReportTo,ContactNo=@ContactNo,ResignedDate=@ResignedDate where EmpCode = @Id", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@EmpName", textEmployeeName.Text);
                cmd.Parameters.AddWithValue("@Dob", Convert.ToDateTime(txtDOB.Text));
                cmd.Parameters.AddWithValue("@Doj", Convert.ToDateTime(txtDOJ.Text));
                cmd.Parameters.AddWithValue("@Department", txtDepartment.Text);
                cmd.Parameters.AddWithValue("@ReportTo", txtReportTo.Text);
                cmd.Parameters.AddWithValue("@ContactNo", txtContact.Text);
                cmd.Parameters.AddWithValue("@ResignedDate", Convert.ToDateTime(txtResignedDate.Text));
                cmd.Parameters.AddWithValue("@Id", this.EmployeeId);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Employee Information is updated successfuly", "Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);

                GetEmployeeRecords();
            }
            else
            {
                MessageBox.Show("Please select employee to update", "Select ?", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (EmployeeId>0)
            {
                SqlCommand cmd = new SqlCommand("Delete from Employees where EmpCode = @Id", con);
                cmd.CommandType = CommandType.Text;               
                cmd.Parameters.AddWithValue("@Id", this.EmployeeId);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Employee Information is deleted successfuly", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);

                GetEmployeeRecords();
            }
            else
            {
                MessageBox.Show("Please select employee to delete", "Select ?", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
